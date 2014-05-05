using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee_IIA.Models
{
    class Game
    {
        #region Fields

        /// <summary>
        ///     Tableau des joueurs
        /// </summary>
        private Player[] _aPlayers; 

        /// <summary>
        ///     Nombre de lancers restants
        /// </summary>
	    private int _nbRoll; 

        /// <summary>
        ///     Tableau des 5 dés
        /// </summary>
	    private Dice[] _dices;

        #endregion

        #region Properties

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

        public Dice[] Dices
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

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe Game
        /// </summary>
        /// <param name="nbPlayers">Nombre de joueurs de la partie</param>
        public Game(int nbPlayers)
        {
            _aPlayers = new Player[nbPlayers];
            _nbRoll = 3;

            for (int i = 0; i < 5; i++)
            {
                Dice dice = new Dice();
                _dices[i] = dice;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Appelle la fonction random pour les dés dont la propriété keep=false
        /// </summary>
        public void roll() 
        {

        }

        /// <summary>
        ///     Génère un nombre aléatoire entre 1 et 5 pour le dé passé en paramètre
        /// </summary>
        /// <param name="dice">Dé sur lequel effectuer le tirage</param>
	    public void random (Dice dice) 
        {
            //TODO: mettre à jour la propriété « number » du dé passé en paramètre avec le nombre aléatoire
        }

        /// <summary>
        ///     Contrôle s'il y a des combinaisons possibles en fonction des 5 dés
        /// </summary>
        /// <returns>True s'il y a des combinaisons possibles, false sinon</returns>
        public bool checkCombinations()
        {
            //TODO: mettre à jour les propriétés « playable » de chaque combinaisons
            return true;
        }

        #endregion
    }
}
