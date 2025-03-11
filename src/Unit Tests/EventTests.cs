using Path = DigitalProduction.IO.Path;

namespace DigitalProduction.UnitTests;

/// <summary>
/// Test cases the the Mathmatics namespace.
/// </summary>
public class EventTests
{
	#region Members

	private string _message = string.Empty;

	#endregion

	#region Tests

	/// <summary>
	/// Test to convert a relative path to an absolute path.
	/// </summary>
	[Fact]
	public void TestModifiedChanged()
	{
		Person person = new("Jon Doe", 50, Gender.Male);
		Assert.False(person.Modified);

		person.ModifiedChanged += OnModifiedChanged;
		person.Age += 5;
		Assert.Equal("modified", GetMessageAndReset());
	}

	/// <summary>
	/// Test to convert a relative path to an absolute path.
	/// </summary>
	[Fact]
	public void TestPropertyChanged()
	{
		Person person = new("Jon Doe", 50, Gender.Male);
		person.PropertyChanged += OnPropertyChanged;
		person.Name = "Jason Mamoa";
		Assert.Equal("Name", GetMessageAndReset());
	}

	/// <summary>
	/// Test to convert a relative path to an absolute path.
	/// </summary>
	[Fact]
	public void TestBothChanged()
	{
		Person person = new("Jon Doe", 50, Gender.Male);
		Assert.False(person.Modified);

		person.ModifiedChanged += OnModifiedChanged;
		person.PropertyChanged += OnPropertyChanged;
		person.Gender = Gender.Female;
		Assert.Equal(14, GetMessageAndReset().Length);
	}

	private void OnModifiedChanged(object sender, bool modified)
	{
		Assert.Equal(typeof(Person), sender.GetType());
		Assert.True(modified);
		_message += "modified";
	}

	private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs eventArgs)
	{
		Assert.NotNull(sender);
		Assert.Equal(typeof(Person), sender.GetType());
		_message += eventArgs.PropertyName;
	}

	private string GetMessageAndReset()
	{
		string temp = _message;
		_message = string.Empty;
		return temp;
	}

	#endregion

} // End class.