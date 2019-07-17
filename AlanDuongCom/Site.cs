using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanDuongCom
{
	class Site
	{
		private List<Page> pages;
		private List<NavItem> navItems;

		private string sitename;
		private string templatePath;

		//navCatetory, "" to show on the navBar without a category, null to not show at all
		public void AddPage(string title, string contentPath, string navCategory)
		{
			pages.Add(new Page(title, contentPath, sitename, templatePath, navItems));

			if (navCategory == "")
			{
				navItems.Add(new NavItem(title, title + ".html"));
			}
		}

		public Site(string sitename, string templatePath)
		{
			this.sitename = sitename;
			this.templatePath = templatePath;
			navItems = new List<NavItem>();
			pages = new List<Page>();
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
