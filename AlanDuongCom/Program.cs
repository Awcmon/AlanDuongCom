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
		//TODO: Replace this with a resource manager?
		static string Script(string src)
		{
			return "<script src=\"" + src + "\"></script>";
		}

		static void Main(string[] args)
		{
			Site site = new Site("alanduong.com", "basic.html");
			site.AddPage("Index", "index.html", "");
			site.AddPage("SUPERCRUISE", "supercruise.html", "Projects");
			site.AddPage("TwoToner", "twotoner.html", "Projects");
			site.AddPage("DominoComputer", "dominocomputer.html", "Projects");
			site.AddPage("alanduong.com", "alanduongcom.html", "Projects");
			site.AddPage("MIL/MOA Calculator", "calculatorMilMoa.html", "Tools").AppendToProperty("#SCRIPTS#", Script("js/convertMilMoa.js"));
			site.AddPage("Blog", "wip.html", "");
			site.Generate();
		}
	}
}
