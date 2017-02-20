using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingBookManager
{
	public class BookManager
	{
		public ObservableCollection<Book> ReadingBooks=new ObservableCollection<Book>();
		public string TotalPage { get; set; }
		public string TotalReadPage { get; set; }

	}
}
