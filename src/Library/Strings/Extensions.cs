using System.Text.RegularExpressions;

namespace DigitalProduction.Strings;

public static class Extensions
{
	#region Trimming

	/// <summary>
	/// Removes substrings from the beginning of a string.
	/// </summary>
	/// <param name="target">Current string.</param>
	/// <param name="trimStrings">The strings to remove from the current string.</param>
	public static string TrimStart(this string target, params string[] trimStrings)
	{
		string result = target;
		foreach (string trimString in trimStrings)
		{
			result = TrimStart(result, trimString);
		}
		return result;
	}

	/// <summary>
	/// Removes a substring from the beginning of a string.
	/// </summary>
	/// <param name="target">Current string.</param>
	/// <param name="trimString">The string to remove from the current string.</param>
	public static string TrimStart(this string target, string trimString)
	{
		if (string.IsNullOrEmpty(target) || string.IsNullOrEmpty(trimString))
		{
			return target;
		}

		string result = target;
		while (result.StartsWith(trimString))
		{
			result = result[trimString.Length..];
		}

		return result;
	}

	/// <summary>
	/// Removes substrings from the end of a string.
	/// </summary>
	/// <param name="target">Current string.</param>
	/// <param name="trimStrings">The strings to remove from the current string.</param>
	public static string TrimEnd(this string target, params string[] trimStrings)
	{
		string result = target;
		foreach (string trimString in trimStrings)
		{
			result = TrimEnd(result, trimString);
		}
		return result;
	}

	/// <summary>
	/// Removes a substring from the end of a string.
	/// </summary>
	/// <param name="target">Current string.</param>
	/// <param name="trimString">The string to remove from the current string.</param>
	public static string TrimEnd(this string target, string trimString)
	{
		if (string.IsNullOrEmpty(target) || string.IsNullOrEmpty(trimString))
		{
			return target;
		}

		string result = target;
		while (result.EndsWith(trimString))
		{
			result = result.Substring(0, result.Length - trimString.Length);
		}

		return result;
	}

	#endregion

	#region Line Endings

	/// <summary>
	/// Checks if two strings are equal, ignoring differences in line endings (e.g., \r\n vs. \n).
	/// </summary>
	/// <param name="firstString">The first string to compare.</param>
	/// <param name="secondString">The second string to compare.</param>
	/// <returns>True if the strings are equal ignoring line endings; otherwise, false.</returns>
	public static bool EqualsIgnoringLineEndings(this string? firstString, string? secondString)
	{
		if (firstString is null || secondString is null)
		{
			return firstString == secondString;
		}

		string normalizedFirstString	= firstString.NormalizeLineEndings();
		string normalizedSecondString	= secondString.NormalizeLineEndings();

		return normalizedFirstString == normalizedSecondString;
	}

	/// <summary>
	/// Normalizes line endings in the input string to a specified line ending. If no line ending is provided,
	/// it defaults to the system's newline character(s).
	/// </summary>
	/// <param name="text">The input string.</param>
	/// <param name="lineEnding">The line ending to replace with.</param>
	/// <returns>The string with line endings normalized.</returns>
	public static string NormalizeLineEndings(this string text, string? lineEnding = "")
	{
		if (string.IsNullOrEmpty(lineEnding))
		{
			lineEnding = Environment.NewLine;
		}
		return ReplaceAllLineEndings(text, lineEnding);
	}

	/// <summary>
	/// Removes the last line ending from the input string.
	/// </summary>
	/// <param name="text">The input string.</param>
	/// <returns>The string with the last line ending removed.</returns>
	public static string RemoveLastLineEnding(this string text)
	{
		return text.TrimEnd("\r\n", "\r", "\n");
	}

	/// <summary>
	/// Removes all line endings from the input string, effectively concatenating all lines into a single line.
	/// </summary>
	/// <param name="text">The input string.</param>
	/// <returns>The string with all line endings removed.</returns>
	public static string RemoveAllLineEnding(this string text)
	{
		return ReplaceAllLineEndings(text, string.Empty);
	}

	/// <summary>
	/// Replaces all types of line endings in the input string with a specified line ending. This method handles
	/// Windows (\r\n), Unix (\n), and old Mac (\r) line endings.
	/// </summary>
	/// <param name="text">The input string.</param>
	/// <param name="lineEnding">The line ending to replace with.</param>
	/// <returns>The string with line endings replaced.</returns>
	private static string ReplaceAllLineEndings(string text, string lineEnding)
	{
		return Regex.Replace(text, @"\r\n|\r(?!\n)|(?<!\r)\n", lineEnding);
	}

	#endregion
}