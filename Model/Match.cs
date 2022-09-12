using PongMe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMe.Model
{
    class Match
    {
        public int Id { get; set; }

        public string Place { get; set; }

        public DateTime Time { get; set; }

        public int CreatorId { get; set; }

        private User creator = new User() { Name = "Default", Surname = "Default"};

        public User Creator
        {
            get
            {
                if (creator == new User() { Name = "Default", Surname = "Default" })
                {
                    SetCreator();
                }
                return creator;
            }
            set { creator = value; }
        }

        public int OpponentId { get; set; }

        public User Opponent { get; set; }

        public Match()
        {

        }

        public Match(string place, DateTime time, User creator)
        {
            this.Place = place;
            this.Time = time;
            this.Creator = creator;
            this.CreatorId = creator.Id;
        }

        private async void SetCreator()
        {
            this.Creator = await UserRepository.ReadUsers(CreatorId);
        }
    }
}
