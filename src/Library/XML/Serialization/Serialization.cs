using DigitalProduction.Xml.XInclude;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DigitalProduction.Xml.Serialization;

/// <summary>
/// Summary not provided for the class Serialization.
/// </summary>
public static class Serialization
{
	#region File Serialize

	/// <summary>
	/// Serialize an object.
	/// </summary>
	/// <param name="settings">SerializationSettings to use for writing.</param>
	public static void SerializeObject(SerializationSettings settings)
	{
		XmlSerializer serializer	= new(settings.SerializeType);
		XmlWriter xmlwriter			= XmlWriter.Create(settings.OutputFile, settings.XmlSettings);

		serializer.Serialize(xmlwriter, settings.SerializeObject);
		xmlwriter.Close();
	}

	/// <summary>
	/// Serialize an object.
	/// </summary>
	/// <param name="objectToSerialize">Object to serialize.</param>
	/// <param name="outputFile">Output file.</param>
	public static void SerializeObject(object objectToSerialize, string outputFile)
	{
		SerializationSettings settings = new(objectToSerialize, outputFile);
		SerializeObject(settings);
	}

	/// <summary>
	/// Serialize an object using full end element closing tags.
	/// </summary>
	/// <param name="settings">SerializationSettings to use for writing.</param>
	public static void SerializeObjectFullEndElement(SerializationSettings settings)
	{
		XmlSerializer serializer				= new(settings.SerializeType);
		XmlTextWriterFullEndElement textwriter	= new(settings.OutputFile, settings.XmlSettings);
		XmlWriter xmlwriter						= XmlTextWriterFullEndElement.Create(textwriter, settings.XmlSettings);

		serializer.Serialize(xmlwriter, settings.SerializeObject);
		xmlwriter.Close();
	}

	/// <summary>
	/// Serialize an object.
	/// </summary>
	/// <param name="objectToSerialize">Object to serialize.</param>
	/// <param name="outputFile">Output file.</param>
	public static void SerializeObjectFullEndElement(object objectToSerialize, string outputFile)
	{
		SerializationSettings settings = new(objectToSerialize, outputFile);
		SerializeObjectFullEndElement(settings);
	}

	#endregion

	#region File Deserialize

	/// <summary>
	/// Deserialize an object from a file.
	/// </summary>
	/// <typeparam name="T">Type of object to deserialize.</typeparam>
	/// <param name="file">File to deserialize from.</param>
	public static T? DeserializeObject<T>(string file)
	{
		XmlSerializer serializer			= new(typeof(T));
		XIncludingReader xmlincludingreader	= new(file);
		T? deserializedobject				= (T?)serializer.Deserialize(xmlincludingreader);
		xmlincludingreader.Close();

		return deserializedobject;
	}

    #endregion

    #region String Serialization

    public static string SerializeObjectToString<T>(T obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        var serializer = new XmlSerializer(typeof(T));

        var settings = new System.Xml.XmlWriterSettings
        {
            Encoding = Encoding.UTF8,
            Indent = true,
            OmitXmlDeclaration = false
        };

        using (Utf8StringWriter stringWriter = new())
        using (XmlWriter xmlWriter = System.Xml.XmlWriter.Create(stringWriter, settings))
        {
            serializer.Serialize(xmlWriter, obj);
            return stringWriter.ToString();
        }
    }

    // Ensures UTF-8 instead of UTF-16 in declaration
    private class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    #endregion

    #region String Deserialization

    public static T? DeserializeObjectFromString<T>(string xml)
    {
        if (string.IsNullOrWhiteSpace(xml))
        {
            throw new ArgumentException("Input XML is null or empty.", nameof(xml));
        }

        XmlSerializer serializer = new(typeof(T));

        using (var stringReader = new StringReader(xml))
        {
            return (T?)serializer.Deserialize(stringReader);
        }
    }

    #endregion

} // End class.