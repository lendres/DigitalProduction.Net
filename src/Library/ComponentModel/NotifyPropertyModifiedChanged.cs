using System.ComponentModel;
using System.Runtime.CompilerServices;
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

	#region Methods

	protected override bool SetValue(object? value, [CallerMemberName] string propertyName = null!)
	{
		if (base.SetValue(value, propertyName))
		{
			Modified = true;
			return true;
		}
		return false;
	}

	protected override void OnPropertyChanged([CallerMemberName] string propertyName = null!)
	{
		base.OnPropertyChanged(propertyName);
		Modified = true;
	}

	#endregion
}