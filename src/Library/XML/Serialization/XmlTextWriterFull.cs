﻿using System.Xml;

namespace DigitalProduction.Xml.Serialization;

/// <summary>
/// 
/// </summary>
public class XmlTextWriterFullEndElement : XmlTextWriter
{
	#region Fields

	#endregion

	#region Construction

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="textWriter">TextWriter.</param>
	public XmlTextWriterFullEndElement(TextWriter textWriter) :
		base(textWriter)
	{
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="filename">File (path) to write to.</param>
	/// <param name="settings">Settings to use for writing.</param>
	public XmlTextWriterFullEndElement(string filename, XmlWriterSettings settings) :
		base(filename, settings.Encoding)
	{
		if (settings.Indent)
		{
			Formatting = Formatting.Indented;
		}
		else
		{
			Formatting = Formatting.None;
		}
	}

	#endregion

	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Override the writing of the end element to use the full end element (&lt;element&gt;&lt;/element&gt;) instead
	/// of the short method (&lt;element/&gt;);
	/// </summary>
	public override void WriteEndElement()
	{
		base.WriteFullEndElement();
	}

	#endregion

} // End class.