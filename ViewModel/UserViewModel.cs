using PongMe.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMe.ViewModel
{
    class UserViewModel
    {
        private static UserViewModel instance = null;

        public static UserViewModel Instance
        {
            get { return instance; }
            set { if (instance == null) { instance = value; } }
        }


        public User CurrentUser { get; set; }

    }
}
