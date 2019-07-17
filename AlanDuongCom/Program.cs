using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlanDuongCom
{
	class Program
	{
		static void Main(string[] args)
		{
			/*
			Page.SetNav("nav.html");
			//AddPage(new Page("Index", "index.html"), true);
			Page index = new Page("Index", "index.html");
			*/

			/*
			DataElement nav = new DataElement("nav.html");
			DataElement home = new DataElement("navItem.html");
			home.SetProperty("$HREF$", "#");
			home.SetProperty("$TITLE$", "Home");
			DataElement projects = new DataElement("navItem.html");
			projects.SetProperty("$HREF$", "#");
			projects.SetProperty("$TITLE$", "Projects");
			nav.children.Add(home);
			nav.children.Add(projects);

			DataElement page = new DataElement("basic.html");
			DataElement indexContent = new DataElement("index.html");
			page.SetProperty("$TITLE$", "Alan Duong");
			page.SetProperty("$NAV$", nav.Bake());
			page.SetProperty("$CONTENT$", indexContent.Bake());

			System.IO.Directory.CreateDirectory("Out");
			System.IO.File.WriteAllText(@"Out\index.html", page.Bake());
			*/

			Site site = new Site("Alan Duong", "basic.html");
			site.AddPage("Index", "index.html", "");
			site.AddPage("SUPERCRUISE", "supercruise.html", "Projects");
			site.AddPage("TwoToner", "wip.html", "Projects");
			site.AddPage("DominoComputer", "wip.html", "Projects");
			site.AddPage("MIL/MOA Calculator", "wip.html", "Tools");
			site.AddPage("About", "wip.html", "");
			site.AddPage("Blog", "wip.html", "");
			site.Generate();
		}
	}
}
