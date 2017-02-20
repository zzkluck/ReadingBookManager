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
		private string readPage;
		private string totalPage;
		public string BookName { get; set; }
		public string BookName2 { get; set; }
		public string Icon { get; set; }
		public string Position { get; set; }
		public string RateOfProgress {
			get
			{
				return string.Format("( {0} / {1} ）", readPage, totalPage);
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
			thisBook.readPage = BookElement.Attributes["ReadPage"].Value;
			thisBook.Position = BookElement.Attributes["Position"].Value;
			return thisBook;
		}
	}
}
