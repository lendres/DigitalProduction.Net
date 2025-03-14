using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DigitalProduction.ComponentModel;

/// <summary>
/// Implements the INotifyPropertyChanged interface to provide a reusable base class.
/// </summary>
public abstract class NotifyPropertyChanged : INotifyPropertyChanged
{
	#region Fields

	public event		PropertyChangedEventHandler?	PropertyChanged;
	private readonly	Dictionary<string, object?>		_properties			= [];

	#endregion

	#region Methods

	protected virtual bool SetValue(object? value, [CallerMemberName] string propertyName = null!)
	{
		if (_properties.TryGetValue(propertyName!, out var item) && item == value)
		{
			return false;
		}

		_properties[propertyName!] = value;
		OnPropertyChanged(propertyName);

		return true;
	}

	protected T GetValueOrDefault<T>(T defaultValue, [CallerMemberName] string propertyName = null!)
	{
		if (_properties.TryGetValue(propertyName!, out var value))
		{
			if (value != null)
			{
				return (T)value;
			}
		}

		return defaultValue;
	}

	protected T? GetValue<T>([CallerMemberName] string propertyName = null!)
	{
		if (_properties.TryGetValue(propertyName!, out var value))
		{
			return (T?)value;
		}

		return default;
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
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	/// <summary>
	/// General method for invoking the PropertyChanged event from a property.  Can be relay changed event from class fields.
	/// 
	/// The PropertyChanged event can only be invoke from inside the class it is declared.
	/// </summary>
	/// <param name="propertyName">The of the property that changed.</param>
	protected virtual void OnPropertyChanged(object? sender, PropertyChangedEventArgs eventArgs)
	{
		PropertyChanged?.Invoke(sender, eventArgs);
	}

	#endregion
}