using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanDuongCom
{
	//Notes: if a property is set, the data better have that property.
	//If there are children, there must be a $CHILDREN$ tag. 
	//TODO: Add error handling for missing properties
	class DataElement
	{
		private string dataPath;

		public List<DataElement> children;

		private Dictionary<string, string> properties;

		public DataElement(string dataPath)
		{
			this.dataPath = dataPath;
			properties = new Dictionary<string, string>();
			children = new List<DataElement>();
		}

		public void SetProperty(string property, string value)
		{
			properties[property] = value;
		}

		//turn this element and all its children into the final html
		public string Bake()
		{
			string output = System.IO.File.ReadAllText(@"Data\" + dataPath);

			foreach(string p in properties.Keys)
			{
				output = output.Replace(p, properties[p]); //write the property
			}

			//bake the children
			if(children.Count > 0)
			{
				string childrenOutput = "";
				foreach (DataElement e in children)
				{
					childrenOutput += e.Bake();
				}
				output = output.Replace("$CHILDREN$", childrenOutput);
			}

			return output;
		}

	}
}
