using System.Xml.Serialization;

namespace DigitalProduction.Xml.Serialization;

public class XInclude
{
	[XmlAttribute("href")]
	public string Href { get; set; } = string.Empty;
}