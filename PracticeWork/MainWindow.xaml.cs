using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;
using System.IO;
using System;
using GeneratingPeople;

namespace PracticeWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new ListRequests(); 
        }
    }
}
