using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee_IIA.Models
{
    class Combination
    {
        private String _name;
	    private String _description; 
	    private int _value; 
	    private bool _filled; 
	    private bool _playable;

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

        public String Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;

                    //this.OnPropertyChanged();
                }
            }
        }

        public int Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;

                    //this.OnPropertyChanged();
                }
            }
        }

        public bool Filled
        {
            get { return _filled; }
            set
            {
                if (_filled != value)
                {
                    _filled = value;

                    //this.OnPropertyChanged();
                }
            }
        }

        public bool Playable
        {
            get { return _playable; }
            set
            {
                if (_playable != value)
                {
                    _playable = value;

                    //this.OnPropertyChanged();
                }
            }
        }

	    public int calculateValue (int[] dices)
        {
        
        
            return 0;
        } 

    }
}
