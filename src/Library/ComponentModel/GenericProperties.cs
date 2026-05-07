using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace DigitalProduction.ComponentModel;

public class GenericProperties
{
	#region Fields

	private readonly Dictionary<string, object?> _properties = [];

	#endregion

	#region Methods

	/// <summary>
	/// Generic set property method that can be used by any property in the class.  The property name is automatically determined
	/// by the CallerMemberName attribute, so this method can be called without specifying the property name.
	/// </summary>
	/// <param name="value">The value to set.</param>
	/// <param name="propertyName">The name of the property. This is automatically provided by the CallerMemberName attribute.</param>
	/// <returns>True if the value was changed, false otherwise.</returns>
	protected virtual bool SetValue(object? value, [CallerMemberName] string propertyName = null!)
	{
		bool foundProperty = _properties.TryGetValue(propertyName!, out var item);

		if (foundProperty)
		{
			// Apparently, there are special cases where value == true and item == true, but value == item is false.
			// Is seems like using "var item" is returning and instance and the "==" operator is saying this instance
			// is not the other instance rather than checking that both are true.
			if (value != null && value.Equals(item))
			{
				return false;
			}

			// If we get here, value is null.  So if item is also null, they are equal and we can return.
			if (item == null)
			{
				return false;
			}
		}

		_properties[propertyName!] = value;

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

	#endregion
}
