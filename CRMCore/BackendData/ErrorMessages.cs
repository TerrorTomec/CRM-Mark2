using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace NavigationCore
{
	public class ErrorMessages
	{
		private static List<ErrorType> currentTypes = new List<ErrorType>();
		public enum ErrorType
		{
			OverlayStuff,
			ServerOverload,
		}

		private class AlertData
		{
			public string Title { get; set; }
			public string Message { get; set; }
		}

		private static AlertData getAlertDate(ErrorType errorType)
		{
			AlertData alertData = null;
			
			switch(errorType)
			{
				case ErrorType.OverlayStuff:
					alertData = new AlertData() { Title = "Kod 1" };
				break;
				case ErrorType.ServerOverload:
					alertData = new AlertData() { Title = "Kod 2", Message = "Serverfel har inträffat, meddela utvecklarna" };
				break;
			}

			if(alertData != null && alertData.Message == null)
				alertData.Message = "Fel har inträffat, meddela utvecklarna";

			return alertData;
		}

		public static void CreateErrorAlert(ErrorType errorType)
		{
			if(!currentTypes.Contains(errorType))
			{
				AlertData alertData = getAlertDate(errorType);

				UIAlertView currentAlert = new UIAlertView(alertData.Title, alertData.Message, null, "Ok");
				currentTypes.Add(errorType);
				currentAlert.Clicked += (sender, e) =>
				{
					currentTypes.Remove(errorType);
				};
				currentAlert.Show();
			}
		}
	}
}

