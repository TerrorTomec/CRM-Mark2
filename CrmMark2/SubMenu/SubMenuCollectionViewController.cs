using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace CrmMark2
{
    public class SubMenuCollectionViewController : UICollectionViewController
    {
		static NSString mainCellId = new NSString ("SubCell");

		FrameworkViewController frameworkController;
		List<SubMenuItem> menuItems = new List<SubMenuItem>();

		UIImageView indicator;

		UIView view;

		public List<SubMenuItem> MenuItems { get { return menuItems; } }
		public FrameworkViewController Framework { get { return frameworkController; } set { frameworkController = value; } }
	
		public SubMenuCollectionViewController (UICollectionViewLayout layout, List<SubMenuItem> items) : base (layout)
		{
			this.menuItems = items;
		}

		public void SetItems(List<SubMenuItem> items)
		{
			/*menuItems = items;
			CollectionView = new UICollectionView(CollectionView.Frame, CollectionView.CollectionViewLayout);
			CollectionView.RegisterClassForCell (typeof(MenuCell), mainCellId);
			CollectionView.ReloadData();*/
		}

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

			//view = new UIView(new Rectangle(0, 0, (int)CollectionView.Frame.Width / 2, (int)CollectionView.Frame.Height));
			//view.BackgroundColor = new UIColor(0.1f, 0.1f, 0.1f, 0.5f);
			//Add(view);

			//CollectionView.Add(view);
			//CollectionView.SendSubviewToBack(view);
			//Console.WriteLine(CollectionView.Subviews.Length);
			CollectionView.BackgroundColor = new UIColor(0.4f, 0.4f, 1f, 1f);
            CollectionView.RegisterClassForCell (typeof(MenuCell), mainCellId);
		}

        public override int NumberOfSections (UICollectionView collectionView)
        {
            return 1;
        }

        public override int GetItemsCount (UICollectionView collectionView, int section)
        {
			if (menuItems != null)
				return menuItems.Count;
			return 0;
        }

        public override UICollectionViewCell GetCell (UICollectionView collectionView, MonoTouch.Foundation.NSIndexPath indexPath)
        {
            var cell = (MenuCell)collectionView.DequeueReusableCell (mainCellId, indexPath);

			var item = menuItems[indexPath.Row];

            cell.Image = item.Image;
			cell.Text = item.Name;

            return cell;
        }

		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			if (menuItems[indexPath.Row].Enabled)
			{
				Console.WriteLine("Item selected: " + indexPath.Row);
				SelectItem(collectionView, indexPath.Row);
			}
		}

		void SelectItem(UICollectionView collectionView, int row)
		{
			foreach (var item in menuItems)
				item.Selected = false;

			//Make the icon selected
			menuItems[row].Selected = true;
			collectionView.ReloadData();

			//frameController.SetupSubmenu(menuItems[row]);
			frameworkController.SetupContent(menuItems[row]);


			//collectionView.SetContentOffset(new PointF(0, row * 100 + 20), true); //+15
			//frameController.SetupSubMenuViewController(menuItems[row]);
		}

        public override void ItemHighlighted (UICollectionView collectionView, NSIndexPath indexPath)
		{
			if (menuItems[indexPath.Row].Enabled)
			{
				//frameworkController.SetupContentViewController(menuItems[indexPath.Row]);
				//CollectionView.ScrollToItem(indexPath, UICollectionViewScrollPosition.Top, true);
				//UpdateIndicator(indexPath);
			}
		}

		public void UpdateIndicator(NSIndexPath indexPath)
		{
			/*var cellInfo = CollectionView.GetLayoutAttributesForItem(indexPath);
			var frame = cellInfo.Frame;

			int displaceX = 23;
			int displaceY = 70;

			PointF endPosition = new PointF(frame.X + displaceX, frame.Y + displaceY);
			PointF spawnPosition = new PointF(endPosition.X, endPosition.Y + 50);

			//Show indicator
			if (indicator == null)
			{
				indicator = new UIImageView(UIImage.FromFile("arrow.png"));
				indicator.Frame = new RectangleF(spawnPosition, new SizeF(indicator.Frame.Width, indicator.Frame.Height));
				View.Add(indicator);
			}

			//Update indicator position
			UIView.Animate(
				0.5f,
				() => { indicator.Frame = new RectangleF(endPosition, new SizeF(indicator.Frame.Width, indicator.Frame.Height)); }
			);*/
		}

		public NSIndexPath GetIndexPath(int itemIndex)
		{
			return NSIndexPath.FromRowSection(itemIndex, 0);
		}

        public override void ItemUnhighlighted (UICollectionView collectionView, NSIndexPath indexPath)
        {
            //var cell = collectionView.CellForItem (indexPath);
            //cell.ContentView.BackgroundColor = UIColor.White;
        }

        public override bool ShouldHighlightItem (UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }

        // for edit menu
        public override bool ShouldShowMenu (UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }

        public override bool CanPerformAction (UICollectionView collectionView, MonoTouch.ObjCRuntime.Selector action, NSIndexPath indexPath, NSObject sender)
        {
            return true;
        }

        public override void PerformAction (UICollectionView collectionView, MonoTouch.ObjCRuntime.Selector action, NSIndexPath indexPath, NSObject sender)
        {
            Console.WriteLine ("code to perform action");
        }
    }
}

