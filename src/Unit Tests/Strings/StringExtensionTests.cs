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

	#region Line Ending Removeal Tests

	#region RemoveLastLineEnding Tests

	[Theory]
	[InlineData("Line1\r\n", "Line1")]
	[InlineData("Line1\r", "Line1")]
	[InlineData("Line1\n", "Line1")]
	[InlineData("Line1", "Line1")]
	[InlineData("", "")]
	public void RemoveLastLineEndingReturnsExpectedResult(string text, string expectedResult)
	{
		string result = text.RemoveLastLineEnding();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("Line1\r\n\r\n", "Line1")]
	[InlineData("Line1\n\n", "Line1")]
	[InlineData("Line1\r\r", "Line1")]
	public void RemoveLastLineEndingOnlyRemovesFinalLineEnding(string text, string expectedResult)
	{
		string result = text.RemoveLastLineEnding();
		Assert.Equal(expectedResult, result);
	}

	[Fact]
	public void RemoveLastLineEndingDoesNotPartiallyRemoveWindowsLineEnding()
	{
		string text = "Line1\r\n";
		string result = text.RemoveLastLineEnding();
		Assert.Equal("Line1", result);
	}

	[Theory]
	[InlineData("Line1\r\nLine2\r\n", "Line1\r\nLine2")]
	[InlineData("Line1\rLine2\r", "Line1\rLine2")]
	[InlineData("Line1\nLine2\n", "Line1\nLine2")]
	public void RemoveLastLineEndingPreservesInternalLineEndings(string text, string expectedResult)
	{
		string result = text.RemoveLastLineEnding();

		Assert.Equal(expectedResult, result);
	}

	#endregion

	#region RemoveAllLineEnding Tests

	[Theory]
	[InlineData("Line1\r\nLine2", "Line1Line2")]
	[InlineData("Line1\rLine2", "Line1Line2")]
	[InlineData("Line1\nLine2", "Line1Line2")]
	[InlineData("Line1\r\nLine2\rLine3\nLine4", "Line1Line2Line3Line4")]
	[InlineData("Line1", "Line1")]
	[InlineData("", "")]
	public void RemoveAllLineEndingReturnsExpectedResult(string text, string expectedResult)
	{
		string result = text.RemoveAllLineEnding();
		Assert.Equal(expectedResult, result);
	}

	[Fact]
	public void RemoveAllLineEndingRemovesWindowsLineEndingsWithoutPartialReplacement()
	{
		string text = "Line1\r\nLine2\r\nLine3";
		string result = text.RemoveAllLineEnding();
		Assert.Equal("Line1Line2Line3", result);
	}

	[Theory]
	[InlineData("\r\n", "")]
	[InlineData("\r", "")]
	[InlineData("\n", "")]
	[InlineData("\r\n\r\n", "")]
	[InlineData("\r\r", "")]
	[InlineData("\n\n", "")]
	public void RemoveAllLineEndingRemovesOnlyLineEndings(string text, string expectedResult)
	{
		string result = text.RemoveAllLineEnding();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("A\r\nB\r\nC", "ABC")]
	[InlineData("A\rB\rC", "ABC")]
	[InlineData("A\nB\nC", "ABC")]
	[InlineData("A\r\nB\rC\nD", "ABCD")]
	public void RemoveAllLineEndingHandlesMixedLineEndingStyles(string text, string expectedResult)
	{
		string result = text.RemoveAllLineEnding();
		Assert.Equal(expectedResult, result);
	}

	#endregion


	#endregion
}