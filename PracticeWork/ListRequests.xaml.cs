using GeneratingPeople;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace PracticeWork
{
    /// <summary>
    /// Логика взаимодействия для ListRequests.xaml
    /// </summary>
    public partial class ListRequests : Page
    {
        public ListRequests()
        {
            InitializeComponent();
            using (var connection = new SqliteConnection("Data Source=Requests.db"))
            {
                string sqlExpression = "DELETE FROM Requests";
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
            for (int x = 1; x < 201; x++)
            {
                new ConcretePeople();
            }
            ShowButtons();
            searchText.GotFocus += RemoveSearchText;
            searchText.LostFocus += AddSearchText;
        }
        public ListRequests(string text)
        {
            InitializeComponent();
            ShowButtons();
            searchText.GotFocus += RemoveSearchText;
            searchText.LostFocus += AddSearchText;
        }
        void ShowButtons()
        {
            using (var connection = new SqliteConnection("Data Source=Requests.db"))
            {
                int id = 1;
                string sqlExpression = "SELECT Name, Surname, Patronymic, Phone, Brand, CarNumber FROM Requests";
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string textButton = $"{id}) {reader.GetValue(1).ToString()} {reader.GetValue(0).ToString()} {reader.GetValue(2).ToString()} {reader.GetString(5)}";
                            Button Button = new Button();
                            Button.Width = 1000;
                            Button.Height = 60;
                            Button.HorizontalContentAlignment = HorizontalAlignment.Left;
                            Button.Margin = new Thickness(0, 5, 0, 0);
                            Button.Name = "requestButton";
                            Button.Content = textButton;
                            Button.HorizontalAlignment = HorizontalAlignment.Center;
                            panelRequests.Children.Add(Button);
                            Button.Click += ShowProfile;
                            id++;
                        }
                    }
                }
            }
        }
        void RemoveSearchText(object sender, RoutedEventArgs e)
        {
            if (searchText.Text == "Поиск заявки")
            {
                searchText.Text = string.Empty;
                searchText.Foreground = Brushes.Black;
            } 
        }
        void AddSearchText(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchText.Text))
            {
                searchText.Foreground = Brushes.Gray;
                searchText.Text = "Поиск заявки";
            }
            ShowButtons();
        }
        void ShowProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new ProfileRequest(int.Parse((sender as Button).Content.ToString().Split(" ")[0].Split(")")[0]));
        }
        void StartSearch(object sender, RoutedEventArgs e)
        {
            panelRequests.Children.Clear();
            ShowButtons();
            if (searchText.Text == "")
            {
                panelRequests.Children.Clear();
                ShowButtons();
            }
            else
            {
                int value = 0;
                foreach (var child in panelRequests.Children.OfType<Button>().ToList())
                {
                    string contentButton = child.ToString().Split(") ")[1].ToLower();
                    if (contentButton.Contains(searchText.Text.ToLower()))
                    {
                        value++;
                    }
                    else
                    {
                        panelRequests.Children.RemoveAt(value);
                    }
                }
            }
        }
    }
}
