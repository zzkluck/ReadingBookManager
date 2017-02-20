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
		public List<ReadingRecord> ReadingRecords=new List<ReadingRecord>();
		private string totalPage;
		public string BookName { get; set; }
		public string BookName2 { get; set; }
		public string Icon { get; set; }
		public string Position { get; set; }
		public string ReadPage{
			get
			{
				if (ReadingRecords.Count == 0)
					return "0";
				else
					return ReadingRecords[ReadingRecords.Count - 1].ReadPage;
			}
		}
		public string RateOfProgress {
			get
			{
				return string.Format("( {0} / {1} ）", ReadPage, totalPage);
			}
		}

		public static Book ReadFromXml(XmlNode BookElement)
		{
			if (BookElement.Name != "Book")
				throw new ArgumentException();
			Book thisBook = new Book();
			thisBook.BookName = BookElement.Attributes["Name"].Value;
			thisBook.BookName2 = BookElement.Attributes["Name2"].Value;
			thisBook.totalPage = BookElement.Attributes["TotalPage"].Value;
			thisBook.Position = BookElement.Attributes["Position"].Value;

			XmlNodeList XmlReadingRecordings = BookElement.ChildNodes;
			foreach (XmlNode item in XmlReadingRecordings)
			{
				thisBook.ReadingRecords.Add(
					new ReadingRecord(item.Attributes["Page"].Value, item.Attributes["Date"].Value));
			}
			return thisBook;
		}
	}

	public class ReadingRecord
	{
		public ReadingRecord(string r,string d)
		{
			ReadPage = r;
			Date = d;
		}
		public string ReadPage;
		public string Date;
	}
}
