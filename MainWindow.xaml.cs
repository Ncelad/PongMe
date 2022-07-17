using PongMe.Model;
using PongMe.Pages;
using PongMe.ViewModel;
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

namespace PongMe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<string,Page> pages { get; private set; } = new Dictionary<string, Page>();

        public MainWindow(User user)
        {
            InitializeComponent();
            UserViewModel.Instance = new UserViewModel();
            UserViewModel.Instance.CurrentUser = user;
            this.DataContext = UserViewModel.Instance;
            pages.Add(typeof(Home).Name,new Home());
            this.Page.Content = pages["Home"];
            this.Close.Width /= 2;
            this.Close.Height /= 2;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                if (e.ChangedButton == MouseButton.Left)
                    this.DragMove();
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        //-------------------------------------------------------------------

        private void Logo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (pages.ContainsKey("Home"))
            {
                this.Page.Content = pages["Home"];
            }
            else
            {
                pages.Add(typeof(Home).Name, new Home());
                this.Page.Content = pages["Home"];
            }
        }

        private void Avatar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (pages.ContainsKey("Example"))
            {
                this.Page.Content = pages["Settings"];
            }
            else
            {
                pages.Add(typeof(Settings).Name, new Settings());
                this.Page.Content = pages["Settings"];
            }
        }

        private void Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (pages.ContainsKey("Settings"))
            {
                this.Page.Content = pages["Settings"];
            }
            else
            {
                pages.Add(typeof(Settings).Name, new Settings());
                this.Page.Content = pages["Settings"];
            }
        }

        private void Faq_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string destinationurl = "https://github.com/Ncelad/PongMe/blob/main/README.md";
            var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
            {
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(sInfo);
        }

        //public BitmapImage LoadImage(byte[] imageData)
        //{
        //    var img = new BitmapImage();
        //    using (MemoryStream stream = new MemoryStream(imageData))
        //    {
        //        try
        //        {
        //            img.BeginInit();

        //            img.CacheOption = BitmapCacheOption.OnLoad;
        //            img.StreamSource = stream;
        //            img.DecodePixelWidth = ;

        //            img.EndInit();

        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}
