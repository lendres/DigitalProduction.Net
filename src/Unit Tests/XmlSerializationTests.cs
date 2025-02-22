using DigitalProduction.Xml.Serialization;

namespace DigitalProduction.UnitTests;

public class XmlSerializationTests
{
	#region XML Serialization

	/// <summary>
	/// Basic serialization and deserialization test.
	/// </summary>
	[Fact]
	public void XmlSerialization1()
	{

		string path = Path.Combine(Path.GetTempPath(), "test1.xml");

		Family family = Family.CreateFamily();

		Serialization.SerializeObject(family, path);
		Family? familyDeserialized = Serialization.DeserializeObject<Family>(path);
		Assert.NotNull(familyDeserialized);

		Person? person = familyDeserialized.GetPerson("Mom");
		Assert.NotNull(person);
		Assert.Equal(36, person.Age);

		person = familyDeserialized.GetPerson("Son");
		Assert.NotNull(person);
		Assert.Equal(4, person.Age);

		System.IO.File.Delete(path);
}

	/// <summary>
	/// 
	/// </summary>
	[Fact]
	public void XmlSerialization2()
	{
		string path = Path.Combine(Path.GetTempPath(), "test2.xml");

		AirlineCompany company = AirlineCompany.CreateAirline();

		company.Serialize(path);
		AirlineCompany? deserialized = Company.Deserialize<AirlineCompany>(path);
		Assert.NotNull(deserialized);

		Person? person = deserialized.GetEmployee("Manager");
		Assert.NotNull(person);

		Assert.Equal(36, person.Age);
		Assert.Equal(10, deserialized.NumberOfPlanes);

		File.Delete(path);
	}

	/// <summary>
	/// Test file 
	/// </summary>
	[Fact]
	public void XmlRepeatedSerializationDeserialization()
	{
		string path = Path.Combine(Path.GetTempPath(), "testrepeated.xml");

		AirlineCompany company = AirlineCompany.CreateAirline();

		for (int i = 0; i < 4; i++)
		{
			company.Serialize(path);
			AirlineCompany? deserialized = Company.Deserialize<AirlineCompany>(path);
			Assert.NotNull(deserialized);
			deserialized = Company.Deserialize<AirlineCompany>(path);
			Assert.NotNull(deserialized);
		}

		File.Delete(path);
	}

	/// <summary>
	/// Test the XML writer that writes full closing elements and never uses the short element close.
	/// </summary>
	[Fact]
	public void XmlTextWriterFullTest()
	{
		string path = Path.Combine(Path.GetTempPath(), "test1.xml");

		AirlineCompany company = AirlineCompany.CreateAirline();

		Serialization.SerializeObjectFullEndElement(company, path);

		File.Delete(path);
	}

	/// <summary>
	/// Serialization settings.
	/// </summary>
	[Fact]
	public void SerializationSettingsTest()
	{
		string path1 = Path.Combine(Path.GetTempPath(), "test1.xml");
		string path2 = Path.Combine(Path.GetTempPath(), "test2.xml");

		AirlineCompany company = AirlineCompany.CreateAirline();

		SerializationSettings settings				= new(company, path1);
		//settings.XmlSettings.Indent					= false;
		settings.XmlSettings.NewLineOnAttributes	= false;
		Serialization.SerializeObject(settings);

		settings.XmlSettings.NewLineOnAttributes	= true;
		settings.OutputFile							= path2;
		Serialization.SerializeObject(settings);

		File.Delete(path1);
		File.Delete(path2);
	}

	/// <summary>
	/// Custom dictionary test.
	/// </summary>
	[Fact]
	public void CustomDictionaryTest()
	{
		string path = Path.Combine(Path.GetTempPath(), "familydictionary.xml");

		FamilyDictionary family = FamilyDictionary.CreateFamily();

		Serialization.SerializeObject(family, path);

		FamilyDictionary? familyDeserialized = Serialization.DeserializeObject<FamilyDictionary>(path);
	
		Assert.True(familyDeserialized != null);
		Assert.Equal(family.NumberOfMembers, familyDeserialized.NumberOfMembers);

		File.Delete(path);
	}

	/// <summary>
	/// Derived custom dictionary test.
	/// </summary>
	[Fact]
	public void DerivedCustomDictionaryTest()
	{
		string path = Path.Combine(Path.GetTempPath(), "familydictionary.xml");

		DerivedFamilyDictionary family = DerivedFamilyDictionary.CreateFamily();

		Serialization.SerializeObject(family, path);

		DerivedFamilyDictionary? familyDeserialized = Serialization.DeserializeObject<DerivedFamilyDictionary>(path);
	
		Assert.True(familyDeserialized != null);
		Assert.Equal(family.NumberOfMembers, familyDeserialized.NumberOfMembers);

		File.Delete(path);
	}

	#endregion

} // End class.