//========================================================================
//
// Copyright (c) 2014/2015 Thomas Hagen Johansen <thj@ic-sys.com>
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
using System.IO;
using System.Xml;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;

//using send.localhost;		// local debugging (http://localhost:8080/Frontend.asmx)
using send.testei.sst.dk;		// remote test system (https://ei.sst.dk/test-ei/Frontend.asmx)

namespace com.icsys.samples.sei.legacy.send
{

	//  Information we need to call a backend method
	public struct Method
	{
		public String fullMethodName;
		public Object[] arguments;
		public Type returnType;
		public String preparedRequest;
	}
		
	/********************************************************************/
	//
	/********************************************************************/
	public class MainClass
	{
		private static readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;
		private static readonly Frontend webservice = new Frontend();

		private const String WebserviceNS = "https://ei.sst.dk";

		public static X509Certificate2 certificate;

		/**************************************************************************/
		// Progam entry point
		/**************************************************************************/
		public static void Main (string[] args)
		{
			certificate = new X509Certificate2( appSettings["certPath"], "Test1234", X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

			//Report ();
			//Cancel ();
			GetUserDetailsByCPREx ();
			//AuthoriseUser ();
		}

		/**************************************************************************/
		// General method to call web service with and without validation
		/**************************************************************************/
		public static string[] Call(string fullMethodName, Object[] arguments, PacketPriority packetPriority, bool withValidation)
		{
			Method method			= new Method();
			method.fullMethodName	= fullMethodName;

			method.arguments		= arguments;
			method.returnType		= typeof(void);
			method.preparedRequest	= null;

			XmlDocument signDoc = Sign(MakeSOAP(method), "DataID");
			if (signDoc == null)
				return new string[0];

			SoapPacket[] sp = new SoapPacket[1];
			sp[0] = new SoapPacket();
			sp[0].SoapData = signDoc.OuterXml;

			ACKPacket[] ack = new ACKPacket[1];
			ack[0]          = new ACKPacket();
			ack[0].PacketID = null;
			ack[0].Priority = packetPriority;


			if( withValidation )
				return SendPacketsWithValidation(method, sp, ack);
			else
				return SendPackets(method, sp, ack);
		}

		/**************************************************************************/
		// Call backend method "dk.hob.ei.general.Plugin_CancelDocument"
		/**************************************************************************/
		public static void Cancel()
		{
			Object[] arguments = new Object[6]
			{
				"documentID",	appSettings ["documentID"],
				"pluginName",	appSettings ["pluginName"],
				"groupID",		appSettings ["groupID"]
			};

			string[] MethodNames = {"dk.hob.ei.general.Plugin_CancelDocument"};
			string[] ids = Call(MethodNames[0], arguments, PacketPriority.High, false);
			if (ids.Length > 0)
			{
				SoapPacket[] sp = webservice.GetPackets2 (ids, MethodNames);
				if (sp.Length > 0)
				{
					if( sp[0].Found )
						System.Console.WriteLine (sp [0].SoapData);
					else
						System.Console.WriteLine ("No response before timeout.");
				}
			}
		}

		/**************************************************************************/
		// Call backend method "dk.hob.ei.users.Admin_GetUserDetailsByCPREx"
		/**************************************************************************/
		public static void GetUserDetailsByCPREx()
		{
			Object[] arguments = new Object[4]
			{
				"cpr",					appSettings ["cpr"],
				"includeLockedUsers",	false,
			};

			string[] MethodNames = {"dk.hob.ei.users.Admin_GetUserDetailsByCPREx"};
			string[] ids = Call(MethodNames[0], arguments, PacketPriority.Admin, false);
			if (ids.Length > 0)
			{
				SoapPacket[] sp = webservice.GetPackets2 (ids, MethodNames);
				if (sp.Length > 0)
				{
					if( sp[0].Found )
						System.Console.WriteLine (sp [0].SoapData);
					else
						System.Console.WriteLine ("No response before timeout.");
				}
			}
		}

		/**************************************************************************/
		// Call backend method "dk.hob.ei.general.Plugin_AuthoriseUser"
		/**************************************************************************/
		public static void AuthoriseUser()
		{
			Object[] arguments = new Object[2]
			{
				"document",		null
			};
			FillDocument (arguments);
			Call("dk.hob.ei.general.Plugin_AuthoriseUser", arguments, PacketPriority.OneWay, false);
		}



		/**************************************************************************/
		// Call backend method "dk.hob.ei.general.Plugin_AuthoriseUser"
		/**************************************************************************/
		public static void FillDocument(Object[] arguments)
		{
			// Load data - for example, exported from the SEI-client application
			arguments [arguments.Length-1] = (File.ReadAllText (appSettings ["dataPath"], Encoding.UTF8)).Replace("\r\n",string.Empty).Replace("\n",string.Empty);
		}


		/**************************************************************************/
		// Call backend method "dk.hob.ei.general.Plugin_Indberet"
		/**************************************************************************/
		public static void Report()
		{
			Object[] arguments = new Object[10]
			{
				"letterID",		null,
				"pluginName",	appSettings["pluginName"],
				"groupID",		appSettings["groupID"],
				"subject",		appSettings["subject"],
				"document",		null
			};
			FillDocument (arguments);
			Call("dk.hob.ei.general.Plugin_Indberet", arguments, PacketPriority.OneWay, true);
		}

		/**************************************************************************/
		// Constructs a SOAP message in XML
		/**************************************************************************/
		public static XmlDocument MakeSOAP(Method method)
		{
			XmlDocument doc = new XmlDocument();

			XmlElement xmlEnvelope = doc.CreateElement("soap", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
			XmlElement xmlBody     = doc.CreateElement("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
			XmlElement xmlMethod   = doc.CreateElement(method.fullMethodName, WebserviceNS);

			// Arguments
			if (method.arguments != null)
			{
				for (int i = 0; i < method.arguments.Length; i += 2)
				{
					String name = (String)method.arguments[i];
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
		public static string[] SendPackets(Method method, SoapPacket[] packets, ACKPacket[] acks)
		{
			ACKPacket[] ackPackets = acks;

			// Send the data packet
			string[] ids = webservice.SendPackets(packets);
			for (int i = 0; i < ids.Length; i++) {
				ackPackets [i].PacketID = ids [i];
			}
			// Send the ACK packet
			webservice.SendACKPackets (ackPackets);
			return ids;
		}

		/**************************************************************************/
		// Send the request to the webservice and validate it
		/**************************************************************************/
		public static string[] SendPacketsWithValidation(Method method, SoapPacket[] packets, ACKPacket[] acks)
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