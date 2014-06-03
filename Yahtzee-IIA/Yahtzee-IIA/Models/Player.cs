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

        /// <summary>
        ///     Booléen permettant de savoir si c'est un nouveau tour
        /// </summary>
        private bool _isStart;

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

        private int _totalScore;
        private int _totalUpperSection;
        private int _totalLowerSection;
        private int _grandTotal;

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
            set { _gameRef.Entity = value; OnGameSetted(); }
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

        public bool IsStart
        {
            get { return _isStart; }
            set { Assign(ref _isStart, value); }
        }

        [Column(DbType = "Int")]
        public int TotalScore
        {
            get { return _totalScore; }
            set { Assign(ref _totalScore, value); }
        }

        [Column(DbType = "Int")]
        public int TotalUpperSection
        {
            get { return _totalUpperSection; }
            set { Assign(ref _totalUpperSection, value); }
        }

        [Column(DbType = "Int")]
        public int TotalLowerSection
        {
            get { return _totalLowerSection; }
            set { Assign(ref _totalLowerSection, value); }
        }

        [Column(DbType = "Int")]
        public int GrandTotal
        {
            get { return _grandTotal; }
            set { Assign(ref _grandTotal, value); }
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

            Name = name;

            NbRoll = 3;
            IsPlayable = false;
            IsStart = false;

            Dices = new Dice[5];

            for (int i = 0; i < 5; i++)
            {
                Dice dice = new Dice();

                Dices[i] = dice;
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
        ///     Appelle la fonction random pour les dés qu'il faut rejouer (keep = false)
        /// </summary>
        public void roll()
        {
            foreach (Dice dice in _dices)
            {
                if (dice.Keep == false)
                {
                    dice.random();
                    System.Diagnostics.Debug.WriteLine(dice.Image.ToString());
                }
            }
        }

        /// <summary>
        ///     Contrôle s'il y a des combinaisons possibles en fonction des 5 dés
        /// </summary>
        /// <returns>True s'il y a des combinaisons possibles, false sinon</returns>
        public bool checkCombinations()
        {
            //TODO: mettre à jour les propriétés « playable » de chaque combinaisons

            // Parcours de chaque combinaison
            foreach (Combination combination in Combinations)
            {
                // Si la combinaison n'est pas encore remplie, on calcule le score possible
                if (combination.IsNotFilled)
                {
                    combination.Calcul(Dices);
                }
            }

            return true;
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

            if (result >= 63)
            {
                result += 35;
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

        private void OnGameSetted()
        {
            string[] names = { "Total des As", 
                                 "Total des Deux", 
                                 "Total des Trois", 
                                 "Total des Quatre", 
                                 "Total des Cinq", 
                                 "Total des Six", 
                                 "Brelan", 
                                 "Carré", 
                                 "Full", 
                                 "Petite suite", 
                                 "Grande suite", 
                                 "Yahtzee",
                                 "Chance", 
                             };

            string[] descriptions = { "Somme des dés 1", 
                                 "Somme des dés 2", 
                                 "Somme des dés 3", 
                                 "Somme des dés 4", 
                                 "Somme des dés 5", 
                                 "Somme des dés 6", 
                                 "3 dés identiques (somme des 5 dés)", 
                                 "4 dés identiques (somme des 5 dés)", 
                                 "Un brelan plus une paire (25 points)", 
                                 "Suite de 4 dés (30 points)", 
                                 "Suite de 5 dés (40 points)", 
                                 "5 dés identiques (50 points)", 
                                 "Somme des 5 dés", 
                             };

            string[] groups = { "upper", 
                                 "upper", 
                                 "upper", 
                                 "upper", 
                                 "upper", 
                                 "upper", 
                                 "lower", 
                                 "lower", 
                                 "lower", 
                                 "lower", 
                                 "lower", 
                                 "lower", 
                                 "lower", 
                             };

            string[] methodes = {"calculateAces", 
                                 "calculateTwos", 
                                 "calculateThrees", 
                                 "calculateFours", 
                                 "calculateFives", 
                                 "calculateSixes", 
                                 "calculateThreeOfAKind", 
                                 "calculateFourOfAKind", 
                                 "calculateFullHouse", 
                                 "calculateSmallStraight", 
                                 "calculateLargeStraight", 
                                 "calculateYahtzee", 
                                 "calculateChance"
                             };


            Func<Dice[], int>[] delegates = new Func<Dice[], int>[] { 
                                 new Func<Dice[], int>(Game.calculateAces),
                                 new Func<Dice[], int>(Game.calculateTwos),
                                 new Func<Dice[], int>(Game.calculateThrees),
                                 new Func<Dice[], int>(Game.calculateFours),
                                 new Func<Dice[], int>(Game.calculateFives),
                                 new Func<Dice[], int>(Game.calculateSixes),
                                 new Func<Dice[], int>(Game.calculateThreeOfAKind),
                                 new Func<Dice[], int>(Game.calculateFourOfAKind),
                                 new Func<Dice[], int>(Game.calculateFullHouse),
                                 new Func<Dice[], int>(Game.calculateSmallStraight),
                                 new Func<Dice[], int>(Game.calculateLargeStraight),
                                 new Func<Dice[], int>(Game.calculateYahtzee),
                                 new Func<Dice[], int>(Game.calculateChance)
                             };

            for (int i = 0; i < names.Length; i++)
            {
                Combination combination = new Combination(names[i], descriptions[i], groups[i], delegates[i]);
                YahtzeeDataContext.Instance.Combination.InsertOnSubmit(combination);

                this._aCombinations.Add(combination);
            }
        }

        #endregion
    }
}
