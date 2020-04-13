using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlanDuongCom
{
	class BlogPost : IComparable<BlogPost>
	{
		public string Title;
		public DateTime Date;
		public string[] Tags;
		public string[] Content;

		public BlogPost(string path)
		{
			Load(path);
		}

		public bool Load(string path)
		{
			if(!File.Exists(path)) { return false; }

			string[] lines = File.ReadAllLines(path);

			if(lines.Count() < 3) { return false; }

			Date = DateTime.FromFileTimeUtc(long.Parse(lines[0]));
			Title = lines[1];
			Tags = lines[2].Split(',');
			
			//clean the tags
			for(int i = 0; i < Tags.Count(); i++)
			{
				Tags[i] = Tags[i].Trim().ToLower();
			}

			Content = lines.Skip(3).ToArray();

			return true;
		}

		//sort by date then title
		public int CompareTo(BlogPost other)
		{
			if(Date.CompareTo(other.Date) != 0)
			{
				return Date.CompareTo(other.Date);
			}
			else
			{
				return Title.CompareTo(other.Title);
			}
		}
	}
}
