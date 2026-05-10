using DigitalProduction.ComponentModel;
using static System.Net.WebRequestMethods;

namespace DigitalProduction.UnitTests;

/// <summary>
/// Test cases for GenericProperties.
/// </summary>
public class GenericPropertiesTests
{
	[Fact]
	public void GetValueReturnsDefaultWhenPropertyHasNotBeenSet()
	{
		GenericPropertiesTestClass testObject = new();

		Assert.Equal("default", testObject.Name);
		Assert.Equal(10, testObject.Count);
		Assert.Null(testObject.OptionalName);
	}

	[Fact]
	public void SetValueStoresPropertyUsingCallerMemberName()
	{
		GenericPropertiesTestClass testObject = new();

		testObject.Name = "first";
		testObject.Count = 25;

		Assert.Equal("first", testObject.Name);
		Assert.Equal(25, testObject.Count);
	}

	[Fact]
	public void SetBool()
	{
		GenericPropertiesTestClass testObject = new();

		// Check the default value.
		Assert.False(testObject.Activated);

		// Test that the property can be set to true.
		testObject.Activated = true;
		Assert.True(testObject.Activated);

		// Test that setting true again returns false.
		Assert.False(testObject.SetActivated(true));

		// Test setting null.
		Assert.True(testObject.SetActivated(null));
		Assert.False(testObject.SetActivated(null));
	}

	[Fact]
	public void SetValueReturnsFalseWhenValueIsUnchanged()
	{
		GenericPropertiesTestClass testObject = new();

		Assert.True(testObject.SetName("first"));
		Assert.False(testObject.SetName("first"));
	}

	[Fact]
	public void SetValueReturnsTrueWhenValueChanges()
	{
		GenericPropertiesTestClass testObject = new();

		Assert.True(testObject.SetName("first"));
		Assert.True(testObject.SetName("second"));

		Assert.Equal("second", testObject.Name);
	}

	[Fact]
	public void TestInstances()
	{
		GenericPropertiesTestClass testObject = new();

		testObject.Person = new Person { Name = "Original Value" };
		testObject.Person.Save();

		// Fist test accessing the property directly.
		Assert.Equal("Original Value", testObject.Person.Name);
		Assert.False(testObject.Person.Modified);

		// Test changing the name.
		testObject.Person.Name = "Updated Value";
		Assert.Equal("Updated Value", testObject.Person.Name);
		Assert.True(testObject.Person.Modified);

		// Reset.
		testObject.Person = new Person { Name = "Original Value" };
		testObject.Person.Save();
		Assert.False(testObject.Person.Modified);

		// Test changing the name by using the test classes property.
		testObject.PersonName = "Updated Value";
		Assert.Equal("Updated Value", testObject.PersonName);
		Assert.True(testObject.Person.Modified);
	}

	[Fact]
	public void SetValueHandlesNullValues()
	{
		GenericPropertiesTestClass testObject = new();

		Assert.True(testObject.SetOptionalName(null));
		Assert.False(testObject.SetOptionalName(null));

		Assert.True(testObject.SetOptionalName("value"));
		Assert.Equal("value", testObject.OptionalName);

		Assert.True(testObject.SetOptionalName(null));
		Assert.Null(testObject.OptionalName);
	}

	[Fact]
	public void SetValueStoresDifferentPropertiesIndependently()
	{
		GenericPropertiesTestClass testObject = new();

		testObject.Name = "first";
		testObject.Count = 25;
		testObject.OptionalName = "optional";

		Assert.Equal("first", testObject.Name);
		Assert.Equal(25, testObject.Count);
		Assert.Equal("optional", testObject.OptionalName);
	}

	#region Test Class

	private class GenericPropertiesTestClass : GenericProperties
	{
		public string Name
		{
			get => GetValueOrDefault("default");
			set => SetValue(value);
		}

		public int Count
		{
			get => GetValueOrDefault(10);
			set => SetValue(value);
		}

		public bool? Activated
		{
			get => GetValueOrDefault(false);
			set => SetValue(value);
		}

		public string? OptionalName
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		public Person Person
		{
			get => GetValueOrDefault<Person>(new Person());
			set => SetValue(value);
		}

		public string PersonName
		{
			get => Person.Name;
			set
			{
				if (Person.Name != value)
				{
					Person.Name = value;
				}
			}
		}

		public bool SetName(string value)
		{
			return SetValue(value, nameof(Name));
		}

		public bool SetOptionalName(string? value)
		{
			return SetValue(value, nameof(OptionalName));
		}

		public bool SetActivated(bool? value)
		{
			return SetValue(value, nameof(Activated));
		}
	}
	
	#endregion
}