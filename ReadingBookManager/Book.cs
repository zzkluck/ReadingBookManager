using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReadingBookManager
{
	public class Book
	{
		public string BookName { get; set; }
		public string BookName2 { get; set; }
		public string Icon { get; set; }
		public string Position { get; set; }
		public int TotalPage { get; set; }
		public List<ReadingRecord> ReadingRecords = new List<ReadingRecord>();
		public int ReadPage{
			get
			{
				if (ReadingRecords.Count == 0)
					return 0;
				else
					return ReadingRecords[ReadingRecords.Count - 1].ReadPage;
			}
		}

		/// <summary>
		/// 从Book节点读取信息的通用方法
		/// </summary>
		/// <param name="BookElement"></param>
		/// <returns>返回值是一个Book类的实例</returns>
		public static Book ReadFromXml(XmlNode BookElement)
		{
			if (BookElement.Name != "Book")
				throw new ArgumentException("错误的Xml节点，请检查是否以正确的方式调用了Book.ReadFromXml函数");
			Book thisBook = new Book();
			thisBook.BookName = BookElement.Attributes["Name"].Value;
			thisBook.BookName2 = BookElement.Attributes["Name2"].Value;
			thisBook.TotalPage = Convert.ToInt32(BookElement.Attributes["TotalPage"].Value);
			thisBook.Position = BookElement.Attributes["Position"].Value;
			thisBook.Icon = BookElement.Attributes["Icon"].Value;

			XmlNodeList XmlReadingRecordings = BookElement.ChildNodes;
			foreach (XmlNode item in XmlReadingRecordings)
			{
				if(item.Name=="ReadingRecord")
					thisBook.ReadingRecords.Add(
						new ReadingRecord(Convert.ToInt32(item.Attributes["Page"].Value), 
											DateTime.Parse(item.Attributes["Date"].Value)));
			}
			return thisBook;
		}
	}

	public class ReadingRecord
	{
		public ReadingRecord(int r,DateTime d)
		{
			ReadPage = r;
			Date = d;
		}
		public int ReadPage;
		public DateTime Date;
	}
}
