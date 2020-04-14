using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlanDuongCom
{
	class Site
	{
		private List<Page> pages;
		public List<NavItem> NavItems { get; private set; }

		public string Sitename { get; private set; }
		public string TemplatePath { get; private set; }

		//turn a string into only lowercase letters, numbers, and hyphens.
		static public string SanitizeURL(string url)
		{
			Regex rgx = new Regex("[^a-zA-Z0-9-]");
			return rgx.Replace(url.ToLower(), "-");
		}

		static public string GenerateURL(string pageName)
		{
			return SanitizeURL(pageName) + ".html";
		}

		//navCategory: "" to show on the navBar without a category, null to not show at all
		//return the page that was added
		public Page CreatePage(string title, string contentPath = null, string navCategory = null, string alternateTemplatePath = null)
		{
			Page page = new Page(this, title, contentPath, alternateTemplatePath);

			//pages.Add(new Page(title, contentPath, sitename, templatePath, navItems));
			pages.Add(page);

			if(navCategory != null)
			{
				AddPageToNav(title, navCategory);
			}
			
			return page;
		}

		public Blog AddBlog(string title, string contentPath = null, string navCategory = null, string alternateTemplatePath = null)
		{
			Blog page = new Blog(this, title, contentPath, alternateTemplatePath);

			//pages.Add(new Page(title, contentPath, sitename, templatePath, navItems));
			pages.Add(page);

			if (navCategory != null)
			{
				AddPageToNav(title, navCategory);
			}

			return page;
		}

		private void AddPageToNav(string title, string navCategory)
		{
			//add an entry for this page to the list of NavItems.
			if (navCategory == null) { return; }
			if (navCategory == "")
			{
				NavItems.Add(new NavItem(title, GenerateURL(title)));
			}
			else
			{
				NavItem category = null;
				foreach (NavItem i in NavItems)
				{
					if (i.title == navCategory) //if there already exists an item for that category
					{
						category = i;
						break;
					}
				}
				if (category == null) //if the category does not exist yet, create it
				{
					category = new NavItem(navCategory, "#");
					NavItems.Add(category);
				}
				category.children.Add(new NavItem(title, GenerateURL(title)));
			}
		}

		public Site(string sitename, string templatePath)
		{
			this.Sitename = sitename;
			this.TemplatePath = templatePath;
			NavItems = new List<NavItem>();
			pages = new List<Page>();
		}

		//bake and write all the pages to the final html output
		public void Generate()
		{
			foreach (Page p in pages)
			{
				p.GenerateNav();
				System.IO.File.WriteAllText(@"../../../" + GenerateURL(p.TemplateElement.Id), p.TemplateElement.Bake());
			}
		}
	}
}
