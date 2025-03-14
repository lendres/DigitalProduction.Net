using DigitalProduction.Projects;

namespace DigitalProduction.UnitTests;

/// <summary>
/// Test cases the the Mathmatics namespace.
/// </summary>
public class ProjectTests
{
	string _file = "testproject.xml";
	
/// <summary>
	/// Serialization of an uncompressed project file.
	/// </summary>
	[Fact]
	public void SerializeUncompressedProject()
	{
		TestProjectUncompressed project = new();
		
		SetupProject(project);
		project.Serialize(_file);
		
		TestProjectUncompressed result = TestProjectCompressed.Deserialize<TestProjectUncompressed>(_file, TestProjectUncompressed.CompressionType);
		CleanUpAndTest(project, result);
	}

	/// <summary>
	/// Serialization of a compressed project file.
	/// </summary>
	[Fact]
	public void SerializeCompressedProject()
	{
		TestProjectCompressed project = new();
		
		SetupProject(project);
		project.Serialize(_file);
		
		TestProjectBase result = TestProjectCompressed.Deserialize<TestProjectCompressed>(_file, TestProjectCompressed.CompressionType);
		CleanUpAndTest(project, result);
	}

	private void CleanUpAndTest(TestProjectBase project, TestProjectBase result)
	{
		System.IO.File.Delete(_file);

		Assert.Equal(project.Attribute, result.Attribute);
		Assert.Equal(project.Element, result.Element);
	}

	private void SetupProject(TestProjectBase project)
	{
		project.Attribute   = "attribute1";
		project.Element     = "element1";
	}

} // End class.