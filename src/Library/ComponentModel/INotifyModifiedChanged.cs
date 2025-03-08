namespace DigitalProduction.ComponentModel;

/// <summary>
/// Interface for a class that can track when it has been modified or saved and raise an event when it gets modified.
/// 
/// Recommended implementation:
/// private bool _modified = false;
/// 
/// <summary>
/// Event for when the object was modified.
/// </summary>
/// public event ModifiedChangedEventHandler? ModifiedChanged;
/// 
/// <summary>
/// Specifies if the project has been modified since last being saved/loaded.
/// </summary>
/// [XmlIgnore()]
/// public bool Modified
/// {
/// 	get => _modified;
/// 
///		private set
///		{
/// 		if (_modified != value)
/// 		{
/// 			_modified = value;
/// 			ModifiedChanged?.Invoke(this, value);
/// 		}
/// 	}
/// }
/// </summary>

/// <summary>
/// Event handler for when modified is changed.
/// </summary>
public delegate void ModifiedChangedEventHandler(object sender, bool modified);
 
public interface INotifyModifiedChanged
{
	/// <summary>
	/// Event for when the object was modified.
	/// </summary>
	event ModifiedChangedEventHandler? ModifiedChanged;

	/// <summary>
	/// Specifies if the project has been modified since last being saved/loaded.
	/// </summary>
	bool Modified { get; }

} // End interface.