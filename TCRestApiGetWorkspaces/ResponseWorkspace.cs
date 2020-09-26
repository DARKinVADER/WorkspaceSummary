/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Xml2CSharp
{
	[XmlRoot(ElementName = "ArrayOfstring", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
	public class ArrayOfstring
	{
		[XmlElement(ElementName = "string", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		public List<string> String { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
		[XmlAttribute(AttributeName = "i", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string I { get; set; }
	}

}
