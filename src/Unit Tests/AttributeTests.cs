using DigitalProduction.Reflection;
using DigitalProduction.Xml.Serialization;

namespace DigitalProduction.UnitTests;

public class AttributeTests
{
	#region Attribute Tests

	/// <summary>
	/// Test retrieval of attributes.
	/// </summary>
	[Fact]
	public void Attributes()
	{
		AirlineCompany company = AirlineCompany.CreateAirline();
		Assert.Equal("Airline", DigitalProduction.Reflection.Attributes.GetDisplayName(company));
		Assert.Equal("Airline", DigitalProduction.Reflection.Attributes.GetDisplayName(typeof(AirlineCompany)));

		Family family = Family.CreateFamily();
		List<string> aliases = DigitalProduction.Reflection.Attributes.GetAliases(family);
		Assert.Equal("Family Members", aliases[0]);
		Assert.Equal("Relatives", aliases[1]);
	}

	/// <summary>
	/// Test retrieval of alternate name attributes.
	/// </summary>
	[Fact]
	public void AlternateNamesAttribute()
	{
		Assert.Equal("737", DigitalProduction.Reflection.Attributes.GetAlternateName(AirPlaneType.Boeing737, AlternateNameType.ShortName));
		Assert.Equal("Boeing 747", DigitalProduction.Reflection.Attributes.GetAlternateName(AirPlaneType.Boeing747, AlternateNameType.LongName));
	}

	#endregion

} // End class.