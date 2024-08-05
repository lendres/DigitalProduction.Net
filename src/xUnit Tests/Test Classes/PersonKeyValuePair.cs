using System.Xml.Serialization;
using System.Xml;
using DigitalProduction.UnitTests;

namespace DigitalProduction.XML.Serialization;

/// <summary>
/// Add serialization to a dictionary.
/// 
/// From:
/// http://stackoverflow.com/questions/495647/serialize-class-containing-dictionary-member
/// </summary>
/// <typeparam name="TKey">Dictionary key type.</typeparam>
/// <typeparam name="TValue">Dictionary value type.</typeparam>
[XmlRoot("person")]
public class PersonKeyValuePair : ISerializableKeyValuePair<string, Person>
{
	#region Fields

	/// <summary>Dictionary key.</summary>
	[XmlAttribute("name")]
	public string? Key { get; set; } = default;

	/// <summary>Dictionary value.</summary>
	[XmlElement("person")]
	public Person? Value  { get; set; }

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public PersonKeyValuePair()
	{
	}

	#endregion

} // End class.