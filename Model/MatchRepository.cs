using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Data.Json;

namespace PongMe.Model
{
    class MatchRepository
    {
        public static async void CreateMatch(Match match)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://ncelad-001-site1.ftempurl.com/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage http = client.PostAsync("match/create", new StringContent(JsonConvert.SerializeObject(match), UnicodeEncoding.UTF8, "application/json")).GetAwaiter().GetResult();
                    if (http.IsSuccessStatusCode)
                    {
                        MessageBox.Show(await http.Content.ReadAsStringAsync());
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

        public static async Task<List<Match>> ReadMatches()
        {
            List<Match> users = new List<Match>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://ncelad-001-site1.ftempurl.com/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage http = await client.GetAsync("match");
                    if (http.IsSuccessStatusCode)
                    {
                        users = JsonConvert.DeserializeObject<List<Match>>(await http.Content.ReadAsStringAsync());
                        return users;
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

        public static async Task<List<Match>> ReadMatches(Match m)
        {
            List<Match> matches = new List<Match>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://ncelad-001-site1.ftempurl.com/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage http = await client.GetAsync($"match/{m.Place}");
                    if (http.IsSuccessStatusCode)
                    {
                        matches = JsonConvert.DeserializeObject<List<Match>>(await http.Content.ReadAsStringAsync());
                        return matches;
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

        public static void UpdateMatch(Match match)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://ncelad-001-site1.ftempurl.com/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage http = client.PostAsync($"match/update", new StringContent(JsonConvert.SerializeObject(match), UnicodeEncoding.UTF8, "application/json")).GetAwaiter().GetResult();
                    if (http.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Success");
                    }
                    else
                    {
                        MessageBox.Show("Failed");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        public static async void DeleteMatch(Match match)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://ncelad-001-site1.ftempurl.com/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage http = await client.DeleteAsync($"match/{match.Id}");
                if (http.IsSuccessStatusCode)
                {
                    MessageBox.Show(await http.Content.ReadAsStringAsync());
                }
            }
        }

    }
}

