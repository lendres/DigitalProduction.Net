using DigitalProduction.XML.Serialization;
using System.Xml.Serialization;

namespace DigitalProduction.Http;

/// <summary>
/// 
/// </summary>
[XmlRoot("customsearchkey")]
public class CustomSearchKey
{
	#region Fields

	// The custom search engine identifier.
	private string                       _cx             = "";

	// API Key.
	private string                       _apiKey         = "";

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public CustomSearchKey()
	{
	}

	/// <summary>
	/// Default constructor.
	/// </summary>
	public CustomSearchKey(string cx, string apiKey)
	{
		_cx		= cx;
		_apiKey	= apiKey;
	}

	#endregion

	#region Properties

	/// <summary>
	/// The custom search engine identifier (cx).
	/// </summary>
	[XmlAttribute("cx")]
	public string Cx { get => _cx; set => _cx = value; }

	/// <summary>
	/// API Key.
	/// </summary>
	[XmlAttribute("apikey")]
	public string ApiKey { get => _apiKey; set => _apiKey = value; }

	#endregion

	#region Methods

	#endregion

	#region XML

	/// <summary>
	/// Write this object to a file to the provided path.
	/// </summary>
	/// <param name="path">Path (full path and filename) to write to.</param>
	/// <exception cref="InvalidOperationException">Thrown when the projects path is not valid.</exception>
	public void Serialize(string path)
	{
		if (!DigitalProduction.IO.Path.PathIsWritable(path))
		{
			throw new InvalidOperationException("The file cannot be saved.  A valid path must be specified.");
		}

		Serialization.SerializeObject(this, path);
	}

	/// <summary>
	/// Create an instance from a file.
	/// </summary>
	/// <param name="path">The file to read from.</param>
	public static CustomSearchKey? Deserialize(string path)
	{
		return Serialization.DeserializeObject<CustomSearchKey>(path);
	}

	#endregion

} // End class.