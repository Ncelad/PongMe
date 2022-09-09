using PongMe.ViewModel;
using System;
using System.Collections.Generic;
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

namespace PongMe.Pages
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            this.DataContext = UserViewModel.Instance;
            this.AgeTextBox.Text = UserViewModel.Instance.CurrentUser.Age.ToString();
            (this.FindName(UserViewModel.Instance.CurrentUser.Gender) as RadioButton).IsChecked = true;
        }

        private void Man_Checked(object sender, RoutedEventArgs e)
        {
            UserViewModel.Instance.CurrentUser.Gender = "Man";
            if (!UserViewModel.Instance.CurrentUser.Avatar.Contains("man-") || UserViewModel.Instance.CurrentUser.Avatar.Contains("wo"))
            {
                UserViewModel.Instance.CurrentUser.Avatar = "man-1";
                this.Avatar.ImageSource = new BitmapImage(new Uri($"..\\..\\Materials\\Avatars\\men\\man-1.png", UriKind.Relative));
                (App.Current.Windows[0] as MainWindow).SetAvatar(UserViewModel.Instance.CurrentUser);
            }
        }

        private void Woman_Checked(object sender, RoutedEventArgs e)
        {
            UserViewModel.Instance.CurrentUser.Gender = "Woman";
            if (!UserViewModel.Instance.CurrentUser.Avatar.Contains("woman-"))
            {
                UserViewModel.Instance.CurrentUser.Avatar = "woman-1";
                this.Avatar.ImageSource = new BitmapImage(new Uri($"..\\..\\Materials\\Avatars\\women\\woman-1.png", UriKind.Relative));
                (App.Current.Windows[0] as MainWindow).SetAvatar(UserViewModel.Instance.CurrentUser);
            }
        }

        private void None_Checked(object sender, RoutedEventArgs e)
        {
            UserViewModel.Instance.CurrentUser.Gender = "None";
            if (UserViewModel.Instance.CurrentUser.Avatar != "user")
            {
                UserViewModel.Instance.CurrentUser.Avatar = "user";
                this.Avatar.ImageSource = new BitmapImage(new Uri($"..\\..\\Materials\\Avatars\\user.png", UriKind.Relative));
                (App.Current.Windows[0] as MainWindow).SetAvatar(UserViewModel.Instance.CurrentUser);
            }
        }

        private void AgeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool IsNumeric = new Regex("[0-9]+").IsMatch(e.Text);
            if (!IsNumeric)
            {
                e.Handled = !IsNumeric;
            }
            else if(int.Parse((sender as TextBox).Text + e.Text) > this.AgeSlider.Maximum)
            {
                e.Handled = IsNumeric;
            }
        }

        private void AgeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.AgeTextBox.Text == "")
            {
                this.AgeTextBox.Text = Convert.ToInt32(this.AgeSlider.Value).ToString();
            }
            this.AgeTextBox.Text = this.AgeTextBox.Text.Split(".".ToCharArray())[0];
            UserViewModel.Instance.CurrentUser.Age = int.Parse(this.AgeTextBox.Text);
        }

        private void ChangeAvatar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string path = "..\\..\\Materials\\Avatars\\";
            if (UserViewModel.Instance.CurrentUser.Gender == "Man")
            {
                path += "men\\";
                string avatar_name = UserViewModel.Instance.CurrentUser.Avatar;
                if (avatar_name.Contains("man-"))
                {
                    int avatar_id = Convert.ToInt32(avatar_name.Replace("man-",""));
                    if(avatar_id == 10)
                    {
                        UserViewModel.Instance.CurrentUser.Avatar = "man-1";
                    }
                    else
                    {
                        UserViewModel.Instance.CurrentUser.Avatar = avatar_name.Replace(avatar_name.Replace("man-", ""), "") + $"{++avatar_id}";
                    }
                    this.Avatar.ImageSource = new BitmapImage(new Uri(path + UserViewModel.Instance.CurrentUser.Avatar + ".png", UriKind.Relative));
                    (App.Current.Windows[0] as MainWindow).SetAvatar(UserViewModel.Instance.CurrentUser);
                }
                else
                {
                    UserViewModel.Instance.CurrentUser.Avatar = "man-1";
                    this.Avatar.ImageSource = new BitmapImage(new Uri(path + "man-1.png", UriKind.Relative));
                    (App.Current.Windows[0] as MainWindow).SetAvatar(UserViewModel.Instance.CurrentUser);
                }
            }
            else if (UserViewModel.Instance.CurrentUser.Gender == "Woman")
            {
                path += "women\\";
                string avatar_name = UserViewModel.Instance.CurrentUser.Avatar;
                if (avatar_name.Contains("woman-"))
                {
                    int avatar_id = Convert.ToInt32(avatar_name.Replace("woman-", ""));
                    if (avatar_id == 10)
                    {
                        UserViewModel.Instance.CurrentUser.Avatar = "woman-1";
                    }
                    else
                    {
                        UserViewModel.Instance.CurrentUser.Avatar = avatar_name.Replace(avatar_name.Replace("woman-", ""), "") + $"{++avatar_id}";
                    }
                    this.Avatar.ImageSource = new BitmapImage(new Uri(path + UserViewModel.Instance.CurrentUser.Avatar + ".png", UriKind.Relative));
                    (App.Current.Windows[0] as MainWindow).SetAvatar(UserViewModel.Instance.CurrentUser);
                }
                else
                {
                    UserViewModel.Instance.CurrentUser.Avatar = "woman-1";
                    this.Avatar.ImageSource = new BitmapImage(new Uri(path + "woman-1.png", UriKind.Relative));
                    (App.Current.Windows[0] as MainWindow).SetAvatar(UserViewModel.Instance.CurrentUser);
                }
            }
            else if(UserViewModel.Instance.CurrentUser.Gender == "None")
            {
                UserViewModel.Instance.CurrentUser.Avatar = "user";
                this.Avatar.ImageSource = new BitmapImage(new Uri(path + "user.png", UriKind.Relative));
                (App.Current.Windows[0] as MainWindow).SetAvatar(UserViewModel.Instance.CurrentUser);
            }
        }
    }
}
