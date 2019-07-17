using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanDuongCom
{
	class Page
	{
		private static string sitename = "Alan Duong - ";
		private static string template = System.IO.File.ReadAllText(@"Data\basic.html");
		private static string nav;

		public static void SetNav(string navPath)
		{
			nav = System.IO.File.ReadAllText(@"Data\" + navPath);
		}

		private string title;
		private string content;
		private string output;

		public Page(string title, string contentPath)
		{
			this.title = title;
			content = System.IO.File.ReadAllText(@"Data\" + contentPath);
			output = String.Copy(template);
			Bake();
			OutputFile();
		}

		private void Bake()
		{
			output = output.Replace("$TITLE$", sitename + title);
			output = output.Replace("$NAV$", nav);
			output = output.Replace("$CONTENT$", content);
		}

		private void OutputFile()
		{
			System.IO.Directory.CreateDirectory("Out");
			System.IO.File.WriteAllText(@"Out\" + title.ToLower() + ".html", output);
		}
	}
}
