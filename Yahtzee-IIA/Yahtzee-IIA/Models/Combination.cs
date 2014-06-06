using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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

        private Func<Dice[], int> _calcul;


        /// <summary>
        ///     Permet de savoir si la combinaison est déjà remplie ou non
        /// </summary>
	    private bool _isNotFilled; 

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
        public bool IsNotFilled
        {
            get { return _isNotFilled; }
            set { Assign(ref _isNotFilled, value); }
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

        public Brush Color
        {
            get
            {
                Brush brush = null;

                switch (this.Group)
                {
                    case "upper" :
                        brush = new SolidColorBrush(Colors.Yellow);
                        break;

                    case "lower":
                        brush = new SolidColorBrush(Colors.Orange);
                        break;

                    case "total":
                        brush = new SolidColorBrush(Colors.Red);
                        break;

                    default:
                        break;
                }

                return brush;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe Combination
        /// </summary>
        /// <param name="name">Nom de la combinaison</param>
        /// <param name="description">Description de la combinaison</param>
        public Combination(String name, String description, String group, Func<Dice[], int> calcul)
        {
            _playerRef = new EntityRef<Player>();
            _name = name;
            _description = description;
            _value = 0;
            _isNotFilled = true;
            _playable = false;
            _group = group;

            _calcul = calcul;
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

        public int Calcul(Dice[] dices)
        {
            Value = this._calcul(dices);
            return Value;
        }



        #endregion
    }
}
