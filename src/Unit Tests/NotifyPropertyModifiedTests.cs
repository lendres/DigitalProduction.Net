using Path = DigitalProduction.IO.Path;

namespace DigitalProduction.UnitTests;

/// <summary>
/// Test cases the the Mathmatics namespace.
/// </summary>
public class NotifyPropertyModifiedTests
{
	#region Members

	private string _message = string.Empty;

	#endregion

	#region Tests

	/// <summary>
	/// Test the modified changed event.
	/// </summary>
	[Fact]
	public void TestModifiedChanged()
	{
		// Setup.
		Person person = new("Jon Doe", 50, Gender.Male, true);
		Assert.False(person.Modified);
		person.ModifiedChanged += OnModifiedChanged;

		// Test event fires and the object is now modified.
		person.Name = "Jason Mamoa";
		Assert.True(person.Modified);
		Assert.Equal("modified", GetMessageAndReset());

		// Test the reset/save.
		person.Save();
		Assert.False(person.Modified);
		Assert.Equal("modified", GetMessageAndReset());

		// Test that setting the same value does not change Modified or fire the event.
		person.Name = "Jason Mamoa";
		Assert.False(person.Modified);
		Assert.Equal("", GetMessageAndReset());

		person.Employed = true;
		Assert.False(person.Modified);
		Assert.Equal("", GetMessageAndReset());
	}

	/// <summary>
	/// Test the property changed event.  Also tests different methods of triggering the property changed event.  The first
	/// method is the "manual" implementation and the second is the automatic way.
	/// </summary>
	[Fact]
	public void TestPropertyChanged()
	{
		Person person = new("Jon Doe", 50, Gender.Male);
		person.PropertyChanged += OnPropertyChanged;
		person.Age += 5;
		Assert.Equal("Age", GetMessageAndReset());
		person.Name = "Jason Mamoa";
		Assert.Equal("Name", GetMessageAndReset());
	}

	/// <summary>
	/// Test both modified and property changed together.  Also tests the changing of an enum.
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