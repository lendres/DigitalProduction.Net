namespace DigitalProduction.UnitTests;

/// <summary>
/// Test cases the the Mathmatics namespace.
/// </summary>
public class ProjectTests
{
	/// <summary>
	/// Covariance test.
	/// </summary>
	[Fact]
	public void SerializeProject()
	{
		TestProject project = new()
		{
			Attribute	= "attribute1",
			Element		= "element1"

		};

		string file = "testproject.xml";
		project.Serialize(file);

		TestProject result = TestProject.Deserialize(file);
		
		System.IO.File.Delete(file);

		Assert.Equal(project.Attribute, result.Attribute);
		Assert.Equal(project.Element, result.Element);
	}

} // End class.