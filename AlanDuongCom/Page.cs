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
		private List<NavItem> navItems;

		public Page(string title, string contentPath, string sitename, string templatePath, List<NavItem> navItems) : base(templatePath)
		{
			this.title = title;
			this.navItems = navItems;

			SetProperty("$TITLE$", title + " | " + sitename );
			SetProperty("$CONTENT$", System.IO.File.ReadAllText(@"Data\" + contentPath));
		}

		//generate the navbar after all the pages are added to the site.
		//the navbar is generated per page because the active item is different for each page.
		//can only handle navs with dropdowns one level deep.
		//TODO: Add active item highlight to dropdown pages?
		public void GenerateNav()
		{
			DataElement output = new DataElement("nav.html"); //create the unpopulated navbar
			foreach (NavItem i in navItems) //iterate through the navitems list populated when adding pages to the site
			{
				DataElement item; //the html element for a particular page or dropdown category
				if (title == i.title)
				{
					item = new DataElement("navItemActive.html"); 
				}
				else if (i.children.Count > 0) //if the item has children, it is a dropdown category rather than a page.
				{
					item = new DataElement("dropUp.html");
					foreach (NavItem c in i.children) //populate the category with its pages
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
				output.children.Add(item); //append the item to the navbar
			}
			SetProperty("$NAV$", output.Bake()); //bake the navbar DataElement into html to replace the $NAV$ macro or property or whatever with.
		}
	}
}
