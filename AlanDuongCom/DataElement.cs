using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlanDuongCom
{
	//TODO: Add warning for when a property is set, but isn't in a given element
	//RESOLVED: handle when a property is in an element, but is not set
	class DataElement
	{
		private string dataPath;

		public string Id { get; set; }

		private Dictionary<string, List<DataElement>> properties;

		public DataElement(string dataPath = "", string id = "")
		{
			this.dataPath = dataPath;
			properties = new Dictionary<string, List<DataElement>>();
			Id = id;
		}

		public void AppendToProperty(string property, DataElement elem)
		{
			if(!properties.ContainsKey(property))
			{
				properties[property] = new List<DataElement>();
			}
			properties[property].Add(elem);
		}

		public void AppendToProperty(string property, string strLiteral)
		{
			AppendToProperty(property, new LiteralElement(strLiteral));
		}

		//turn this element and all its children into the final html
		public virtual string Bake()
		{
			string output = "";
			if(dataPath != null && dataPath != "")
			{
				output = System.IO.File.ReadAllText(@"Data\" + dataPath);
			}

			//for each property
			foreach(string p in properties.Keys)
			{
				//bake its children elements
				List<DataElement> children = properties[p];
				if (children != null && children.Count > 0)
				{
					string childrenOutput = "";
					foreach (DataElement e in children)
					{
						childrenOutput += e.Bake();
					}
					output = output.Replace(p, childrenOutput); //write to the property
				}
			}

			//replace all unset properties in the page with an empty string
			//note: a property can be any alphanumeric string with hyphens and underscores surrounded by #'s
			Regex rgx = new Regex(@"#[a-zA-Z0-9_-]*#");
			return rgx.Replace(output, "");
			//return output;
		}

	}
}
