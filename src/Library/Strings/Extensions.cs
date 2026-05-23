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

		string normalizedFirstString	= NormalizeLineEndings(firstString);
		string normalizedSecondString	= NormalizeLineEndings(secondString);

		return normalizedFirstString == normalizedSecondString;
	}

	private static string NormalizeLineEndings(string text)
	{
		return text
			.Replace("\r\n", "\n")
			.Replace('\r', '\n');
	}

	#endregion
}