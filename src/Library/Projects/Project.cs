using DigitalProduction.ComponentModel;
using DigitalProduction.Xml.Serialization;
using System.Xml.Serialization;

namespace DigitalProduction.Projects;

/// <summary>
/// Base class for a Project.  Provides common functionality.
/// </summary>
public abstract class Project : NotifyPropertyModifiedChanged
{
	#region Events

	/// <summary>
	/// Occurs when the project is initialized.  This event occurs every time a project is created, regardless of how it is created.  For
	/// example, this event will fire if the project is created by instantiating a new instance of a project (new Project) or if the Project
	/// is created by deserializing from disk.
	/// </summary>
	public event Action?							Initialized;

	/// <summary>
	/// Occurs after a Project has been deserialized from disk.  Note that this event does not fire when creating a new instance of a
	/// Project (new Project()).  Hook into this event to perform any operations or GUI setup required to be performed after opening
	/// a Project from disk.
	/// </summary>
	public event Action?							Opened;

	/// <summary>
	/// Occurs after a Project has been closed.
	/// </summary>
	public event Action?							Closed;

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

	private const string							_projectFileName				= "Project.xml";

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	protected Project(CompressionType compressionType)
	{
		CompressionType = compressionType;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Specifies how a project should be saved (with compression or without).
	/// </summary>
	[XmlIgnore()]
	public CompressionType CompressionType { get; private set; } = CompressionType.Compressed;

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
			//string thisVersion	= DigitalProduction.Strings.Format.MajorMinorVersionNumber(Version);
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
	public bool IsSaveable { get => DigitalProduction.IO.Path.PathIsWritable(Path); }

	/// <summary>
	/// Specifies that the project has finished initialization and should fire events from this point forward.
	/// </summary>
	[XmlIgnore()]
	public bool IsInitialized
	{
		get => _initialized;

		protected set
		{
			if (_initialized != value)
			{
				_initialized = value;

				if (_initialized == true)
				{
					// Fire events.  Allows any hooks the GUI added to update and synch the GUI with the project.  The OnModifiedChanged event might not
					// fire when the Modified property was set (if the value was the same) so we ensure it is fired below.
					RaiseInitializedEvent();

					if (_creationMethod == CreationMethod.Deserialized)
					{
						RaiseOpenedEvent();
					}

					// After we setup controls and such, the project may be reading that it was modified.  Since it is a brand new blank project,
					// we reset the modified value.
					// Initialized was not true, but now it is, so we reset the _modified value.
					Modified = false;
				}
			}
		}
	}

	/// <summary>
	/// Specifies if the project has been closed.
	/// </summary>
	[XmlIgnore()]
	public bool IsClosed { get; private set; }

	#endregion

	#region Methods

	/// <summary>
	/// Call back when the objects held by the projects are modified.
	/// </summary>
	protected void OnChildModifiedChanged(object sender, bool modified)
	{
		// If a child changed from unmodified to modified, then we need to set ourself as modified.
		// Modified == true propigates from children to parent.
		// Modified == false can only propigate from parent to child through saving.
		if (modified)
		{
			Modified = true;
		}
	}

	/// <summary>
	/// Access for manually firing event for external sources.
	/// </summary>
	private void RaiseInitializedEvent()
	{
		// Trigger event only if there are any subscribers.
		Initialized?.Invoke();
	}

	/// <summary>
	/// Access for manually firing event for external sources.
	/// </summary>
	private void RaiseOpenedEvent()
	{
		// Trigger event only if there are any subscribers.
		System.Diagnostics.Debug.Assert(_projectExtractor != null);
		Opened?.Invoke();
	}

	/// <summary>
	/// Clean up.
	/// </summary>
	public virtual void Close()
	{
		IsClosed = true;
		Closed?.Invoke();
	}

	#endregion

	#region XML

	/// <summary>
	/// Create an instance from a file.
	/// </summary>
	/// <param name="projectExtractor">ProjectExtractor used to unzip project files.</param>
	public static T Deserialize<T>(string path, CompressionType compressionType) where T : Project
	{
		T project;
		switch (compressionType)
		{
			case CompressionType.Compressed:
				project = DeserializeCompressedFile<T>(path);
				break;
			case CompressionType.Uncompressed:
				project = DeserializeProjectFile<T>(path);
				break;
			default:
				throw new Exception("Invalid project compression type.");
		}

		project.CompressionType		= compressionType;
		project.Path				= path;
		return project;
	}

	/// <summary>
	/// Create an instance from a file.
	/// </summary>
	/// <param name="projectExtractor">ProjectExtractor used to unzip project files.</param>
	private static T DeserializeCompressedFile<T>(string path) where T : Project
	{
		ProjectExtractor projectExtractor = ProjectExtractor.ExtractFiles(path);

		Project project				= DeserializeProjectFile<T>(projectExtractor.GetFilePath(_projectFileName));
		project._projectExtractor	= projectExtractor;

		return (T)project;
	}

	/// <summary>
	/// Create an instance from a file.
	/// </summary>
	/// <param name="path">The file to read from.</param>
	private static T DeserializeProjectFile<T>(string path) where T : Project
	{
		Project? project			= Serialization.DeserializeObject<T>(path);
		System.Diagnostics.Trace.Assert(project != null);
		project._creationMethod		= CreationMethod.Deserialized;

		// When deserializing, the pointers to the parent (containing) instances are not established so we need to do that manually.
		project.DeserializationInitialization();

		return (T)project;
	}

	/// <summary>
	/// Writes a Project file.
	/// </summary>
	public virtual void Serialize()
	{
		if (!IsSaveable)
		{
			throw new InvalidOperationException("The Project cannot be currently saved.  A valid path must be specified.");
		}
		SerializeWorker();
	}

	/// <summary>
	/// Writes a Project file to the path specified.
	/// </summary>
	public virtual void Serialize(string path)
	{
		Path = path;
		SerializeWorker();
	}

	/// <summary>
	/// Main work of serialization and/or compressing project files.
	/// 
	/// The Path must be set and represent a valid path or this method will throw an exception.
	/// <exception cref="InvalidOperationException">Thrown when the projects path is not set or not valid.</exception>
	/// </summary>
	protected void SerializeWorker()
	{
		switch (CompressionType)
		{
			case CompressionType.Compressed:
				// Create the ProjectCompressor.
				ProjectCompressor projectCompressor = new(Path);

				string projectFilePath = projectCompressor.RegisterFile(_projectFileName);
				RegisterFilesForSaving(projectCompressor);

				Serialization.SerializeObject(this, projectFilePath);

				projectCompressor.CompressFiles();
				break;

			case CompressionType.Uncompressed:
				Serialization.SerializeObject(this, Path);
				break;

			default:
				throw new Exception("Invalid project compression type.");
		}

		Modified = false;
	}

	/// <summary>
	/// Adds additional file for projects that are compressed.  A derived class should override this to add any additional files.
	/// </summary>
	/// <param name="projectCompressor">ProjectCompressor.</param>
	protected virtual void RegisterFilesForSaving(ProjectCompressor projectCompressor)
	{
	}

	/// <summary>
	/// Initialize data structure after reading from XML file.
	/// 
	/// When desearializing, the pointers to other objects are not valid and events are not hooked up so we need to do that
	/// manually.
	/// </summary>
	protected virtual void DeserializationInitialization()
	{
	}

	#endregion

} // End class.