using PongMe.Model;
using PongMe.ViewModel;
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

namespace PongMe.Pages
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            this.DataContext = MatchViewModel.Instance;
            MatchViewModel.Instance = new MatchViewModel();
            this.Search_TextBox.GotFocus += RemoveText;
            this.Search_TextBox.LostFocus += AddText;
            this.Search_TextBox.Foreground = new SolidColorBrush(Colors.Gray);
            this.Search_TextBox.Text = "Search matches by place";
            GetMatches();
        }

        private async void GetMatches()
        {
            var result = await MatchRepository.ReadMatches();
            if(result == null)
            {
                MatchViewModel.Instance.Matches = new List<Match>();
            }
            else
            {
                MatchViewModel.Instance.Matches = result;
            }
            foreach (var item in MatchViewModel.Instance.Matches)
            {
                item.Creator = await UserRepository.ReadUsers(item.CreatorId);
                if (item.Creator.Avatar == "user")
                {
                    item.Creator.AvatarImage = new BitmapImage(new Uri($"..\\..\\Materials\\Avatars\\{item.Creator.Avatar}.png", UriKind.Relative));
                }
                else
                {
                    item.Creator.AvatarImage = new BitmapImage(new Uri($"..\\..\\Materials\\Avatars\\{ item.Creator.Gender.ToLower().Replace("a", "e") }\\{item.Creator.Avatar}.png", UriKind.Relative));

                }
            }
            this.MatchesListBox.ItemsSource = MatchViewModel.Instance.Matches;
        }

        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.Search_TextBox.Text))
            {
                this.Search_TextBox.Foreground = new SolidColorBrush(Colors.Gray);
                this.Search_TextBox.Text = "Search matches by place";
            }

        }

        public void RemoveText(object sender, EventArgs e)
        {
            if (this.Search_TextBox.Text == "Search matches by place")
            {
                this.Search_TextBox.Foreground = new SolidColorBrush(Colors.Black);
                this.Search_TextBox.Text = "";
            }
        }

        private async void Search_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Match> matches = new List<Match>();
                if (!string.IsNullOrWhiteSpace((sender as TextBox).Text))
                {
                    matches = await MatchRepository.ReadMatches(new Match() { Place = (sender as TextBox).Text });
                }
                if(matches.Count != 0 && matches[0].Place != null)
                {
                    foreach (var item in matches)
                    {
                        item.Creator = await UserRepository.ReadUsers(item.CreatorId);
                        if (item.Creator.Avatar == "user")
                        {
                            item.Creator.AvatarImage = new BitmapImage(new Uri($"..\\..\\Materials\\Avatars\\{item.Creator.Avatar}.png", UriKind.Relative));
                        }
                        else
                        {
                            item.Creator.AvatarImage = new BitmapImage(new Uri($"..\\..\\Materials\\Avatars\\{ item.Creator.Gender.ToLower().Replace("a", "e") }\\{item.Creator.Avatar}.png", UriKind.Relative));

                        }
                    }
                    this.MatchesListBox.ItemsSource = matches;
                }
                else
                {
                    this.MatchesListBox.ItemsSource = new List<Match>();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
