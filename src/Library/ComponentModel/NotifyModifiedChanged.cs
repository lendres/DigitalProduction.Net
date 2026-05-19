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

	/// <summary>
	/// Marks the object as saved, which sets Modified to false.  Override this method to perform
	/// any necessary actions to save the object, such as writing to disk.
	/// </summary>
	public virtual void Save()
	{
		Modified = false;
	}

	/// <summary>
	/// Generic set property method that can be used by any property in the class.  The property name is automatically determined
	/// by the CallerMemberName attribute, so this method can be called without specifying the property name.
	/// </summary>
	/// <param name="value">The value to set.</param>
	/// <param name="propertyName">The name of the property. This is automatically provided by the CallerMemberName attribute.</param>
	/// <returns>True if the value was changed, false otherwise.</returns>
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