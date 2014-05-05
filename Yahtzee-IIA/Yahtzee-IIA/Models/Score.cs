using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee_IIA.Models
{
    class Score
    {
        private Combination[] _aCombinations; 

        public Combination[] aCombinations 
        {
            get { return _aCombinations; }
            set
            {
                if (_aCombinations != value)
                {
                    _aCombinations = value;

                    //this.OnPropertyChanged();
                }
            }
        }


	public int calculateTotalScore() {
        return 0;
    }
	public int calculateTotalUpperSection() {
        return 0;
    }
	public int calculateTotalLowerSection() {
        return 0;
    }
    public int calculateGrandTotal()
    {
        return 0;
    }

    }
}
