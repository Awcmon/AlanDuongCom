using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanDuongCom
{
	//TODO: Consider making an interface for both DataElements and LiteralElements rather than basing literals off data elements
	//TODO: Consider id's for literals too? Can't really see the purpose though.
	class LiteralElement : DataElement
	{
		private string literal;

		public LiteralElement(string literal)
		{
			this.literal = literal;
		}

		public override string Bake()
		{
			return literal;
		}
	}
}
