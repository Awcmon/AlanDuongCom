using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//RESOLVED: Add BlogPost card truncation
//TODO: Implement view blog posts by tags

namespace AlanDuongCom
{
	class Blog : Page 
	{
		SortedSet<BlogPost> blogPosts;

		public Blog(Site site, string title, string contentPath = null, string subDirectory = null, string altTemplatePath = null) : base(site, title, contentPath, subDirectory, altTemplatePath)
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

			//Populate blog with blogpost cards and their respective pages
			foreach (BlogPost b in blogPosts)
			{
				Page postPage = site.CreatePage(b.Title, null, null, "blog");
				postPage.ContentElement = GenerateBlogCard(b);
				postPage.ContentElement.AppendToProperty("#URL#", postPage.GenerateRelativeURL(postPage));

				DataElement blogCard = GenerateBlogCard(b, 5);
				blogCard.AppendToProperty("#URL#", postPage.GenerateURL());
				ContentElement.AppendToProperty("#POSTS#", blogCard);
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

			Regex isImage = new Regex(@"^[a-z0-9\$\-_\.\+\!\*\'\(\)\,\\\/:]*\.(png|jpg|jpeg|gif)$", RegexOptions.IgnoreCase);
			Regex isElement = new Regex(@"^[ \t]*<.*", RegexOptions.IgnoreCase);

			ret.AppendToProperty("#TITLE#", p.Title);
			ret.AppendToProperty("#DATE#", p.Date.ToLongDateString());

			int curLines = 0;
			foreach (string s in p.Content)
			{
				if(s != null && s != "")
				{
					if(isImage.IsMatch(s))
					{
						ret.AppendToProperty("#CONTENT#", new LiteralElement(String.Format("<img src=\"{0}\" class=\"img-fluid\" alt=\"{1}\">\n", s, s)));
					}
					else if(isElement.IsMatch(s))
					{
						ret.AppendToProperty("#CONTENT#", new LiteralElement(s));
					}
					else
					{
						ret.AppendToProperty("#CONTENT#", new LiteralElement(String.Format("<p>{0}</p>\n", s)));
					}
					curLines++;
				}
				if(lineLimit > 0 && curLines >= lineLimit)
				{
					ret.AppendToProperty("#CONTENT#", new LiteralElement(String.Format("<a href=\"{0}\"><font color=#FFFFFF>Continue reading</font></a>", "#URL#")));
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
