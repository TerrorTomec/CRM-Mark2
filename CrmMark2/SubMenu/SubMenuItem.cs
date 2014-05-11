using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace CrmMark2
{
	/// <summary>
	/// Contains all info about a sub category
	/// </summary>
	public class SubMenuItem
	{
		//Add enum for each content page that needs to be activated from another content page
		public enum RemoteActivate
		{
			None,
			ClientCard,
			ContactMail,
			ClientSearch
		}

		string name;
		//string iconFileName;
		string contentName = "";
		//bool enabled = true;
		int mainIndex = -1;
		int subIndex = -1;
		RemoteActivate remoteActivation = RemoteActivate.None;
		//ContentViewControllerBase contentViewOriginal;

		bool enabled = true;
		bool selected = false;
		string currentIcon;
		string iconFileName;
		string iconSelectedFileName;
		UIColor normalColor = new UIColor(0.56f, 0.56f, 0.56f, 1f);
		UIColor selectedColor = new UIColor(1f, 1f, 1f, 1f);

		/// <summary>
		/// Constructor chaining, pretty cool!
		/// </summary>
		public SubMenuItem(string name, string iconFileName/*, ContentViewControllerBase contentViewController*/) : this(name, iconFileName/*, contentViewController*/, RemoteActivate.None) { }

		public SubMenuItem(string name, string iconFileName/*, ContentViewControllerBase contentViewController*/, RemoteActivate remoteActivation)
		{
			this.enabled = true;
			this.name = name;
			this.iconFileName = "test_icon_3"; /*"IconsWhite/" + iconFileName;*/
			this.iconSelectedFileName = "test_icon";
			this.currentIcon = this.iconFileName;
			this.mainIndex = -1;
			this.subIndex = -1;

			//contentViewOriginal = contentViewController;
			this.remoteActivation = remoteActivation;
		}

		public void SetIndex(int mainIndex, int subIndex)
		{
			this.mainIndex = mainIndex;
			this.subIndex = subIndex;
			//if (contentViewOriginal != null)
			//	contentViewOriginal.Index = Index;
		}

		//public ContentViewControllerBase ContentViewController{ get { return contentViewOriginal; } set { contentViewOriginal = value; } }

		void SetSelected(bool value)
		{
			selected = value;

			if (value)
				currentIcon = iconSelectedFileName;
			else
				currentIcon = iconFileName;
		}

		UIColor GetTextColor()
		{
			return selected ? selectedColor : normalColor;
		}

		public bool Selected { get { return selected; } set { SetSelected(value); } }
		public RemoteActivate RemoteActivation { get { return remoteActivation; } }
		public Size Index { get { return new Size(mainIndex, subIndex); } }
		public string Name { get { return name; } }
		public bool Enabled { get { return enabled; } }
		public UIImage Image { get { if (enabled) return UIImage.FromBundle(currentIcon); else return UIImage.FromBundle("disabled.png"); } }
		public string ContentName { get { return contentName; } }
	}
}

