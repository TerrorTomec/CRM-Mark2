using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace CrmMark2
{
	public partial class FrameworkViewController : UIViewController
	{
		int mainMenuWidth = 50;

		MainMenuCollectionViewController mainMenuViewController;

		public FrameworkViewController() : base("FrameworkViewController", null)
		{
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);


		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			#region Main menu
			var mainLineLayout = new MainMenuFlowLayout () { ScrollDirection = UICollectionViewScrollDirection.Vertical	};

			mainMenuViewController = new MainMenuCollectionViewController(mainLineLayout);
			mainMenuViewController.View.BackgroundColor = new UIColor(0.3f, 0.3f, 0.3f, 1f);
			mainLineLayout.Controller = mainMenuViewController;

			View.Add(mainMenuViewController.View);

			mainMenuViewController.MainController = this;
			mainMenuViewController.View.AutoresizingMask = UIViewAutoresizing.All;
			#endregion

			var items = new List<MainMenuItem>();
			items.Add(new MainMenuItem("Meny 1", ""));
			items.Add(new MainMenuItem("Meny 2", ""));
			items.Add(new MainMenuItem("Meny 3", ""));
			items.Add(new MainMenuItem("Meny 4", ""));
			items.Add(new MainMenuItem("Meny 5", ""));
			mainMenuViewController.SetMainItems(items);
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			#region Set main menu sizes
			var width = View.Frame.Width;
			var height = View.Frame.Height;

			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft ||
				UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight)
				height = width;

			mainMenuViewController.View.Frame = new RectangleF(0, TopLayoutGuide.Length, mainMenuWidth, height - TopLayoutGuide.Length);
			#endregion
		}
	}
}

