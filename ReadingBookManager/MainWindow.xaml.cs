using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace ReadingBookManager
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		BookManager MyBookManager = new BookManager();
		public MainWindow()
		{
			InitializeComponent();
			LstBxBooks.DataContext = MyBookManager.ReadingBooks;
			LstVwReadingRecords.DataContext = MyBookManager.ReadingRecordsList;
			StateBar.DataContext = MyBookManager;
			SplashScreen ss = new SplashScreen("Loading.png");
			ss.Show(true);
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			MyBookManager.ReadFromXml();
			MyBookManager.GetReadingRecordByPage();
		}

		private void ButtonHeader_Click(object sender, RoutedEventArgs e)
		{
            GridViewColumn gvc = (e.OriginalSource as GridViewColumnHeader).Column;
			switch (gvc.Header.ToString())
			{
				case "BookName":
					LstVwReadingRecords.DataContext =
				MyBookManager.ReadingRecordsList.OrderBy((ReadingRecordByPage r) => r.BookName);
					break;
				case "Page":
					LstVwReadingRecords.DataContext =
				MyBookManager.ReadingRecordsList.OrderBy((ReadingRecordByPage r) => r.ReadPage);
					break;
				case "Date":
					LstVwReadingRecords.DataContext =
				MyBookManager.ReadingRecordsList.OrderBy((ReadingRecordByPage r) => r.Date);
					break;
				default:
					throw new Exception("Somethins error");
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				LstBxBooks.Visibility = (LstBxBooks.Visibility == Visibility) ? Visibility.Collapsed : Visibility.Visible;
				LstVwReadingRecords.Visibility = (LstVwReadingRecords.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
			}
		}
    }
}

