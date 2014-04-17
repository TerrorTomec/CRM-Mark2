using System;
using NavigationCore;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace CRMCore
{
	public static class NotificationHandler
	{
		/*
		public class NotificationTimeSpan
		{
			public TimeSpan NotificationSpan;
			public bool HaveNotification;
		}

		static Dictionary<string, NotificationTimeSpan> notificationTimes = new Dictionary<string, NotificationTimeSpan>();

		public static Dictionary<string, NotificationTimeSpan> GetDictionary()
		{
			#region Setting up the dictionary
			if(notificationTimes.Count == 0)
			{
				notificationTimes.Add("0", new NotificationTimeSpan()
				{
					HaveNotification = false,
					NotificationSpan = new TimeSpan(0, 0, 0)
				});
				notificationTimes.Add("1", new NotificationTimeSpan()
				{
					HaveNotification = true,
					NotificationSpan = new TimeSpan(0, 0, 0)
				});
				notificationTimes.Add("2", new NotificationTimeSpan()
				{
					HaveNotification = true,
					NotificationSpan = new TimeSpan(0, 5, 0)
				});
				notificationTimes.Add("3", new NotificationTimeSpan()
				{
					HaveNotification = true,
					NotificationSpan = new TimeSpan(0, 15, 0)
				});
				notificationTimes.Add("4", new NotificationTimeSpan()
				{
					HaveNotification = true,
					NotificationSpan = new TimeSpan(0, 30, 0)
				});
				notificationTimes.Add("5", new NotificationTimeSpan()
				{
					HaveNotification = true,
					NotificationSpan = new TimeSpan(1, 0, 0)
				});
				notificationTimes.Add("6", new NotificationTimeSpan()
				{
					HaveNotification = true,
					NotificationSpan = new TimeSpan(2, 0, 0)
				});
				notificationTimes.Add("7", new NotificationTimeSpan()
				{
					HaveNotification = true,
					NotificationSpan = new TimeSpan(1, 0, 0, 0)
				});
				notificationTimes.Add("8", new NotificationTimeSpan()
				{
					HaveNotification = true,
					NotificationSpan = new TimeSpan(2, 0, 0, 0)
				});
				notificationTimes.Add("9", new NotificationTimeSpan()
				{
					HaveNotification = true,
					NotificationSpan = new TimeSpan(7, 0, 0, 0)
				});
			}
			#endregion
			return notificationTimes;
		}

		public static void SetNotification(CardTodoData todoData, string index)
		{
			var timeSpanData = GetDictionary()[index];

			CancelNotification(todoData);
		
			if(!timeSpanData.HaveNotification)
				return;

			var notificationTime = todoData.StartTime - timeSpanData.NotificationSpan;

			UILocalNotification notification = new UILocalNotification();

			var dictionary = new NSMutableDictionary();
			dictionary.Add(new NSString("ID"), new NSString(todoData.ID));
			dictionary.Add(new NSString("TimeID"), new NSString(index));

			notification.UserInfo = dictionary;

			notification.FireDate = notificationTime;
			notification.AlertAction = todoData.CompanyName;
			notification.AlertBody = todoData.Note;

			UIApplication.SharedApplication.ScheduleLocalNotification(notification);
		}

		public static void CancelNotification(CardTodoData todoData)
		{
			var notifications = UIApplication.SharedApplication.ScheduledLocalNotifications;

			foreach(UILocalNotification notification in notifications)
			{
				if(notification.UserInfo.ContainsKey(new NSString("ID")))
					if(notification.UserInfo.ValueForKey(new NSString("ID")).ToString() == todoData.ID)
						UIApplication.SharedApplication.CancelLocalNotification(notification);
			}
		}

		public static UILocalNotification GetNotificationFromTodo(CardTodoData todoData)
		{
			var notifications = UIApplication.SharedApplication.ScheduledLocalNotifications;

			foreach(UILocalNotification notification in notifications)
			{
				if(notification.UserInfo.ContainsKey(new NSString("ID")))
					if(notification.UserInfo.ValueForKey(new NSString("ID")).ToString() == todoData.ID)
						return notification;
			}
			return null;
		}

		public static string GetIndexFromTodo(CardTodoData todoData)
		{
			string returnValue = null;
			var notification = GetNotificationFromTodo(todoData);
			if(notification != null && notification.UserInfo.ContainsKey(new NSString("TimeID")))
				returnValue = notification.UserInfo.ValueForKey(new NSString("TimeID")).ToString();

			if(returnValue == null)
				returnValue = "0";

			return returnValue;
		}
		*/
	}
}

