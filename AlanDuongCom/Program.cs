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
			Site site = new Site("alanduong.com", "basic.html");
			site.AddPage("Index", "index.html", "");
			site.AddPage("SUPERCRUISE", "supercruise.html", "Projects");
			site.AddPage("TwoToner", "wip.html", "Projects");
			site.AddPage("DominoComputer", "wip.html", "Projects");
			site.AddPage("alanduong.com", "alanduongcom.html", "Projects");
			site.AddPage("MIL/MOA Calculator", "wip.html", "Tools");
			site.AddPage("Blog", "wip.html", "");
			site.Generate();
		}
	}
}
