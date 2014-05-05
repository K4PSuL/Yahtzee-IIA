using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee_IIA.Models
{
    class Score
    {
        #region Fields

        private Combination[] _aCombinations;

        #endregion

        #region Properties

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

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe Score
        /// </summary>
        public Score()
        {
            string[] names = { "Total des As", 
                                 "Total des Deux", 
                                 "Total des Trois", 
                                 "Total des Quatre", 
                                 "Total des Cinq", 
                                 "Total des Six", 
                                 "Prime des 35 points", 
                                 "Brelan", 
                                 "Carré", 
                                 "full", 
                                 "Petite suite", 
                                 "Grande suite", 
                                 "Yahtzee",
                                 "Chance", 
                             };

            string[] descriptions = { "", 
                                 "", 
                                 "", 
                                 "", 
                                 "", 
                                 "", 
                                 "", 
                                 "", 
                                 "", 
                                 "", 
                                 "", 
                                 "", 
                                 "", 
                                 "", 
                             };

            string[] groups = { "upper", 
                                 "upper", 
                                 "upper", 
                                 "upper", 
                                 "upper", 
                                 "upper", 
                                 "bonus", 
                                 "lower", 
                                 "lower", 
                                 "lower", 
                                 "lower", 
                                 "lower", 
                                 "lower", 
                                 "lower", 
                             };

            for (int i = 0; i < names.Length; i++)
            {
                Combination combination = new Combination(names[i], descriptions[i], groups[i]);
                combination.Id = i;
                _aCombinations[i] = combination;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Calcule le sous-total de la section supérieure (sans la prime)
        /// </summary>
        /// <returns>Sous-total de la section supérieure</returns>
        public int calculateTotalScore() {

            int result = 0;

            foreach (Combination item in _aCombinations)
            {
                if (item.Group == "upper")
                {
                    result += item.Value;
                }
            }
            
            return result;
        }

        /// <summary>
        ///     Calcule le total de la section supérieure (avec la prime)
        /// </summary>
        /// <returns>Total de la section supérieure</returns>
	    public int calculateTotalUpperSection() {

            int result = 0;

            result += calculateTotalScore();

            foreach (Combination item in _aCombinations)
            {
                if (item.Group == "bonus")
                {
                    result += item.Value;
                }
            }

            return result;
        }

        /// <summary>
        ///     Calcule le total de la section inférieure
        /// </summary>
        /// <returns>Total de la section inférieure</returns>
	    public int calculateTotalLowerSection() {

            int result = 0;

            foreach (Combination item in _aCombinations)
            {
                if (item.Group == "lower")
                {
                    result += item.Value;
                }
            }

            return result;
        }

        /// <summary>
        ///     Calcule le total général
        /// </summary>
        /// <returns>Total général</returns>
        public int calculateGrandTotal()
        {
            int result = 0;

            result += calculateTotalUpperSection() + calculateTotalLowerSection();

            return result;
        }

        #endregion
    }
}
