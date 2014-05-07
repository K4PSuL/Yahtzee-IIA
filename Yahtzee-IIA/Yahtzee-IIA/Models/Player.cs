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
    public class Player : ObservableObject
    {
        #region Fields

        private long _id;
        private long _idGame;
        private EntityRef<Game> _gameRef;
        private String _name;
        private EntitySet<Combination> _aCombinations;

        #endregion

        #region Properties

        [Column(IsPrimaryKey = true, DbType = "BigInt NOT NULL IDENTITY", CanBeNull = false, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long Id
        {
            get { return _id; }
            set { Assign(ref _id, value); }
        }

        [Column(DbType = "BigInt NOT NULL", CanBeNull = false)]
        public long IdGame
        {
            get { return _idGame; }
            set { Assign(ref _idGame, value); }
        }

        [Association(Storage = "Game", ThisKey = "IdGame", IsForeignKey = true)]
        public Game Game
        {
            get { return _gameRef.Entity; }
            set { _gameRef.Entity = value; }
        }

        [Column(DbType = "NVarChar(140)", CanBeNull = false)]
        public String Name
        {
            get { return _name; }
            set { Assign(ref _name, value); }
        }

        [Association(Storage = "Combinations", OtherKey = "IdPlayer")]
        public EntitySet<Combination> Combinations
        {
            get { return _aCombinations; }
            set { _aCombinations.Assign(value); }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe Player
        /// </summary>
        /// <param name="id">Id du joueur</param>
        /// <param name="name">Nom du joueur</param>
        /// <param name="score">Score du joueur</param>
        public Player(String name)
        {
            _gameRef = new EntityRef<Game>();
            _aCombinations = new EntitySet<Combination>(AttachCombination, DetachCombination);
            _name = name;

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
                YahtzeeDataContext.Instance.Combination.InsertOnSubmit(combination);
            }
        }

        #endregion

        #region Methods

        private void AttachCombination(Combination combination)
        {
            combination.Player = this;
            OnPropertyChanged("Combinations");
        }

        private void DetachCombination(Combination combination)
        {
            combination.Player = null;
            OnPropertyChanged("Combinations");
        }

        /// <summary>
        ///     Calcule le sous-total de la section supérieure (sans la prime)
        /// </summary>
        /// <returns>Sous-total de la section supérieure</returns>
        public int calculateTotalScore()
        {

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
        public int calculateTotalUpperSection()
        {

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
        public int calculateTotalLowerSection()
        {

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
