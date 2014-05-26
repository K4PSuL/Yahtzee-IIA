using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Core;

namespace Yahtzee_IIA.Models
{
    [Table]
    public class Game : ObservableObject
    {
        #region Fields

        private long _id;
        private EntitySet<Player> _aPlayers;
        //private Player[] _aPlayers;

        /// <summary>
        ///     Nombre de lancers restants
        /// </summary>
	    private int _nbRoll;

        /// <summary>
        ///     Booléen permettant de savoir si le joueur peut rejouer
        /// </summary>
        private bool _isPlayable;

        /// <summary>
        ///     Tableau des 5 dés
        /// </summary>
	    private Dice[] _dices;

        #endregion

        #region Properties

        [Column(IsPrimaryKey = true, DbType = "BigInt NOT NULL IDENTITY", CanBeNull = false, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long Id
        {
            get { return _id; }
            set { Assign(ref _id, value); }
        }

        [Association(Storage = "Players", OtherKey = "IdGame")]
        public EntitySet<Player> Players
        {
            get { return _aPlayers; }
            set { _aPlayers.Assign(value); }
        }

        [Column(DbType = "Int", CanBeNull = false)]
        public int NbRoll
        {
            get { return _nbRoll; }
            set { Assign(ref _nbRoll, value); }
        }

        [Column(DbType = "Bit", CanBeNull = false)]
        public bool IsPlayable
        {
            get { return _isPlayable; }
            set { Assign(ref _isPlayable, value); }
        }

        public Dice[] Dices
        {
            get { return _dices; }
            set { Assign(ref _dices, value); }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe Game
        /// </summary>
        /// <param name="nbPlayers">Nombre de joueurs de la partie</param>
        /// <param name="listPlayers">Liste des joueurs</param>
        public Game()
        {
            //_aPlayers = new Player[nbPlayers];
            _aPlayers = new EntitySet<Player>(AttachPlayer, DetachPlayer);
            _nbRoll = 3;
            _isPlayable = true;

            //for (int i = 0; i < 5; i++)
            //{
            //    Dice dice = new Dice();

            //    _dices[i] = dice;
            //}
        }

        #endregion

        #region Methods

        private void AttachPlayer(Player player)
        {
            player.Game = this;
            OnPropertyChanged("Players");
        }

        private void DetachPlayer(Player player)
        {
            player.Game = null;
            OnPropertyChanged("Players");
        }

        /// <summary>
        ///     Appelle la fonction random pour les dés dont la propriété keep=false
        /// </summary>
        public void roll() 
        {
            foreach (Dice dice in _dices)
            {
                if (dice.Keep == false)
                {
                    this.random(dice);
                }
            }
        }

        /// <summary>
        ///     Génère un nombre aléatoire entre 1 et 5 pour le dé passé en paramètre
        /// </summary>
        /// <param name="dice">Dé sur lequel effectuer le tirage</param>
	    public void random (Dice dice) 
        {
            //Tirage d'un nombre au hasard entre 1 et 6 et mise à jour de la propriété « number » du dé
            Random random = new Random();
            int randomNumber = random.Next(1, 6);
            dice.Number = randomNumber;

            //Mise à jour de la propriété « image » du dé
            dice.Image = "de" + randomNumber + ".png";
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
