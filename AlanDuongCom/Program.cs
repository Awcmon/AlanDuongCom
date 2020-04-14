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
			Site site = new Site("alanduong.com", "defaultTemplate.html");
			site.CreatePage("Index", "index.html", "");
			site.CreatePage("SUPERCRUISE", "supercruise.html", "Projects");
			site.CreatePage("TwoToner", "twotoner.html", "Projects");
			site.CreatePage("DominoComputer", "dominocomputer.html", "Projects");
			site.CreatePage("Springy Sanic", "springysanic.html", "Projects");
			site.CreatePage("alanduong.com", "alanduongcom.html", "Projects");
			site.CreatePage("MIL/MOA Calculator", "calculatorMilMoa.html", "Tools").AddScript("js/convertMilMoa.js");
			site.AddBlog("Blog", "blog.html", "");
			site.Generate();
		}
	}
}
