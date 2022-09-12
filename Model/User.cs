using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PongMe.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public byte[] Password { get; set; }

        public string Avatar { get; set; }

        public string Gender { get; set; }

        public BitmapImage AvatarImage { get; set; }

        public User()
        {

        }

        public User(string email, byte[] password)
        {
            this.Email = email;
            this.Password = password;
        }

        public User(string name, string surname, string email, byte[] password, string avatar)
        {
            this.Name = name;
            this.Surname = surname;
            this.Age = 1;
            this.Email = email;
            this.Password = password;
            this.Avatar = avatar;
            this.Gender = "None";
        }

        public User(string name, string surname, int age, string email, byte[] password, string avatar, string gender)
        {
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Age = age;
            this.Password = password;
            this.Avatar = avatar;
            this.Gender = gender;
        }
    }
}
