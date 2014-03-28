//========================================================================
//
// Copyright (c) 2014 Thomas Hagen Johansen <thj@ic-sys.com>
//
// This software is provided 'as-is', without any express or implied
// warranty. In no event will the authors be held liable for any damages
// arising from the use of this software.
//
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it
// freely, subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented; you must not
//    claim that you wrote the original software. If you use this software
//    in a product, an acknowledgment in the product documentation would
//    be appreciated but is not required.
//
// 2. Altered source versions must be plainly marked as such, and must not
//    be misrepresented as being the original software.
//
// 3. This notice may not be removed or altered from any source
//    distribution.
//
//========================================================================

using System;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Text;
using System.Security;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using send.ei.sst.dk;

namespace com.icsys.samples.sei.legacy.send
{

	// Holds all the information needed to call a backend method
	public struct Method
	{
		public String fullMethodName;
		public Object[] arguments;
		public Type returnType;
		public String preparedRequest;
	}

	/********************************************************************/
	// Test indberetning af skema til børnedatabasen.
	/********************************************************************/
	public class MainClass
	{
		private static readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;
		private static readonly Frontend webservice = new Frontend();

		private const String WebserviceNS = "https://ei.sst.dk";

		public static X509Certificate2 certificate;


		public static void Main (string[] args)
		{
			certificate = new X509Certificate2( appSettings["certPath"], "Test1234", X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

			Method method			= new Method();
			method.fullMethodName	= "dk.hob.ei.general.Plugin_Indberet";
			Object[] arguments = new Object[10]
			{
				"letterID",		null,
				"pluginName",	appSettings["pluginName"],
				"groupID",		appSettings["groupID"],
				"subject",		appSettings["subject"],
				"document",		null
			};
			// Load data (could for example be exported from the SEI-client application)
			arguments [9] = (File.ReadAllText (appSettings ["dataPath"], Encoding.UTF8)).Replace("\r\n",string.Empty).Replace("\n",string.Empty);

			method.arguments		= arguments;
			method.returnType		= typeof(void);
			method.preparedRequest	= null;

			SoapPacket[] sp = new SoapPacket[1];
			sp[0] = new SoapPacket();
			sp[0].SoapData = PrepareRequest(method);


			ACKPacket[] ack = new ACKPacket[1];
			ack[0]          = new ACKPacket();
			ack[0].PacketID = null;
			ack[0].Priority = PacketPriority.OneWay;

			// Submit
			SendSynchronousRequests(method, sp, ack);
		}

		/**************************************************************************/
		// Prepares the request given and return the prepared request as a string
		/**************************************************************************/
		public static String PrepareRequest(Method method)
		{
			XmlDocument signDoc = Sign(MakeSOAP(method), "DataID");
			if (signDoc == null)
				return String.Empty;

			return signDoc.OuterXml;
		}
			
		/**************************************************************************/
		// Constructs a SOAP message in XML
		/**************************************************************************/
		public static XmlDocument MakeSOAP(Method method)
		{
			// Create an empty XML document
			XmlDocument doc = new XmlDocument();

			// <soap:Envelope>
			XmlElement xmlEnvelope = doc.CreateElement("soap", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");

			// <soap:Body>
			XmlElement xmlBody     = doc.CreateElement("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");

			// <Method>
			XmlElement xmlMethod   = doc.CreateElement(method.fullMethodName, WebserviceNS);

			// Arguments
			if (method.arguments != null)
			{
				for (int i = 0; i < method.arguments.Length; i += 2)
				{
					String name         = (String)method.arguments[i];
					XmlElement xmlParam = doc.CreateElement(name, WebserviceNS);
					AppendSOAPArgument(xmlParam, method.arguments[i + 1]);
					xmlMethod.AppendChild(doc.CreateComment(name));
					xmlMethod.AppendChild(xmlParam);
				}
			}

			xmlBody.AppendChild(xmlMethod);
			xmlEnvelope.AppendChild(xmlBody);
			doc.AppendChild(xmlEnvelope);

			return doc;
		}

		/**************************************************************************/
		// Append a parameter to the SOAP document
		/**************************************************************************/
		public static void AppendSOAPArgument(XmlNode param, Object value)
		{
			if (value is String)
			{
				XmlText val = param.OwnerDocument.CreateTextNode((String)value);
				param.AppendChild(val);
			}
		}


		/**************************************************************************/
		// Sign the document given
		/**************************************************************************/
		public static XmlDocument Sign(XmlDocument doc, String dataID)
		{
			// Sign
			SignedXml sxml = new SignedXml ();
			sxml.SigningKey = certificate.PrivateKey;
			sxml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigC14NTransformUrl;

			DataObject dataObject = new DataObject(dataID,System.Net.Mime.MediaTypeNames.Text.Xml.ToString(),"utf-8",doc.DocumentElement);
			sxml.AddObject(dataObject);

			Reference reference = new Reference();
			reference.Uri = "#" + dataID;
			sxml.AddReference(reference);

			KeyInfo keyInfo = new KeyInfo();
			keyInfo.AddClause(new RSAKeyValue((RSACryptoServiceProvider)certificate.PublicKey.Key));
			keyInfo.AddClause(new KeyInfoX509Data(certificate));
			sxml.KeyInfo = keyInfo;

			sxml.ComputeSignature();

			//	Create new document and verify it
			XmlDocument newDoc = new XmlDocument();
			newDoc.PreserveWhitespace = true;
			XmlNode node = newDoc.ImportNode(sxml.GetXml(),true);
			newDoc.AppendChild (node);

			if (!Verify(newDoc))
				return null;

			return newDoc;
		}


		/**************************************************************************/
		// Verify the document given
		/**************************************************************************/
		public static bool Verify(XmlDocument doc)
		{
			// Create a SignedXml
			SignedXml signedXml = new SignedXml();

			// Load the XML
			XmlNodeList nodeList = doc.GetElementsByTagName("Signature");
			signedXml.LoadXml((XmlElement)nodeList[0]);

			return signedXml.CheckSignature();
		}


		/**************************************************************************/
		// Send the request to the webservice
		/**************************************************************************/
		public static String[] SendSynchronousRequests(Method method, SoapPacket[] packets, ACKPacket[] acks)
		{
			ACKPacket[] ackPackets = acks;

			// Send the data packet
			SendPacketsWithValidationResult result = webservice.SendPacketsWithValidation(packets);
			String[] ids = result.PacketIDs;

			if (result.ErrorMessages [0].errorCode == 0)
			{
				for (int i = 0; i < ids.Length; i++) {
					ackPackets [i].PacketID = ids [i];
				}
				// Send the ACK packet
				if (webservice.SendACKPackets (ackPackets))
					return ids;
			}
			else
			{ // ERROR
			}

			return null;
		}
	}
}