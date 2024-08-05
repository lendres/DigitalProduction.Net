using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DigitalProduction.XML.Serialization;

/// <summary>
/// Add serialization to a dictionary.
///
/// From:
/// http://stackoverflow.com/questions/495647/serialize-class-containing-dictionary-member
/// </summary>
/// <typeparam name="TKey">Dictionary key type.</typeparam>
/// <typeparam name="TValue">Dictionary value type.</typeparam>
[XmlRoot("dictionary")]
public class CustomSerializableDictionary<TKey, TValue, TSerializeableKeyValuePair> :
	Dictionary<TKey, TValue>,
	IXmlSerializable
	where TKey : notnull
	where TSerializeableKeyValuePair : ISerializableKeyValuePair<TKey, TValue>, new()
{
	#region Fields
	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public CustomSerializableDictionary()
	{
	}

	#endregion

	#region XML

	/// <summary>
	/// Get the schema.
	///
	/// Returns null.  This object does not have a schema.
	/// </summary>
	public System.Xml.Schema.XmlSchema? GetSchema()
	{
		return null;
	}

	/// <summary>
	/// Read XML.
	/// </summary>
	/// <param name="reader">XmlReader.</param>
	public void ReadXml(XmlReader reader)
	{
		XDocument? document = null;
		using (XmlReader subtreeReader = reader.ReadSubtree())
		{
			document = XDocument.Load(subtreeReader);
		}

		ReadItems(document);

		reader.ReadEndElement();
	}

	private void ReadItems(XDocument? document)
	{
		XmlSerializer serializer = new(typeof(TSerializeableKeyValuePair));

		string? elementName = DigitalProduction.Reflection.Attributes.GetXmlElement(typeof(TSerializeableKeyValuePair), "Value");
		System.Diagnostics.Debug.Assert(elementName != null);

		foreach (XElement item in document!.Elements().First().Elements(XName.Get(elementName)))
		{
			using XmlReader itemReader = item.CreateReader();
			if (serializer.Deserialize(itemReader) is TSerializeableKeyValuePair keyValuePair)
			{
				if (keyValuePair.Key != null && keyValuePair.Value != null)
				{
					Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}
	}

	/// <summary>
	/// Write XML.
	/// </summary>
	/// <param name="writer">XmlWriter.</param>
	public void WriteXml(System.Xml.XmlWriter writer)
	{
		XmlSerializer serializer            = new(typeof(TSerializeableKeyValuePair));
		XmlSerializerNamespaces namespaces  = new();
		namespaces.Add("", "");

		foreach (TKey key in Keys)
		{
			TValue value							= this[key];
			TSerializeableKeyValuePair keyvaluepair   = new()
			{
				Key		= key,
				Value   = value
			};
			serializer.Serialize(writer, keyvaluepair, namespaces);
		}
	}

	#endregion

} // End class.