using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace CrmMark2
{
	public partial class FrameworkViewController : UIViewController
	{
		int mainMenuWidth = 75;

		MainMenuCollectionViewController mainMenuViewController;
		SubMenuCollectionViewController subMenuViewController;

		UIView contentViewController;
		UILabel contentLabel;

		public FrameworkViewController() : base("FrameworkViewController", null)
		{
		}

		public override UIStatusBarStyle PreferredStatusBarStyle ()
		{
			return UIStatusBarStyle.LightContent;
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//View to cover status bar
			var statusView = new UIView(new RectangleF(0, 0, 1024, 20));
			statusView.BackgroundColor = new UIColor(0.1f, 0.1f, 0.1f, 1f);
			View.Add(statusView);

			#region Main menu
			var mainLineLayout = new MainMenuFlowLayout () { ScrollDirection = UICollectionViewScrollDirection.Vertical	};

			mainMenuViewController = new MainMenuCollectionViewController(mainLineLayout);
			//mainMenuViewController.View.BackgroundColor = new UIColor(0.3f, 0.3f, 0.3f, 1f);
			//mainLineLayout.Controller = mainMenuViewController;

			View.Add(mainMenuViewController.View);

			mainMenuViewController.Framework = this;
			mainMenuViewController.View.AutoresizingMask = UIViewAutoresizing.All;
			#endregion

			#region Sub menu

			var subLineLayout = new SubMenuFlowLayout(0, 0){ ScrollDirection = UICollectionViewScrollDirection.Horizontal};

			subMenuViewController = new SubMenuCollectionViewController(subLineLayout, null);
			//subMenuViewController.View.BackgroundColor = new UIColor(0.3f, 0.3f, 0.3f, 1f);

			View.Add(subMenuViewController.View);

			subMenuViewController.Framework = this;
			subMenuViewController.View.AutoresizingMask = UIViewAutoresizing.All;

			//subMenuViewController.View.BackgroundColor = UIColor.Brown;
			//subMenuViewController.CollectionView.BackgroundColor = UIColor.Green;
			#endregion

			//Test
			var items = new List<MainMenuItem>();
			var sub1 = new List<SubMenuItem>() { new SubMenuItem("Sub 1", ""), new SubMenuItem("Sub 2", ""), new SubMenuItem("Sub A", ""), new SubMenuItem("Sub B", ""), new SubMenuItem("Sub C", ""), new SubMenuItem("Sub D", "") };
			var sub2 = new List<SubMenuItem>() { new SubMenuItem("Sub 3", ""), new SubMenuItem("Sub 4", "") };
			var sub3 = new List<SubMenuItem>() { new SubMenuItem("Sub 5", ""), new SubMenuItem("Sub 6", "") };
			var sub4 = new List<SubMenuItem>() { new SubMenuItem("Sub 7", ""), new SubMenuItem("Sub 8", "") };
			var sub5 = new List<SubMenuItem>() { new SubMenuItem("Sub 9", ""), new SubMenuItem("Sub 10", "") };
			items.Add(new MainMenuItem("Meny 1", "", sub1));
			items.Add(new MainMenuItem("Meny 2", "", sub2));
			items.Add(new MainMenuItem("Meny 3", "", sub3));
			items.Add(new MainMenuItem("Meny 4", "", sub4));
			items.Add(new MainMenuItem("Meny 5", "", sub5));
			mainMenuViewController.SetMainItems(items);

			contentViewController = new UIView();
			View.Add(contentViewController);
			contentLabel = new UILabel();
			contentViewController.Add(contentLabel);
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			#region Set main & sub menu sizes
			var width = View.Frame.Width;
			var height = View.Frame.Height;

			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft ||
				UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight)
			{
				var memory = height;
				height = width;
				width = memory;
			}

			mainMenuViewController.View.Frame = new RectangleF(0, TopLayoutGuide.Length, mainMenuWidth, height - TopLayoutGuide.Length);
			subMenuViewController.View.Frame = new RectangleF(mainMenuWidth, TopLayoutGuide.Length, width - mainMenuWidth, mainMenuWidth);
			#endregion

			//Recalculate distances in submenu
			//var subLineWidth = ;
			//var subLineLayout = new SubMenuFlowLayout(subMenuViewController.GetItemsCount, subLineWidth) { ScrollDirection = UICollectionViewScrollDirection.Horizontal};
			var oldSubLineLayout = subMenuViewController.Layout as SubMenuFlowLayout;
			oldSubLineLayout.UpdateWidth(subMenuViewController.GetItemsCount(null, 0), subMenuViewController.View.Frame.Width);

			//Temporary content test
			contentViewController.Frame = new RectangleF(mainMenuViewController.View.Frame.Width,
				subMenuViewController.View.Frame.Height + TopLayoutGuide.Length, 
				subMenuViewController.View.Frame.Width, 
				mainMenuViewController.View.Frame.Height - subMenuViewController.View.Frame.Height);
			contentViewController.BackgroundColor = new UIColor(0.9f, 0.9f, 1f, 1f);
			contentLabel.Frame = new RectangleF(0, 0, contentViewController.Frame.Width, contentViewController.Frame.Height);
			contentLabel.TextAlignment = UITextAlignment.Center;
			contentLabel.TextColor = UIColor.Black;
			contentLabel.Text = "";
		}

		public void SetupSubmenu(MainMenuItem mainMenuItem)
		{
			//Load the submenu with the mainmenuitem's content

			//Disable the selection for all submenu items
			foreach (var item in mainMenuItem.Items)
				item.Selected = false;

			var x = subMenuViewController.View.Frame.X;
			var y = subMenuViewController.View.Frame.Y;
			var width = subMenuViewController.View.Frame.Width;
			var height = subMenuViewController.View.Frame.Height;

			var subLineLayout = new SubMenuFlowLayout(mainMenuItem.Items.Count, width) { ScrollDirection = UICollectionViewScrollDirection.Horizontal};

			//Remove old subview
			if (subMenuViewController != null)
			{
				subMenuViewController.RemoveFromParentViewController();
				subMenuViewController.View.RemoveFromSuperview();
				subMenuViewController.Dispose();
				subMenuViewController = null;
			}

			//var view = new UIView(new RectangleF(x, y, width, height));
			//view.BackgroundColor = new UIColor(0.1f, 0.1f, 0.1f, 1f);
			//View.Add(view);

			subMenuViewController = new SubMenuCollectionViewController(subLineLayout, mainMenuItem.Items);
			subMenuViewController.Framework = this;
			subMenuViewController.View.AutoresizingMask = UIViewAutoresizing.All;
			subMenuViewController.View.Frame = new RectangleF(x, y, width, height);

			CALayer layer = new CALayer();
			subMenuViewController.View.ClipsToBounds = true;
			layer.MasksToBounds = true;
			//layer.Bounds = new RectangleF(0, 0, 50, 50);
			//layer.EdgeAntialiasingMask = CAEdgeAntialiasingMask.All;
			//layer.Opaque = true;
			layer.BorderColor = new CGColor(0.1f, 0.1f, 0.1f);
			layer.BorderWidth = 10;
			layer.Frame = new RectangleF(-10, -10, subMenuViewController.View.Frame.Width + 20, subMenuViewController.View.Frame.Height + 30);
			layer.CornerRadius = 20f;
			subMenuViewController.View.Layer.AddSublayer(layer);

			View.Add(subMenuViewController.View);

		}

		public void SetupContent(SubMenuItem subMenuItem)
		{
			contentLabel.Text = subMenuItem.Name;
		}
	}
}

