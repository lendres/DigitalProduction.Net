using DigitalProduction.Strings;

namespace DigitalProduction.UnitTests;

public class StringExtensionsTests
{
	#region String Trimming Tests

	#region TrimStart Tests

	[Theory]
	[InlineData("abcValue", "abc", "Value")]
	[InlineData("abcabcValue", "abc", "Value")]
	[InlineData("ValueabcValue", "abc", "ValueabcValue")]
	[InlineData("Value", "", "Value")]
	public void TrimStartSingleStringReturnsExpectedResult(string target, string trimString, string expectedResult)
	{
		string result = target.TrimStart(trimString);
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("///###Value", "Value")]
	[InlineData("///Value", "Value")]
	[InlineData("###Value", "Value")]
	public void TrimStartMultipleStringsReturnsExpectedResult(string target, string expectedResult)
	{
		string result = target.TrimStart("/", "#");
		Assert.Equal(expectedResult, result);
	}

	#endregion

	#region TrimEnd Tests

	[Theory]
	[InlineData("Valueabc", "abc", "Value")]
	[InlineData("Valueabcabc", "abc", "Value")]
	[InlineData("ValueabcValue", "abc", "ValueabcValue")]
	[InlineData("Value", "", "Value")]
	public void TrimEndSingleStringReturnsExpectedResult(string target, string trimString, string expectedResult)
	{
		string result = target.TrimEnd(trimString);
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("Value###///", "Value")]
	[InlineData("Value///", "Value")]
	[InlineData("Value###", "Value")]
	public void TrimEndMultipleStringsReturnsExpectedResult(string target, string expectedResult)
	{
		string result = target.TrimEnd("/", "#");
		Assert.Equal(expectedResult, result);
	}

	#endregion

	#endregion

	#region Line Ending Comparison Tests

	[Fact]
	public void EqualsIgnoringLineEndingsReturnsTrueForIdenticalStrings()
	{
		string firstString = "Line1\nLine2\nLine3";
		string secondString = "Line1\nLine2\nLine3";

		bool result = firstString.EqualsIgnoringLineEndings(secondString);

		Assert.True(result);
	}

	[Fact]
	public void EqualsIgnoringLineEndingsReturnsTrueForWindowsAndUnixLineEndings()
	{
		string firstString = "Line1\r\nLine2\r\nLine3";
		string secondString = "Line1\nLine2\nLine3";

		bool result = firstString.EqualsIgnoringLineEndings(secondString);

		Assert.True(result);
	}

	[Fact]
	public void EqualsIgnoringLineEndingsReturnsTrueForOldMacAndUnixLineEndings()
	{
		string firstString = "Line1\rLine2\rLine3";
		string secondString = "Line1\nLine2\nLine3";

		bool result = firstString.EqualsIgnoringLineEndings(secondString);

		Assert.True(result);
	}

	[Fact]
	public void EqualsIgnoringLineEndingsReturnsTrueForMixedLineEndings()
	{
		string firstString = "Line1\r\nLine2\rLine3\nLine4";
		string secondString = "Line1\nLine2\nLine3\nLine4";

		bool result = firstString.EqualsIgnoringLineEndings(secondString);

		Assert.True(result);
	}

	[Fact]
	public void EqualsIgnoringLineEndingsReturnsFalseForDifferentContent()
	{
		string firstString = "Line1\r\nLine2\r\nLine3";
		string secondString = "Line1\nDifferentLine\nLine3";

		bool result = firstString.EqualsIgnoringLineEndings(secondString);

		Assert.False(result);
	}

	[Fact]
	public void EqualsIgnoringLineEndingsReturnsFalseWhenOneStringHasExtraLine()
	{
		string firstString = "Line1\r\nLine2\r\nLine3";
		string secondString = "Line1\nLine2\nLine3\nLine4";

		bool result = firstString.EqualsIgnoringLineEndings(secondString);

		Assert.False(result);
	}

	[Fact]
	public void EqualsIgnoringLineEndingsReturnsTrueWhenBothStringsAreNull()
	{
		string? firstString = null;
		string? secondString = null;

		bool result = firstString.EqualsIgnoringLineEndings(secondString);

		Assert.True(result);
	}

	[Fact]
	public void EqualsIgnoringLineEndingsReturnsFalseWhenFirstStringIsNull()
	{
		string? firstString = null;
		string secondString = "Line1\nLine2";

		bool result = firstString.EqualsIgnoringLineEndings(secondString);

		Assert.False(result);
	}

	[Fact]
	public void EqualsIgnoringLineEndingsReturnsFalseWhenSecondStringIsNull()
	{
		string firstString = "Line1\nLine2";
		string? secondString = null;

		bool result = firstString.EqualsIgnoringLineEndings(secondString);

		Assert.False(result);
	}

	[Fact]
	public void EqualsIgnoringLineEndingsReturnsTrueForEmptyStrings()
	{
		string firstString = "";
		string secondString = "";

		bool result = firstString.EqualsIgnoringLineEndings(secondString);

		Assert.True(result);
	}

	#endregion
}