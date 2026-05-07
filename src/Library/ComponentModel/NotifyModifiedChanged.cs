using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DigitalProduction.ComponentModel;

public abstract class NotifyModifiedChanged : GenericProperties, INotifyModifiedChanged
{
	#region Fields

	/// <summary>
	/// Occurs when data in the object is modified.  Used, for example, to enable/disable the a Save button based on whether the object
	/// has been modified and needs to be saved.
	/// </summary>
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

	#endregion
}