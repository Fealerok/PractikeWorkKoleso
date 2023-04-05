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
using System.Windows.Shapes;

namespace PracticeWork
{
    /// <summary>
    /// Логика взаимодействия для PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public string password { get; set; }
        public PasswordWindow()
        {
            InitializeComponent();
        }
        void SavePassword(object sender, RoutedEventArgs e)
        {
            if (passwordtext.Text.ToLower() == "пароль")
            {
                password = "пароль";
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный пароль. Попробуйте еще раз", "Ок");
            }
        }
    }
}
