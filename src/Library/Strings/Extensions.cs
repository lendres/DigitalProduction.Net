namespace DigitalProduction.Strings;

public static class Extensions
{
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
}