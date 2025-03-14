using DigitalProduction.Projects;
using System.Xml.Serialization;

namespace DigitalProduction.UnitTests;

public class TestProjectUncompressed : TestProjectBase
{
	public TestProjectUncompressed() : base(CompressionType.Uncompressed) {}
}

public class TestProjectCompressed : TestProjectBase
{
	public TestProjectCompressed() : base(CompressionType.Compressed) {}
}

[XmlRoot("testproject")]
public class TestProjectBase : Project
{
	public TestProjectBase(CompressionType compressionType) :
		base(compressionType)
	{
	}

	/// <summary>
	/// Attribute test.
	/// </summary>
	[XmlAttribute("attribute")]
	public string Attribute { get; set; } = string.Empty;

	/// <summary>
	/// Element test.
	/// </summary>
	[XmlAttribute("element")]
	public string Element { get; set; } = string.Empty;

}