using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanDuongCom
{
	class Page : DataElement
	{
		public string title;
		//private string content;
		//private string output;

		//private string sitename;
		private List<NavItem> navItems;

		public Page(string title, string contentPath, string sitename, string templatePath, List<NavItem> navItems) : base(templatePath)
		{
			this.title = title;
			//content = System.IO.File.ReadAllText(@"Data\" + contentPath);
			this.navItems = navItems;

			SetProperty("$TITLE$", sitename + " - " + title);
			//SetProperty("$NAV$", GenerateNav().Bake());
			SetProperty("$CONTENT$", System.IO.File.ReadAllText(@"Data\" + contentPath));
		}

		//can only handle navs with dropdowns one level deep.
		public void GenerateNav()
		{
			DataElement output = new DataElement("nav.html");
			foreach (NavItem i in navItems)
			{
				DataElement item;
				if (title == i.title)
				{
					item = new DataElement("navItemActive.html");
				}
				else if (i.children.Count > 0)
				{
					item = new DataElement("dropUp.html");
					foreach (NavItem c in i.children)
					{
						DataElement child = new DataElement("dropDownItem.html");
						child.SetProperty("$TITLE$", c.title);
						child.SetProperty("$HREF$", c.href);
						item.children.Add(child);
					}
				}
				else
				{
					item = new DataElement("navItem.html");
				}
				item.SetProperty("$TITLE$", i.title);
				item.SetProperty("$HREF$", i.href);
				output.children.Add(item);
			}
			SetProperty("$NAV$", output.Bake());
		}
	}
}
