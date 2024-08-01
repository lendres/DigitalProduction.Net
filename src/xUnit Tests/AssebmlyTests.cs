namespace DigitalProduction.UnitTests;

/// <summary>
/// Test cases the the Mathmatics namespace.
/// </summary>
public class AssebmlyTests
{
	#region Members
	#endregion

	#region Tests

	/// <summary>
	/// Covariance test.
	/// </summary>
	[Fact]
	public void CompanyName()
	{
		//System.Reflection.Assembly? callingAssembly = System.Reflection.Assembly.GetCallingAssembly();
		System.Reflection.Assembly? assembly = System.Reflection.Assembly.GetExecutingAssembly();
		//System.Reflection.Assembly? assembly = System.Reflection.Assembly.GetEntryAssembly();
		Assert.NotNull(assembly);

		string result = DigitalProduction.Reflection.Assembly.Company(assembly);

		Assert.Equal("Digital Production", result);
	}

	/// <summary>
	/// Authors test.
	/// </summary>
	[Fact]
	public void AuthorsName()
	{
		System.Reflection.Assembly? assembly = System.Reflection.Assembly.GetExecutingAssembly();
		Assert.NotNull(assembly);

		string result = DigitalProduction.Reflection.Assembly.Authors(assembly);

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

		Assert.Equal("https://github.com/lendres/C-Sharp-Dot-Net-Library", result);
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

		Assert.Equal("https://github.com/lendres/C-Sharp-Dot-Net-Library/issues", result);
	}

	/// <summary>
	/// Version test.
	/// </summary>
	[Fact]
	public void Version()
	{
		System.Reflection.Assembly? assembly = System.Reflection.Assembly.GetExecutingAssembly();
		Assert.NotNull(assembly);

		string result	= DigitalProduction.Reflection.Assembly.Version(assembly);
		Assert.Equal("1.1.2.0", result);

		result			= DigitalProduction.Reflection.Assembly.Version(assembly, true);
		Assert.Equal("1.1.2", result);
	}	

	#endregion

} // End class.