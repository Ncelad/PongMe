using PongMe.Pages;
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

namespace PongMe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<string,Page> pages { get; private set; } = new Dictionary<string, Page>();

        public MainWindow()
        {
            InitializeComponent();
            pages.Add(typeof(Home).Name,new Home());
            this.Page.Content = pages["Home"];
            this.Close.Width /= 2;
            this.Close.Height /= 2;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
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
                this.Page.Content = pages["Example"];
            }
            else
            {
                pages.Add(typeof(Example).Name, new Example());
                this.Page.Content = pages["Example"];
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
    }
}
