namespace DigitalProduction.UnitTests;

/// <summary>
/// Test cases the the Mathmatics namespace.
/// </summary>
public class AssemblyTests
{
	#region Members
	#endregion

	#region Tests

	/// <summary>
	/// Find assembly by name test.
	/// </summary>
	[Fact]
	public void FindAssemblyByName()
	{
		System.Reflection.Assembly? assembly = DigitalProduction.Reflection.Assembly.GetAssemblyByName("DigitalProduction.Net");
		Assert.NotNull(assembly);
	}

	/// <summary>
	/// Company name test.
	/// </summary>
	[Fact]
	public void CompanyName()
	{
		string result = DigitalProduction.Reflection.Assembly.Company();
		Assert.Equal("Digital Production", result);
	}

	/// <summary>
	/// Authors test.
	/// </summary>
	[Fact]
	public void AuthorsName()
	{
		string result = DigitalProduction.Reflection.Assembly.Authors();
		Assert.Equal("Lance A. Endres", result);
	}

	/// <summary>
	/// Website test.
	/// </summary>
	[Fact]
	public void Website()
	{
		System.Reflection.Assembly? assembly = System.Reflection.Assembly.GetExecutingAssembly();
		Assert.NotNull(assembly);

		string result = DigitalProduction.Reflection.Assembly.Website(assembly);

		Assert.Equal("https://github.com/lendres/DigitalProduction.Net", result);
	}

	/// <summary>
	/// Issues address test.
	/// </summary>
	[Fact]
	public void IssuesAddress()
	{
		System.Reflection.Assembly? assembly = System.Reflection.Assembly.GetExecutingAssembly();
		Assert.NotNull(assembly);

		string result = DigitalProduction.Reflection.Assembly.IssuesAddress(assembly);

		Assert.Equal("https://github.com/lendres/DigitalProduction.Net/issues", result);
	}

	/// <summary>
	/// Issues address test.
	/// </summary>
	[Fact]
	public void DocumentationAddress()
	{
		string result = DigitalProduction.Reflection.Assembly.DocumentationAddress();

		Assert.Equal("https://github.com/lendres/DigitalProduction.Net/wiki", result);
	}

	/// <summary>
	/// Version test.
	/// </summary>
	[Fact]
	public void Version()
	{
		string result	= DigitalProduction.Reflection.Assembly.Version();
		Assert.Equal("1.1.3.0", result);

		result			= DigitalProduction.Reflection.Assembly.Version(true);
		Assert.Equal("1.1.3", result);
	}

	/// <summary>
	/// Location test.
	/// </summary>
	[Fact]
	public void Location()
	{
		// The location is difficult to test.
		// It will change depending on the location we run from.
		// You don't seem to be able to edit the location or instantiate an assembly directly.
		// Therefore, we will just run some basic checks.
		string? result	= DigitalProduction.Reflection.Assembly.Path();
		Assert.NotNull(result);

		foreach (string prefix in DigitalProduction.IO.Path.DosDevicePathPrefixes)
		{
			Assert.False(result.StartsWith(prefix));
		}

		// The location of the assembly will change a lot, so we won't bother to test it, but we will print it to the output window.		
		System.Diagnostics.Debug.WriteLine("");
		System.Diagnostics.Debug.WriteLine(result);
		System.Diagnostics.Debug.WriteLine("");
	}

	#endregion

} // End class.