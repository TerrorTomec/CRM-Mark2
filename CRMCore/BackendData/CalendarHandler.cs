using System;
using MonoTouch.Foundation;
using MonoTouch.EventKit;
using System.Collections.Generic;

namespace NavigationCore
{
	public static class CalendarHandler
	{

		private static DateTime NSDateToDateTime(MonoTouch.Foundation.NSDate sourceDate)
		{
			NSTimeZone sourceTimeZone = new NSTimeZone("UTC");
			NSTimeZone destinationTimeZone = NSTimeZone.LocalTimeZone;

			int sourceGMTOffset = sourceTimeZone.SecondsFromGMT(sourceDate);
			int destinationGMTOffset = destinationTimeZone.SecondsFromGMT(sourceDate);
			int interval = destinationGMTOffset - sourceGMTOffset;

			var destinationDate = sourceDate.AddSeconds(interval);

			var dateTime = new DateTime(2001, 1, 1, 0, 0, 0).AddSeconds(destinationDate.SecondsSinceReferenceDate);
			return dateTime;
		}

		private static NSDate DateTimeToNSDate(DateTime date)
		{
			return NSDate.FromTimeIntervalSinceReferenceDate((date-(new DateTime(2001,1,1,0,0,0))).TotalSeconds);
		}
		/*
		public static void GetEventsInRange(DateTime start, DateTime stop, Func<List<CardTodoData>, bool> onComplete)
		{
			EKEventStore estore = new EKEventStore();

			estore.RequestAccess(EKEntityType.Event, (bool granted, NSError error) => {
				EKEvent[] events = null;
				if(granted)
				{
					NSDate startDate = DateTimeToNSDate(start);
					NSDate stopDate =  DateTimeToNSDate(stop);

					EKCalendar[] calendars = estore.GetCalendars(EKEntityType.Event);
					NSPredicate predicate  = estore.PredicateForEvents(startDate, stopDate, calendars);
					events = estore.EventsMatching(predicate);
				}

				List<CardTodoData> todoData = new List<CardTodoData>();

				if(events != null)
				{
					foreach (var calendarEvent in events)
					{
						if (calendarEvent.AllDay)
							continue;

						todoData.Add(new CardTodoData(){
							StartTime = NSDateToDateTime(calendarEvent.StartDate),
							EndTime = NSDateToDateTime(calendarEvent.EndDate),
							Done = false,
							Note = calendarEvent.Notes,
							Subject = calendarEvent.Title,
							FromServer = false,
						});
					}
				}

				if(onComplete != null)
					onComplete(todoData);

			});
		}
		*/
	}
}

