using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DigitalProduction.ComponentModel;

/// <summary>
/// Implements the INotifyPropertyChanged interface to provide a reusable base class.
/// </summary>
public abstract class NotifyPropertyChanged : GenericProperties, INotifyPropertyChanged
{
	#region Fields

	public event PropertyChangedEventHandler? PropertyChanged;

	#endregion

	#region Properties

	/// <summary>
	/// Specifies whether the PropertyChanged event should be invoked when a property value is changed.
	/// This can be used to temporarily disable change events when making multiple changes to a class.
	/// </summary>
	public bool InvokeChangeEvents { get; set; } = true;

	#endregion

	#region Methods

	protected override bool SetValue(object? value, [CallerMemberName] string propertyName = null!)
	{
		if (base.SetValue(value, propertyName))
		{
			OnPropertyChanged(propertyName);
			return true;
		}
		return false;
	}

	/// <summary>
	/// Easy method for invoking the PropertyChanged event from a property.  If no argument is supplied, the property name
	/// will automatically be used.
	/// 
	/// The PropertyChanged event can only be invoke from inside the class it is declared.
	/// </summary>
	/// <param name="propertyName">The of the property that changed.</param>
	protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
	{
		if (InvokeChangeEvents)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	/// <summary>
	/// General method for invoking the PropertyChanged event from a property.  Can be relay changed event from class fields.
	/// 
	/// The PropertyChanged event can only be invoke from inside the class it is declared.
	/// </summary>
	/// <param name="propertyName">The of the property that changed.</param>
	protected virtual void OnPropertyChanged(object? sender, PropertyChangedEventArgs eventArgs)
	{
		if (InvokeChangeEvents)
		{
			PropertyChanged?.Invoke(sender, eventArgs);
		}
	}

	#endregion
}