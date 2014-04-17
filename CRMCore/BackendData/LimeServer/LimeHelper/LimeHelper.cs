using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Security;

namespace LimeServer
{

	public partial class LimeHelper //Helper
	{
		#region helpData
		private static readonly List<string> availablePriceGroups = new List<string>()
		{
			"price1",
			"price2",
			"price3",
			"price4",
			"price5",
			"price6",
		};

		public static bool UseServerURL = true;

		public static string NonBreakSpace {
			get
			{
				return ((char)160).ToString();
			}
		}


		public static DatabaseStructure.DatabaseData DatabaseData = null;
		public static int RowCount = 35;
		public static string ServerDateFormat = "yyyy-MM-dd";
		public static string ServerDateTimeFormat = "yyyy-MM-dd HH:mm";
		public static string ServerTimeFormat = "HH:mm";

		static string ServerURL;
		static string TestServerURL;

		public static void SetServerURLs(string serverURL, string testServerURL)
		{
			ServerURL = serverURL;
			TestServerURL = testServerURL;
		}

		public static string GetServerURL()
		{
			if (UseServerURL)
				return ServerURL;
				//return "http://lime.beltton.se:33776/wb_lime/";
			else
				return TestServerURL;
				//return "http://crm.10fingers.se:8351/wbapp/";

			// Vår Gamla med Basic HTTP: 	"http://crm.10fingers.se:8351/wbapp/"
			// Vår Nya med WS HTTP: 		"http://10fserver-prod:8024/test/"
			// Wulffs med WS HTTP: 			"http://lime.beltton.se:33776/wb_lime/"

			// Med /meta i slutet så kan man få tag i meta-datan.
		}
		#endregion

		#region HelperFunctions
		/// <summary>
		/// Calculates number of business days, taking into account:
		///  - weekends (Saturdays and Sundays)
		///  - bank holidays in the middle of the week
		/// </summary>
		/// <param name="firstDay">First day in the time interval</param>
		/// <param name="lastDay">Last day in the time interval</param>
		/// <param name="bankHolidays">List of bank holidays excluding weekends</param>
		/// <returns>Number of business days during the 'span'</returns>
		public static int BusinessDaysUntil(DateTime firstDay, DateTime lastDay, DateTime[] bankHolidays)
		{
			firstDay = firstDay.Date;
			lastDay = lastDay.Date;
			if (firstDay > lastDay)
				throw new ArgumentException("Incorrect last day " + lastDay);

			TimeSpan span = lastDay - firstDay;
			int businessDays = span.Days + 1;
			int fullWeekCount = businessDays / 7;
			// find out if there are weekends during the time exceedng the full weeks
			if (businessDays > fullWeekCount*7)
			{
				// we are here to find out if there is a 1-day or 2-days weekend
				// in the time interval remaining after subtracting the complete weeks
				int firstDayOfWeek = firstDay.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)firstDay.DayOfWeek;
				int lastDayOfWeek = lastDay.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)lastDay.DayOfWeek;

				if (lastDayOfWeek < firstDayOfWeek)
					lastDayOfWeek += 7;
				if (firstDayOfWeek <= 6)
				{
					if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
						businessDays -= 2;
					else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
						businessDays -= 1;
				}
				else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
					businessDays -= 1;
			}

			// subtract the weekends during the full weeks in the interval
			businessDays -= fullWeekCount + fullWeekCount;

			// subtract the number of bank holidays during the time interval
			foreach (DateTime bankHoliday in bankHolidays)
			{
				DateTime bh = bankHoliday.Date;
				if (firstDay <= bh && bh <= lastDay && !(bh.DayOfWeek == DayOfWeek.Sunday || bh.DayOfWeek == DayOfWeek.Saturday))
					--businessDays;
			}

			return businessDays;
		}

		static XmlWriter GetXMLWriter(ref StringWriter stringWriter)
		{
			if(stringWriter != null)
			{
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.OmitXmlDeclaration = true;
				settings.ConformanceLevel = ConformanceLevel.Fragment;
				return XmlWriter.Create(stringWriter, settings);
			}
			return null;
		}

		static void AddAttribute(ref XmlWriter writer, string name, string value)
		{
			if(value != null)
				writer.WriteAttributeString(name, value);
		}

		static void AddRemoveAttribute(ref XmlWriter writer, bool willBeRemoved)
		{
			string name = "status";
			string value = "2";
			if(willBeRemoved)
				writer.WriteAttributeString(name, value);
		}

		public static float ConvertToNumber(string text)
		{
			try
			{
				text = text.Replace(".", ",").Replace(NonBreakSpace, null).Replace(" ", null);
				float value = float.Parse(text, System.Globalization.CultureInfo.CreateSpecificCulture("sv-SE"));
				return (float)Math.Round(value, 2);
			}
			catch
			{
				return 0;
			}
		}

		static List<string> GetNumericFieldsAndRemoveThemFromList(ref List<string> fields, string value, ServerQueryType queryType)
		{
			bool isNumber = true;
			try
			{
				float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
			}
			catch
			{
				isNumber = false;
			}
			string tableName = GetTableName(queryType);
			List<string> numericTypesToFind = new List<string>() {
				"businessid",
				"id"+tableName,
				"price1","price2","price3","price4","price5","price6","price7","price8",
			};

			List<string> numericFields = fields.FindAll(delegate(string obj) {
				var splitted = obj.Split('.');
				var checkString = obj;
				if(splitted.Length > 0)
					checkString = splitted[splitted.Length - 1];
				return numericTypesToFind.Contains(checkString);
			});

			for(int i = 0; i < numericFields.Count; i++)
			{
				fields.Remove(numericFields[i]);
			}
			if(!isNumber)
				numericFields = new List<string>();
			return numericFields;
		}
		#endregion

	}

	public enum ServerQueryType
	{
		None,

		//Uploading
		UploadFile,
		UploadActivity,
		UploadTodoData,
		UploadCompany,
		UploadBusiness,
		UploadBusinessRows,
		UploadContact,
		UploadDemoedArticles,
		UploadColdClient,

		//Specific data for pickers and such
		DataCountry,
		DataCompanyCategory,
		DataOrderType,
		DataPaymentTerms,
		DataContact,
		DataTranslation,

		//Extra data for som cards
		BusinessRows,
		DemoedArticles,
		CampaignRows,
		ArticleResource,

		ArticleBoughtBy,

		//Cards (popups)
		CardClientData,
		CardBusinessData,
		CardArticleData,
		CardContactData,
		CardActivityData,
		CardTodoData,
		CardCampaignData,

		//Files/Documents
		DownloadFile,

		//Maps
		MapsNearbyClients,
		MapsRoute,

		//MenuSubItems
		ClientToDo,
		ClientHistory,
		ClientOrderOffers,
		ClientArticlesBought,
		ClientArticlesDemoed,
		ClientContacts,
		ClientDocuments,
		ClientSearch,

		ActivityHistory,
		ActivityAlarms,
		ActivityPlanning,


		OrderOfferHistory,
		OrderOfferUrgents,

		ArticleCampaigns,
		ArticleSearch,

		//My pages
		MyPagesStatistics,
		MyPagesGoalValues,

		//Add items
		AddArticleDemoed,
		AddArticleBusiness,


	}

}

