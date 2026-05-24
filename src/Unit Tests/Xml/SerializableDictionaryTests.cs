using DigitalProduction.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitalProduction.UnitTests;

public class SerializableDictionaryTests
{
	#region Basic Tests

	/// <summary>
	/// Serializable dictionary copy constructor.
	/// </summary>
	[Fact]
	public void SerializableDictionaryCopyConstructor()
	{
		// Arrange.
		SerializableDictionary<string, string> original = new()
		{
			{ "key1", "value1" },
			{ "key2", "value2" },
			{ "key3", "value3" }
		};

		// Act.
		SerializableDictionary<string, string> copy = new(original);

		// Assert.
		Assert.NotSame(original, copy);
		Assert.Equal(original.Count, copy.Count);
		Assert.Equal("value1", copy["key1"]);
		Assert.Equal("value2", copy["key2"]);
		Assert.Equal("value3", copy["key3"]);

		original["key1"] = "changed";
		original.Add("key4", "value4");

		Assert.Equal("value1", copy["key1"]);
		Assert.False(copy.ContainsKey("key4"));
	}

	[Fact]
	public void SerializableDictionaryCopyConstructorCreatesEmptyCopy()
	{
		SerializableDictionary<string, string> original = new();

		SerializableDictionary<string, string> copy = new(original);

		Assert.NotSame(original, copy);
		Assert.Empty(copy);
	}

	[Fact]
	public void SerializableDictionaryCopyConstructorCopiesAllItems()
	{
		SerializableDictionary<string, string> original = new()
		{
			{ "alpha", "one" },
			{ "beta", "two" },
			{ "gamma", "three" }
		};

		SerializableDictionary<string, string> copy = new(original);

		Assert.Equal(original.Count, copy.Count);
		Assert.Equal(original["alpha"], copy["alpha"]);
		Assert.Equal(original["beta"], copy["beta"]);
		Assert.Equal(original["gamma"], copy["gamma"]);
	}

	[Fact]
	public void SerializableDictionaryCopyConstructorCopyCanBeModifiedIndependently()
	{
		SerializableDictionary<string, string> original = new()
		{
			{ "key", "original" }
		};

		SerializableDictionary<string, string> copy = new(original);

		copy["key"] = "copy";
		copy.Add("newKey", "newValue");

		Assert.Equal("original", original["key"]);
		Assert.False(original.ContainsKey("newKey"));
	}

	[Fact]
	public void SerializableDictionaryCopyConstructorOriginalCanBeModifiedIndependently()
	{
		SerializableDictionary<string, string> original = new()
		{
			{ "key", "original" }
		};

		SerializableDictionary<string, string> copy = new(original);

		original["key"] = "changed";
		original.Add("newKey", "newValue");

		Assert.Equal("original", copy["key"]);
		Assert.False(copy.ContainsKey("newKey"));
	}

	[Fact]
	public void SerializableDictionaryCopyConstructorThrowsForNullSource()
	{
		Assert.Throws<ArgumentNullException>(() => new SerializableDictionary<string, string>(null!));
	}

	#endregion

	#region Serialization Tests

	[Fact]
	public void SerializableDictionarySerializesItems()
	{
		SerializableDictionary<string, string> dictionary = new()
		{
			{ "key1", "value1" },
			{ "key2", "value2" }
		};

		XmlSerializer serializer = new(typeof(SerializableDictionary<string, string>));

		using StringWriter stringWriter = new();

		serializer.Serialize(stringWriter, dictionary);

		string xml = stringWriter.ToString();

		Assert.Contains("<item", xml);
		Assert.Contains("key=\"key1\"", xml);
		Assert.Contains("<value>value1</value>", xml);
		Assert.Contains("key=\"key2\"", xml);
		Assert.Contains("<value>value2</value>", xml);
	}

	[Fact]
	public void SerializableDictionaryDeserializesItems()
	{
		string xml =
			"""
			<?xml version="1.0" encoding="utf-16"?>
			<dictionary>
				<item key="key1">
					<value>value1</value>
				</item>
				<item key="key2">
					<value>value2</value>
				</item>
			</dictionary>
			""";

		XmlSerializer serializer = new(typeof(SerializableDictionary<string, string>));

		using StringReader stringReader = new(xml);

		SerializableDictionary<string, string>? dictionary =
			serializer.Deserialize(stringReader)
				as SerializableDictionary<string, string>;

		Assert.NotNull(dictionary);
		Assert.Equal(2, dictionary.Count);
		Assert.Equal("value1", dictionary["key1"]);
		Assert.Equal("value2", dictionary["key2"]);
	}

	[Fact]
	public void SerializableDictionaryRoundTripPreservesItems()
	{
		SerializableDictionary<string, string> original = new()
		{
			{ "alpha", "one" },
			{ "beta", "two" },
			{ "gamma", "three" }
		};

		XmlSerializer serializer = new(typeof(SerializableDictionary<string, string>));

		string xml;

		using (StringWriter stringWriter = new())
		{
			serializer.Serialize(stringWriter, original);
			xml = stringWriter.ToString();
		}

		SerializableDictionary<string, string>? copy;

		using (StringReader stringReader = new(xml))
		{
			copy = serializer.Deserialize(stringReader)
				as SerializableDictionary<string, string>;
		}

		Assert.NotNull(copy);
		Assert.Equal(original.Count, copy.Count);
		Assert.Equal("one", copy["alpha"]);
		Assert.Equal("two", copy["beta"]);
		Assert.Equal("three", copy["gamma"]);
	}

	[Fact]
	public void SerializableDictionaryRoundTripEmptyDictionary()
	{
		SerializableDictionary<string, string> original = new();

		XmlSerializer serializer = new(typeof(SerializableDictionary<string, string>));

		string xml;

		using (StringWriter stringWriter = new())
		{
			serializer.Serialize(stringWriter, original);
			xml = stringWriter.ToString();
		}

		SerializableDictionary<string, string>? copy;

		using (StringReader stringReader = new(xml))
		{
			copy = serializer.Deserialize(stringReader)
				as SerializableDictionary<string, string>;
		}

		Assert.NotNull(copy);
		Assert.Empty(copy);
	}
	#endregion
}