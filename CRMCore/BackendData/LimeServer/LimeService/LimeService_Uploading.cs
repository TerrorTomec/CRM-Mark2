using System;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace LimeServer
{
	public partial class LimeService //Uploading
	{
		/*
		public void UploadFile(string path, string comment, string clientId, UIViewController _viewController)
		{
			//string documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			//string tmp = Path.Combine (documents, "..", "tmp");
			string wholePath = path;// Path.Combine(tmp, fileName);

			string[] splitted = path.Split('.');
			string fileExtension = null;
			if(splitted.Length > 1)
				fileExtension = splitted[splitted.Length - 1];
			else
				throw new InvalidFileNameException("Filename does not contain a fileextension");


			byte[] content;
			using (FileStream file = new FileStream(wholePath, FileMode.Open, FileAccess.Read))
			{
				content = new byte[file.Length];
				int bytesToRead = (int)file.Length;
				int bytesRead = 0;
				while (bytesToRead > 0)
				{
					int i = file.Read(content, bytesRead, bytesToRead);

					if (0 == i)
						break;

					bytesRead += i;
					bytesToRead -= i;
				}
			}

			string contentOfFile = Convert.ToBase64String(content);




			var data = LimeHelper.GetFileUploadQuery(contentOfFile, fileExtension, comment, clientId, LimeHelper.LoggedInCoworker.Id);

			var fullQuery = _openEnvelope +
				"<tan:UpdateData>" +
				"<tan:data>" +
				"<![CDATA[" +
				"<records>" +
				data + 
				"</records>" +
				"]]>" +
				"</tan:data>" +
				"</tan:UpdateData>" + 
				_closeEnvelope;

			WebClient webClient = GetNewWebClient(RequestType.UpdateData);

			webClient.UploadStringCompleted += (object sender, UploadStringCompletedEventArgs e) => {
				_viewController.InvokeOnMainThread( () => {
					try
					{
						UploadDataResult resultData = LimeXMLParser.ParseToUploadDataResult(e.Result, ServerQueryType.UploadFile);
						if(UploadFileCompleted != null)
						{
							UploadFileCompleted(this, resultData);
						}
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(UploadFileCompleted != null)
						{
							UploadFileCompleted(this, UploadDataResult.ErrorResult());
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
				if(UploadFileCompleted != null)
				{
					UploadFileCompleted(this, UploadDataResult.ErrorResult());
				}
			}

		}

		public void UpdateTodo(CardTodoData todoData, UIViewController _viewController)
		{
			UploadTodo(todoData.ID, todoData, _viewController);
		}
		public void CreateTodo(CardTodoData todoData, UIViewController _viewController)
		{
			UploadTodo(null, todoData, _viewController);
		}
		void UploadTodo(string todoId, CardTodoData todoData, UIViewController _viewController)
		{
			if(todoId == null)
			{
				todoId = "-1";
			}

			if(!(int.Parse(todoId) < 0))
			{
				todoData.CoworkerID = null;
			}


			var data = LimeHelper.GetTodoData(todoData);

			var fullQuery = _openEnvelope +
				"<tan:UpdateData>" +
				"<tan:data>" +
				"<![CDATA[" +
				"<data>" +
				data + 
				"</data>" +
				"]]>" +
				"</tan:data>" +
				"</tan:UpdateData>" + 
				_closeEnvelope;

			WebClient webClient = GetNewWebClient(RequestType.UpdateData);
			webClient.UploadStringCompleted += (object sender, UploadStringCompletedEventArgs e) => {
				_viewController.InvokeOnMainThread(() => {
					try
					{
						UploadDataResult resultData = LimeXMLParser.ParseToUploadDataResult(e.Result, ServerQueryType.UploadTodoData);
						if(UploadDataCompleted != null)
						{
							UploadDataCompleted(this, resultData);
						}
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(UploadDataCompleted != null)
						{
							UploadDataCompleted(this, UploadDataResult.ErrorResult());
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
				if(UploadDataCompleted != null)
				{
					UploadDataCompleted(this, UploadDataResult.ErrorResult());
				}
			}


		}

		public void CreateColdClient(CardColdClientData coldClientData, UIViewController _viewController)
		{
			UploadColdClient(null, coldClientData, _viewController);
		}
		void UploadColdClient(string coldClientId, CardColdClientData coldClientData, UIViewController _viewController)
		{
			//Om man inte anger ett id så skapas en ny post.
			if(coldClientId == null)
			{
				coldClientId = "-1";
			}
			coldClientData.ID = coldClientId;



			LimeService activityService = new LimeService(LimeHelper.GetServerURL());
			try
			{
				activityService.UploadDataCompleted += (activitySender, activityResult) =>
				{
					if(activityResult.Success)
					{
						coldClientData.ActivityID = activityResult.NewId;
						var data = LimeHelper.GetColdClientData(coldClientData);

						var fullQuery = _openEnvelope +
							"<tan:UpdateData>" +
							"<tan:data>" +
							"<![CDATA[" +
							"<data>" +
							data + 
							"</data>" +
							"]]>" +
							"</tan:data>" +
							"</tan:UpdateData>" + 
							_closeEnvelope;


						WebClient webClient = GetNewWebClient(RequestType.UpdateData);
						webClient.UploadStringCompleted += (object ColdClientSender, UploadStringCompletedEventArgs e) => {
							_viewController.InvokeOnMainThread(() => {
								try
								{
									UploadDataResult resultData = LimeXMLParser.ParseToUploadDataResult(e.Result, ServerQueryType.UploadColdClient);
									if(resultData.Success)
									{
										if(UploadDataCompleted != null)
										{
											UploadDataCompleted(this, resultData);
										}
									}
									else
									{
										if(UploadDataCompleted != null)
										{
											UploadDataCompleted(this, UploadDataResult.ErrorResult());
										}
									}
								}
								catch(Exception serverException)
								{
									Console.WriteLine(serverException.StackTrace);
									AlertServerError();
									if(UploadDataCompleted != null)
									{
										UploadDataCompleted(this, UploadDataResult.ErrorResult());
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
							if(UploadDataCompleted != null)
							{
								UploadDataCompleted(this, UploadDataResult.ErrorResult());
							}
						}
					}
					else
					{

					}
				};
				activityService.CreateActivity(new CardActivityData()
					{
						CompanyID = null,
						CoworkerID = LimeHelper.LoggedInCoworker.Id,
						PersonID = null,
					}, _viewController);
			}
			catch(Exception serverException)
			{
				Console.WriteLine(serverException.StackTrace);
				if(UploadDataCompleted != null)
					UploadDataCompleted(this, UploadDataResult.ErrorResult());
			}
		}

		public void UpdateActivity(CardActivityData activityData, UIViewController _viewController)
		{
			UploadActivity(activityData.ID, activityData, _viewController);
		}
		public void CreateActivity(CardActivityData activityData, UIViewController _viewController)
		{
			UploadActivity(null, activityData, _viewController);
		}
		void UploadActivity(string activityId, CardActivityData activityData, UIViewController _viewController)
		{
			//Om man inte anger ett id så skapas en ny post.
			if(activityId == null)
			{
				activityId = "-1";
			}
			activityData.ID = activityId;

			if(!(int.Parse(activityId) < 0))
				activityData.CoworkerID = null;


			var data = LimeHelper.GetActivityData(activityData);

			var fullQuery = _openEnvelope +
				"<tan:UpdateData>" +
				"<tan:data>" +
				"<![CDATA[" +
				"<data>" +
				data + 
				"</data>" +
				"]]>" +
				"</tan:data>" +
				"</tan:UpdateData>" + 
				_closeEnvelope;

			WebClient webClient = GetNewWebClient(RequestType.UpdateData);
			webClient.UploadStringCompleted += (object sender, UploadStringCompletedEventArgs e) => {
				_viewController.InvokeOnMainThread(() => {
					try
					{
						UploadDataResult resultData = LimeXMLParser.ParseToUploadDataResult(e.Result, ServerQueryType.UploadActivity);
						if(resultData.Success)
						{
							if (activityData.DemoedArticles != null && activityData.DemoedArticles.Count > 0)
							{
								if(resultData.IsCreated)
								{
									CreateDemoedArticles(activityId, activityData.DemoedArticles, _viewController);
								}
								else if(resultData.IsEdited)
								{
									UpdateDemoedArticles(activityId, activityData.DemoedArticles, _viewController);
								}
							}
							else
							{
								if(UploadDataCompleted != null)
								{
									UploadDataCompleted(this, resultData);
								}
							}
						}
						else
						{
							if(UploadDataCompleted != null)
							{
								UploadDataCompleted(this, UploadDataResult.ErrorResult());
							}
						}
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(UploadDataCompleted != null)
						{
							UploadDataCompleted(this, UploadDataResult.ErrorResult());
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
				if(UploadDataCompleted != null)
				{
					UploadDataCompleted(this, UploadDataResult.ErrorResult());
				}
			}


		}

		public void UpdateCompany(NavigationCore.CardClientData clientCardData, UIViewController _viewController)
		{
			clientCardData.FillVariables();
			UploadCompany(clientCardData.CompanyId, clientCardData, _viewController);
		}
		public void CreateCompany(NavigationCore.CardClientData clientCardData, UIViewController _viewController)
		{
			clientCardData.FillVariables();
			UploadCompany(null, clientCardData, _viewController);
		}
		void UploadCompany(string companyId, NavigationCore.CardClientData clientCardData, UIViewController _viewController)
		{
			//Om man inte anger ett id så skapas en ny post.
			if(companyId == null)
			{
				companyId = "-1";
			}


			var data = LimeHelper.GetCompanyData(companyId, clientCardData);

			var fullQuery = _openEnvelope +
				"<tan:UpdateData>" +
				"<tan:data>" +
				"<![CDATA[" +
				"<data>" +
				data + 
				"</data>" +
				"]]>" +
				"</tan:data>" +
				"</tan:UpdateData>" + 
				_closeEnvelope;

			WebClient webClient = GetNewWebClient(RequestType.UpdateData);
			webClient.UploadStringCompleted += (object sender, UploadStringCompletedEventArgs e) => {
				_viewController.InvokeOnMainThread(() => {
					try
					{
						UploadDataResult resultData = LimeXMLParser.ParseToUploadDataResult(e.Result, ServerQueryType.UploadCompany);
						if(UploadDataCompleted != null)
						{
							UploadDataCompleted(this, resultData);
						}
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(UploadDataCompleted != null)
						{
							UploadDataCompleted(this, UploadDataResult.ErrorResult());
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
				if(UploadDataCompleted != null)
				{
					UploadDataCompleted(this, UploadDataResult.ErrorResult());
				}
			}
		}

		void UpdateDemoedArticles(string historyID, List<NavigationCore.ArticleDemo> demoedArticles, UIViewController _viewController)
		{
//			for(int i = 0; i < demoedArticles.Count; i++)
//			{
//				if(!(int.Parse(demoedArticles[i].ID) > 0))
//					demoedArticles[i].ID = (-(i+1)).ToString(); // får inte vara samma id.
//			}
			UploadDemoedArticles(historyID, demoedArticles, _viewController);
		}
		void CreateDemoedArticles(string historyID, List<NavigationCore.ArticleDemo> demoedArticles, UIViewController _viewController)
		{
			for(int i = 0; i < demoedArticles.Count; i++)
			{
				demoedArticles[i].ID = (-(i+1)).ToString(); // får inte vara samma id.
			}
			UploadDemoedArticles(historyID, demoedArticles, _viewController);
		}
		void UploadDemoedArticles(string historyID, List<NavigationCore.ArticleDemo> demoedArticles, UIViewController _viewController)
		{
			for(int i = 0; i < demoedArticles.Count; i++)
			{
				demoedArticles[i].HistoryID = historyID;
			}

			var data = LimeHelper.GetArticleDemoedData(demoedArticles);

			var fullQuery = _openEnvelope +
				"<tan:UpdateData>" +
				"<tan:data>" +
				"<![CDATA[" +
				"<data>" +
				data + 
				"</data>" +
				"]]>" +
				"</tan:data>" +
				"</tan:UpdateData>" + 
				_closeEnvelope;

			WebClient webClient = GetNewWebClient(RequestType.UpdateData);
			webClient.UploadStringCompleted += (object sender, UploadStringCompletedEventArgs e) => {
				_viewController.InvokeOnMainThread(() => {
					try
					{
						UploadDataResult resultData = LimeXMLParser.ParseToUploadDataResult(e.Result, ServerQueryType.UploadDemoedArticles);

						if(resultData.Success)
						{
							if(UploadDataCompleted != null)
							{
								UploadDataCompleted(this, resultData);
							}
						}
						else
						{
							if(UploadDataCompleted != null)
							{
								UploadDataCompleted(this, UploadDataResult.ErrorResult());
							}
						}

					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(UploadDataCompleted != null)
						{
							UploadDataCompleted(this, UploadDataResult.ErrorResult());
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
				if(UploadDataCompleted != null)
				{
					UploadDataCompleted(this, UploadDataResult.ErrorResult());
				}
			}
		}

		void UpdateOrderRows(string businessID, List<NavigationCore.BusinessRow> businessRowsData, UIViewController _viewController, Func<UploadDataResult, bool> onComplete)
		{
			UploadOrderRows(businessID, businessRowsData, _viewController, onComplete);
		}
		void CreateOrderRows(string businessID, List<NavigationCore.BusinessRow> businessRowsData, UIViewController _viewController, Func<UploadDataResult, bool> onComplete)
		{
			for(int i = 0; i < businessRowsData.Count; i++)
			{
				businessRowsData[i].ID = (-(i+1)).ToString(); // får inte vara samma id.
			}
			UploadOrderRows(businessID, businessRowsData, _viewController, onComplete);
		}
		void UploadOrderRows(string businessID, List<NavigationCore.BusinessRow> businessRowsData, UIViewController _viewController, Func<UploadDataResult, bool> onComplete)
		{
			for(int i = 0; i < businessRowsData.Count; i++)
			{
				businessRowsData[i].BusinessID = businessID;
			}

			var data = LimeHelper.GetOrderRowsData(businessRowsData);

			var fullQuery = _openEnvelope +
				"<tan:UpdateData>" +
				"<tan:data>" +
				"<![CDATA[" +
				"<data>" +
				data + 
				"</data>" +
				"]]>" +
				"</tan:data>" +
				"</tan:UpdateData>" + 
				_closeEnvelope;

			WebClient webClient = GetNewWebClient(RequestType.UpdateData);
			webClient.UploadStringCompleted += (object sender, UploadStringCompletedEventArgs e) => {
				_viewController.InvokeOnMainThread(() => {
					try
					{
						UploadDataResult resultData = LimeXMLParser.ParseToUploadDataResult(e.Result, ServerQueryType.UploadBusinessRows);

						onComplete(resultData);

					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						onComplete(UploadDataResult.ErrorResult());
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
				onComplete(UploadDataResult.ErrorResult());
			}
		}

		public void UpdateContact(NavigationCore.CardContactData cardContactData, UIViewController _viewController)
		{
			UploadContact(cardContactData.ID, cardContactData, _viewController);
		}
		public void CreateContact(NavigationCore.CardContactData cardContactData, UIViewController _viewController)
		{
			UploadContact(null, cardContactData, _viewController);
		}
		void UploadContact(string contactID, NavigationCore.CardContactData cardContactData, UIViewController _viewController)
		{
			//Om man inte anger ett id så skapas en ny post.
			if(contactID == null)
			{
				contactID = "-1";
			}
			cardContactData.ID = contactID;

			var data = LimeHelper.GetContactData(cardContactData);

			var fullQuery = _openEnvelope +
				"<tan:UpdateData>" +
				"<tan:data>" +
				"<![CDATA[" +
				"<data>" +
				data + 
				"</data>" +
				"]]>" +
				"</tan:data>" +
				"</tan:UpdateData>" + 
				_closeEnvelope;

			WebClient webClient = GetNewWebClient(RequestType.UpdateData);
			webClient.UploadStringCompleted += (object sender, UploadStringCompletedEventArgs e) => {
				_viewController.InvokeOnMainThread(() => {
					try
					{
						UploadDataResult resultData = LimeXMLParser.ParseToUploadDataResult(e.Result, ServerQueryType.UploadContact);
						if(resultData.Success)
						{
							if(UploadDataCompleted != null)
							{
								UploadDataCompleted(this, resultData);
							}
						}
						else
						{
							if(UploadDataCompleted != null)
							{
								UploadDataCompleted(this, UploadDataResult.ErrorResult());
							}
						}
					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(UploadDataCompleted != null)
						{
							UploadDataCompleted(this, UploadDataResult.ErrorResult());
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
				if(UploadDataCompleted != null)
				{
					UploadDataCompleted(this, UploadDataResult.ErrorResult());
				}
			}


		}

		public void UpdateBusiness(NavigationCore.CardBusinessData businessCardData, UIViewController _viewController)
		{
			UploadBusiness(businessCardData.ID, businessCardData, _viewController);
		}
		public void CreateBusiness(NavigationCore.CardBusinessData businessCardData, UIViewController _viewController)
		{
			UploadBusiness(null, businessCardData, _viewController);
		}
		void UploadBusiness(string businessId, NavigationCore.CardBusinessData businessCardData, UIViewController _viewController)
		{
			//Om man inte anger ett id så skapas en ny post.
			if(businessId == null)
			{
				businessId = "-1";
			}


			var data = LimeHelper.GetBusinessData(businessId, businessCardData);

			var fullQuery = _openEnvelope +
				"<tan:UpdateData>" +
				"<tan:data>" +
				"<![CDATA[" +
				"<data>" +
				data + 
				"</data>" +
				"]]>" +
				"</tan:data>" +
				"</tan:UpdateData>" + 
				_closeEnvelope;

			WebClient webClient = GetNewWebClient(RequestType.UpdateData);
			webClient.UploadStringCompleted += (object sender, UploadStringCompletedEventArgs e) => {
				_viewController.InvokeOnMainThread(() => {
					try
					{
						UploadDataResult resultData = LimeXMLParser.ParseToUploadDataResult(e.Result, ServerQueryType.UploadBusiness);
						if(resultData.Success)
						{
							if (businessCardData.OrderRows != null && businessCardData.OrderRows.Count > 0)
							{
								if(resultData.IsCreated)
								{
									CreateOrderRows(resultData.NewId, businessCardData.OrderRows, _viewController, (UploadDataResult result) => {
										if(result.Success)
										{
											if(UploadDataCompleted != null)
												UploadDataCompleted(this, resultData);
										}
										else
										{
											if(UploadDataCompleted != null)
												UploadDataCompleted(this, UploadDataResult.ErrorResult());
										}
										return true;
									});
								}
								else if(resultData.IsEdited)
								{
									UpdateOrderRows(businessId, businessCardData.OrderRows, _viewController, (UploadDataResult result) => {
										if(result.Success)
										{
											if(UploadDataCompleted != null)
												UploadDataCompleted(this, resultData);
										}
										else
										{
											if(UploadDataCompleted != null)
												UploadDataCompleted(this, UploadDataResult.ErrorResult());
										}
										return true;
									});
								}
							}
							else
							{
								if(UploadDataCompleted != null)
								{
									UploadDataCompleted(this, resultData);
								}
							}

						}
						else
						{
							if(UploadDataCompleted != null)
							{
								UploadDataCompleted(this, UploadDataResult.ErrorResult());
							}
						}

					}
					catch(Exception serverException)
					{
						Console.WriteLine(serverException.StackTrace);
						AlertServerError();
						if(UploadDataCompleted != null)
						{
							UploadDataCompleted(this, UploadDataResult.ErrorResult());
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
				if(UploadDataCompleted != null)
				{
					UploadDataCompleted(this, UploadDataResult.ErrorResult());
				}
			}


		}
		*/
	}
}

