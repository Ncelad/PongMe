using PongMe.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PongMe.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
            this.Name_TextBox.GotFocus += RemoveText;
            this.Name_TextBox.LostFocus += AddText;
            this.Name_TextBox.Foreground = new SolidColorBrush(Colors.Gray);
            this.Name_TextBox.Text = "Name Surname";

            this.Email_TextBox.GotFocus += RemoveText;
            this.Email_TextBox.LostFocus += AddText;
            this.Email_TextBox.Foreground = new SolidColorBrush(Colors.Gray);
            this.Email_TextBox.Text = "Email(example@gmail.com)";
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = this.Name_TextBox.Text.Split(' ')[0].Replace(" ", "");
            string surname = this.Name_TextBox.Text.Split(' ')[1].Replace(" ", "");
            string email = this.Email_TextBox.Text.Replace(" ", "");

            if (Validation(name,surname,email))
            {
                User user = new User(name, surname, email, Encoding.ASCII.GetBytes(this.PasswordBox.Password), "user");
                UserRepository.CreateUser(user);
                new MainWindow(await UserRepository.ReadUsers(user.Email)).Show();
                Application.Current.MainWindow.Close();
            }
        }


        private void Login_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow as Authorization.Authorization).Page.Content = new Login();
        }

        public bool Validation(string name, string surname, string email)
        {

            Regex regex = new Regex("[A-Za-z]");


            foreach (var c in name)
            {
                if (!regex.IsMatch(c.ToString()))
                {
                    MessageBox.Show("Name must contain only letters!");
                    return false;
                }
            }

            foreach (var c in surname)
            {
                if (!regex.IsMatch(c.ToString()))
                {
                    MessageBox.Show("Surname must contain only letters!");
                    return false;
                }
            }

            if (!email.Contains("@gmail.com"))
            {
                MessageBox.Show("Email must contain \"@gmail.com\"!");
                return false;
            }

            return true;
        }

        public void AddText(object sender, EventArgs e)
        {
            var textBox = (sender as TextBox);
            if (textBox.Name == "Name_TextBox" && string.IsNullOrWhiteSpace(this.Name_TextBox.Text))
            {
                this.Name_TextBox.Foreground = new SolidColorBrush(Colors.Gray);
                this.Name_TextBox.Text = "Name Surname";
            }
            else if (textBox.Name == "Email_TextBox" && string.IsNullOrWhiteSpace(this.Email_TextBox.Text))
            {
                this.Email_TextBox.Foreground = new SolidColorBrush(Colors.Gray);
                this.Email_TextBox.Text = "Email(example@gmail.com)";
            }

        }

        public void RemoveText(object sender, EventArgs e)
        {
            var textBox = (sender as TextBox);
            if (textBox.Name == "Name_TextBox" && this.Name_TextBox.Text == "Name Surname")
            {
                this.Name_TextBox.Foreground = new SolidColorBrush(Colors.Black);
                this.Name_TextBox.Text = "";
            }
            else if (textBox.Name == "Email_TextBox" && this.Email_TextBox.Text == "Email(example@gmail.com)")
            {
                this.Email_TextBox.Foreground = new SolidColorBrush(Colors.Black);
                this.Email_TextBox.Text = "";
            }
        }
    }
}
