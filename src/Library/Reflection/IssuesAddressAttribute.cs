﻿namespace DigitalProduction.Reflection;

/// <summary>
/// An attribute to the assembly to add an address for reporting issues.
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