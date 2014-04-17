using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using MonoTouch.UIKit;

namespace LimeServer
{
	public partial class LimeService
	{
		/*
		Uri _URL;
		#region Constructors
		public LimeService(string URL)
		{
			this._URL = new Uri(URL);
		}

		public LimeService(Uri URL)
		{
			this._URL = URL;
		}
		#endregion

		#region Help data
		enum RequestType
		{
			GetXMLQueryData,
			UpdateData,
			GetDataStructure
		}

		string GetSoapActionString(RequestType type)
		{
			string soapActionString = null;
			switch(type)
			{
				case RequestType.GetXMLQueryData:
					soapActionString = "http://lundalogik.se/Tangelo/IDataService/GetXmlQueryData";
				break;
				case RequestType.UpdateData:
					soapActionString = "http://lundalogik.se/Tangelo/IDataService/UpdateData";
				break;
				case RequestType.GetDataStructure:
					soapActionString = "http://lundalogik.se/Tangelo/IMetaDataService/GetDataStructure";
				break;
			}
			return soapActionString;
		}
		string _openEnvelope = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tan=\"http://lundalogik.se/Tangelo/\"><soapenv:Header/><soapenv:Body>";
		string _closeEnvelope = "</soapenv:Body></soapenv:Envelope>";
		string _contentTypeString {
			get
			{
//				if(LimeHelper.useWulffCrmServer)
//					return "application/soap+xml;charset=utf-8";
//				else
				return "text/xml;charset=UTF-8";
			}
		}



		WebClient GetNewWebClient(RequestType type)
		{
			string soapActionString = GetSoapActionString(type);

			WebClient webClient = new WebClient();
			webClient.Headers.Add("Content-Type", _contentTypeString);
			webClient.Headers.Add("SOAPAction", soapActionString);

//			var credentials = new NetworkCredential()
//			{
//				UserName = "",
//				Password = "",
//			};
//			webClient.Credentials = credentials;


			return webClient;
		}
				
		#endregion

		#region Help functions
		void AlertServerError()
		{
			NavigationCore.ErrorMessages.CreateErrorAlert(NavigationCore.ErrorMessages.ErrorType.ServerOverload);
		}

		void MoveCountryToTop(string countryCode)
		{
			if(countryCode == null)
				return;

			var countryObj = LimeHelper.Countries.Find((obj) => {
				return obj.Code == countryCode;
			});
			if(countryObj == null)
				return;
			bool success = LimeHelper.Countries.Remove(countryObj);
			if(success)
			{
				LimeHelper.Countries.Insert(0, countryObj);
			}
		}

		#endregion

		public void Login(string _salesId, string _password, UIViewController _currentViewController)
		{

			var query = LimeHelper.GetLoginQuery(_salesId, _password);
			var fullQuery = _openEnvelope +
				"<tan:GetXmlQueryData>" +
				"<tan:query>" +
				query +
				"</tan:query>" +
				"</tan:GetXmlQueryData>" +
				_closeEnvelope;

			WebClient webClient = GetNewWebClient(RequestType.GetXMLQueryData);
			webClient.UploadStringCompleted += (object sender, UploadStringCompletedEventArgs e) => {
				_currentViewController.InvokeOnMainThread( () => {
					try
					{
						bool success;
						List<NavigationCore.CoworkerData> coworkers = LimeXMLParser.ParseLogin(e.Result);

						NavigationCore.CoworkerData loggedInCoworker = coworkers.Find(delegate(NavigationCore.CoworkerData coworker)
							{
								return coworker.SaleNr == _salesId;
							});

						coworkers.Remove(loggedInCoworker);

						if(loggedInCoworker != null)
							success = true;
						else
							success = false;

						if(success)
						{
							loggedInCoworker.SubCoworkers = coworkers;
							GetDistricts(loggedInCoworker, coworkers, _currentViewController);
						}
						else
						{
							if(LoginDone != null)
							{
								LoginDone(this, new LoginResult(){Coworker = null, Message = "Felaktiga inloggningsuppgifter.", Success = false});
							}
						}
					}
					catch(Exception ex)
					{
						Console.WriteLine(ex.StackTrace);
						AlertServerError();
						if(LoginDone != null)
						{
							LoginDone(this, new LoginResult(){Coworker = null, Message = "Serverfel", Success = false});
						}
					}
				});
			};
			try
			{
				webClient.UploadStringAsync(_URL, fullQuery);
			}
			catch(Exception ex)
			{
				if(LoginDone != null)
				{					
					LoginDone(this, new LoginResult(){Coworker = null, Message = ex.Message, Success = false});
				}
			}
		}

		*/

		/*public class InvalidFileNameException : Exception
		{
			public InvalidFileNameException(string message) : base(message)
			{
			}
		}
		*/


	}


}
