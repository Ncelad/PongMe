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
            }
            this.MatchesListBox.ItemsSource = MatchViewModel.Instance.Matches;
        }
    }
}
