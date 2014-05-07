using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Core;

namespace Yahtzee_IIA.Models
{
    class Combination : ObservableObject
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
            set { Assign(ref _id, value); }
           
        }

        public String Name
        {
            get { return _name; }
            set { Assign(ref _name, value); }
        }

        public String Description
        {
            get { return _description; }
            set { Assign(ref _description, value); }
        }

        public int Value
        {
            get { return _value; }
            set { Assign(ref _value, value); }
        }

        public bool Filled
        {
            get { return _filled; }
            set { Assign(ref _filled, value); }
        }

        public bool Playable
        {
            get { return _playable; }
            set { Assign(ref _playable, value); }
        }

        public String Group
        {
            get { return _group; }
            set { Assign(ref _group, value); }
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
