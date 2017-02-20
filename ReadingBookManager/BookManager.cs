using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingBookManager
{
	public class BookManager:Zzkluck.CSharpTools.BindableBase
	{
		public ObservableCollection<Book> ReadingBooks=new ObservableCollection<Book>();

		private string totalPage;

		public string TotalPage
		{
			get { return totalPage; }
			set { SetProperty(ref totalPage, value); }
		}

		private string totalReadPage;

		public string TotalReadPage
		{
			get { return totalReadPage; }
			set { SetProperty(ref totalReadPage, value); }
		}

		private string rateOfRead;

		public string RateOfRead
		{
			get { return rateOfRead; }
			set { SetProperty(ref rateOfRead, value); }
		}


		public void Flush()
		{
			int tp=0, trp=0;
			foreach (Book book in ReadingBooks)
			{
				tp += Convert.ToInt32(book.totalPage);
				trp += Convert.ToInt32(book.ReadPage);
			}
			TotalPage = tp.ToString();
			TotalReadPage = trp.ToString();
			RateOfRead=string.Format("( {0} / {1} ）", trp, tp);
		}


	}
}
