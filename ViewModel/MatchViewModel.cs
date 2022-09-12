using PongMe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMe.ViewModel
{
    class MatchViewModel
    {
        private static MatchViewModel instance = null;

        public static MatchViewModel Instance
        {
            get { return instance; }
            set { if (instance == null) { instance = value; } }
        }

        private List<Match> matches = new List<Match>();

        public List<Match> Matches
        {
            get { return matches; }
            set { matches = value; }
        }

    }
}
