using PongMe.Model;
using PongMe.Pages;
using PongMe.View.Pages;
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
            SetAvatar(user);
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
            this.AddButon.Visibility = Visibility.Visible;
            this.Page.Content = new Home();
        }

        private void Avatar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.AddButon.Visibility = Visibility.Hidden;
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

        private void Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.AddButon.Visibility = Visibility.Hidden;
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserRepository.UpdateUser(UserViewModel.Instance.CurrentUser);
        }

        public void SetAvatar(User user)
        {
            if (user.Avatar == "user")
            {
                user.AvatarImage = new BitmapImage(new Uri($"Materials\\Avatars\\{user.Avatar}.png", UriKind.Relative));
            }
            else
            {
                user.AvatarImage = new BitmapImage(new Uri($"Materials\\Avatars\\{ user.Gender.ToLower().Replace("a", "e") }\\{user.Avatar}.png", UriKind.Relative));
            
            }
            this.Avatar.Source = user.AvatarImage;        
        }

        private void AddButon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.AddButon.Visibility = Visibility.Hidden;
            this.Page.Content = new CreateMatch();
        }
    }
}
