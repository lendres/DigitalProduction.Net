using System;

namespace DigitalProduction.Reflection;

/// <summary>
/// An attribute to add additional authors to the assembly.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class WebsiteAttribute : Attribute
{
	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public WebsiteAttribute()
	{
	}

	/// <summary>
	/// Default constructor.
	/// </summary>
	public WebsiteAttribute(string url)
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