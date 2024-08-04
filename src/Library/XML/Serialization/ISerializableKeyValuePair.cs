using System.Xml.Serialization;
using System.Xml;

namespace DigitalProduction.XML.Serialization;

/// <summary>
/// Add serialization to a dictionary.
/// 
/// From:
/// http://stackoverflow.com/questions/495647/serialize-class-containing-dictionary-member
/// </summary>
/// <typeparam name="TKey">Dictionary key type.</typeparam>
/// <typeparam name="TValue">Dictionary value type.</typeparam>
public interface ISerializableKeyValuePair<TKey, TValue> where TKey : notnull
{
	#region Properties

	/// <summary>Dictionary key.</summary>
	public TKey?	Key	{ get; set; }

	/// <summary>Dictionary value.</summary>
	public TValue? Value	{ get; set; }

	#endregion

} // End class.