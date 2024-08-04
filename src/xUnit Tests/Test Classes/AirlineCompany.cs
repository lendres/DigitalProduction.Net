using System.Xml.Serialization;
using System.ComponentModel;

namespace DigitalProduction.UnitTests;

/// <summary>
/// An airline company.
/// </summary>
[XmlRoot("airline")]
[DisplayName("Airline")]
[Description("A company that owns and operates airplanes.")]
public class AirlineCompany : Company
{
	#region Construction

	/// <summary>
	/// Default constructor.  Required for serialization.
	/// </summary>
	public AirlineCompany()
	{
	}

	#endregion

	#region Properties

	/// <summary>
	/// Number of planes the airline has available.
	/// </summary>
	[XmlAttribute("numberofplanes")]
	public int NumberOfPlanes { get; set; } = 0;

	#endregion

	#region Static Functions

	/// <summary>
	/// Helper function to create an airline.
	/// </summary>
	/// <returns>A new airline populated with some default values.</returns>
	public static AirlineCompany CreateAirline()
	{
		AirlineCompany company = new()
		{
			Name            = "Oceanic",
			NumberOfPlanes  = 10
		};
		company.Employees.Add(new Person("Manager", 36, Gender.Female));
		company.Employees.Add(new Person("Luggage Handler", 37, Gender.Male));
		company.Employees.Add(new Person("Pilot", 28, Gender.Female));
		company.Employees.Add(new Person("Captain", 30, Gender.Male));
		return company;
	}

	#endregion

} // End class.