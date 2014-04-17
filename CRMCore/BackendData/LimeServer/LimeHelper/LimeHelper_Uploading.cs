using System;
using System.Xml;
using System.IO;

namespace LimeServer
{
	public partial class LimeHelper //Uploading
	{
		/*
		// Uppladdning av data
		public static string GetColdClientData(NavigationCore.CardColdClientData coldClientData)
		{
			ServerQueryType type = ServerQueryType.UploadColdClient;
			string tableName = GetTableName(type);

			StringWriter stringWriter = new StringWriter();
			XmlWriter writer = GetXMLWriter(ref stringWriter);

			AddAttribute(ref writer, "id" + tableName, coldClientData.ID);
			AddAttribute(ref writer, "history" + tableName, coldClientData.ActivityID);
			AddAttribute(ref writer, "coworker", coldClientData.CoworkerId);
			AddAttribute(ref writer, "name", coldClientData.ClientName);
			AddAttribute(ref writer, "note", coldClientData.Note);
			AddAttribute(ref writer, "address", coldClientData.Address);
			AddAttribute(ref writer, "city", coldClientData.City);
			AddAttribute(ref writer, "zipcode", coldClientData.ZipCode);
			AddAttribute(ref writer, "date", coldClientData.Date.ToString(LimeHelper.ServerDateTimeFormat));
			AddAttribute(ref writer, "gpslong", coldClientData.Latitude.ToString().Replace(',', '.'));
			AddAttribute(ref writer, "gpslat", coldClientData.Longitude.ToString().Replace(',', '.'));
			writer.Close();

			return ("<" + tableName + " " + stringWriter + "/>");
		}

		// Uppladdning av data
		public static string GetActivityData(NavigationCore.CardActivityData activityData)
		{
			ServerQueryType type = ServerQueryType.UploadActivity;
			string tableName = GetTableName(type);

			StringWriter stringWriter = new StringWriter();
			XmlWriter writer = GetXMLWriter(ref stringWriter);

			AddAttribute(ref writer, "id" + tableName, activityData.ID);
			AddAttribute(ref writer, "company", activityData.CompanyID);
			AddAttribute(ref writer, "type", activityData.Type);
			AddAttribute(ref writer, "note", activityData.Note);
			AddAttribute(ref writer, "coworker", activityData.CoworkerID);
			AddAttribute(ref writer, "person", activityData.PersonID);
			AddAttribute(ref writer, "coldclient", activityData.ColdClientID);

			if (activityData.Date != null)
			{
				var date = (DateTime)activityData.Date;
				AddAttribute(ref writer, "date", date.ToString(ServerDateTimeFormat));
			}

			writer.Close();

			return ("<" + tableName + " " + stringWriter.ToString() + "/>");
		}

		// Uppladdning av data
		public static string GetCompanyData(string id, NavigationCore.CardClientData clientData)
		{
			ServerQueryType type = ServerQueryType.UploadCompany;
			string tableName = GetTableName(type);

			StringWriter stringWriter = new StringWriter();
			XmlWriter writer = GetXMLWriter(ref stringWriter);

			AddAttribute(ref writer, "id" + tableName, id);
			AddAttribute(ref writer, "name", clientData.CompanyName);
			AddAttribute(ref writer, "phone", clientData.Phone);
			AddAttribute(ref writer, "www", clientData.Website);
			AddAttribute(ref writer, "email", clientData.Email);
			AddAttribute(ref writer, "invoicezipcode", clientData.InvoiceZipcode);
			AddAttribute(ref writer, "invoicecity", clientData.InvoiceCity);
			AddAttribute(ref writer, "invoiceaddress1", clientData.InvoiceAddress);
			AddAttribute(ref writer, "deliveryname", clientData.DeliveryName);
			AddAttribute(ref writer, "labelno", clientData.LittraNr);
			AddAttribute(ref writer, "deliveryaddress1", clientData.DeliveryAddress);
			AddAttribute(ref writer, "deliveryaddress2", clientData.DeliveryAddress2);
			AddAttribute(ref writer, "country", clientData.Country);
			AddAttribute(ref writer, "deliveryzipcode", clientData.DeliveryZipcode);
			AddAttribute(ref writer, "deliverycity", clientData.DeliveryCity);
			AddAttribute(ref writer, "registrationno", clientData.OrganisationNr);
			AddAttribute(ref writer, "customerno", clientData.CustomerNr);
			AddAttribute(ref writer, "companycategory", clientData.CompanyCategory);
			AddAttribute(ref writer, "district", clientData.District);
			AddAttribute(ref writer, "active", clientData.Active);
			AddAttribute(ref writer, "creditwarning", clientData.CreditWarning);

			writer.Close();

			return ("<" + tableName + " " + stringWriter.ToString() + "/>");
		}

		// Uppladdning av data
		public static string GetArticleDemoedData(List<NavigationCore.ArticleDemo> demoedArticle)
		{
			ServerQueryType type = ServerQueryType.UploadDemoedArticles;
			string tableName = GetTableName(type);

			string data = "";
			foreach(var rowData in demoedArticle)
			{
				StringWriter stringWriter = new StringWriter();
				XmlWriter writer = GetXMLWriter(ref stringWriter);

				AddAttribute(ref writer, "id" + tableName, rowData.ID);
				AddAttribute(ref writer, "article", rowData.ArticleID);
				AddAttribute(ref writer, "history", rowData.HistoryID);
				AddRemoveAttribute(ref writer, rowData.Remove);
				//AddAttribute(ref writer, "ordered", rowData.Ordered);

				writer.Close();

				data += ("<" + tableName + " " + stringWriter.ToString() + "/>");
			}
			return data;
		}

		// Uppladdning av data
		public static string GetOrderRowsData(List<NavigationCore.BusinessRow> businessRows)
		{
			ServerQueryType type = ServerQueryType.UploadBusinessRows;
			string tableName = GetTableName(type);


			string data = "";
			foreach(var rowData in businessRows)
			{
				StringWriter stringWriter = new StringWriter();
				XmlWriter writer = GetXMLWriter(ref stringWriter);

				AddAttribute(ref writer, "id" + tableName, rowData.ID);
				AddAttribute(ref writer, "article", rowData.ArticleID);
				AddAttribute(ref writer, "business", rowData.BusinessID);
				AddAttribute(ref writer, "name", rowData.Name);
				AddAttribute(ref writer, "price", rowData.Price.Replace(",", "."));
				AddAttribute(ref writer, "quantity", rowData.Quantity);
				AddRemoveAttribute(ref writer, rowData.Remove);
				//AddAttribute(ref writer, "selected", rowData.Selected);

				writer.Close();

				data += ("<" + tableName + " " + stringWriter.ToString() + "/>");
			}
			return data;
		}

		// Uppladdning av data
		public static string GetTodoData(NavigationCore.CardTodoData todoData)
		{
			ServerQueryType type = ServerQueryType.UploadTodoData;
			string tableName = GetTableName(type);

			StringWriter stringWriter = new StringWriter();
			XmlWriter writer = GetXMLWriter(ref stringWriter);

			AddAttribute(ref writer, "id" + tableName, todoData.ID);
			AddAttribute(ref writer, "company", todoData.CompanyID);
			AddAttribute(ref writer, "coworker", todoData.CoworkerID);
			AddAttribute(ref writer, "person", todoData.PersonID);
			AddAttribute(ref writer, "note", todoData.Note);
			AddAttribute(ref writer, "endtime", todoData.EndTime.ToString(ServerDateTimeFormat));
			AddAttribute(ref writer, "starttime", todoData.StartTime.ToString(ServerDateTimeFormat));
			AddAttribute(ref writer, "done", todoData.Done ? "1" : "0");
			AddAttribute(ref writer, "subject", todoData.Subject);
			AddAttribute(ref writer, "canceled", todoData.Cancelled ? "1" : "0");

			writer.Close();

			return ("<" + tableName + " " + stringWriter.ToString() + "/>");
		}

		// Uppladdning av data
		public static string GetContactData(NavigationCore.CardContactData contactData)
		{
			ServerQueryType type = ServerQueryType.UploadContact;
			string tableName = GetTableName(type);

			StringWriter stringWriter = new StringWriter();
			XmlWriter writer = GetXMLWriter(ref stringWriter);

			AddAttribute(ref writer, "id" + tableName, contactData.ID);
			AddAttribute(ref writer, "company", contactData.CompanyID);
			AddAttribute(ref writer, "email", contactData.Email);
			AddAttribute(ref writer, "firstname", contactData.FirstName);
			AddAttribute(ref writer, "lastname", contactData.LastName);
			AddAttribute(ref writer, "misc", contactData.Misc);
			AddAttribute(ref writer, "mobilephone", contactData.MobilePhone);
			AddAttribute(ref writer, "phone", contactData.Phone);
			AddAttribute(ref writer, "position", contactData.Position);
			AddAttribute(ref writer, "active", contactData.Active);

			writer.Close();

			return ("<" + tableName + " " + stringWriter.ToString() + "/>");
		}

		// Uppladdning av data
		public static string GetBusinessData(string id, NavigationCore.CardBusinessData clientData)
		{
			ServerQueryType type = ServerQueryType.UploadBusiness;
			string tableName = GetTableName(type);

			StringWriter stringWriter = new StringWriter();
			XmlWriter writer = GetXMLWriter(ref stringWriter);

			AddAttribute(ref writer, "id" + tableName, id);
			AddAttribute(ref writer, "company", clientData.CompanyID);
			AddAttribute(ref writer, "deliveryaddress1", clientData.CompanyDeliveryAddress);
			AddAttribute(ref writer, "deliveryaddress2", clientData.CompanyDeliveryAddress2);
			AddAttribute(ref writer, "deliverycity", clientData.CompanyDeliveryCity);
			AddAttribute(ref writer, "deliveryname", clientData.CompanyDeliveryName);
			AddAttribute(ref writer, "deliveryzipcode", clientData.CompanyDeliveryZipCode);
			AddAttribute(ref writer, "person", clientData.CompanyReferenceID);
			AddAttribute(ref writer, "coworker", clientData.CoworkerID);
			AddAttribute(ref writer, "email", clientData.EmailConfirmation);
			AddAttribute(ref writer, "marking", clientData.Mark);
			AddAttribute(ref writer, "note", clientData.Note);
			AddAttribute(ref writer, "orderdate", clientData.OrderDate);
			AddAttribute(ref writer, "deliverydate", clientData.OrderDeliveryDate);
			AddAttribute(ref writer, "businesstatus", clientData.OrderStatus);
			AddAttribute(ref writer, "paymentterms", clientData.OrderTerms);
			AddAttribute(ref writer, "ordertype", clientData.OrderType);
			AddAttribute(ref writer, "tenderduedate", clientData.OrderEndDate);

			writer.Close();

			return ("<" + tableName + " " + stringWriter.ToString() + "/>");
		}

		// Uppladdning av fil
		public static string GetFileUploadQuery(string contentOfFile, string fileExtension, string comment, string clientId, string coworkerId)
		{
			ServerQueryType type = ServerQueryType.UploadFile;
			string tableName = GetTableName(type);

			//<records>
			//	<coworker idcoworker="-1" name="Lars Larsson">
			//		<portrait fileextension="jpg" xmlns:dt="urn:schemas-microsoft-com:datatypes" dt:dt="bin.base64">{binär sträng}</portrait>
			//	</coworker>
			//</records>

			string companyString = "";
			if(clientId != null)
				companyString = " company=\"" + clientId + "\"";

			string coworkerString = "";
			if(coworkerId != null)
				coworkerString = " coworker=\"" + coworkerId + "\"";

			string records = "<" + tableName + " id" + tableName + "=\"-1\" comment=\"" + comment + "\" type=\"40801\"" + companyString + coworkerString + ">" +
				"<document fileextension=\"" + fileExtension + "\" xmlns:dt=\"urn:schemas-microsoft-com:datatypes\" dt:dt=\"bin.base64\">" + contentOfFile + "</document>" +
				"</" + tableName + ">";

			return records;
		}
		*/
	}
}

