using DigitalProduction.ComponentModel;
using System.Xml.Serialization;

namespace DigitalProduction.UnitTests;

/// <summary>
/// A person.
/// </summary>
public class Person : NotifyPropertyModifiedChanged
{
	#region

	int _age = 0;

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.  Required for serialization.
	/// </summary>
	public Person()
	{
	}

	/// <summary>
	/// Constructor to populate fields.
	/// </summary>
	public Person(string name, int age, Gender gender)
	{
		Name		= name;
		Age			= age;
		Gender		= gender;
		Modified	= false;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Name.
	/// </summary>
	[XmlAttribute("name")]
	public string Name { get => GetValueOrDefault<string>(string.Empty); set => SetValue(value); }

	/// <summary>
	/// Age.
	/// </summary>
	[XmlAttribute("age")]
	public int Age
	{
		get => _age;

		set
		{
			if (_age != value)
			{
				_age = value;
				Modified = true;
				OnPropertyChanged();
			}
		}
	}

	/// <summary>
	/// Gender.
	/// </summary>
	[XmlAttribute("gender")]
	public Gender Gender { get => GetValueOrDefault<Gender>(Gender.Female); set => SetValue(value); }

	#endregion

} // End class.