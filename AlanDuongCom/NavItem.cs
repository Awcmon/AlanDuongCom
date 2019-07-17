using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanDuongCom
{
	class NavItem
	{
		public NavItem children;
		public string title;
		public string href;

		public NavItem(string title, string href)
		{
			this.title = title;
			this.href = href;
		}
	}
}
