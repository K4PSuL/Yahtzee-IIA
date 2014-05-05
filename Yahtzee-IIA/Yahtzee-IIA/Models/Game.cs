using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee_IIA.Models
{
    class Game
    {
        private Player[] _aPlayers; 
	    private int _nbRoll; 
	    private int[] _dices; 
	
        public Player[] aPlayers 
        {
            get { return _aPlayers; }
            set
            {

                if (_aPlayers != value)
                {
                    _aPlayers = value;

                    //this.OnPropertyChanged();
                }
            }
         }

        public int NbRoll
        {
            get { return _nbRoll; }
            set
            {
                if (_nbRoll != value)
                {
                    _nbRoll = value;

                    //this.OnPropertyChanged();
                }
            }
        }

        public int[] Dices
        {
            get { return _dices; }
            set
            {
                if (_dices != value)
                {
                    _dices = value;

                    //this.OnPropertyChanged();
                }
            }
        }

	    public void _throw() 
        {

        }

	    public void random () 
        {
        
        }

        public bool checkCombinations()
        {
            return true;
        }


            // exemple
        public Game ()
	    {
            _aPlayers = new Player[10];
	    }

    }
}
