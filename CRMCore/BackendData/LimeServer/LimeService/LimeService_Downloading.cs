using System;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace LimeServer
{
	public partial class LimeService //Downloading
	{
		/*
		public void DownloadFiles(string documentId, UIViewController _viewController)
		{
			DownloadFiles(new List<string>() { documentId }, _viewController);
		}
		public void DownloadFiles(List<string> documentIds, UIViewController _viewController)
		{
			List<Condition> searchConditions = new List<Condition>() {
				new Condition()
				{
					ConditionType = Condition.ConditionTypeEnum.Statement,

					LeftValue = documentIds,
					LeftType = Condition.ValueTypeEnum.Numeric,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = "iddocument",
					RightType = Condition.ValueTypeEnum.Field
				}
			};

			var query = LimeHelper.GetTableQuery(ServerQueryType.DownloadFile, searchConditions, null, null);
			var fullQuery = _openEnvelope +
				"<tan:GetXmlQueryData>" +
				"<tan:query>" +
				query +
				"</tan:query>" +
				"</tan:GetXmlQueryData>" +
				_closeEnvelope;

			WebClient webClient = GetNewWebClient(RequestType.GetXMLQueryData);

			webClient.UploadStringCompleted += (object sender, UploadStringCompletedEventArgs e) => {
				_viewController.InvokeOnMainThread(() => {
					try
					{
						List<string> ReceivedFiles = LimeXMLParser.ParseToFile(e.Result, ServerQueryType.DownloadFile);

						if (ReceivedFiles != null)
						{
							if (DownloadFilesCompleted != null)
							{
								DownloadFilesCompleted(this, new DownloadFilesResult(){Success = true, Paths = ReceivedFiles});
							}
						}
						else
						{
							if (DownloadFilesCompleted != null)
							{
								DownloadFilesCompleted(this, new DownloadFilesResult(){Success = false, Paths = null});
							}
						}
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if (DownloadFilesCompleted != null)
						{
							DownloadFilesCompleted(this, new DownloadFilesResult(){Success = false, Paths = null});
						}
					}

				});
			};
			webClient.UploadStringAsync(_URL, fullQuery);
		}

		public void GetContactData(UIViewController _currentViewController, string clientID, Func<List<NavigationCore.ContactData>, bool> onComplete)
		{
			ServerQueryType queryType = ServerQueryType.DataContact;
			var query = LimeHelper.GetTableQuery(queryType, new List<Condition>()
				{
					new Condition()
					{
						LeftValue = new List<string>() { "company.idcompany" },
						LeftType = Condition.ValueTypeEnum.Field,

						Operator = Condition.OperatorEnum.EqualTo,

						RightValue = clientID,
						RightType = Condition.ValueTypeEnum.Numeric
					},
					new Condition()
					{
						LeftValue = new List<string>() { "active" },
						LeftType = Condition.ValueTypeEnum.Field,

						Operator = Condition.OperatorEnum.EqualTo,

						RightValue = "1",
						RightType = Condition.ValueTypeEnum.String
					}
				}
			);
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
						List<NavigationCore.ContactData> contactData = LimeXMLParser.ParseToContactData(e.Result);
						if (onComplete != null)
							onComplete(contactData);
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if (onComplete != null)
							onComplete(null);
					}

				});
			};

			try
			{
				webClient.UploadStringAsync(_URL, fullQuery);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
				if (onComplete != null)
					onComplete(null);
			}
		}

		public void GetCoworkerGoalValues(string coworkerID, UIViewController _currentViewController, Func<List<NavigationCore.CoworkerGoalValueData>, bool> onComplete)
		{
			List<string> coworkerIDs = new List<string>();
			coworkerIDs.Add(coworkerID);
			GetCoworkerGoalValues(coworkerIDs, _currentViewController, onComplete);
		}
		public void GetCoworkerGoalValues(List<string> coworkerIDs, UIViewController _currentViewController, Func<List<NavigationCore.CoworkerGoalValueData>, bool> onComplete)
		{
			ServerQueryType queryType = ServerQueryType.MyPagesGoalValues;

			List<Condition> conditions = new List<Condition>()
			{
				new Condition()
				{
					isOR = false,

					LeftValue = coworkerIDs,
					LeftType = Condition.ValueTypeEnum.Numeric,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = "coworker.idcoworker",
					RightType = Condition.ValueTypeEnum.Field,
				}
			};

			var query = LimeHelper.GetTableQuery(queryType, conditions);
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
						List<NavigationCore.CoworkerGoalValueData> coworkerGoalValues = LimeXMLParser.ParseToCoworkerGoalValues(e.Result);
						if (onComplete != null)
							onComplete(coworkerGoalValues);
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if (onComplete != null)
							onComplete(null);
					}

				});
			};

			try
			{
				webClient.UploadStringAsync(_URL, fullQuery);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
				if (onComplete != null)
					onComplete(null);
			}
		}

		public void GetCoworkerStatistics(string coworkerID, UIViewController _currentViewController, Func<List<NavigationCore.CoworkerStatisticsData>, bool> onComplete)
		{
			List<string> coworkerIDs = new List<string>();
			coworkerIDs.Add(coworkerID);
			GetCoworkerStatistics(coworkerIDs, _currentViewController, onComplete);
		}
		public void GetCoworkerStatistics(List<string> coworkerIDs, UIViewController _currentViewController, Func<List<NavigationCore.CoworkerStatisticsData>, bool> onComplete)
		{
			ServerQueryType queryType = ServerQueryType.MyPagesStatistics;

			List<Condition> conditions = new List<Condition>()
			{
				new Condition()
				{
					isOR = false,

					LeftValue = coworkerIDs,
					LeftType = Condition.ValueTypeEnum.Numeric,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = "idcoworker",
					RightType = Condition.ValueTypeEnum.Field,
				}
			};

			var query = LimeHelper.GetTableQuery(queryType, conditions);
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
						List<NavigationCore.CoworkerStatisticsData> statistics = LimeXMLParser.ParseToCoworkerStatistics(e.Result);
						if (onComplete != null)
							onComplete(statistics);
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if (onComplete != null)
							onComplete(null);
					}

				});
			};

			try
			{
				webClient.UploadStringAsync(_URL, fullQuery);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
				if (onComplete != null)
					onComplete(null);
			}
		}

		public string DownloadTableFromServer(ServerQueryType queryType, List<Condition> searchConditions = null, QueryAttributes queryAttributes = null, string clientId = null, bool withSearch = false)
		{
			string guid = System.Guid.NewGuid().ToString();
			var query = LimeHelper.GetTableQuery(queryType, searchConditions, queryAttributes, clientId, withSearch);
			if(query != null)
			{
				var fullQuery = _openEnvelope +
					"<tan:GetXmlQueryData>" +
					"<tan:query>" +
					query +
					"</tan:query>" +
					"</tan:GetXmlQueryData>" + 
					_closeEnvelope;

				WebClient webClient = GetNewWebClient(RequestType.GetXMLQueryData);
				webClient.UploadStringCompleted += (object sender, UploadStringCompletedEventArgs e) => {
					try
					{
						if(GotTable != null)
						{					
							GotTable(this, new GotTableResult(){QueryID = guid, QueryType = queryType, Response = e.Result, Success = true });
						}
					}
					catch
					{
						if(GotTable != null)
						{
							GotTable(this, new GotTableResult(){QueryID = guid, QueryType = queryType, Response = null, Success = false });
						}
					}
				};
				try
				{
					webClient.UploadStringAsync(_URL, fullQuery);
				}
				catch(Exception ex)
				{
					new UIAlertView("Error", ex.Message, null, "Ok").Show();
					if(GotTable != null)
					{
						GotTable(this, new GotTableResult(){QueryID = guid, QueryType = queryType, Response = null, Success = false });
					}
				}
			}
			else
			{
				if(GotTable != null)
				{
					GotTable(this, new GotTableResult(){QueryID = guid, QueryType = queryType, Response = null, Success = false });
				}
			}
			return guid;
		}

		#region Download Cards and data for them
		public void DownloadCardCampaignData(string campaignID, UIViewController _currentViewController)
		{
			ServerQueryType type = ServerQueryType.CardCampaignData;
			List<Condition> conditions = new List<Condition>()
			{
				new Condition()
				{
					LeftValue = new List<string>() { "idarticlecampaign" },
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = campaignID,
					RightType = Condition.ValueTypeEnum.Numeric
				},
			};
			this.DownloadTableFromServer(type, conditions, new QueryAttributes() { Top = 1 });

			bool first = true;

			this.GotTable += (sender, Result) => {
				if(!first) return;
				first = false;


				_currentViewController.InvokeOnMainThread( () => {
					NavigationCore.CardCampaignData campaign = LimeXMLParser.ParseToCardCampaignData(Result.Response);
					if(campaign != null)
					{
						DownloadCampaginRows(campaign, _currentViewController, delegate(NavigationCore.CardCampaignData campaignData) {
							if(GotCardCampaignData != null)
							{
								GotCardCampaignData(this, new CardCampaignDataResult(){ Campaign = campaignData, Success = campaignData != null});
							}
							return true;
						});
					}
					else
					{
						AlertServerError();
						if(GotCardCampaignData != null)
						{
							GotCardCampaignData(this, new CardCampaignDataResult(){ Campaign = null, Success = false});
						}
					}
				});
			};

		}
		void DownloadCampaginRows(NavigationCore.CardCampaignData campaign, UIViewController _currentViewController, Func<NavigationCore.CardCampaignData, bool> onComplete)
		{
			ServerQueryType type = ServerQueryType.CampaignRows;
			List<Condition> conditions = new List<Condition>()
			{
				new Condition()
				{
					LeftValue = new List<string>() { "articlecampaign.idarticlecampaign" },
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = campaign.ID,
					RightType = Condition.ValueTypeEnum.Numeric
				},
			};

			this.DownloadTableFromServer(type, conditions);
			this.GotTable += (sender, Result) => {
				_currentViewController.InvokeOnMainThread( () => {
					try
					{
						List<NavigationCore.CampaignRow> campaginRows = LimeXMLParser.ParseToCampaignRows(Result.Response);
						campaign.CampaignRows = campaginRows;
						if(onComplete != null)
							onComplete(campaign);
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(onComplete != null)
							onComplete(campaign);
					}
				});
			};
		}

		public void DownloadCardBusinessData(string businesssId, UIViewController _currentViewController)
		{
			ServerQueryType type = ServerQueryType.CardBusinessData;
			List<Condition> conditions = new List<Condition>()
			{
				new Condition()
				{
					LeftValue = new List<string>() { "idbusiness" },
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = businesssId,
					RightType = Condition.ValueTypeEnum.Numeric
				},
			};
			bool once = true;
			this.DownloadTableFromServer(type, conditions, new QueryAttributes() { Top = 1 });
			this.GotTable += (sender, Result) => {
				if(!once) return;

				once = false;

				_currentViewController.InvokeOnMainThread( () => {
					try
					{
						NavigationCore.CardBusinessData business = LimeXMLParser.ParseToCardBusinessData(Result.Response);
						if(business != null)
						{
							DownloadBusinessRows(business, _currentViewController, delegate(NavigationCore.CardBusinessData businessFilled) {
								try
								{
									if(GotCardBusinessData != null)
									{
										GotCardBusinessData(this, new CardBusinessDataResult(){ Business = businessFilled, Success = true});
									}
								}
								catch(Exception ex)
								{
									Console.WriteLine(ex.StackTrace);
									if(GotCardBusinessData != null)
									{
										GotCardBusinessData(this, new CardBusinessDataResult(){ Business = null, Success = false});
									}
								}

								return true;
							});
						}
						else
						{
							AlertServerError();
							if(GotCardBusinessData != null)
							{
								GotCardBusinessData(this, new CardBusinessDataResult(){ Business = null, Success = false});
							}
						}
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(GotCardBusinessData != null)
						{
							GotCardBusinessData(this, new CardBusinessDataResult(){ Business = null, Success = false});
						}
					}
				});
			};

		}
		void DownloadBusinessRows(NavigationCore.CardBusinessData business, UIViewController _currentViewController, Func<NavigationCore.CardBusinessData, bool> onComplete)
		{
			ServerQueryType type = ServerQueryType.BusinessRows;
			List<Condition> conditions = new List<Condition>()
			{
				new Condition()
				{
					LeftValue = new List<string>() { "business.idbusiness" },
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = business.ID,
					RightType = Condition.ValueTypeEnum.Numeric
				},
			};

			this.DownloadTableFromServer(type, conditions);
			this.GotTable += (sender, Result) => {
				_currentViewController.InvokeOnMainThread( () => {
					try
					{
						List<NavigationCore.BusinessRow> businessRows = LimeXMLParser.ParseToBusinessRows(Result.Response);
						business.OrderRows = businessRows;
						if(onComplete != null)
							onComplete(business);
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(onComplete != null)
							onComplete(business);
					}
				});
			};
		}

		public void DownloadCardArticleData(string articleID, UIViewController _currentViewController)
		{
			ServerQueryType type = ServerQueryType.CardArticleData;
			List<Condition> conditions = new List<Condition>()
			{
				new Condition()
				{
					LeftValue = new List<string>() { "idarticle" },
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = articleID,
					RightType = Condition.ValueTypeEnum.Numeric
				},
			};
			bool first = true;
			this.DownloadTableFromServer(type, conditions, new QueryAttributes() { Top = 1 });
			this.GotTable += (sender, Result) => {
				if(!first) return;

				first = false;

				_currentViewController.InvokeOnMainThread( () => {
					try
					{
						NavigationCore.CardArticleData article = LimeXMLParser.ParseToCardArticleData(Result.Response);

						DownloadArticleResources(article, _currentViewController, delegate(CardArticleData articleData)
							{
								if(GotCardArticleData != null)
								{
									GotCardArticleData(this, new CardArticleDataResult(){ Article = articleData, Success = articleData != null});
								}
								return true;
							});

					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(GotCardArticleData != null)
						{
							GotCardArticleData(this, new CardArticleDataResult(){ Article = null, Success = false });
						}
					}
				});
			};

		}
		void DownloadArticleResources(NavigationCore.CardArticleData article, UIViewController _currentViewController, Func<NavigationCore.CardArticleData, bool> onComplete)
		{
			ServerQueryType type = ServerQueryType.ArticleResource;
			List<Condition> conditions = new List<Condition>()
			{
				new Condition()
				{
					LeftValue = new List<string>() { "article.idarticle" },
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = article.ID,
					RightType = Condition.ValueTypeEnum.Numeric
				},
			};

			this.DownloadTableFromServer(type, conditions);
			this.GotTable += (sender, Result) => {
				_currentViewController.InvokeOnMainThread( () =>
					{
						try
						{
							List<NavigationCore.ArticleResource> resourceRows = LimeXMLParser.ParseToArticleResources(Result.Response);
							article.Resources = resourceRows;
							if(onComplete != null)
								onComplete(article);
						}
						catch(Exception serverException)
						{
							Console.WriteLine(serverException.StackTrace);
							AlertServerError();
							if(onComplete != null)
								onComplete(article);
						}
					});
			};
		}
		void DownloadArticlesDemoed(NavigationCore.CardActivityData history, UIViewController _currentViewController, Func<NavigationCore.CardActivityData, bool> onComplete)
		{
			ServerQueryType type = ServerQueryType.DemoedArticles;
			List<Condition> conditions = new List<Condition>()
			{
				new Condition()
				{
					LeftValue = new List<string>() { "history.idhistory" },
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = history.ID,
					RightType = Condition.ValueTypeEnum.Numeric
				},
			};

			this.DownloadTableFromServer(type, conditions);
			this.GotTable += (sender, Result) => {
				_currentViewController.InvokeOnMainThread( () => {
					try
					{
						List<NavigationCore.ArticleDemo> demoedArticles = LimeXMLParser.ParseToArticlesDemoed(Result.Response);
						history.DemoedArticles = demoedArticles;
						if(onComplete != null)
							onComplete(history);
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(onComplete != null)
							onComplete(history);
					}
				});
			};
		}
		public string DownloadArticlesBoughtBy(string articleId)
		{
			List<Condition> conditions = new List<Condition>()
			{

				new Condition()
				{
					isOR = false,
					ConditionType = Condition.ConditionTypeEnum.Statement,

					LeftType = Condition.ValueTypeEnum.String,
					LeftValue = LimeHelper.GetDistrictCodes(),

					Operator = Condition.OperatorEnum.EqualTo,

					RightType = Condition.ValueTypeEnum.Field,
					RightValue = "business.company.district.code",
				},
				new Condition()
				{
					isOR = false,
					ConditionType = Condition.ConditionTypeEnum.LeftParenthesis
				},
				new Condition()
				{
					isOR = false,

					LeftValue = new List<string>(){"article.idarticle"},
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = articleId,
					RightType = Condition.ValueTypeEnum.Numeric,
				},
				new Condition()
				{
					isOR = false,

					LeftValue = new List<string>(){"selected"},
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = "233001",
					RightType = Condition.ValueTypeEnum.Numeric,
				},
				new Condition()
				{
					isOR = false,

					LeftValue = new List<string>(){"business.businesstatus"},
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = "15001",
					RightType = Condition.ValueTypeEnum.Numeric,
				},
				new Condition()
				{
					isOR = false,
					ConditionType = Condition.ConditionTypeEnum.RightParenthesis,
				},
			};

			return DownloadTableFromServer(ServerQueryType.ArticleBoughtBy, conditions);
		}

		public void DownloadCardActivityData(string activityID, UIViewController _currentViewController)
		{
			ServerQueryType type = ServerQueryType.CardActivityData;
			List<Condition> conditions = new List<Condition>()
			{
				new Condition()
				{
					LeftValue = new List<string>() { "idhistory" },
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = activityID,
					RightType = Condition.ValueTypeEnum.Numeric
				},
			};
			this.DownloadTableFromServer(type, conditions, new QueryAttributes() { Top = 1 });

			bool once = true;

			this.GotTable += (sender, Result) => {
				if(!once) return;
				once = false;


				_currentViewController.InvokeOnMainThread( () => {
					NavigationCore.CardActivityData activity = LimeXMLParser.ParseToCardActivityData(Result.Response);
					if(activity != null)
					{
						DownloadArticlesDemoed(activity, _currentViewController, delegate(NavigationCore.CardActivityData activityData) {
							if(GotCardActivityData != null)
							{
								GotCardActivityData(this, new CardActivityDataResult(){ Activity = activityData, Success = activityData != null});
							}
							return true;
						});
					}
					else
					{
						AlertServerError();
						if(GotCardActivityData != null)
						{
							GotCardActivityData(this, new CardActivityDataResult(){ Activity = null, Success = false});
						}
					}
				});
			};
		}

		public void DownloadCardTodoData(string TodoID, UIViewController _currentViewController)
		{
			ServerQueryType type = ServerQueryType.CardTodoData;
			List<Condition> conditions = new List<Condition>()
			{
				new Condition()
				{
					LeftValue = new List<string>() { "idtodo" },
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = TodoID,
					RightType = Condition.ValueTypeEnum.Numeric
				},
			};
			this.DownloadTableFromServer(type, conditions, new QueryAttributes() { Top = 1 });
			this.GotTable += (sender, Result) => {
				_currentViewController.InvokeOnMainThread( () => {
					try
					{
						NavigationCore.CardTodoData todo = LimeXMLParser.ParseToCardTodoData(Result.Response);

						if(GotCardTodoData != null)
						{
							GotCardTodoData(this, new CardTodoDataResult(){ Todo = todo, Success = (todo != null) });
						}

					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(GotCardTodoData != null)
						{
							GotCardTodoData(this, new CardTodoDataResult(){ Todo = null, Success = false });
						}
					}
				});
			};

		}

		public void DownloadCardContactData(string contactID, UIViewController _currentViewController)
		{
			ServerQueryType type = ServerQueryType.CardContactData;
			List<Condition> conditions = new List<Condition>()
			{
				new Condition()
				{
					LeftValue = new List<string>() { "idperson" },
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = contactID,
					RightType = Condition.ValueTypeEnum.Numeric
				},
			};
			this.DownloadTableFromServer(type, conditions, new QueryAttributes() { Top = 1 });
			this.GotTable += (sender, Result) => {
				_currentViewController.InvokeOnMainThread( () => {
					try
					{
						NavigationCore.CardContactData contact = LimeXMLParser.ParseToCardContactData(Result.Response);

						if(GotCardContactData != null)
						{
							GotCardContactData(this, new CardContactDataResult(){ Contact = contact, Success = (contact != null) });
						}

					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(GotCardContactData != null)
						{
							GotCardContactData(this, new CardContactDataResult(){ Contact = null, Success = false });
						}
					}
				});
			};

		}

		public void DownloadCardClientData(string clientId, UIViewController _currentViewController)
		{

			var query = LimeHelper.GetCardClientQuery(clientId);
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
						NavigationCore.CardClientData client = LimeXMLParser.ParseToCardClientData(e.Result, ServerQueryType.CardClientData);
						if(GotCardClientData != null)
						{
							GotCardClientData(this, new CardClientDataResult(){Client = client, Success = true});
						}
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(GotCardClientData != null)
						{
							GotCardClientData(this, new CardClientDataResult(){Client = null, Success = false });
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
				Console.WriteLine(ex.StackTrace);
				if(GotCardClientData != null)
				{
					GotCardClientData(this, new CardClientDataResult(){ Client = null, Success = false });
				}
			}
		}
		#endregion

		#region Download HelpData
		public void GetOrderTypes(UIViewController _currentViewController, Func<List<NavigationCore.OrderTypeData>, bool> onComplete)
		{
			ServerQueryType queryType = ServerQueryType.DataOrderType;
			var query = LimeHelper.GetTableQuery(queryType);
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
						List<NavigationCore.OrderTypeData> orderTypes = LimeXMLParser.ParseToOrderTypeData(e.Result);
						if (onComplete != null)
							onComplete(orderTypes);
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if (onComplete != null)
							onComplete(null);
					}

				});
			};

			try
			{
				webClient.UploadStringAsync(_URL, fullQuery);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
				if (onComplete != null)
					onComplete(null);
			}
		}

		public void GetPaymentTerms(UIViewController _currentViewController, Func<List<NavigationCore.PaymentTermsData>, bool> onComplete)
		{
			ServerQueryType queryType = ServerQueryType.DataPaymentTerms;
			var query = LimeHelper.GetTableQuery(queryType);
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
						List<NavigationCore.PaymentTermsData> paymentTerms = LimeXMLParser.ParseToPaymentTermsData(e.Result);
						if (onComplete != null)
							onComplete(paymentTerms);
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if (onComplete != null)
							onComplete(null);
					}

				});
			};

			try
			{
				webClient.UploadStringAsync(_URL, fullQuery);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
				if (onComplete != null)
					onComplete(null);
			}
		}

		void GetCountries(UIViewController _currentViewController, Func<bool, bool> onComplete)
		{
			ServerQueryType queryType = ServerQueryType.DataCountry;
			var query = LimeHelper.GetTableQuery(queryType);
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
						List<NavigationCore.CountryData> countries = LimeXMLParser.ParseToCountryData(e.Result);
						LimeHelper.Countries = countries;

						MoveCountryToTop("FI");
						MoveCountryToTop("NO");
						MoveCountryToTop("DK");
						MoveCountryToTop("SE");

						if (onComplete != null)
							onComplete(true);
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if (onComplete != null)
							onComplete(false);
					}

				});
			};

			try
			{
				webClient.UploadStringAsync(_URL, fullQuery);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
				if (onComplete != null)
					onComplete(false);
			}
		}

		void GetCompanyCategories(UIViewController _currentViewController, Func<bool, bool> onComplete)
		{
			ServerQueryType queryType = ServerQueryType.DataCompanyCategory;
			var query = LimeHelper.GetTableQuery(queryType);
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
						List<NavigationCore.CompanyCategoryData> companyCategories = LimeXMLParser.ParseToCompanyCategoryData(e.Result);
						LimeHelper.CompanyCategories = companyCategories;
						if(onComplete != null)
						{
							onComplete(true);
						}
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(onComplete != null)
						{
							onComplete(false);
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
				Console.WriteLine(ex.StackTrace);
				if(onComplete != null)
				{
					onComplete(false);
				}
			}
		}

		void GetTranslationData(UIViewController _currentViewController, Func<bool, bool> onComplete)
		{
			ServerQueryType queryType = ServerQueryType.DataTranslation;
			var query = LimeHelper.GetTableQuery(queryType);
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
						bool success = LimeXMLParser.ParseToSetTranslationData(e.Result);

						if (onComplete != null)
							onComplete(success);
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if (onComplete != null)
							onComplete(false);
					}

				});
			};

			try
			{
				webClient.UploadStringAsync(_URL, fullQuery);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
				if (onComplete != null)
					onComplete(false);
			}
		}

		void GetDistricts(NavigationCore.CoworkerData _coworker, List<NavigationCore.CoworkerData> _subCoworkers, UIViewController _currentViewController)
		{
			List<string> saleIds = new List<string>();
			if(_subCoworkers != null)
			{
				for(int i = 0; i < _subCoworkers.Count; i++)
				{
					saleIds.Add(_subCoworkers[i].Id);
				}
			}
			saleIds.Add(_coworker.Id);

			var query = LimeHelper.GetDistrictQuery(saleIds, _coworker.FullAccess);
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
						List<NavigationCore.DistrictData> districts = LimeXMLParser.ParseToDistricts(e.Result);

						_coworker.Districts = districts;

						if (_coworker.Districts.Count > 0)
						{
							GetCountries(_currentViewController, delegate(bool SuccessCountries)
								{
									if (SuccessCountries)
									{
										GetCompanyCategories(_currentViewController, delegate(bool SuccessCategories)
											{
												if (SuccessCategories)
												{
													//GetTranslationData(_currentViewController, delegate(bool SuccessTranslation)
													{
														//if(SuccessTranslation)
														{
															if(LoginDone != null)
															{
																LoginDone(this, new LoginResult(){Coworker = _coworker, Success = true});
															}
														}
//														else
//												{
//													if(LoginDone != null)
//													{
//														LoginDone(this, new LoginResult(){Coworker = null, Message = "Lyckades inte hämta hem översättningar från server", Success = false});
//													}
//												}
//												return true;
													}//);

												}
												else
												{
													if(LoginDone != null)
													{
														LoginDone(this, new LoginResult(){Coworker = null, Message = "Lyckades inte hämta hem företagskategorier från server", Success = false});
													}
												}
												return true;

											});
									}
									else
									{
										if(LoginDone != null)
										{
											LoginDone(this, new LoginResult(){Coworker = null, Message = "Lyckades inte hämta hem länder från server", Success = false});
										}
									}
									return true;
								});
						}
						else
						{
							if(LoginDone != null)
							{
								LoginDone(this, new LoginResult(){Coworker = null, Message = "Du har inga distrikt på din användare", Success = false});
							}
						}
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(LoginDone != null)
						{
							LoginDone(this, new LoginResult(){Coworker = null, Message = "Serverfel.", Success = false});
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

		public void GetDatabaseStructure(UIViewController _currentViewController)
		{

			var fullQuery = _openEnvelope +
				"<tan:GetDataStructure/>" +
				_closeEnvelope;

			WebClient webClient = GetNewWebClient(RequestType.GetDataStructure);
			webClient.UploadStringCompleted += (object sender, UploadStringCompletedEventArgs e) => {
				_currentViewController.InvokeOnMainThread( () => {
					try
					{
						string XML = e.Result;
						LimeHelper.DatabaseData = LimeXMLParser.ParseDataStructure(XML);
						if(LimeHelper.DatabaseData != null)
						{
							if (DataStructureCompleted != null)
								DataStructureCompleted(this, true);
						}
						else
						{
							if (DataStructureCompleted != null)
								DataStructureCompleted(this, false);
						}
					}
					catch
					{
						if (DataStructureCompleted != null)
							DataStructureCompleted(this, false);
					}

				});

			};

			try
			{
				webClient.UploadStringAsync(_URL, fullQuery);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
				if (DataStructureCompleted != null)
					DataStructureCompleted(this, false);
			}
		}
		#endregion

		*/

	}
}

