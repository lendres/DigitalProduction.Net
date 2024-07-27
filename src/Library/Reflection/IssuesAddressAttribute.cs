namespace DigitalProduction.Reflection;

/// <summary>
/// An attribute to add additional authors to the assembly.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class IssuesAddressAttribute : Attribute
{
	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public IssuesAddressAttribute()
	{
	}

	/// <summary>
	/// Default constructor.
	/// </summary>
	public IssuesAddressAttribute(string url)
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