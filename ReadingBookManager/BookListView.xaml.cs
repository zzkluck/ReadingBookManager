using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ReadingBookManager
{
    /// <summary>
    /// BookListView.xaml 的交互逻辑
    /// </summary>
    public partial class BookListView : Page
    {
        public BookListView()
        {
            InitializeComponent();
            LstVwReadingRecords.DataContext = MainWindow.MyBookManager.ReadingRecordsList;
        }
        private void ButtonHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumn gvc = (e.OriginalSource as GridViewColumnHeader).Column;
            switch (gvc.Header.ToString())
            {
                case "BookName":
                    LstVwReadingRecords.DataContext =
                MainWindow.MyBookManager.ReadingRecordsList.OrderBy((ReadingRecordByPage r) => r.BookName);
                    break;
                case "Page":
                    LstVwReadingRecords.DataContext =
                MainWindow.MyBookManager.ReadingRecordsList.OrderBy((ReadingRecordByPage r) => r.ReadPage);
                    break;
                case "Date":
                    LstVwReadingRecords.DataContext =
                MainWindow.MyBookManager.ReadingRecordsList.OrderBy((ReadingRecordByPage r) => r.Date);
                    break;
                default:
                    throw new Exception("Somethins error");
            }
            }
        }


}
