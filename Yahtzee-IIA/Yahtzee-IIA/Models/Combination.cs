using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee_IIA.Models
{
    class Combination
    {
        #region Fields

        private int _id;
        private String _name;
	    private String _description; 
	    private int _value;

        /// <summary>
        ///     Permet de savoir si la combinaison est déjà remplie ou non
        /// </summary>
	    private bool _filled; 

        /// <summary>
        ///     Permet de savoir si la combinaison est jouable (si sélection possible)
        /// </summary>
	    private bool _playable;

        /// <summary>
        ///     Permet de savoir à quelle section la combinaison appartient (upper, prime, lower)
        /// </summary>
        private String _group;

        #endregion

        #region Properties

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

        public String Group
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;

                    //this.OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe Combination
        /// </summary>
        /// <param name="name">Nom de la combinaison</param>
        /// <param name="description">Description de la combinaison</param>
        public Combination(String name, String description, String group)
        {
            _name = name;
            _description = description;
            _value = 0;
            _filled = false;
            _playable = false;
            _group = group;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Calcule la valeur possible d'après les 5 dés passés en paramètre
        /// </summary>
        /// <param name="dices">Tableau des 5 dés</param>
        /// <returns>Valeur possible de la combinaison</returns>
        public int calculateValue (int[] dices)
        {
            return 0;
        }

        #endregion
    }
}
