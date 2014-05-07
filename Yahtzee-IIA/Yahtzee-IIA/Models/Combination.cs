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
    class Combination : ObservableObject
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

        [Column(DbType = "Integer", CanBeNull = false)]
        public int Value
        {
            get { return _value; }
            set { Assign(ref _value, value); }
        }

        [Column(DbType = "Boolean NOT NULL", CanBeNull = false)]
        public bool Filled
        {
            get { return _filled; }
            set { Assign(ref _filled, value); }
        }

        [Column(DbType = "Boolean NOT NULL", CanBeNull = false)]
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
