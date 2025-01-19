using DigitalProduction.Xml.Serialization;
using DigitalProduction.Strings;

namespace DigitalProduction.UnitTests;

public class StringTests
{
	/// <summary>
	/// Basic serialization and deserialization test.
	/// </summary>
	[Fact]
	public void DateTimeWithPreciseSecondsTest()
	{
		// Year, month, day, hour, minute, second.
		DateTime dateTime = new(1992, 3, 19, 15, 19, 29);

		string result = Format.DateTimeWithPreciseSeconds(dateTime, DateTimePrecisionFormat.Descending);
		Assert.Equal("1992/3/19 15:19:29.0000000", result);

		result = Format.DateTimeWithPreciseSeconds(dateTime, DateTimePrecisionFormat.US12Hour);
		Assert.Equal("3/19/1992 3:19:29.0000000 PM", result);

		result = Format.DateTimeWithPreciseSeconds(dateTime, DateTimePrecisionFormat.US24Hour);
		Assert.Equal("3/19/1992 15:19:29.0000000", result);

		result = Format.DateTimeWithPreciseSeconds(dateTime, DateTimePrecisionFormat.International);
		Assert.Equal("19/3/1992 15:19:29.0000000", result);
	}

} // End class.