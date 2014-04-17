using System;
using LimeServer;
using System.Collections.Generic;

namespace LimeServer
{
	public partial class LimeService //Events
	{
		/*
		#region EventArgumentsdata
		public struct LoginResult
		{
			public string Message { get; set; }
			public bool Success { get; set; }
			public NavigationCore.CoworkerData Coworker { get; set; }
		}

		public struct CardClientDataResult
		{
			public bool Success { get; set; }
			public NavigationCore.CardClientData Client { get; set; }
		}

		public struct CardBusinessDataResult
		{
			public bool Success { get; set; }
			public NavigationCore.CardBusinessData Business { get; set; }
		}

		public struct CardContactDataResult
		{
			public bool Success { get; set; }
			public NavigationCore.CardContactData Contact { get; set; }
		}

		public struct CardTodoDataResult
		{
			public bool Success { get; set; }
			public NavigationCore.CardTodoData Todo { get; set; }
		}

		public struct CardActivityDataResult
		{
			public bool Success { get; set; }
			public NavigationCore.CardActivityData Activity { get; set; }
		}

		public struct CardCampaignDataResult
		{
			public bool Success { get; set; }
			public NavigationCore.CardCampaignData Campaign { get; set; }
		}

		public struct CardArticleDataResult
		{
			public bool Success { get; set; }
			public NavigationCore.CardArticleData Article { get; set; }
		}

		public struct DownloadFilesResult
		{
			public List<string> Paths { get; set; }
			public bool Success { get; set; }
		}

		public struct UploadDataResult
		{
			public bool Success { get; set; }
			public string TableName { get; set; }
			public string TransactionId { get; set; }
			public bool IsCreated { get; set; }
			public bool IsEdited { get; set; }
			public string NewId { get; set; }

			public static UploadDataResult ErrorResult()
			{
				return new UploadDataResult() {
					IsCreated = false,
					IsEdited = false,
					NewId = null,
					Success = false,
					TableName = null,
					TransactionId = null,
				};
			}
		}

		public struct GotTableResult
		{
			public string QueryID { get; set; }
			public bool Success { get; set; }
			public ServerQueryType QueryType { get; set; }
			public string Response { get; set; }
		}
		#endregion

		#region EventDelegates
		public delegate void GotTableEvent(object sender,GotTableResult Result);

		public delegate void LoginEvent(object sender,LoginResult Result);

		public delegate void GotCardClientDataEvent(object sender,CardClientDataResult Result);

		public delegate void GotCardBusinessDataEvent(object sender,CardBusinessDataResult Result);

		public delegate void GotCardContactDataEvent(object sender,CardContactDataResult Result);

		public delegate void GotCardTodoDataEvent(object sender,CardTodoDataResult Result);

		public delegate void GotCardArticleDataEvent(object sender,CardArticleDataResult Result);

		public delegate void GotCardActivityDataEvent(object sender,CardActivityDataResult Result);

		public delegate void GotCardCampaignDataEvent(object sender,CardCampaignDataResult Result);

		public delegate void UploadFileComplete(object sender,UploadDataResult Result);

		public delegate void DownloadFilesComplete(object sender,DownloadFilesResult Result);

		public delegate void UploadDataComplete(object sender,UploadDataResult Result);

		public delegate void DataStructureComplete(object sender,bool Success);
		#endregion

		#region Events
		public event GotTableEvent GotTable;
		public event LoginEvent LoginDone;
		public event GotCardClientDataEvent GotCardClientData;
		public event GotCardContactDataEvent GotCardContactData;
		public event GotCardTodoDataEvent GotCardTodoData;
		public event GotCardActivityDataEvent GotCardActivityData;
		public event GotCardCampaignDataEvent GotCardCampaignData;
		public event GotCardArticleDataEvent GotCardArticleData;
		public event GotCardBusinessDataEvent GotCardBusinessData;
		public event UploadFileComplete UploadFileCompleted;
		public event DownloadFilesComplete DownloadFilesCompleted;
		public event UploadDataComplete UploadDataCompleted;
		public event DataStructureComplete DataStructureCompleted;
		#endregion
		*/
	}
}

