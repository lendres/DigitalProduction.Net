namespace DigitalProduction.Reflection;

/// <summary>
/// An attribute to the assembly to add an address for documentation/help.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class DocumentationAddressAttribute : Attribute
{
	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public DocumentationAddressAttribute()
	{
	}

	/// <summary>
	/// Default constructor.
	/// </summary>
	public DocumentationAddressAttribute(string url)
	{
		Url = url;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Authors.
	/// </summary>
	public string Url { get; set; } = "";

	#endregion

} // End class.