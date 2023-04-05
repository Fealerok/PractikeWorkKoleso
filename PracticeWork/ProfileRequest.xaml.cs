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
using Microsoft.Data.Sqlite;

namespace PracticeWork
{
    /// <summary>
    /// Логика взаимодействия для ProfileRequest.xaml
    /// </summary>
    public partial class ProfileRequest : Page
    {
        bool isEditMode = true;
        public string nameUser { get; set; }
        public string surnameUser { get; set; }
        public string patronymicUser { get; set; }
        public long phoneUser { get; set; }
        public string brand { get; set; }
        public string carNumber { get; set; }
        public long price { get; set; }
        public string breakdown { get; set; }
        public int Rowid { get; set; }
        public ProfileRequest()
        {
            InitializeComponent();
        }
        public ProfileRequest(int id)
        {
            int randomNumber = new Random().Next(0, 91);
            Rowid = id;
            InitializeComponent();
            using (var connection = new SqliteConnection("Data Source=Requests.db"))
            {
                connection.Open();
                string sqlExpression = $"VACUUM";
                SqliteCommand command2 = new SqliteCommand(sqlExpression, connection);
                command2.Connection = connection;
                command2.ExecuteNonQuery();
                sqlExpression = $"SELECT * FROM Requests WHERE rowid={Rowid}";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.Connection = connection;
                command.ExecuteNonQuery();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            nameUser = reader.GetString(0);
                            surnameUser = reader.GetString(1);
                            patronymicUser = reader.GetString(2);
                            phoneUser = reader.GetInt64(3);
                            brand = reader.GetString(4);
                            carNumber = reader.GetString(5);
                            breakdown = reader.GetString(6);
                            price = reader.GetInt64(7);
                        }
                    }
                }
            }
            nameText.Text = nameUser;
            surnameText.Text = surnameUser;
            patronymicText.Text = patronymicUser;
            phoneText.Text = phoneUser.ToString();
            brandText.Text = brand;
            carNumberText.Text = carNumber;
            breakdownText.Text = breakdown;
            priceText.Text = $"{price}";
            if (randomNumber >= 0 && randomNumber <= 30)
            {
                statusText.Text = "Готово";
                statusText.Foreground = Brushes.LawnGreen;
            }
            if (statusText.Text == "Готово")
            {
                deleteButton.Visibility= Visibility.Hidden;
                editButton.Visibility= Visibility.Hidden;
                backButton.Visibility= Visibility.Hidden;
                takeCarButton.Visibility= Visibility.Visible;
            }
        }
        void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new ListRequests("Back");
        }
        void TakeCar(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Спасибо за использование наших услуг! Заявка будет автоматически удалена.");
            using (var connection = new SqliteConnection("Data Source=Requests.db"))
            {
                connection.OpenAsync();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"VACUUM";
                command.ExecuteNonQuery();
                string sqlExpression = $"DELETE FROM Requests WHERE rowid={Rowid}";
                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
                NavigationService.Content = new ListRequests("Back");
            }
        }
        void Edit(object sender, RoutedEventArgs e)
        {
            var win = new PasswordWindow();
            win.ShowDialog();
            if (win.password == "пароль")
            {
                if (isEditMode == true)
                {
                    deleteButton.Visibility = Visibility.Hidden;
                    backButton.Visibility = Visibility.Hidden;
                    nameText.IsReadOnly = false;
                    surnameText.IsReadOnly = false;
                    patronymicText.IsReadOnly = false;
                    brandText.IsReadOnly = false;
                    carNumberText.IsReadOnly = false;
                    phoneText.IsReadOnly = false;
                    breakdownText.IsReadOnly = false;
                    priceText.IsReadOnly = false;
                    isEditMode = false;

                    nameText.Cursor = Cursors.IBeam;
                    surnameText.Cursor = Cursors.IBeam;
                    patronymicText.Cursor = Cursors.IBeam;
                    brandText.Cursor = Cursors.IBeam;
                    carNumberText.Cursor = Cursors.IBeam;
                    phoneText.Cursor = Cursors.IBeam;
                    breakdownText.Cursor = Cursors.IBeam;
                    priceText.Cursor = Cursors.IBeam;
                    editButton.Content = "Сохранить";
                }
                else
                {
                    deleteButton.Visibility = Visibility.Visible;
                    backButton.Visibility = Visibility.Visible;
                    nameText.IsReadOnly = true;
                    surnameText.IsReadOnly = true;
                    patronymicText.IsReadOnly = true;
                    brandText.IsReadOnly = true;
                    carNumberText.IsReadOnly = true;
                    phoneText.IsReadOnly = true;
                    breakdownText.IsReadOnly = true;
                    priceText.IsReadOnly = true;
                    isEditMode = false;

                    nameText.Cursor = Cursors.Arrow;
                    surnameText.Cursor = Cursors.Arrow;
                    patronymicText.Cursor = Cursors.Arrow;
                    brandText.Cursor = Cursors.Arrow;
                    carNumberText.Cursor = Cursors.Arrow;
                    phoneText.Cursor = Cursors.Arrow;
                    breakdownText.Cursor = Cursors.Arrow;
                    priceText.Cursor = Cursors.Arrow;
                    isEditMode = true;
                    editButton.Content = "Редактировать";
                    using (var connection = new SqliteConnection("Data Source=Requests.db"))
                    {
                        connection.OpenAsync();
                        SqliteCommand command = new SqliteCommand();
                        command.Connection = connection;
                        string sqlExpression = $"UPDATE Requests SET Name='{nameText.Text}', Surname='{surnameText.Text}', Patronymic='{patronymicText.Text}'," +
                            $"Brand='{brandText.Text}', CarNumber='{carNumberText.Text}', Phone='{phoneText.Text}', Breakdown='{breakdownText.Text}', Price='{priceText.Text}' WHERE rowid={Rowid}";
                        command.CommandText = sqlExpression;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        void Delete(object sender, RoutedEventArgs e)
        {
            var win = new PasswordWindow();
            win.ShowDialog();
            if (win.password == "пароль")
            {
                using (var connection = new SqliteConnection("Data Source=Requests.db"))
                {
                    connection.OpenAsync();
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"VACUUM";
                    command.ExecuteNonQuery();

                    string sqlExpression = $"DELETE FROM Requests WHERE rowid={Rowid}";
                    command.CommandText = sqlExpression;
                    command.ExecuteNonQuery();
                    NavigationService.Content = new ListRequests("Back");
                }
            }
        }
    }
}
