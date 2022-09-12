using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        }

        private async void Submit_MouseDown(object sender, MouseButtonEventArgs e)
        {
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
                        while(token.Children().Count() == 1)
                        {
                            token = token.First;
                        }
                        if(token.Children().All(i => i.ToString().Contains("title")))
                        {
                            foreach (var item in token.Children())
                            {
                                MessageBox.Show(item.First.First.ToString());
                            }
                        }
                        else
                        {
                            MessageBox.Show(token.First.First.ToString());
                        }
                    }
                    else
                    {
                        throw new Exception(http.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
        }
    }
}
