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
    public class Combination : ObservableObject
    {
        #region Fields

        private long _id;
        private long _idPlayer;
        private EntityRef<Player> _playerRef;
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

        [Column(IsPrimaryKey = true, DbType = "BigInt NOT NULL IDENTITY", CanBeNull = false, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long Id
        {
            get { return _id; }
            set { Assign(ref _id, value); }
           
        }

        [Column(DbType = "BigInt NOT NULL", CanBeNull = false)]
        public long IdPlayer
        {
            get { return _idPlayer; }
            set { Assign(ref _idPlayer, value); }
        }

        [Association(Storage = "Player", ThisKey = "IdPlayer", IsForeignKey = true)]
        public Player Player
        {
            get { return _playerRef.Entity; }
            set { _playerRef.Entity = value; }
        }

        [Column(DbType = "NVarChar(140) NOT NULL", CanBeNull = false)]
        public String Name
        {
            get { return _name; }
            set { Assign(ref _name, value); }
        }

        [Column(DbType = "NVarChar(1024)", CanBeNull = false)]
        public String Description
        {
            get { return _description; }
            set { Assign(ref _description, value); }
        }

        [Column(DbType = "Int", CanBeNull = false)]
        public int Value
        {
            get { return _value; }
            set { Assign(ref _value, value); }
        }

        [Column(DbType = "Bit NOT NULL", CanBeNull = false)]
        public bool Filled
        {
            get { return _filled; }
            set { Assign(ref _filled, value); }
        }

        [Column(DbType = "Bit NOT NULL", CanBeNull = false)]
        public bool Playable
        {
            get { return _playable; }
            set { Assign(ref _playable, value); }
        }

        [Column(DbType = "NVarChar(10) NOT NULL", CanBeNull = false)]
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
            _playerRef = new EntityRef<Player>();
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
        ///     Compte le nombre de dés ayant comme valeur le nombre passé en paramètre
        /// </summary>
        /// <param name="number">Nombre à rechercher</param>
        /// <param name="dices">Tableau des 5 dés</param>
        /// <returns>Nombre de dés correspondants</returns>
        public int countNumberOf(int number, Dice[] dices)
        {
            int sum = 0;

            foreach (Dice dice in dices)
            {
                if (dice.Number == number)
                {
                    sum++;
                }
            }

            return sum;
        }

        /// <summary>
        ///     Calcule la valeur possible de la combinaison d'après les 5 dés passés en paramètre
        /// </summary>
        /// <param name="dices">Tableau des 5 dés</param>
        /// <returns>Valeur possible de la combinaison</returns>
        public int calculateValue(Dice[] dices)
        {
            int result = 0;

            switch (_id)
            {
                //Combination aces;
                case 0:
                    result = 1 * countNumberOf(1, dices);
                    break;

                //Combination twos ;
                case 1:
                    result = 2 * countNumberOf(2, dices);
                    break;

                //Combination threes;
                case 2:
                    result = 3 * countNumberOf(3, dices);
                    break;

                //Combination fours;
                case 3:
                    result = 4 * countNumberOf(4, dices);
                    break;

                //Combination fives;
                case 4:
                    result = 5 * countNumberOf(5, dices);
                    break;

                //Combination sixes;
                case 5:
                    result = 6 * countNumberOf(6, dices);
                    break;

                //Combination 3ofAKind; // Brelan -> Somme des  5 dés
                case 6:
                    foreach (Dice dice in dices)
                    {
                        result += dice.Number;
                    }
                    break;

                //Combination 4ofAKind; // Carré -> Somme des  5 dés
                case 7:
                    foreach (Dice dice in dices)
                    {
                        result += dice.Number;
                    }
                    break;

                //Combination fullHouse; // Full
                case 8:
                    result = 25;
                    break;

                //Combination smallStraight; // Petite suite
                case 9:
                    result = 30;
                    break;

                //Combination longStraight; // Grande suite
                case 10:
                    result = 40;
                    break;

                //Combination yahtzee;
                case 11:
                    result = 50;
                    break;

                //Combination chance; -> Somme des  5 dés
                case 12:
                    foreach (Dice dice in dices)
                    {
                        result += dice.Number;
                    }
                    break;

                default:
                    result = 0;
                    break;
            }

            return result;
        }

        #endregion
    }
}
