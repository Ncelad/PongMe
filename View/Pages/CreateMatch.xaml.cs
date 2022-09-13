using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PongMe.Model;
using PongMe.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// Логика взаимодействия для CreateMatch.xaml
    /// </summary>
    public partial class CreateMatch : Page
    {
        public CreateMatch()
        {
            InitializeComponent();
            this.Place_TextBox.GotFocus += RemoveText;
            this.Place_TextBox.LostFocus += AddText;
            this.Place_TextBox.Foreground = new SolidColorBrush(Colors.Gray);
            this.Place_TextBox.Text = "Place(Name or Adress)";
            this.Time_TextBox.GotFocus += RemoveText;
            this.Time_TextBox.LostFocus += AddText;
            this.Time_TextBox.FontSize = 14;
            this.Time_TextBox.Foreground = new SolidColorBrush(Colors.Gray);
            this.Time_TextBox.Text = "Time(day/month/year hours:minutes AM/PM)";
        }
        

        private void Submit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(DateTime.TryParse(this.Time_TextBox.Text, out DateTime Time))
            {
                MatchRepository.CreateMatch(new Match(this.Place_TextBox.Text, Time, UserViewModel.Instance.CurrentUser));
            }
            else
            {
                MessageBox.Show("Incorrect format of Time and Date!\r\nThe Format: month/day/year hours:minutes (AM/PM)");
            }
        }

        private async Task<List<string>> GetAdresses()
        {
            List<string> adresses = new List<string>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri($"https://geocode.search.hereapi.com/v1/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage http = await client.GetAsync($"geocode?q={this.Place_TextBox.Text}&apiKey={ConfigurationManager.ConnectionStrings["apikey"]}");
                    if (http.IsSuccessStatusCode)
                    {
                        JObject obj = JObject.Parse(await http.Content.ReadAsStringAsync());
                        JToken token = obj.First;
                        while (token.Children().Count() == 1)
                        {
                            token = token.First;
                        }
                        if (token.Children().All(i => i.ToString().Contains("title")))
                        {
                            foreach (var item in token.Children())
                            {
                                adresses.Add(item.First.First.ToString());
                            }
                        }
                        else
                        {
                            adresses.Add(token.First.First.ToString());
                        }
                    }
                    return adresses;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Try again!");
                return adresses;
            }
        }

        private async void Place_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.Place_TextBox.Text != "Place(Name or Adress)" || string.IsNullOrWhiteSpace(this.Place_TextBox.Text))
            {
                this.Adresses_ComboBox.ItemsSource = await GetAdresses();
                this.Adresses_ComboBox.IsDropDownOpen = true;
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            var textBox = (sender as TextBox);
            if (textBox.Name == "Place_TextBox" && string.IsNullOrWhiteSpace(this.Place_TextBox.Text))
            {
                this.Place_TextBox.Foreground = new SolidColorBrush(Colors.Gray);
                this.Place_TextBox.Text = "Place(Name or Adress)";
            }
            else if(textBox.Name == "Time_TextBox" && string.IsNullOrWhiteSpace(this.Time_TextBox.Text))
            {
                this.Time_TextBox.FontSize = 14;
                this.Time_TextBox.Foreground = new SolidColorBrush(Colors.Gray);
                this.Time_TextBox.Text = "Time(day/month/year hours:minutes AM/PM)";
            }

        }

        public void RemoveText(object sender, EventArgs e)
        {
            var textBox = (sender as TextBox);
            if (textBox.Name == "Place_TextBox" && this.Place_TextBox.Text == "Place(Name or Adress)")
            {
                this.Place_TextBox.Foreground = new SolidColorBrush(Colors.Black);
                this.Place_TextBox.Text = "";
            }
            else if(textBox.Name == "Time_TextBox" && this.Time_TextBox.Text == "Time(day/month/year hours:minutes AM/PM)")
            {
                this.Time_TextBox.FontSize = 20;
                this.Time_TextBox.Foreground = new SolidColorBrush(Colors.Black);
                this.Time_TextBox.Text = "";
            }
        }
    }
}
