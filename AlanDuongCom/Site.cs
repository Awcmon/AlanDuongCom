using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanDuongCom
{
	class Site
	{
		private List<Page> pages = new List<Page>();
		private List<NavItem> navItems = new List<NavItem>();

		private string sitename = "Alan Duong";
		private string templatePath = "basic.html";

		private DataElement nav = new DataElement("nav.html");

		//navCatetory, "" to show on the navBar without a category, null to not show at all
		public void AddPage(string title, string contentPath, string navCategory)
		{
			pages.Add(new Page(title, contentPath, sitename, templatePath, navItems));

			if (navCategory == "")
			{
				navItems.Add(new NavItem(title, title + ".html"));
			}
		}

		public Site()
		{

		}

		public void Generate()
		{
			System.IO.Directory.CreateDirectory("Out");
			foreach (Page p in pages)
			{
				p.GenerateNav();
				System.IO.File.WriteAllText(@"Out\" + p.title.ToLower() + ".html", p.Bake());
			}
		}
	}
}
