using System.Xml.Serialization;

namespace DigitalProduction.ComponentModel;

/// <summary>
/// Combined the NotifyPropertyChanged and NotifyModifiedChanged into a single base class.
/// </summary>
public abstract class NotifyPropertyModifiedChanged : NotifyPropertyChanged, INotifyModifiedChanged
{
	#region Fields

	public event ModifiedChangedEventHandler? ModifiedChanged;

	private	bool _modified = false;

	#endregion

	#region Properties

	/// <summary>
	/// Specifies if changes have been made since the last save.
	/// </summary>
	[XmlIgnore()]
	public bool Modified
	{
		get => _modified;

		protected set
		{
			if (_modified != value)
			{
				_modified = value;
				ModifiedChanged?.Invoke(this, value);
			}
		}
	}

	#endregion
}