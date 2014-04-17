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
		string iconFileName;
		bool enabled = true;

		public MainMenuItem(string name, string iconFileName/*, Func<FrameworkViewController, List<SubMenuItem>> populateMethod*/)
		{
			this.name = name;
			this.iconFileName = "test_icon"; /*"IconsWhite/" + iconFileName;*/

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

		public int Index { get; set; }

		public string Name { get { return name; } }
		public bool Enabled { get { return enabled; } }
		public UIImage Image { get { if (enabled) return UIImage.FromBundle(iconFileName); else return UIImage.FromBundle("disabled.png"); } }
	}
}

