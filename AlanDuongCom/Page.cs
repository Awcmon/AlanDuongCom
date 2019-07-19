using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanDuongCom
{
	class Page : DataElement
	{
		private List<NavItem> navItems;

		public Page(string templatePath, string id, List<NavItem> navItems) : base(templatePath, id)
		{
			this.navItems = navItems;
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
				if (Id == i.title)
				{
					item = new DataElement("navItemActive.html"); 
				}
				else if (i.children.Count > 0) //if the item has children, it is a dropdown category rather than a page.
				{
					item = new DataElement("dropUp.html");
					foreach (NavItem c in i.children) //populate the category with its pages
					{
						DataElement child = new DataElement("dropDownItem.html");
						child.AppendToProperty("#TITLE#", new LiteralElement(c.title));
						child.AppendToProperty("#HREF#", new LiteralElement(c.href));
						item.AppendToProperty("#CHILDREN#", child);
					}
				}
				else
				{
					item = new DataElement("navItem.html");
				}
				item.AppendToProperty("#TITLE#", new LiteralElement(i.title));
				item.AppendToProperty("#HREF#", new LiteralElement(i.href));
				output.AppendToProperty("#CHILDREN#", item); //append the item to the navbar
			}
			AppendToProperty("#NAV#", output); //bake the navbar DataElement into html to replace the #NAV# macro or property or whatever with.
		}
	}
}
