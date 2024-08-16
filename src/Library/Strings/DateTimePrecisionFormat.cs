using System.ComponentModel;

namespace DigitalProduction.Strings;

/// <summary>
/// Add summary here.
/// 
/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
/// 
/// The "Length" enumeration can be used in loops as a convenient way of terminating a loop that does not have to be changed if
/// the number of items in the enumeration changes.  The "Length" enumeration must be the last item.
/// for (int i = 0; i &lt; (int)EnumType.Length; i++) {...}
/// </summary>
public enum DateTimePrecisionFormat
{
	/// <summary>Year, month, data, 24 hour, minute, second, seconds fraction</summary>
	[Description("Decending")]
	Descending,

	/// <summary></summary>
	[Description("International")]
	International,

	/// <summary></summary>
	[Description("US24Hour")]
	US12Hour,

	/// <summary></summary>
	[Description("US24Hour")]
	US24Hour,

	/// <summary>The number of types/items in the enumeration.</summary>
	[Description("Length")]
	Length

} // End enum.