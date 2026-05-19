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

	#region String Serialization

    [Fact]
    public void DeserializeObjectFromString_ValidPersonXml_ReturnsPerson()
    {
        // Arrange.
        string xml = """
            <person name="Alice" age="35" gender="Female" employed="true" />
            """;

        // Act.
        Person? result = Serialization.DeserializeObjectFromString<Person>(xml);

        // Assert.
        Assert.NotNull(result);
        Assert.Equal("Alice", result.Name);
        Assert.Equal(35, result.Age);
        Assert.Equal(Gender.Female, result.Gender);
        Assert.True(result.Employed);
    }

    [Fact]
    public void DeserializeObjectFromString_ValidPersonXmlWithFalseEmployment_ReturnsPerson()
    {
        // Arrange.
        string xml = """
            <person name="Bob" age="42" gender="Male" employed="false" />
            """;

        // Act.
        Person? result = Serialization.DeserializeObjectFromString<Person>(xml);

        // Assert.
        Assert.NotNull(result);
        Assert.Equal("Bob", result.Name);
        Assert.Equal(42, result.Age);
        Assert.Equal(Gender.Male, result.Gender);
        Assert.False(result.Employed);
    }

    [Fact]
    public void DeserializeObjectFromString_MissingOptionalAttributes_UsesDefaults()
    {
        // Arrange.
        string xml = """
            <person name="Carol" age="28" />
            """;

        // Act.
        Person? result = Serialization.DeserializeObjectFromString<Person>(xml);

        // Assert.
        Assert.NotNull(result);
        Assert.Equal("Carol", result.Name);
        Assert.Equal(28, result.Age);
        Assert.Equal(Gender.Female, result.Gender);
        Assert.True(result.Employed);
    }

    [Fact]
    public void DeserializeObjectFromString_NullXml_ThrowsArgumentException()
    {
        // Arrange.
        string? xml = null;

        // Act.
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => Serialization.DeserializeObjectFromString<Person>(xml!));

        // Assert.
        Assert.Equal("xml", exception.ParamName);
    }

    [Fact]
    public void DeserializeObjectFromString_EmptyXml_ThrowsArgumentException()
    {
        // Arrange.
        string xml = "";

        // Act.
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => Serialization.DeserializeObjectFromString<Person>(xml));

        // Assert.
        Assert.Equal("xml", exception.ParamName);
    }

    [Fact]
    public void DeserializeObjectFromString_InvalidAge_ThrowsInvalidOperationException()
    {
        // Arrange.
        string xml = """
            <person name="Alice" age="NotAnInteger" gender="Female" employed="true" />
            """;

        // Act and assert.
        Assert.Throws<InvalidOperationException>(
            () => Serialization.DeserializeObjectFromString<Person>(xml));
    }

    [Fact]
    public void DeserializeObjectFromString_WrongRootElement_ThrowsInvalidOperationException()
    {
        // Arrange.
        string xml = """
            <Person name="Alice" age="35" gender="Female" employed="true" />
            """;

        // Act and assert.
        Assert.Throws<InvalidOperationException>(
            () => Serialization.DeserializeObjectFromString<Person>(xml));
    }

    [Fact]
    public void SerializeObjectToString_ValidPerson_ReturnsExpectedXml()
    {
        // Arrange.
        var person = new Person("Alice", 35, Gender.Female, true);

        // Act.
        string xml = Serialization.SerializeObjectToString(person);

        // Assert.
        Assert.Contains("<person", xml);
        Assert.Contains(""""name="Alice"""", xml);
        Assert.Contains(""""age="35"""", xml);
        Assert.Contains(""""gender="Female"""", xml);
        Assert.Contains(""""employed="true"""", xml);
    }

    [Fact]
    public void SerializeObjectToString_ThenDeserializeObjectFromString_RoundTripPreservesValues()
    {
        // Arrange.
        var original = new Person("Bob", 42, Gender.Male, false);

        // Act.
        string xml		= Serialization.SerializeObjectToString(original);
        Person? result	= Serialization.DeserializeObjectFromString<Person>(xml);

        // Assert.
        Assert.NotNull(result);
        Assert.Equal(original.Name, result.Name);
        Assert.Equal(original.Age, result.Age);
        Assert.Equal(original.Gender, result.Gender);
        Assert.Equal(original.Employed, result.Employed);
    }

	#endregion

} // End class.