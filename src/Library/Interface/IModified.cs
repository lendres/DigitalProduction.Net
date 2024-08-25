using DigitalProduction.Delegates;

namespace DigitalProduction.Interface;

/// <summary>
/// Interface for a class that can track when it has been modified or saved and raise an event when it gets modified.
/// 
/// Recommended implementation:
/// bool _modified = false;
/// 
/// <summary>
/// Specifies if the project has been modified since last being saved/loaded.
/// </summary>
/// [XmlIgnore()]
/// public bool Modified
/// {
/// 	get => _modified;
/// 
///		set
///		{
/// 		if (_modified != value)
/// 		{
/// 			_modified = value;
/// 			RaiseOnModifiedChangedEvent();
/// 		}
/// 	}
/// }
/// 
/// <summary>
/// Access for manually firing event for external sources.
/// </summary>
/// private void RaiseOnModifiedChangedEvent() => OnModifiedChanged?.Invoke();
/// </summary>
public interface IModified
{
	/// <summary>
	/// Event for when the object was modified.
	/// </summary>
	event NoArgumentsEventHandler OnModifiedChanged;

	/// <summary>
	/// Specifies if the project has been modified since last being saved/loaded.
	/// </summary>
	bool Modified { get; set; }

} // End interface.