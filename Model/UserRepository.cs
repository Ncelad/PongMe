using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace PongMe.Model
{
    static class UserRepository
    {

        public static async void CreateUser(User user)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://ncelad-001-site1.ftempurl.com/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage http = client.PostAsync("user/create", new StringContent(JsonConvert.SerializeObject(user), UnicodeEncoding.UTF8, "application/json")).GetAwaiter().GetResult();
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

        public static async Task<List<User>> ReadUsers()
        {
            List<User> users = new List<User>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://ncelad-001-site1.ftempurl.com/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage http = await client.GetAsync("user");
                    if (http.IsSuccessStatusCode)
                    {
                        users = JsonConvert.DeserializeObject<List<User>>(await http.Content.ReadAsStringAsync());
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

        public static async Task<User> ReadUsers(User u)
        {
            User user = new User();
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://ncelad-001-site1.ftempurl.com/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage http = await client.GetAsync($"user/{u.Email}");
                    if (http.IsSuccessStatusCode)
                    {
                        user = JsonConvert.DeserializeObject<User>(await http.Content.ReadAsStringAsync());
                        return user;
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

        public static void UpdateUser(User user)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://ncelad-001-site1.ftempurl.com/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage http = client.PostAsync($"user/update", new StringContent(JsonConvert.SerializeObject(user), UnicodeEncoding.UTF8, "application/json")).GetAwaiter().GetResult();
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

        public static async void Delete(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://ncelad-001-site1.ftempurl.com/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage http = await client.DeleteAsync($"user/{user.Id}");
                if (http.IsSuccessStatusCode)
                {
                    MessageBox.Show(await http.Content.ReadAsStringAsync());
                }
            }
        }

    }
}
