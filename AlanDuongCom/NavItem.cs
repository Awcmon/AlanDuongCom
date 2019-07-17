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
		public string href;

		public NavItem(string title, string href)
		{
			this.title = title;
			this.href = href;
			children = new List<NavItem>();
		}
	}
}
