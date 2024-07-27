namespace DigitalProduction.Reflection;

/// <summary>
/// An attribute to add additional authors to the assembly.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class AuthorsAttribute : Attribute
{
	#region Fields
	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public AuthorsAttribute()
	{
	}

	/// <summary>
	/// Default constructor.
	/// </summary>
	public AuthorsAttribute(string authors)
	{
		Authors = authors;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Authors.
	/// </summary>
	public string Authors { get; set; } = "";

	#endregion

	#region Methods

	#endregion

} // End class.