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
			string template = System.IO.File.ReadAllText(@"Data\basic.html");
			string nav = System.IO.File.ReadAllText(@"Data\nav.html");
			/*
			foreach (string f in Directory.GetFiles("Data", "*.html"))
			{
				Console.WriteLine(f);
			}
			*/
			string indexContent = System.IO.File.ReadAllText(@"Data\index.html");

			string index = String.Copy(template);

			index = index.Replace("$CONTENT$", indexContent);
			index = index.Replace("$NAV$", nav);
			index = index.Replace("$TITLE$", "Alan Duong");

			System.IO.Directory.CreateDirectory("Out");
			System.IO.File.WriteAllText(@"Out\index.html", index);
		}
	}
}
