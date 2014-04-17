using System;

namespace LimeServer
{
	public partial class LimeHelper //Tables
	{
		public static string GetTableName(ServerQueryType _type, bool withSearch = false)
		{
			string tableName = null;
			switch(_type)
			{
				case ServerQueryType.BusinessRows:
					tableName = "businessrow";
				break;
				case ServerQueryType.CampaignRows:
					tableName = "articlecampaignrow";
				break;
				case ServerQueryType.DemoedArticles:
					tableName = "demoarticle";
				break;
				case ServerQueryType.UploadDemoedArticles:
					tableName = "demoarticle";
				break;
				case ServerQueryType.UploadBusinessRows:
					tableName = "businessrow";
				break;
				case ServerQueryType.UploadContact:
					tableName = "person";
				break;
				case ServerQueryType.UploadTodoData:
					tableName = "todo";
				break;
				case ServerQueryType.UploadBusiness:
					tableName = "business";
				break;
				case ServerQueryType.UploadCompany:
					tableName = "company";
				break;
				case ServerQueryType.UploadActivity:
					tableName = "history";
				break;
				case ServerQueryType.UploadColdClient:
					tableName = "coldclient";
				break;
				case ServerQueryType.UploadFile:
					tableName = "document";
				break;
				case ServerQueryType.ArticleBoughtBy:
					tableName = "businessrow";
				break;
				case ServerQueryType.DataContact:
					tableName = "person";
				break;
				case ServerQueryType.DataOrderType:
					tableName = "ordertype";
				break;
				case ServerQueryType.DataPaymentTerms:
					tableName = "paymentterms";
				break;
				case ServerQueryType.DataCountry:
					tableName = "country";
				break;
				case ServerQueryType.DataTranslation:
					tableName = "localize";
				break;
				case ServerQueryType.DataCompanyCategory:
					tableName = "companycategory";
				break;
				case ServerQueryType.CardClientData:
					tableName = "company";
				break;
				case ServerQueryType.MapsRoute:
					tableName = "todo";
				break;
				case ServerQueryType.MapsNearbyClients:
					tableName = "company";
				break;
				case ServerQueryType.DownloadFile:
					tableName = "document";
				break;
				case ServerQueryType.ArticleResource:
					tableName = "resource";
				break;
				case ServerQueryType.ClientToDo:
					tableName = "todo";
				break;
				case ServerQueryType.ClientHistory:
					tableName = "history";
				break;
				case ServerQueryType.ClientOrderOffers:
				case ServerQueryType.CardBusinessData:
					tableName = "business";
				break;
				case ServerQueryType.ClientArticlesBought:
					tableName = "businessrow";
				break;
				case ServerQueryType.ClientArticlesDemoed:
					tableName = "demoarticle";
				break;
				case ServerQueryType.CardArticleData:
					tableName = "article";
				break;
				case ServerQueryType.ClientContacts:
				case ServerQueryType.CardContactData:
					tableName = "person";
				break;
				case ServerQueryType.CardActivityData:
					tableName = "history";
				break;
				case ServerQueryType.ActivityPlanning:
				case ServerQueryType.CardTodoData:
					tableName = "todo";
				break;
				case ServerQueryType.CardCampaignData:
					tableName = "articlecampaign";
				break;
				case ServerQueryType.MyPagesStatistics:
					tableName = "coworker";
				break;
				case ServerQueryType.MyPagesGoalValues:
					tableName = "goalvalue";
				break;
				case ServerQueryType.ClientDocuments:
					tableName = "document";
				break;
				case ServerQueryType.ClientSearch:
					tableName = "company";
				break;
				case ServerQueryType.ActivityHistory:
					tableName = "history";
				break;
				case ServerQueryType.ActivityAlarms:
					tableName = "company";
				break;
				case ServerQueryType.OrderOfferHistory:
					tableName = "business";
				break;
				case ServerQueryType.OrderOfferUrgents:
					tableName = "business";
				break;
				case ServerQueryType.ArticleCampaigns:
					tableName = "articlecampaign";
				break;
				case ServerQueryType.ArticleSearch:
					tableName = "article";
				break;
				case ServerQueryType.AddArticleDemoed:
					if(withSearch)
						tableName = "article";
					else
						tableName = "demoarticle";
				break;
				case ServerQueryType.AddArticleBusiness:
					if(withSearch)
						tableName = "article";
					else
						tableName = "businessrow";
				break;
			}
			return tableName;
		}
	}
}

