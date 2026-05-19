using DigitalProduction.ComponentModel;

namespace DigitalProduction.UnitTests;

public class NotifyModifiedChangedTests
{
	[Fact]
	public void ModifiedDefaultValueTest()
	{
		TestObject testObject = new();

		Assert.False(testObject.Modified);
	}

	[Fact]
	public void SetValueChangesModifiedToTrueTest()
	{
		TestObject testObject = new();

		testObject.Name = "Test Name";

		Assert.True(testObject.Modified);
	}

	[Fact]
	public void SetValueReturnsFalseForSameValueTest()
	{
		TestObject testObject = new();

		testObject.Name = "Test Name";
		testObject.Save();

		testObject.Name = "Test Name";

		Assert.False(testObject.Modified);
	}

	[Fact]
	public void SaveChangesModifiedToFalseTest()
	{
		TestObject testObject = new();

		testObject.Name = "Test Name";

		Assert.True(testObject.Modified);

		testObject.Save();

		Assert.False(testObject.Modified);
	}

	[Fact]
	public void ModifiedChangedEventRaisedWhenValueChangesTest()
	{
		TestObject testObject = new();

		bool eventRaised = false;
		bool modifiedValue = false;

		testObject.ModifiedChanged += (sender, modified) =>
		{
			eventRaised = true;
			modifiedValue = modified;
		};

		testObject.Name = "Test Name";

		Assert.True(eventRaised);
		Assert.True(modifiedValue);
	}

	[Fact]
	public void ModifiedChangedEventNotRaisedWhenValueDoesNotChangeTest()
	{
		TestObject testObject = new();

		testObject.Name = "Test Name";
		testObject.Save();

		bool eventRaised = false;

		testObject.ModifiedChanged += (sender, modified) =>
		{
			eventRaised = true;
		};

		testObject.Name = "Test Name";

		Assert.False(eventRaised);
	}

	[Fact]
	public void ModifiedChangedEventRaisedWhenSaveChangesModifiedToFalseTest()
	{
		TestObject testObject = new();

		testObject.Name = "Test Name";

		bool eventRaised = false;
		bool modifiedValue = true;

		testObject.ModifiedChanged += (sender, modified) =>
		{
			eventRaised = true;
			modifiedValue = modified;
		};

		testObject.Save();

		Assert.True(eventRaised);
		Assert.False(modifiedValue);
	}

	[Fact]
	public void ModifiedChangedEventNotRaisedWhenSaveDoesNotChangeModifiedTest()
	{
		TestObject testObject = new();

		bool eventRaised = false;

		testObject.ModifiedChanged += (sender, modified) =>
		{
			eventRaised = true;
		};

		testObject.Save();

		Assert.False(eventRaised);
	}

	private class TestObject : NotifyModifiedChanged
	{
		public string Name
		{
			get => GetValueOrDefault(string.Empty);
			set => SetValue(value);
		}

		public int Count
		{
			get => GetValueOrDefault(0);
			set => SetValue(value);
		}
	}
}