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
		public static BookManager MyBookManager = new BookManager();
        int i = 0;
        public MainWindow()
		{
			InitializeComponent();
			StatusRateOfRead.DataContext = MyBookManager;
			SplashScreen ss = new SplashScreen("Loading.png");
			ss.Show(true);
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			MyBookManager.ReadFromXml();
			MyBookManager.GetReadingRecordByPage();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
            if (i++ % 2 == 0)
                FrameMain.Navigate(new BookListView());
            else
                FrameMain.Navigate(new BookListBox());
        }
        public void ButtonDrived_FrameNavigate(object sender, RoutedEventArgs e)
        {
            Button senderButton = sender as Button;
            switch (senderButton.Name)
            {
                case "ReadingBookManagerNavigateButton":
                    FrameMain.Navigate(new BookListBox());
                    break;
                case "PhysicsSimulationNavigateButton":
                    FrameMain.Navigate(new PhysicsSimulation());
                    break;
                default:
                    break;
            }
        }
    }

}

