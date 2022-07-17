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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow(new User("Milosh", "Numerov", "mishnum@gmail.com", Encoding.ASCII.GetBytes("milosh1234"), ImageToBytes(new BitmapImage(new Uri("../../Materials/Icons/logo.png", UriKind.Relative))))).Show();
            Application.Current.MainWindow.Close();
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

        private void Register_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow as Authorization.Authorization).Page.Content = new Register();
        }
    }
}
