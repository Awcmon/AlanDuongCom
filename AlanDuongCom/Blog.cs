using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO: Add BlogPost card truncation

namespace AlanDuongCom
{
	class Blog : Page 
	{
		SortedSet<BlogPost> blogPosts;

		public Blog(Site site, string title, string contentPath = null, string altTemplatePath = null) : base(site, title, contentPath, altTemplatePath)
		{
			blogPosts = new SortedSet<BlogPost>();
			ProcessBlogPosts(@"BlogPosts\", @"ProcessedBlogPosts\");

			LoadBlogPosts(@"ProcessedBlogPosts\", blogPosts);

			/*
			foreach (BlogPost b in blogPosts)
			{
				foreach(string s in b.Content)
				{
					AppendToProperty("#CONTENT#", new LiteralElement(String.Format("<p>{0}</p>", s)));
				}
				
			}
			*/

			foreach (BlogPost b in blogPosts)
			{
				DataElement blogCard = GenerateBlogCard(b, 5);
				blogCard.AppendToProperty("#URL#", Site.GenerateURL(b.Title));
				ContentElement.AppendToProperty("#POSTS#", blogCard);

				Page postPage = site.CreatePage(b.Title);
				postPage.ContentElement = GenerateBlogCard(b);
				postPage.ContentElement.AppendToProperty("#URL#", Site.GenerateURL(b.Title));
			}
		}

		private void LoadBlogPosts(string dir, SortedSet<BlogPost> dest)
		{
			if(Directory.Exists(dir))
			{
				string[] postPaths = Directory.GetFiles(dir);

				foreach(string p in postPaths)
				{
					dest.Add(new BlogPost(p));
				}
			}
			else
			{
				Console.WriteLine("Blog Post load dir not found");
			}
		}

		//0 or negative number to have no limit
		private DataElement GenerateBlogCard(BlogPost p, int lineLimit = -1)
		{
			DataElement ret = new DataElement("blogPostCard.html");

			ret.AppendToProperty("#TITLE#", p.Title);
			ret.AppendToProperty("#DATE#", p.Date.ToLongDateString());

			int curLines = 0;
			foreach (string s in p.Content)
			{
				if(s != null && s != "")
				{
					ret.AppendToProperty("#CONTENT#", new LiteralElement(String.Format("<p>{0}</p>\n", s)));
					curLines++;
				}
				if(lineLimit > 0 && curLines >= lineLimit)
				{
					ret.AppendToProperty("#CONTENT#", new LiteralElement(String.Format("<a href=\"{0}\"><font color=#FFFFFF>Continue reading</font></a>", Site.GenerateURL(p.Title))));
					break;
				}
			}

			return ret;
		}

		public void ProcessBlogPosts(string srcDir, string outputDir)
		{
			if (!Directory.Exists(srcDir))
			{
				Console.WriteLine("Src dir does not exist.");
				return;
			}

			if(!Directory.Exists(outputDir))
			{
				Directory.CreateDirectory(outputDir);
			}

			string[] srcBlogPosts = Directory.GetFiles(srcDir);
			foreach(string p in srcBlogPosts)
			{
				ProcessBlogPost(p, outputDir);
			}
		}

		public bool ProcessBlogPost(string path, string outputDir) //process a raw blog post
		{
			//the first line is the title
			//the second line are tags
			//every line after should be the content.
			string[] lines = File.ReadAllLines(path);

			if(lines.Count() < 2 || !File.Exists(path))
			{
				Console.WriteLine(path + " is not correctly formatted.");
				return false;
			}

			string fileName = path.Split(new char[] { '/', '\\' }).Last();
			string processedPathName = String.Format("{0}/{1}", outputDir, fileName);

			if (!File.Exists(processedPathName))
			{
				var processed = File.CreateText(processedPathName);
				processed.WriteLine(DateTime.Now.ToFileTimeUtc());
				foreach (String l in lines)
				{
					processed.WriteLine(l);
				}
				processed.Close();
			}

			File.Delete(path); //delete the original file now that we have processed it.

			return true;
		}

	}
}
