using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//TODO: Perhaps add STYLES property to page template?
//RESOLVED: Generate pages under subdirectories
//TODO: Add ability for nested subdirectories?
//TODO: Prettify final output HTML?

namespace AlanDuongCom
{
	class Page
	{
		//a template page must have these properties: title, content scripts
		public DataElement TemplateElement { get; protected set; }
		public string Title { get; protected set; }

		private DataElement _contentElement;
		public DataElement ContentElement
		{
			get { return _contentElement; }
			set
			{
				_contentElement = value;
				TemplateElement.ClearProperty("#CONTENT#");
				TemplateElement.AppendToProperty("#CONTENT#", value);
			}
		}

		protected Site ParentSite { get; private set; } //the site this page belongs to

		public string SubDirectory { get; private set; }

		public Page(Site site, string title, string contentPath = null, string subDirectory = null, string altTemplatePath = null)
		{
			ParentSite = site;
			Title = title;
			SubDirectory = subDirectory;

			if(SubDirectory != null && SubDirectory != "" && !Directory.Exists(SubDirectory))
			{
				Directory.CreateDirectory(site.RootDir + SubDirectory);
			}

			if (altTemplatePath != null)
			{
				TemplateElement = new DataElement(altTemplatePath, title);
			}
			else
			{
				TemplateElement = new DataElement(ParentSite.TemplatePath, title);
			}

			TemplateElement.AppendToProperty("#TITLE#", new LiteralElement(title + " - " + ParentSite.Sitename)); //page title
			if(contentPath != null)
			{
				ContentElement = new DataElement(contentPath);
			}
		}

		//TODO: Replace this with a resource manager?
		private string Script(string src)
		{
			return "<script src=\"" + src + "\"></script>";
		}

		public void AddScript(string scriptPath)
		{
			TemplateElement.AppendToProperty("#SCRIPTS#", Script(scriptPath));
		}

		//generate the navbar after all the pages are added to the site.
		//the navbar is generated per page because the active item is different for each page.
		//can only handle navs with dropdowns one level deep.
		//TODO: Add active item highlight to dropdown pages?
		public void GenerateNav()
		{
			DataElement output = new DataElement("nav.html"); //create the unpopulated navbar
			foreach (NavItem i in ParentSite.NavItems) //iterate through the navitems list populated when adding pages to the site
			{
				DataElement item; //the html element for a particular page or dropdown category
				if (TemplateElement.Id == i.title)
				{
					item = new DataElement("navItemActive.html"); 
				}
				else if (i.children.Count > 0) //if the item has children, it is a dropdown category rather than a page.
				{
					item = new DataElement("dropUp.html");
					foreach (NavItem c in i.children) //populate the category with its pages
					{
						DataElement child = new DataElement("dropDownItem.html");
						child.AppendToProperty("#TITLE#", new LiteralElement(c.title));
						child.AppendToProperty("#HREF#", new LiteralElement(GenerateRelativeURL(c.LinkedPage)));
						item.AppendToProperty("#CHILDREN#", child);
					}
				}
				else
				{
					item = new DataElement("navItem.html");
				}
				item.AppendToProperty("#TITLE#", new LiteralElement(i.title));
				item.AppendToProperty("#HREF#", new LiteralElement(GenerateRelativeURL(i.LinkedPage)));
				output.AppendToProperty("#CHILDREN#", item); //append the item to the navbar
			}
			TemplateElement.AppendToProperty("#NAV#", output); //bake the navbar DataElement into html to replace the #NAV# macro or property or whatever with.
		}

		public string GenerateRelativeURL(Page other)
		{
			if(other == null)
			{
				return "#";
			}

			if (SubDirectory == other.SubDirectory) //if we are in the same subdirectory, just go to the other page
			{
				return other.GenerateURL(false);
			}
			else
			{
				string relativeUrl = "";
				if (SubDirectory != null) //if this subdirectory is not null and not equal to the linked page's, we need to go back
				{
					relativeUrl += "../";
				}
				if (other.SubDirectory != null) //if the linked page has its own subdirectory, we need to go through it
				{
					relativeUrl += other.GenerateURL();
				}
				else //if not, then we don't.
				{
					relativeUrl += other.GenerateURL(false);
				}
				return relativeUrl;
			}
		}

		public string GenerateURL(bool includeSubDirectory = true, bool isFilePath = false)
		{
			if (SubDirectory == null || SubDirectory == "" || !includeSubDirectory)
			{
				return Site.SanitizeURL(Title) + (isFilePath ? ".html" : "");
			}
			else
			{
				return Site.SanitizeURL(SubDirectory) + "/" + Site.SanitizeURL(Title) + (isFilePath ? ".html" : "");
			}
		}
	}
}
