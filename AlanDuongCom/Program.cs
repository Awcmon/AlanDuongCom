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
			foreach (string f in Directory.GetFiles("Data", "*.html"))
			{
				Console.WriteLine(f);
			}
		}
	}
}
