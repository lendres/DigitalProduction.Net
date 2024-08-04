using System.Xml.Serialization;
using System.Xml;

namespace DigitalProduction.XML.Serialization;

/// <summary>
/// Add serialization to a dictionary.
/// 
/// From:
/// http://stackoverflow.com/questions/495647/serialize-class-containing-dictionary-member
/// </summary>
/// <typeparam name="KeyType">Dictionary key type.</typeparam>
/// <typeparam name="ValueType">Dictionary value type.</typeparam>
public interface ISerializableKeyValuePair<KeyType, ValueType> where KeyType : notnull
{
	#region Properties

	/// <summary>Dictionary key.</summary>
	public KeyType?	Key	{ get; set; }

	/// <summary>Dictionary value.</summary>
	public ValueType? Value	{ get; set; }

	#endregion

} // End class.