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
        public string Email { get; set; }

        public byte[] Password { get; set; }

        public string Avatar { get; set; }

        public BitmapImage AvatarImage { get; set; }

        public User()
        {

        }

        public User(string email, byte[] password)
        {
            this.Email = email;
            this.Password = password;
        }

        public User(string name, string surname, string email, byte[] password)
        {
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Password = password;
        }

        public User(string name, string surname, string email, byte[] password, string avatar)
        {
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Password = password;
            this.Avatar = avatar;
        }

    }
}
