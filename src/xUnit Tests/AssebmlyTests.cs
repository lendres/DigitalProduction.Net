using DigitalProduction.Mathmatics;
using DigitalProduction.Strings;

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


	#endregion

} // End class.