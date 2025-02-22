using DigitalProduction.Xml;
using DigitalProduction.Xml.Serialization;
using System;

namespace DigitalProduction.UnitTests;

public class XmlProcessingTests
{
	#region Fields

	private int         _recordCount            = 0;
	private int         _recordsProcessed       = 0;
	private string      _recordName             = string.Empty;
	private string      _currentElementName     = string.Empty;

	#endregion

	#region Processing First Row

	/// <summary>
	/// The objective of this test is to extract certain element names.  The rules are:
	/// - Extract only elements under the "row" elements.
	/// - Only process the first "row" element.  The rest are assume identical in terms of element names.
	/// - Only extract element names that contain a value.
	/// </summary>
	[Fact]
	public void OneRowOfElementsContainingTextTest()
	{
		ProcessXmlFile(@".\Test Files\textinelements.xml");
	}

	/// <summary>
	/// Similar to the test above, but using attributes instead of internal strings.
	/// 
	/// The objective of this test is to extract certain element names.  The rules are:
	/// - Extract only elements under the "row" elements.
	/// - Only process the first "row" element.  The rest are assume identical in terms of element names.
	/// - Only extract element that have an attribute that is not blank.
	/// </summary>
	[Fact]
	public void OneRowOfElementsWithAttributesTest()
	{
		ProcessXmlFile(@".\Test Files\valuesinattributes.xml");
	}

	/// <summary>
	/// Similar to the test above, but using mixed element types.
	/// 
	/// The objective of this test is to extract certain element names.  The rules are:
	/// - Extract only elements under the "row" elements.
	/// - Only process the first "row" element.  The rest are assume identical in terms of element names.
	/// - Only extract element that have an attribute that is not blank.
	/// </summary>
	[Fact]
	public void OneRowOfMixedElementsTest()
	{
		ProcessXmlFile(@".\Test Files\mixed.xml");
	}

	private List<string> ProcessXmlFile(string file)
	{
		ResetValues();

		XmlTextProcessor xmlProcessor = new(file);
		List<string> elements = new();

		// The first element is the document element and we are going to ignore that.  So we just continue reading.
		XmlHandlerList handlers = new();
		handlers.AddHandler(HandlerType.Default, new XmlHandlerFunction(FirstRowProcessingHandler));
		xmlProcessor.Process(handlers, elements);

		xmlProcessor.Close();

		Assert.Equal("row", _recordName);
		Assert.Equal(1, _recordsProcessed);
		Assert.Equal(2, _recordCount);
		Assert.Equal(4, elements.Count);

		return elements;
	}

	private void FirstRowProcessingHandler(XmlTextProcessor xmlProcessor, object? data)
	{
		List<string> elements = (List<string>)data!;

		_recordName = xmlProcessor.XmlTextReader!.LocalName;
		_recordCount++;

		// Only run once.  On the first pass, we have no headers yet.
		if (elements.Count == 0)
		{
			_recordsProcessed++;
			ProcessElementForHeaders(xmlProcessor, elements);
		}
		else
		{
			XmlHandlerList handlers = new();
			handlers.AddHandler(HandlerType.Default, new XmlHandlerFunction(SkipElements));
			xmlProcessor.Process(handlers, data);
		}
	}

	#endregion

	#region Processing All Rows

	/// <summary>
	/// The objective of this test is to processing all elements.  The rules are:
	/// - Extract only elements under the "row" elements.
	/// - Only extract element names that contain a value.
	/// </summary>
	[Fact]
	public void AllRowsOfElementsContainingTextTest()
	{
		ProcessAllElementsOfXmlFile(@".\Test Files\textinelements.xml");
	}

	/// <summary>
	/// Similar to the test above, but using attributes instead of internal strings.
	/// 
	/// The objective of this test is to processing all elements.  The rules are:
	/// - Extract only elements under the "row" elements.
	/// - Only process the first "row" element.  The rest are assume identical in terms of element names.
	/// - Only extract element that have an attribute that is not blank.
	/// </summary>
	[Fact]
	public void AllRowsOfElementsWithAttributesTest()
	{
		ProcessAllElementsOfXmlFile(@".\Test Files\valuesinattributes.xml");
	}

	/// <summary>
	/// Similar to the test above, but using mixed element types.
	/// 
	/// The objective of this test is to processing all elements.  The rules are:
	/// - Extract only elements under the "row" elements.
	/// - Only process the first "row" element.  The rest are assume identical in terms of element names.
	/// - Only extract element that have an attribute that is not blank.
	/// </summary>
	[Fact]
	public void AllRowsMixedElementsTest()
	{
		ProcessAllElementsOfXmlFile(@".\Test Files\mixed.xml");
	}

	private List<string> ProcessAllElementsOfXmlFile(string file)
	{
		ResetValues();

		XmlTextProcessor xmlProcessor = new(file);
		List<string> elements = new();

		// The first element is the document element and we are going to ignore that.  So we just continue reading.
		XmlHandlerList handlers = new();
		handlers.AddHandler(HandlerType.Default, new XmlHandlerFunction(AllRowsProcessingHandler));
		xmlProcessor.Process(handlers, elements);

		xmlProcessor.Close();

		Assert.Equal("row", _recordName);
		Assert.Equal(2, _recordsProcessed);
		Assert.Equal(2, _recordCount);
		Assert.Equal(8, elements.Count);

		return elements;
	}

	private void AllRowsProcessingHandler(XmlTextProcessor xmlProcessor, object? data)
	{
		List<string> elements = (List<string>)data!;

		_recordName = xmlProcessor.XmlTextReader!.LocalName;
		_recordCount++;

		_recordsProcessed++;
		ProcessElementForHeaders(xmlProcessor, elements);
	}

	#endregion

	#region Handlers

	private void ElementForHeadersHandler(XmlTextProcessor xmlProcessor, object? data)
	{
		List<string> elements = (List<string>)data!;
		ProcessElementForHeaders(xmlProcessor, elements);
	}

	private void TextForHeadersHandler(XmlTextProcessor xmlProcessor, object? data)
	{
		List<string> elements = (List<string>)data!;
		elements.Add(_currentElementName);
	}

	private void ProcessElementForHeaders(XmlTextProcessor xmlProcessor, List<string> elements)
	{
		_currentElementName = xmlProcessor.XmlTextReader!.LocalName.ToLower();

		AttributeList attributes = xmlProcessor.GetAttributes();
		if (attributes.Count > 0)
		{
			foreach (DigitalProduction.Xml.Attribute attribute in attributes)
			{
				if (attribute.Value != "")
				{
					elements.Add(_currentElementName + "." + attribute.Name);
				}
			}
		}

		XmlHandlerList handlers = new();
		handlers.AddHandler(HandlerType.Default, new XmlHandlerFunction(ElementForHeadersHandler));
		handlers.AddHandler(HandlerType.Text, new XmlHandlerFunction(TextForHeadersHandler));
		xmlProcessor.Process(handlers, elements);
	}

	private void SkipElements(XmlTextProcessor xmlProcessor, object? data)
	{
		XmlHandlerList handlers = new();
		handlers.AddHandler(HandlerType.Default, new XmlHandlerFunction(SkipElements));
		xmlProcessor.Process(handlers, data);
	}

	private void ResetValues()
	{
		_recordCount            = 0;
		_recordsProcessed       = 0;
		_recordName             = string.Empty;
		_currentElementName     = string.Empty;
	}

	#endregion

} // End class.