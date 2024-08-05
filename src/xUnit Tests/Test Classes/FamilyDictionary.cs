using DigitalProduction.ComponentModel;
using System.ComponentModel;
using System.Xml.Serialization;
using DigitalProduction.XML.Serialization;

namespace DigitalProduction.UnitTests;

/// <summary>
/// A family.
/// </summary>
[XmlRoot("family")]
[DisplayName("Family Members")]
[Alias("Family Members")]
[Alias("Relatives")]
[Description("A group of related people.")]
public class FamilyDictionary
{
	#region Construction

	/// <summary>
	/// Default constructor.  Required for serialization.
	/// </summary>
	public FamilyDictionary()
	{
	}

	#endregion

	#region Properties

	/// <summary>
	/// Number of people in the family.
	/// </summary>
	[XmlIgnore()]
	public int NumberOfMembers { get => Members.Count; }

	/// <summary>
	/// Members of the family.
	/// </summary>
	[XmlElement("members")]
	public CustomSerializableDictionary<string, Person, PersonKeyValuePair<string, Person>> Members { get; set; } = new();

	#endregion

	#region Methods
	#endregion
	
	#region Static Functions

	/// <summary>
	/// Helper function to create a family.
	/// </summary>
	/// <returns>A new Family populated with some default values.</returns>
	public static FamilyDictionary CreateFamily()
	{
		FamilyDictionary family = new();
		family.Members.Add("Mom", new Person("Mom", 36, Gender.Female));
		family.Members.Add("Dad", new Person("Dad", 37, Gender.Male));
		family.Members.Add("Daughter", new Person("Daughter", 6, Gender.Female));
		family.Members.Add("Son", new Person("Son", 4, Gender.Male));
		return family;
	}

	#endregion

} // End class.