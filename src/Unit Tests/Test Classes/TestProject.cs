using DigitalProduction.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitalProduction.UnitTests;

[XmlRoot("testproject")]
public class TestProject : DigitalProduction.Projects.Project
{
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

	/// <summary>
	/// Writes a Project file (compressed file containing all the project's files).  Uses a ProjectCompressor to zip all files.  An
	/// event of RaiseOnSavingEvent fires allowing other files to be added to the project.
	///
	/// The Path must be set and represent a valid path or this method will throw an exception.
	/// </summary>
	/// <exception cref="InvalidOperationException">Thrown when the projects path is not set or not valid.</exception>
	//public override void Serialize()
	//{
	//	base.Serialize();
	//}

	public static TestProject Deserialize(string file)
	{
		return Project.Deserialize<TestProject>(file);
	}

	/// <summary>
	/// Initialize references.
	/// </summary>
	protected override void DeserializationInitialization()
	{
	}
}