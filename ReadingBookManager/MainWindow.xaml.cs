﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
			Main.DataContext = MyBookManager.ReadingBooks;
			StateBar.DataContext = MyBookManager;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"\\ReadingBooks.xml";
			XmlDocument xd = new XmlDocument();
			xd.Load(path);
			XmlElement rootNode = xd.DocumentElement;
			XmlNodeList Books = rootNode.GetElementsByTagName("Book");

			foreach (XmlNode node in Books)
			{
				Book book = Book.ReadFromXml(node);
				MyBookManager.ReadingBooks.Add(book);
			}
			MyBookManager.Flush();
			
		}

	}
}
