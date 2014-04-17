using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace CrmMark2
{
	/// <summary>
	/// Class is used in main collection view (also used in sub)
	/// </summary>
	public class MenuCell : UICollectionViewCell
	{
		public UIImage Image { get { return imageView.Image; } set { imageView.Image = value; UpdateCellImage();} }
		public string Text { set { label.Text = value; } }
		public UIColor TextColor { set { label.TextColor = value; } }

		UIImageView imageView;
		UILabel label;

		[Export ("initWithFrame:")]
		public MenuCell (System.Drawing.RectangleF frame) : base (frame)
		{
			BackgroundView = new UIView { BackgroundColor = UIColor.LightGray.ColorWithAlpha(0) };

			SelectedBackgroundView = new UIView { BackgroundColor = UIColor.Green.ColorWithAlpha(0) };

			ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			ContentView.Layer.BorderWidth = 0.0f; //2f
			ContentView.BackgroundColor = UIColor.LightGray.ColorWithAlpha(0);
			ContentView.Transform = CGAffineTransform.MakeScale(0.9f, 0.9f);

			imageView = new UIImageView(UIImage.FromBundle("test_icon_red.png"));

			imageView.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);

			label = new UILabel();
			label.Frame = new RectangleF(-10, 35, 100, 70);
			label.TextAlignment = UITextAlignment.Center;
			label.BackgroundColor = UIColor.Red.ColorWithAlpha(0);
			label.Font = UIFont.FromName("Avenir-Medium", 15f);  //("HelveticaNeue-Light", 14f);
			label.TextColor = UIColor.White;

			ContentView.AddSubview(imageView);
			ContentView.AddSubview(label);
		}

		void UpdateCellImage()
		{
			imageView.Frame = new RectangleF(0, 0, imageView.Image.Size.Width, imageView.Image.Size.Height);
			imageView.Center = new PointF(Frame.Width / 2f, Frame.Height / 2f - 10f);
		}
	}
}

