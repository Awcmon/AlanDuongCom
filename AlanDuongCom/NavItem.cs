using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanDuongCom
{
	//this class is used soley to generate the navbar
	class NavItem
	{
		public List<NavItem> children;
		public string title;
		public Page LinkedPage { get; private set; }

		public NavItem(string title, Page page)
		{
			this.title = title;
			LinkedPage = page;
			children = new List<NavItem>();
		}
	}
}
