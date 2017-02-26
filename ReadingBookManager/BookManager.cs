using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Zzkluck.CSharpTools;

namespace ReadingBookManager
{
	public class BookManager:BindableBase
	{
		static string path = 
			Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\ReadingBooks.xml";
		public string User { get; set; }

		public ObservableCollection<Book> ReadingBooks=new ObservableCollection<Book>();

		private int totalPage;
		private int totalReadPage;
		private string rateOfRead;
		public int TotalPage
		{
			get { return totalPage; }
			set { SetProperty(ref totalPage, value); }
		}
		public int TotalReadPage
		{
			get { return totalReadPage; }
			set { SetProperty(ref totalReadPage, value); }
		}
		public string RateOfRead
		{
			get { return rateOfRead; }
			set { SetProperty(ref rateOfRead, value); }
		}

		public void Flush()
		{
			//刷新TotalPage,TotalReadPage,RateOfRead的值
			int tp=0, trp=0;
			foreach (Book book in ReadingBooks)
			{
				tp += book.TotalPage;
				trp += book.ReadPage;
			}
			TotalPage = tp;
			TotalReadPage = trp;
			RateOfRead = string.Format("( {0} / {1} ）", trp, tp);
		}

		public void ReadFromXml()
		{
			XmlDocument xd = new XmlDocument();
			xd.Load(path);
			XmlElement rootNode = xd.DocumentElement;
			if (rootNode.Name != "Books")
			{
				throw new ArgumentException("错误的Xml文件，请检查引用的数据文件是否是制定的格式");
			}
			User = rootNode.Attributes["User"].Value;

			XmlNodeList Books = rootNode.GetElementsByTagName("Book");
			foreach (XmlNode node in Books)
			{
				this.ReadingBooks.Add(Book.ReadFromXml(node));
			}
			this.Flush();
		}


	}
}
