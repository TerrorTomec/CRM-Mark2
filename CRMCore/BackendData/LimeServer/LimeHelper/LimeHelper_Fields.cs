using System;
using System.Collections.Generic;

namespace LimeServer
{
	public partial class LimeHelper //Field data
	{

		public static List<string> GetFields(ServerQueryType type, bool withSearch = false)
		{
			List<string> fields = new List<string>();
			string tableName = GetTableName(type, withSearch);

			switch(type)
			{
				case ServerQueryType.DemoedArticles:
					fields.Add("article.idarticle");
					fields.Add("article.name");
					fields.Add("article.articleno");
					fields.Add("history.idhistory");
					fields.Add("ordered");
				break;
				case ServerQueryType.CampaignRows:
					fields.Add("article.articleno");
					fields.Add("article.name");
					fields.Add("article.idarticle");
					fields.Add("price");
				break;
				case ServerQueryType.BusinessRows:
					fields.Add("name");
					fields.Add("quantity");
					fields.Add("price");
					fields.Add("totalprice");
					fields.Add("selected");
					fields.Add("article.idarticle");
					fields.Add("article.name");
					fields.Add("article.articleno");
					fields.Add("business.idbusiness");
				break;
				case ServerQueryType.ArticleBoughtBy:
					fields.Add("business.orderdate");
					fields.Add("business.company.idcompany");
					fields.Add("business.company.name");
					fields.Add("business.company.customerno");
					fields.Add("business.company.deliverycity");
					fields.Add("quantity");
					fields.Add("price");
					fields.Add("business.person.name");
					fields.Add("business.person.phone");
				break;
				case ServerQueryType.DataContact:
					fields.Add("name");
					fields.Add("email");
				break;
				case ServerQueryType.DataOrderType:
					fields.Add("code");
					fields.Add("name");
				break;
				case ServerQueryType.DataPaymentTerms:
					fields.Add("code");
					fields.Add("name");
				break;
				case ServerQueryType.DataCountry:
					fields.Add("code");
					fields.Add("name");
				break;
				case ServerQueryType.DataTranslation:
					fields.Add("code");
					fields.Add("sv");
					fields.Add("en_us");
					fields.Add("no");
					fields.Add("fi");
				break;
				case ServerQueryType.DataCompanyCategory:
					fields.Add("code");
					fields.Add("name");
				break;
				case ServerQueryType.CardClientData:
					fields.Add("name");
					fields.Add("phone");
					fields.Add("telefax");
					fields.Add("www");
					fields.Add("email");
					fields.Add("invoiceaddress1");
					fields.Add("deliveryname");
					fields.Add("labelno");
					fields.Add("deliveryaddress1");
					fields.Add("invoicezipcode");
					fields.Add("invoicecity");
					fields.Add("deliveryaddress2");
					fields.Add("country.name");
					fields.Add("deliveryzipcode");
					fields.Add("deliverycity");
					fields.Add("registrationno");
					fields.Add("customerno");
					fields.Add("companycategory.name");
					fields.Add("district.name");
					fields.Add("active");
					fields.Add("creditwarning");

					fields.Add("gpslong");
					fields.Add("gpslat");
				break;
				case ServerQueryType.MapsRoute:
					fields.Add("company.deliveryaddress1");
					fields.Add("company.deliverycity");
					fields.Add("company.email");
					fields.Add("company.name");
					fields.Add("company.phone");
					fields.Add("company.deliveryzipcode");
					fields.Add("starttime");
					fields.Add("company.gpslong");
					fields.Add("company.gpslat");
				break;
				case ServerQueryType.MapsNearbyClients:
					fields.Add("deliveryaddress1");
					fields.Add("deliverycity");
					fields.Add("email");
					fields.Add("name");
					fields.Add("phone");
					fields.Add("deliveryzipcode");
					fields.Add("gpslong");
					fields.Add("gpslat");
				break;
				case ServerQueryType.DownloadFile:
					fields.Add("document");
					fields.Add("document__data");
				break;
				case ServerQueryType.ArticleResource:
					fields.Add("name");
					fields.Add("link");
					fields.Add("article.idarticle");
				break;
					//case ServerQueryType.ActivityPlanning:
					/*fields.Add("starttime");
					fields.Add("coworker.name");
					fields.Add("subject");
					fields.Add("person.name");
					fields.Add("company.name");
					fields.Add("note");*/
					//break;
				case ServerQueryType.ClientToDo:
					fields.Add("starttime");
					fields.Add("endtime");
					fields.Add("subject");
					fields.Add("person.name");
					fields.Add("note");
					fields.Add("coworker.name");
				break;
				case ServerQueryType.ClientHistory:
					fields.Add("date");
					fields.Add("type");
					fields.Add("company.name");
					fields.Add("person.name");
					fields.Add("note");
					fields.Add("business.businessid");
					fields.Add("coworker.name");
				break;
				case ServerQueryType.ClientOrderOffers:
					fields.Add("orderdate");
					fields.Add("businessid");
					fields.Add("company.name");
					fields.Add("person.name");
					fields.Add("businesstatus");
					fields.Add("coworker.name");
					fields.Add("businessvalue");
				break;
				case ServerQueryType.ActivityPlanning:
				case ServerQueryType.CardTodoData:
					fields.Add("starttime");
					fields.Add("endtime");
					fields.Add("coworker.name");
					fields.Add("coworker.idcoworker");
					fields.Add("subject");
					fields.Add("person.name");
					fields.Add("person.idperson");
					fields.Add("company.name");
					fields.Add("company.idcompany");
					fields.Add("note");
					fields.Add("done");
					fields.Add("canceled");
				break;
				case ServerQueryType.CardCampaignData:
					fields.Add("headline");
					fields.Add("description");
					fields.Add("startdate");
					fields.Add("enddate");
				break;
				case ServerQueryType.CardActivityData:
					fields.Add("note");
					fields.Add("date");
					fields.Add("type");
					fields.Add("coworker.idcoworker");
				break;
				case ServerQueryType.CardContactData:
					fields.Add("company.name");
					fields.Add("company.idcompany");
					fields.Add("email");
					fields.Add("firstname");
					fields.Add("lastname");
					fields.Add("misc");
					fields.Add("mobilephone");
					fields.Add("phone");
					fields.Add("position");
					fields.Add("active");
				break;
				case ServerQueryType.CardBusinessData:
					fields.Add("businessid");
					fields.Add("ordertype");
					fields.Add("coworker.name");
					fields.Add("coworker.idcoworker");
					fields.Add("businesstatus");
					fields.Add("marking");
					fields.Add("paymentterms");
					fields.Add("company.name");
					fields.Add("company.idcompany");
					fields.Add("person.name");
					fields.Add("person.idperson");
					fields.Add("email");
					fields.Add("note");
					fields.Add("deliveryname");
					fields.Add("deliveryaddress1");
					fields.Add("deliveryaddress2");
					fields.Add("deliveryzipcode");
					fields.Add("deliverycity");
					fields.Add("orderdate");
					fields.Add("tenderduedate");
					fields.Add("deliverydate");
				break;
				case ServerQueryType.ClientArticlesBought:
					fields.Add("article.idarticle");
					fields.Add("business.orderdate");
					fields.Add("article.articleno");
					fields.Add("article.name");
					fields.Add("quantity");
					fields.Add("price");
					fields.Add("totalprice");
					fields.Add("business.coworker.name");
				break;
				case ServerQueryType.ClientArticlesDemoed:
					fields.Add("article.idarticle");
					fields.Add("history.date");
					fields.Add("article.articleno");
					fields.Add("article.name");
					fields.Add("history.coworker.name");
				break;
				case ServerQueryType.CardArticleData:
					fields.Add("articleno");
					fields.Add("name");
					fields.Add("division.name");
					fields.Add("active");
					fields.Add("vat");
					fields.Add("prodgroup");
					fields.Add("price1");
					fields.Add("price2");
					fields.Add("price3");
					fields.Add("price4");
					fields.Add("price5");
					fields.Add("price6");
					fields.Add("price7");
					fields.Add("price8");
				break;
				case ServerQueryType.ClientContacts:
					fields.Add("name");
					fields.Add("position");
					fields.Add("company.name");
					fields.Add("phone");
					fields.Add("email");
					fields.Add("active");

					//Will be hidden
					fields.Add("misc");
				break;
				case ServerQueryType.ClientDocuments:
					//fields.Add("createdtime");
					fields.Add("coworker.name");
					fields.Add("type");
					fields.Add("comment");
					fields.Add("document");
				break;
				case ServerQueryType.MyPagesStatistics:
					fields.Add("name");
					fields.Add("todayvisits");
					fields.Add("todayorders");
					fields.Add("todaydemos");
					fields.Add("todaysales");
					fields.Add("todaybookings");
				break;
				case ServerQueryType.MyPagesGoalValues:
					fields.Add("coworker.name");
					fields.Add("coworker.idcoworker");
					fields.Add("coworker.periodsales");
					fields.Add("coworker.yearlysales");
					fields.Add("coworker.yearlybudget");
					fields.Add("startdate");
					fields.Add("enddate");
					fields.Add("budget");

				break;
				case ServerQueryType.ClientSearch:
					fields.Add("name");
					fields.Add("deliveryaddress1");
					fields.Add("deliverycity");
					fields.Add("phone");
					fields.Add("district.name");
					fields.Add("customerno");

					//fields.Add("deliveryzipcode");
					//fields.Add("registrationno");
				break;
				case ServerQueryType.ActivityHistory:
					fields.Add("date");
					fields.Add("type");
					fields.Add("company.name");
					fields.Add("person.name");
					fields.Add("note");
					fields.Add("coworker.name");
					fields.Add("business.businessvalue");
				break;
				case ServerQueryType.ActivityAlarms:
					fields.Add("latesthistory");
					fields.Add("customerno");
					fields.Add("name");
					fields.Add("latesthistorytype");
				break;
				case ServerQueryType.OrderOfferHistory:
					fields.Add("orderdate");
					fields.Add("businessid");
					fields.Add("company.name");
					fields.Add("person.name");
					fields.Add("businesstatus");
					fields.Add("coworker.name");
					fields.Add("businessvalue");
				break;
				case ServerQueryType.OrderOfferUrgents:
					fields.Add("orderdate");
					fields.Add("businessid");
					fields.Add("company.name");
					fields.Add("person.name");
					fields.Add("businesstatus");
					fields.Add("coworker.name");
					fields.Add("businessvalue");
				break;
				case ServerQueryType.ArticleCampaigns:
					fields.Add("headline");
					fields.Add("description");
					fields.Add("startdate");
					fields.Add("enddate");
				break;
				case ServerQueryType.ArticleSearch:
					fields.Add("articleno");
					fields.Add("name");
					fields.Add("division.name");

					foreach(var item in availablePriceGroups)
					{
						fields.Add(item);
					}
				break;
				case ServerQueryType.AddArticleDemoed:
					if(withSearch)
					{
						fields.Add("articleno");
						fields.Add("name");
						fields.Add("division.name");
						foreach(var item in availablePriceGroups)
						{
							fields.Add(item);
						}
					}
					else
					{
						fields.Add("history.date");
						fields.Add("article.idarticle");
						fields.Add("article.articleno");
						fields.Add("article.name");
						fields.Add("article.division.name");

						foreach(var item in availablePriceGroups)
						{
							fields.Add("article." + item);
						}
					}
				break;
				case ServerQueryType.AddArticleBusiness:
					if(withSearch)
					{
						fields.Add("articleno");
						fields.Add("name");
						fields.Add("division.name");
						foreach(var item in availablePriceGroups)
						{
							fields.Add(item);
						}
					}
					else
					{
						fields.Add("business.orderdate");
						fields.Add("article.idarticle");
						fields.Add("article.articleno");
						fields.Add("article.name");
						fields.Add("article.division.name");
						foreach(var item in availablePriceGroups)
						{
							fields.Add("article." + item);
						}
					}
				break;

			}
			if(fields.Count > 0)
			{
				fields.Insert(0, "timestamp");
				fields.Insert(0, "id" + tableName);
			}

			return fields;
		}

		public static List<string> GetSearchFields(ServerQueryType type, bool withSearch = false)
		{
			List<string> fields = new List<string>();

			switch(type)
			{
				case ServerQueryType.MapsNearbyClients:
				break;
				case ServerQueryType.ClientToDo:
					fields.Add("starttime");
					fields.Add("endtime");
					fields.Add("subject");
					fields.Add("person.name");
					fields.Add("note");
					fields.Add("coworker.name");
				break;
				case ServerQueryType.ClientHistory:
					fields.Add("date");
					fields.Add("type");
					fields.Add("company.name");
					fields.Add("person.name");
					fields.Add("note");
					fields.Add("business.businessid");
					fields.Add("coworker.name");
				break;
				case ServerQueryType.ClientContacts:
					fields.Add("name");
					fields.Add("position");
					fields.Add("company.name");
					fields.Add("phone");
					fields.Add("email");
					fields.Add("misc");
				break;
				case ServerQueryType.ClientOrderOffers:
					fields.Add("orderdate");
					fields.Add("businessid");
					fields.Add("company.name");
					fields.Add("person.name");
					fields.Add("businesstatus");
					fields.Add("coworker.name");
					fields.Add("marking");
					fields.Add("businessvalue");
				break;
				case ServerQueryType.ClientArticlesBought:
					fields.Add("business.orderdate");
					fields.Add("article.articleno");
					fields.Add("article.name");
					//					fields.Add("quantity");
					//					fields.Add("price");
					//					fields.Add("totalprice");
					fields.Add("business.coworker.name");
				break;
				case ServerQueryType.ClientArticlesDemoed:
					fields.Add("history.date");
					fields.Add("article.articleno");
					fields.Add("article.name");
					fields.Add("history.coworker.name");
				break;
				case ServerQueryType.ClientDocuments:
					fields.Add("coworker.name");
					fields.Add("type");
					fields.Add("comment");
				break;
				case ServerQueryType.MyPagesStatistics:
					//Kan inte söka(Är ingen table)
				break;
				case ServerQueryType.MyPagesGoalValues:
					//Kan inte söka(Är ingen table)
				break;
				case ServerQueryType.ClientSearch:

					fields.Add("companycategory.name");
					fields.Add("country.name");
					fields.Add("customerno");

					fields.Add("deliveryaddress1");
					fields.Add("deliveryaddress2");
					fields.Add("deliverycity");
					fields.Add("deliveryname");
					fields.Add("deliveryzipcode");
					fields.Add("district.name");

					fields.Add("email");

					fields.Add("invoiceaddress1");
					fields.Add("invoicecity");
					fields.Add("invoicezipcode");

					fields.Add("labelno");

					fields.Add("name");

					fields.Add("person.name");
					fields.Add("person.phone");
					fields.Add("person.mobilephone");
					fields.Add("person.email");
					fields.Add("phone");

					fields.Add("registrationno");

					fields.Add("telefax");

					fields.Add("www");

				break;
				case ServerQueryType.ActivityHistory:
					fields.Add("date");
					fields.Add("type");
					fields.Add("company.name");
					fields.Add("person.name");
					fields.Add("note");
					fields.Add("business.businessid");
					fields.Add("coworker.name");
				break;
				case ServerQueryType.ActivityAlarms:
					fields.Add("latesthistory");
					fields.Add("customerno");
					fields.Add("name");
				break;
				case ServerQueryType.OrderOfferHistory:
					fields.Add("orderdate");
					fields.Add("businessid");
					fields.Add("company.name");
					fields.Add("person.name");
					fields.Add("businesstatus");
					fields.Add("marking");
					fields.Add("coworker.name");
				break;
				case ServerQueryType.OrderOfferUrgents:
					fields.Add("orderdate");
					fields.Add("businessid");
					fields.Add("company.name");
					fields.Add("person.name");
					fields.Add("businesstatus");
					fields.Add("marking");
					fields.Add("coworker.name");
				break;
				case ServerQueryType.ArticleCampaigns:
					fields.Add("headline");
					fields.Add("description");
					fields.Add("startdate");
					fields.Add("enddate");
				break;
				case ServerQueryType.ArticleSearch:
					fields.Add("articleno");
					fields.Add("name");
					fields.Add("division.name");
					foreach(var item in availablePriceGroups)
					{
						fields.Add(item);
					}
				break;
				case ServerQueryType.AddArticleDemoed:
					if(withSearch)
					{
						fields.Add("articleno");
						fields.Add("name");
						fields.Add("division.name");
						foreach(var item in availablePriceGroups)
						{
							fields.Add(item);
						}
					}
					else
					{
						fields.Add("article.articleno");
						fields.Add("article.name");
						fields.Add("article.division.name");
						foreach(var item in availablePriceGroups)
						{
							fields.Add("article." + item);
						}
					}
				break;
				case ServerQueryType.AddArticleBusiness:
					if(withSearch)
					{
						fields.Add("articleno");
						fields.Add("name");
						fields.Add("division.name");
						foreach(var item in availablePriceGroups)
						{
							fields.Add(item);
						}
					}
					else
					{
						fields.Add("article.articleno");
						fields.Add("article.name");
						fields.Add("article.division.name");
						foreach(var item in availablePriceGroups)
						{
							fields.Add("article." + item);
						}
					}
				break;
			}
			return fields;
		}

		public static List<string> GetHideFields(ServerQueryType type, bool withSearch = false)
		{
			List<string> fields = new List<string>();

			fields.Add("createduser");
			fields.Add("createdtime");
			fields.Add("updateduser");
			fields.Add("timestamp");
			switch(type)
			{
				case ServerQueryType.ClientDocuments:
					fields.Add("document");
				break;
				case ServerQueryType.AddArticleDemoed:
					fields.Add("history.date");
				break;
				case ServerQueryType.AddArticleBusiness:
					fields.Add("business.orderdate");
				break;
				case ServerQueryType.ClientContacts:
					fields.Add("misc");
				break;
			}

			return fields;
		}

		public static List<string> GetExtraFields(ServerQueryType type, bool withSearch = false)
		{
			List<string> fields = new List<string>();
			switch(type)
			{
				case ServerQueryType.ClientDocuments:
					fields.Add("document__fileextension");
					//fields.Add("document__size");
				break;
			}
			return fields;
		}

		static string getFieldString(ServerQueryType type, bool withSearch = false)
		{

			List<string> fields = GetFields(type, withSearch);
			string fieldString = "";

			if(fields.Count > 0)
			{
				fieldString = "<fields>";
				for(int i = 0; i < fields.Count; i++)
				{
					bool sortThisField = false;

					/*if(type == ServerQueryType.ActivityAlarms)
					{
						if(fields[i] == "latesthistory")
							sortThisField = true;
					}
					else */if(type == ServerQueryType.AddArticleBusiness)
					{
						if(fields[i] == "business.orderdate")
							sortThisField = true;
					}
					else if(type == ServerQueryType.AddArticleDemoed)
					{
						if(fields[i] == "history.date")
							sortThisField = true;
					}
					else if(fields[i] == "timestamp")
					{
						sortThisField = true;
					}

					string sortString = "";
					if(sortThisField)
						sortString = " sortorder=\"desc\" sortindex=\"1\"";

					fieldString += "<field" + sortString + ">" + fields[i] + "</field>";
				}
				fieldString += "</fields>";

			}
			return fieldString;
		}

		public static string GetClickIdName(ServerQueryType queryType)
		{
			string clickIdName = null;

			switch (queryType)
			{
				case ServerQueryType.ClientArticlesBought:
					clickIdName = "article.idarticle";
				break;
				case ServerQueryType.ClientArticlesDemoed:
					clickIdName = "article.idarticle";
				break;
				case ServerQueryType.AddArticleDemoed:
					clickIdName = "article.idarticle";
				break;
				case ServerQueryType.AddArticleBusiness:
					clickIdName = "article.idarticle";
				break;
				case ServerQueryType.ArticleBoughtBy:
					clickIdName = "business.company.idcompany";
				break;
			}

			return clickIdName;
		}
				
	}
}

