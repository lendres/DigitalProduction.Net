namespace DigitalProduction.ComponentModel;

/// <summary>
/// Attribute for applying alternate names to a class.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public class AliasAttribute : Attribute
{
	/// <summary>
	/// Default constructor.
	/// </summary>
	public AliasAttribute() {}

		
	/// <summary>
	/// Default constructor.
	/// </summary>
	public AliasAttribute(string alias)
	{
		Alias = alias;
	}

	/// <summary>
	/// An alternate name for the class/structure.
	/// </summary>
	public string Alias { get; set; } = string.Empty;

} // End class.