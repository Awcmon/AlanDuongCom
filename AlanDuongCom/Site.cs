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
		private List<Page> navPages;

		private DataElement nav = new DataElement("nav.html");

		//navCatetory, "" to show on the navBar without a category, null to not show at all
		void AddPage(Page page, string navCategory)
		{
			pages.Add(page);
			//if (AddToNav)
			{
				navPages.Add(page);
			}
		}

		public Site()
		{

		}
	}
}
