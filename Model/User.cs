using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMe.Model
{
    public class User
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        private byte[] Password { get; set; }

        public byte[] Avatar { get; set; }

        public User(string name, string surname, string email, byte[] password)
        {
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Password = password;
        }

        public User(string name, string surname, string email, byte[] password, byte[] avatar)
        {
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Password = password;
            this.Avatar = avatar;
        }

    }
}
