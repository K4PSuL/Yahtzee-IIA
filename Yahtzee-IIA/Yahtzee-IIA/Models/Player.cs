using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee_IIA.Models
{
    class Player
    {

        private int _id;
        private String _name;
        private Score _score;


        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;

                    //this.OnPropertyChanged();
                }
            }
        }

        public String Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;

                    //this.OnPropertyChanged();
                }
            }
        }
        public Score Score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;

                    //this.OnPropertyChanged();
                }
            }
        }

    }
}
