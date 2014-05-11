using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CrmMark2
{
    public class MainMenuCollectionViewController : UICollectionViewController
    {
		static NSString mainCellId = new NSString ("MainCell");

		FrameworkViewController frameController;
		List<MainMenuItem> menuItems;

		public FrameworkViewController Framework { get { return frameController; } set { frameController = value; } }

        public MainMenuCollectionViewController (UICollectionViewLayout layout) : base (layout)
        {
        }

		public void SetMainItems(List<MainMenuItem> items)
		{
			menuItems = items;
		}

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

			CollectionView.ContentSize = new SizeF(200, 400);
			CollectionView.BackgroundColor = new UIColor(0.1f, 0.1f, 0.1f, 1f);
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

			var menuItem = menuItems[indexPath.Row];

            cell.Image = menuItem.Image;
			cell.Text = menuItem.Name;
			cell.TextColor = menuItem.TextColor;

            return cell;
        }

		public override void ItemHighlighted(UICollectionView collectionView, NSIndexPath indexPath)
		{
		}

		public override void ItemDeselected(UICollectionView collectionView, NSIndexPath indexPath)
		{
		}

		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			if (menuItems[indexPath.Row].Enabled)
			{
				Console.WriteLine("Item selected: " + indexPath.Row);
				SelectItem(collectionView, indexPath.Row);
			}
		}

		public override bool ShouldSelectItem (UICollectionView collectionView, NSIndexPath indexPath)
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
            return false;
        }

        public override void PerformAction (UICollectionView collectionView, MonoTouch.ObjCRuntime.Selector action, NSIndexPath indexPath, NSObject sender)
        {
        }

		void SelectItem(UICollectionView collectionView, int row)
		{
			foreach (var item in menuItems)
				item.Selected = false;

			//Make the icon selected
			menuItems[row].Selected = true;
			collectionView.ReloadData();

			frameController.SetupSubmenu(menuItems[row]);

			//collectionView.SetContentOffset(new PointF(0, row * 100 + 20), true); //+15
			//frameController.SetupSubMenuViewController(menuItems[row]);
		}

		public void SelectFirstItem()
		{
			SelectItem(CollectionView, 0);
		}

		public void ActivateMainMenuItem(int index)
		{
			SelectItem(CollectionView, menuItems[index].Index);
		}

		/*public MainMenuItem ActivateMainMenuItem(Func<FrameworkViewController, List<SubMenuItem>> methodKey)
		{
			try
			{
				for (int i = 0; i < menuItems.Count; i++)
				{
					if (menuItems[i].PopulateMethod == methodKey)
					{
						SelectItem(CollectionView, menuItems[i].Index);
						return menuItems[i];
					}
				}
			}
			catch
			{
				new UIAlertView("Error", "Could not open the menu you requested", null, "Ok").Show();
			}

			return null;
		}*/
    }
}

