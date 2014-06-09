using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WP.Core;

namespace Yahtzee_IIA.Models
{
    [Table]
    public class Game : ObservableObject
    {
        #region Fields

        private long _id;
        private EntitySet<Player> _aPlayers;

        #endregion

        #region Properties

        public long Id
        {
            get { return _id; }
            set { Assign(ref _id, value); }
        }

        public EntitySet<Player> Players
        {
            get { return _aPlayers; }
            set { _aPlayers.Assign(value); }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe Game
        /// </summary>
        public Game()
        {
            _aPlayers = new EntitySet<Player>(AttachPlayer, DetachPlayer);
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
        ///     Contrôle s'il y a des combinaisons possibles en fonction des 5 dés
        /// </summary>
        /// <returns>True s'il y a des combinaisons possibles, false sinon</returns>
        public bool checkCombinations()
        {
            //TODO: mettre à jour les propriétés « playable » de chaque combinaisons
            return true;
        }

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
        ///     Compte le nombre de dés 1 obtenus et calcule la valeur de la combinaison
        /// </summary>
        /// <param name="dices">Tableau des 5 dés</param>
        /// <returns>Valeur que peut prendre la combinaison avec ces dés</returns>
        public int calculateAces(Dice[] dices)
        {
            return 1 * countNumberOf(1, dices);
        }

        /// <summary>
        ///     Compte le nombre de dés 2 obtenus et calcule la valeur de la combinaison
        /// </summary>
        /// <param name="dices">Tableau des 5 dés</param>
        /// <returns>Valeur que peut prendre la combinaison avec ces dés</returns>
        public int calculateTwos(Dice[] dices)
        {
            return 2 * countNumberOf(2, dices);
        }

        /// <summary>
        ///     Compte le nombre de dés 3 obtenus et calcule la valeur de la combinaison
        /// </summary>
        /// <param name="dices">Tableau des 5 dés</param>
        /// <returns>Valeur que peut prendre la combinaison avec ces dés</returns>
        public int calculateThrees(Dice[] dices)
        {
            return 3 * countNumberOf(3, dices);
        }

        /// <summary>
        ///     Compte le nombre de dés 4 obtenus et calcule la valeur de la combinaison
        /// </summary>
        /// <param name="dices">Tableau des 5 dés</param>
        /// <returns>Valeur que peut prendre la combinaison avec ces dés</returns>
        public int calculateFours(Dice[] dices)
        {
            return 4 * countNumberOf(4, dices);
        }

        /// <summary>
        ///     Compte le nombre de dés 5 obtenus et calcule la valeur de la combinaison
        /// </summary>
        /// <param name="dices">Tableau des 5 dés</param>
        /// <returns>Valeur que peut prendre la combinaison avec ces dés</returns>
        public int calculateFives(Dice[] dices)
        {
            return 5 * countNumberOf(5, dices);
        }

        /// <summary>
        ///     Compte le nombre de dés 6 obtenus et calcule la valeur de la combinaison
        /// </summary>
        /// <param name="dices">Tableau des 5 dés</param>
        /// <returns>Valeur que peut prendre la combinaison avec ces dés</returns>
        public int calculateSixes(Dice[] dices)
        {
            return 6 * countNumberOf(6, dices);
        }

        /// <summary>
        ///     Contrôle si le joueur peut avoir la prime et calcule la valeur de la combinaison
        /// </summary>
        /// <param name="totalScore">Sous-total de la section supérieure (sans la prime)</param>
        /// <returns>Valeur de la prime</returns>
        public int calculateBonus(int totalScore)
        {
            int result = 0;

            if (totalScore >= 63)
            {
                result = 35;
            }

            return result;
        }

        /// <summary>
        ///     Contrôle si un brelan est possible et calcule la valeur de la combinaison
        /// </summary>
        /// <param name="dices">Tableau des 5 dés</param>
        /// <returns>Valeur que peut prendre la combinaison avec ces dés</returns>
        public int calculateThreeOfAKind(Dice[] dices)
        {
            int sum = 0;
            bool threeOfAKind = false;

            // Boucle pour chaque nombre de 1 à 6
            for (int i = 1; i <= 6; i++)
            {
                int cpt = 0;
                // Boucle pour chaque dé
                for (int j = 0; j < 5; j++)
                {
                    // Incrémentation du compteur à chaque fois qu'un dé correspond au nombre en cours
                    if (dices[j].Number == i)
                    {
                        cpt++;
                    }

                    // S'il y a au moins 3 dés identiques, le brelan est possible
                    if (cpt > 2)
                    {
                        threeOfAKind = true;
                    }
                }
            }

            // Si un brelan est possible, on calcule la valeur de la combinaison
            if (threeOfAKind)
            {
                for (int k = 0; k < 5; k++)
                {
                    sum += dices[k].Number;
                }
            }

            return sum;
        }

        /// <summary>
        ///     Contrôle si un carré est possible et calcule la valeur de la combinaison
        /// </summary>
        /// <param name="dices">Tableau des 5 dés</param>
        /// <returns>Valeur que peut prendre la combinaison avec ces dés</returns>
        public int calculateFourOfAKind(Dice[] dices)
        {
            int sum = 0;
            bool fourOfAKind = false;

            // Boucle pour chaque nombre de 1 à 6
            for (int i = 1; i <= 6; i++)
            {
                int cpt = 0;
                // Boucle pour chaque dé
                for (int j = 0; j < 5; j++)
                {
                    // Incrémentation du compteur à chaque fois qu'un dé correspond au nombre en cours
                    if (dices[j].Number == i)
                    {
                        cpt++;
                    }

                    // S'il y a au moins 4 dés identiques, le carré est possible
                    if (cpt > 3)
                    {
                        fourOfAKind = true;
                    }
                }
            }

            // Si un carré est possible, on calcule la valeur de la combinaison
            if (fourOfAKind)
            {
                for (int k = 0; k < 5; k++)
                {
                    sum += dices[k].Number;
                }
            }

            return sum;
        }

        /// <summary>
        ///     Contrôle si un full est possible 
        /// </summary>
        /// <param name="dices">Tableau des 5 dés</param>
        /// <returns>Valeur que peut prendre la combinaison</returns>
        public int calculateFullHouse(Dice[] dices)
        {
            int sum = 0;
            int[] i = new int[5];

            // Récupération des valeurs de chaque dé
            i[0] = dices[0].Number;
            i[1] = dices[1].Number;
            i[2] = dices[2].Number;
            i[3] = dices[3].Number;
            i[4] = dices[4].Number;

            // Tri des valeurs par ordre croissant
            Array.Sort(i);

            // Test s'il y a un full
            if ((((i[0] == i[1]) && (i[1] == i[2])) && // Three of a Kind
                  (i[3] == i[4]) && // Two of a Kind
                  (i[2] != i[3])) ||

                 ((i[0] == i[1]) && // Two of a Kind
                 ((i[2] == i[3]) && (i[3] == i[4])) && // Three of a Kind
                  (i[1] != i[2])))
            {
                sum = 25; // La valeur de cette combinaison sera toujours de 25
            }

            return sum;
        }

        /// <summary>
        ///     Contrôle si une grande suite est possible
        /// </summary>
        /// <param name="dices">Tableau des 5 dés</param>
        /// <returns>Valeur que peut prendre la combinaison</returns>
        public int calculateLargeStraight(Dice[] dices)
        {
            int sum = 0;
            int[] i = new int[5];

            // Récupération des valeurs de chaque dé
            i[0] = dices[0].Number;
            i[1] = dices[1].Number;
            i[2] = dices[2].Number;
            i[3] = dices[3].Number;
            i[4] = dices[4].Number;

            // Tri des valeurs par ordre croissant
            Array.Sort(i);

            // Test s'il y a une grande suite
            if (((i[0] == 1) &&
                 (i[1] == 2) &&
                 (i[2] == 3) &&
                 (i[3] == 4) &&
                 (i[4] == 5)) ||

                ((i[0] == 2) &&
                 (i[1] == 3) &&
                 (i[2] == 4) &&
                 (i[3] == 5) &&
                 (i[4] == 6)))
            {
                sum = 40; // La valeur de cette combinaison sera toujours de 40
            }

            return sum;
        }

        /// <summary>
        ///     Contrôle si une petite suite est possible
        /// </summary>
        /// <param name="dices">Tableau des 5 dé</param>
        /// <returns>Valeur que peut prendre la combinaison</returns>
        public int calculateSmallStraight(Dice[] dices)
        {
            int sum = 0;
            int[] i = new int[5];

            // Récupération des valeurs de chaque dé
            i[0] = dices[0].Number;
            i[1] = dices[1].Number;
            i[2] = dices[2].Number;
            i[3] = dices[3].Number;
            i[4] = dices[4].Number;

            // Tri des valeurs par ordre croissant
            Array.Sort(i);

            // Un problème se pose lorsque deux des nombres sont identiques. 
            // Ceci est résolu en déplaçant le nombre qui est le même 
            // à l'extrémité de la matrice et en l'ignorant 
            for (int j = 0; j < 4; j++)
            {
                int temp = 0;

                if (i[j] == i[j + 1])
                {
                    temp = i[j];

                    for (int k = j; k < 4; k++)
                    {
                        i[k] = i[k + 1];
                    }

                    i[4] = temp;
                }
            }

            // Test s'il y a une petite suite
            if (((i[0] == 1) && (i[1] == 2) && (i[2] == 3) && (i[3] == 4)) ||
                ((i[0] == 2) && (i[1] == 3) && (i[2] == 4) && (i[3] == 5)) ||
                ((i[0] == 3) && (i[1] == 4) && (i[2] == 5) && (i[3] == 6)) ||
                ((i[1] == 1) && (i[2] == 2) && (i[3] == 3) && (i[4] == 4)) ||
                ((i[1] == 2) && (i[2] == 3) && (i[3] == 4) && (i[4] == 5)) ||
                ((i[1] == 3) && (i[2] == 4) && (i[3] == 5) && (i[4] == 6)))
            {
                sum = 30; // La valeur de cette combinaison sera toujours de 30
            }

            return sum;
        }

        /// <summary>
        ///     Contrôle si un Yahtzee est possible
        /// </summary>
        /// <param name="dices">Tableau des 5 dé</param>
        /// <returns>Valeur que peut prendre la combinaison</returns>
        public int calculateYahtzee(Dice[] dices)
        {
            int sum = 0;

            // Boucle pour chaque nombre de 1 à 6
            for (int i = 1; i <= 6; i++)
            {

                int cpt = 0;

                // Boucle pour chaque dé
                for (int j = 0; j < 5; j++)
                {
                    // Incrémentation du compteur à chaque fois qu'un dé correspond au nombre en cours
                    if (dices[j].Number == i)
                    {
                        cpt++;
                    }

                    // Si les 5 dés sont identiques, le yahtzee est possible
                    if (cpt > 4)
                    {
                        sum = 50;   // La valeur de cette combinaison sera toujours de 50
                    }
                }
            }

            return sum;
        }

        /// <summary>
        ///     Calcul de la chance
        /// </summary>
        /// <param name="dices">Tableau des 5 dé</param>
        /// <returns>Valeur que peut prendre la combinaison</returns>
        public int calculateChance(Dice[] dices)
        {
            int sum = 0;

            // Somme des 5 dés
            for (int i = 0; i < 5; i++)
            {
                sum += dices[i].Number;
            }

            return sum;
        }


        #endregion
    }
}
