using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace CrmMark2
{
	public class SubMenuFlowLayout : UICollectionViewFlowLayout
	{
		public const float ITEM_SIZE = 80.0f;
		public const int ACTIVE_DISTANCE = 100;
		public const float ZOOM_FACTOR = 0.3f;

		public SubMenuFlowLayout (int itemsCount, float width)
		{	
			ItemSize = new SizeF (ITEM_SIZE, ITEM_SIZE);
			ScrollDirection = UICollectionViewScrollDirection.Horizontal;
			//SectionInset = new UIEdgeInsets(0, 100, 0, 0);
			//MinimumLineSpacing = 100.0f;
			MinimumInteritemSpacing = 1000f;

			UpdateWidth(itemsCount, width);
		}

		public void UpdateWidth(int itemsCount, float width)
		{
			float interval = width / (itemsCount + 1);

			SectionInset = new UIEdgeInsets(0, interval - ITEM_SIZE / 2, 0, 0);
			MinimumLineSpacing = interval - ITEM_SIZE;
		}

		public override bool ShouldInvalidateLayoutForBoundsChange (RectangleF newBounds)
		{
			return true;
		}

		public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect (RectangleF rect)
		{
			var array = base.LayoutAttributesForElementsInRect (rect);
			var visibleRect = new RectangleF (CollectionView.ContentOffset, CollectionView.Bounds.Size);

			foreach (var attributes in array) 
			{
				if (attributes.Frame.IntersectsWith (rect)) 
				{
					float distance = visibleRect.GetMidX () - attributes.Center.X;
					float normalizedDistance = distance / ACTIVE_DISTANCE;
					if (Math.Abs (distance) < ACTIVE_DISTANCE) 
					{
						//This disables the zooming (zoom is configured for horizontal scrolling)
						//float zoom = 1 + ZOOM_FACTOR * (1 - Math.Abs (normalizedDistance));
						//attributes.Transform3D = CATransform3D.MakeScale (zoom, zoom, 1.0f);
						attributes.ZIndex = 1;											
					}
				}
			}
			return array;
		}

		public override PointF TargetContentOffset (PointF proposedContentOffset, PointF scrollingVelocity)
		{
			float offSetAdjustment = float.MaxValue;
			float horizontalCenter = (float)(proposedContentOffset.X + (this.CollectionView.Bounds.Size.Width / 2.0));
			RectangleF targetRect = new RectangleF (proposedContentOffset.X, 0.0f, this.CollectionView.Bounds.Size.Width, this.CollectionView.Bounds.Size.Height);
			var array = base.LayoutAttributesForElementsInRect (targetRect);
			foreach (var layoutAttributes in array) {
				float itemHorizontalCenter = layoutAttributes.Center.X;
				if (Math.Abs (itemHorizontalCenter - horizontalCenter) < Math.Abs (offSetAdjustment)) {
					offSetAdjustment = itemHorizontalCenter - horizontalCenter;
				}
			}
			return new PointF (proposedContentOffset.X + offSetAdjustment, proposedContentOffset.Y);
		}

	}
}

