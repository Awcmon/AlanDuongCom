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

		public void GenerateNav()
		{
			DataElement output = new DataElement("nav.html");
			foreach (NavItem i in navItems)
			{
				DataElement item = new DataElement("navItem.html");
				item.SetProperty("$TITLE$", i.title);
				item.SetProperty("$HREF$", i.href);
				output.children.Add(item);
			}
			SetProperty("$NAV$", output.Bake());
		}

		/*
		public void Bake()
		{
			output = output.Replace("$TITLE$", sitename + title);
			output = output.Replace("$NAV$", nav.Bake());
			output = output.Replace("$CONTENT$", content);
		}
		*/

		/*
		private void OutputFile()
		{
			System.IO.Directory.CreateDirectory("Out");
			System.IO.File.WriteAllText(@"Out\" + title.ToLower() + ".html", output);
		}
		*/
	}
}
