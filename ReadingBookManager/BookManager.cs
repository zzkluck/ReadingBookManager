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

		public ObservableCollection<Book> 
			ReadingBooks = new ObservableCollection<Book>();
		public ObservableCollection<ReadingRecordByPage> 
			ReadingRecordsList = new ObservableCollection<ReadingRecordByPage>();

		private int totalPage;
		private int totalReadPage;
		private string rateOfRead;

		#region Functions
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

		#endregion

		public void GetReadingRecordByPage()
		{
			foreach(Book book in ReadingBooks)
			{
				if(book.ReadingRecords.Count!=0)
					ReadingRecordsList.Add(new ReadingRecordByPage(book.ReadingRecords[0], book.BookName));
				for(int i = 1; i < book.ReadingRecords.Count; i++)
				{
					ReadingRecordsList.Add(new ReadingRecordByPage
						(book.ReadingRecords[i - 1], book.ReadingRecords[i], book.BookName));
				}
			}
		}
	}

	public class ReadingRecordByPage
	{
		public ReadingRecordByPage(DateTime date,int readPage,string bookName)
		{
			this.Date = date;
			this.ReadPage = readPage;
			this.BookName = bookName;
		}
		public ReadingRecordByPage(ReadingRecordByBook bookRecord1,ReadingRecordByBook bookRecord2, string bookName)
			:this(bookRecord2.Date, bookRecord2.ReadPage - bookRecord1.ReadPage, bookName)
		{}
		public ReadingRecordByPage(ReadingRecordByBook firstBookRecord,string bookName):
			this(firstBookRecord.Date,firstBookRecord.ReadPage,bookName)
		{}
		public DateTime Date { get; set; }
		public string DateString { get { return Date.ToShortDateString(); } }
		public int ReadPage { get; set; }
		public string BookName { get; set; }
	}
}
