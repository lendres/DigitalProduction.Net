namespace DigitalProduction.Xml.Serialization;

/// <summary>
/// Interface for a key value pair.
/// One of several classes that add serialization to a dictionary.
/// 
/// Implementations must use:
/// [XmlRoot("personkeyvaluepair")]
/// </summary>
/// <typeparam name="TKey">Dictionary key type.</typeparam>
/// <typeparam name="TValue">Dictionary value type.</typeparam>
public interface ISerializableKeyValuePair<TKey, TValue> where TKey : notnull
{
	#region Properties

	/// <summary>
	/// Dictionary key.
	/// 
	/// Implementations must use either:
	/// [XmlAttribute("")]
	/// [XmlElement("")]
	/// </summary>
	public TKey?	Key	{ get; set; }

	/// <summary>
	/// Dictionary value.
	///
	/// Implementations must use either:
	/// [XmlAttribute("")]
	/// [XmlElement("")]
	/// </summary>
	public TValue? Value	{ get; set; }

	#endregion

} // End class.