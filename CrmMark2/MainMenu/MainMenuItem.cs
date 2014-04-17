using System;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace CrmMark2
{
	/// <summary>
	/// Contains all info about a main category
	/// </summary>
	public class MainMenuItem
	{
		string name;
		string currentIcon;
		string iconFileName;
		string iconSelectedFileName;
		bool enabled = true;
		bool selected = false;
		UIColor normalColor = new UIColor(0.56f, 0.56f, 0.56f, 1f);
		UIColor selectedColor = new UIColor(1f, 1f, 1f, 1f);

		public MainMenuItem(string name, string iconFileName/*, Func<FrameworkViewController, List<SubMenuItem>> populateMethod*/)
		{
			this.name = name;
			this.iconFileName = "test_icon_3"; /*"IconsWhite/" + iconFileName;*/
			this.iconSelectedFileName = "test_icon";
			this.currentIcon = this.iconFileName;
			//this.PopulateMethod = populateMethod;
		}

		//public Func<FrameworkViewController, List<SubMenuItem>> PopulateMethod { get; set; }

		/*public List<SubMenuItem> Populate(FrameworkViewController framework)
		{
			var population = PopulateMethod(framework);

			for (int i = 0; i < population.Count; i++)
				population[i].SetIndex(Index, i);

			return population;
		}*/

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

		public int Index { get; set; }

		public bool Selected { get { return selected; } set { SetSelected(value); } }
		public UIColor TextColor { get { return GetTextColor(); } }
		public string Name { get { return name; } }
		public bool Enabled { get { return enabled; } }
		public UIImage Image { get { if (enabled) return UIImage.FromBundle(currentIcon); else return UIImage.FromBundle("disabled.png"); } }
	}
}

