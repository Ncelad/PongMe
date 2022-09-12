using PongMe.Model;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace PongMe.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = this.Email_TextBox.Text.Replace(" ", "");
            if (Validation(email,this.PasswordBox.Password))
            {
                User user = new User(email, Encoding.ASCII.GetBytes(this.PasswordBox.Password));
                User confirm = await UserRepository.ReadUsers(user.Email);
                if (confirm.Email.Replace(" ", "") == user.Email.Replace(" ", ""))
                {
                    if(Encoding.ASCII.GetString(confirm.Password) == Encoding.ASCII.GetString(user.Password))
                    {
                        new MainWindow(confirm).Show();
                        Application.Current.MainWindow.Close();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect password");
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect email!");
                }
            }
        }

        private void Register_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow as Authorization.Authorization).Page.Content = new Register();
        }

        public byte[] ImageToBytes(BitmapImage image)
        {
            using (var stream = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }

        public bool Validation(string email, string password)
        {

            if (!email.Contains("@gmail.com"))
            {
                MessageBox.Show("Email must contain \"@gmail.com\"!");
                return false;
            }
            if(string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password field cannot be empty!");
                return false;
            }

            return true;
        }
    }
}
