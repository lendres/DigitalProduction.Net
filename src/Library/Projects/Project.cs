using DigitalProduction.Delegates;
using DigitalProduction.Interface;
using DigitalProduction.XML.Serialization;
using System;
using System.Xml.Serialization;

namespace DigitalProduction.Projects;

/// <summary>
/// Base class for a Project.  Provides common functionality.
/// </summary>
public abstract class Project : IModified
{
	#region Events

	/// <summary>
	/// Occurs when data in the project is modified.  Used, for example, to enable/disable the Save button based on whether the project
	/// has been modified and needs to be saved.
	/// </summary>
	public event ModifiedEventHandler?				OnModifiedChanged;

	/// <summary>
	/// Occurs when the project is initialized.  This event occurs every time a project is created, regardless of how it is created.  For
	/// example, this event will fire if the project is created by instantiating a new instance of a project (new Project) or if the Project
	/// is created by deserializing from disk.
	/// </summary>
	public event NoArgumentsEventHandler?			OnInitialized;

	/// <summary>
	/// Occurs after a Project has been deserialized from disk.  Note that this event does not fire when creating a new instance of a
	/// Project (new Project()).  Hook into this event to perform any operations or GUI setup required to be performed after opening
	/// a Project from disk.
	/// </summary>
	public event ProjectOpenedEventHandler?			OnOpened;

	/// <summary>
	/// Occurs before a Project is serialized to disk.  Hook into this event to save related file for the project or any other events
	/// that must occur before a project can be serialized.
	/// </summary>
	public event ProjectSavingEventHandler?			OnSaving;

	/// <summary>
	/// Occurs after a Project has been closed.
	/// </summary>
	public event NoArgumentsEventHandler?			OnClosed;

	#endregion

	#region Fields

	// Handling opening/creation methods and events.
	private CreationMethod							_creationMethod					= CreationMethod.Instantiated;
	private ProjectExtractor?						_projectExtractor;

	/// <summary>Project description.</summary>
	protected string								_description					= "";

	// State variables.

	// XML serialization doesn't let us separate our XML reading from our Properties.  This variable is used to indicate when the Project
	// has been fully constructed (either by the constructor or by XML reading being complete) to prevent function calls on variable that
	// have not been initialized.
	private bool									_initialized					= false;
	private bool									_modified						= false;
	private bool									_closed							= false;

	private const string							_projectFileName				= "Project.xml";

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor for designer.
	/// </summary>
	public Project()
	{
	}

	#endregion

	#region Properties

	/// <summary>
	/// Software version.  We will use it as the file version as well.  Force the file and software versions to match or throw an exception.
	/// </summary>
	[XmlAttribute("version")]
	public static string Version
	{
		get
		{
			System.Reflection.Assembly entryAssembly = System.Reflection.Assembly.GetEntryAssembly() ??
				throw new Exception("Entry assembly not found.");
			return DigitalProduction.Reflection.Assembly.Version(entryAssembly);
		}

		set
		{
			//string thisVersion	= DigitalProduction.Strings.Format.MajorMinorVersionNumber(this.Version);
			//	string valueVersion = DigitalProduction.Strings.Format.MajorMinorVersionNumber(value);

			//if (thisVersion != valueVersion)
			//{
			//	throw new FormatException("The project file version is not valid for this version of the software.  Update the file to the current version.");
			//}
		}
	}

	/// <summary>
	/// The location of the project file that this file was serialized from and will be serialized to.
	/// </summary>
	[XmlIgnore()]
	public string Path { get; set; } = "";

	/// <summary>
	/// Project file name with the file extension.
	/// </summary>
	[XmlIgnore()]
	public string FileName { get => System.IO.Path.GetFileName(Path); }

	/// <summary>
	/// Specifies is the project is currently savable.  Check before calling "Save()".  Calling "Save" with "Savable" false will throw an exception.
	/// </summary>
	[XmlIgnore()]
	public bool Saveable { get => DigitalProduction.IO.Path.PathIsWritable(Path); }

	/// <summary>
	/// Specifies that the project has finished initialization and should fire events from this point forward.
	/// </summary>
	[XmlIgnore()]
	public bool Initialized
	{
		get => _initialized;

		set
		{
			if (_initialized != value)
			{
				_initialized = value;

				// Fire events.  Allows any hooks the GUI added to update and synch the GUI with the project.  The OnModifiedChanged event might not
				// fire when the Modified property was set (if the value was the same) so we ensure it is fired below.
				RaiseOnInitializedEvent();
				RaiseOnModifiedChangedEvent();

				if (_creationMethod == CreationMethod.Deserialized)
				{
					RaiseOnOpenedEvent();
				}

				// After we setup controls and such, the project may be reading that it was modified.  Since it is a brand new blank project,
				// we reset the modified value.
				if (_initialized == true)
				{
					// Initialized was not true, but now it is, so we reset the _modified value.
					this.Modified = false;
				}
			}
		}
	}

	/// <summary>
	/// Specifies if the project has been modified since last being saved/loaded.
	/// </summary>
	[XmlIgnore()]
	public bool Modified
	{
		get => _modified;

		set
		{
			if (_modified != value)
			{
				_modified = value;
				RaiseOnModifiedChangedEvent();
			}
		}
	}

	/// <summary>
	/// Specifies if the project has been closed.
	/// </summary>
	[XmlIgnore()]
	public bool IsClosed { get => _closed; }

	#endregion

	#region Methods

	/// <summary>
	/// Access for manually firing event for external sources.
	/// </summary>
	private void RaiseOnModifiedChangedEvent()
	{
		// Two conditions are required to fire the event.
		//
		// 1. Only allow event triggering to occur after the project is fully initialized.  That way we are not setting
		// controls and such during file reading.  That would cause a problem because the controls themselves could
		// trigger events.
		//
		// 2. Only run the event if we have event subscribers.
		if (_initialized && OnModifiedChanged != null)
		{
			OnModifiedChanged(_modified);
		}
	}

	/// <summary>
	/// Call back when the objects held by the projects are modified.
	/// </summary>
	protected void SetAsModified()
	{
		Modified = true;
	}

	/// <summary>
	/// Access for manually firing event for external sources.
	/// </summary>
	private void RaiseOnInitializedEvent()
	{
		// Trigger event only if there are any subscribers.
		this.OnInitialized?.Invoke();
	}

	/// <summary>
	/// Access for manually firing event for external sources.
	/// </summary>
	private void RaiseOnOpenedEvent()
	{
		// Trigger event only if there are any subscribers.
		System.Diagnostics.Debug.Assert(_projectExtractor != null);
		this.OnOpened?.Invoke(_projectExtractor);
	}

	/// <summary>
	/// Access for manually firing event for external sources.
	/// </summary>
	/// <param name="projectCompressor">ProjectCompressor used for saving the project.</param>
	private void RaiseOnSavingEvent(ProjectCompressor projectCompressor)
	{
		// Trigger event only if there are any subscribers.
		System.Diagnostics.Debug.Assert(_projectExtractor != null);
		this.OnSaving?.Invoke(projectCompressor);
	}

	/// <summary>
	/// Access for manually firing event for external sources.
	/// </summary>
	private void RaiseOnClosedEvent()
	{
		// Trigger event only if there are any subscribers.
		this.OnClosed?.Invoke();
	}

	/// <summary>
	/// Clean up.
	/// </summary>
	public virtual void Close()
	{
		_closed = true;
		RaiseOnClosedEvent();
	}

	#endregion

	#region XML

	/// <summary>
	/// Create an instance from a file.
	/// </summary>
	/// <param name="projectExtractor">ProjectExtractor used to unzip project files.</param>
	protected static T Deserialize<T>(ProjectExtractor projectExtractor) where T : Project
	{
		Project project				= Deserialize<T>(projectExtractor.GetFilePath(_projectFileName));
		project._projectExtractor	= projectExtractor;

		// Project needs to remember where the main file was saved from, not the temporary file we used during the unzipping.
		project.Path				= projectExtractor.Path;

		return (T)project;
	}

	/// <summary>
	/// Create an instance from a file.
	/// </summary>
	/// <param name="path">The file to read from.</param>
	protected static T Deserialize<T>(string path) where T : Project
	{
		Project? project				= Serialization.DeserializeObject<T>(path);
		System.Diagnostics.Trace.Assert(project != null);
		project._creationMethod			= CreationMethod.Deserialized;
		project.Path					= path;

		// When deserializing, the pointers to the parent (containing) instances are not established so we need to do that manually.
		project.DeserializationInitialization();

		return (T)project;
	}

	/// <summary>
	/// Writes a Project file (compressed file containing all the project's files).  Uses a ProjectCompressor to zip all files.  An
	/// event of RaiseOnSavingEvent fires allowing other files to be added to the project.
	///
	/// The this.Path must be set and represent a valid path or this method will throw an exception.
	/// </summary>
	/// <exception cref="InvalidOperationException">Thrown when the projects path is not set or not valid.</exception>
	public virtual void Serialize()
	{
		SerializeWorker();

		this.Modified = false;
	}

	/// <summary>
	/// Main work of serialization and compressing project files.
	/// </summary>
	protected void SerializeWorker()
	{
		if (!this.Saveable)
		{
			throw new InvalidOperationException("The Project cannot be currently saved.  A valid path must be specified.");
		}

		// Create the ProjectCompressor.
		ProjectCompressor projectCompressor = new(Path);

		// Have any subscribers add any files for compressing.
		RaiseOnSavingEvent(projectCompressor);

		string projectFilePath = projectCompressor.RegisterFile(_projectFileName);
		Serialization.SerializeObject(this, projectFilePath);

		projectCompressor.CompressFiles();
	}

	/// <summary>
	/// Write this object to a file to the provided path.
	/// </summary>
	/// <param name="path">Path (full path and filename) to write to.</param>
	/// <exception cref="InvalidOperationException">Thrown when the projects path is not set or not valid.</exception>
	public void Serialize(string path)
	{
		if (!DigitalProduction.IO.Path.PathIsWritable(path))
		{
			throw new InvalidOperationException("The Project cannot be currently saved.  A valid path must be specified.");
		}

		Serialization.SerializeObject(this, path);
	}

	/// <summary>
	/// Initialize data structure after reading from XML file.
	/// 
	/// When desearializing, the pointers to other objects are not valid and events are not hooked up so we need to do that
	/// manually.
	/// </summary>
	protected abstract void DeserializationInitialization();

	#endregion

} // End class.