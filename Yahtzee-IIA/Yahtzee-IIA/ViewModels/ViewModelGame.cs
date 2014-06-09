using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WP.Core;
using Yahtzee_IIA.Models;

namespace Yahtzee_IIA.ViewModels
{
    public class ViewModelGame : ObservableObject
    {
        #region Fields
            private DelegateCommand _nextPlayerCommand;
            private DelegateCommand _clickRollCommand;
            private DelegateCommand _clickDiceCommand;
            private DelegateCommand _clickScoreCommand;

            private Player _selectedPlayer;
            private Combination _selectedCombination;

            private Game _game;
            private Player[] _listPlayers;

            private int _iPlayer;
            private int _nbPlayer;

            /// <summary>
            ///     Nombre de tours (13 pour chaque partie)
            /// </summary>
            private int _nbLaps;

            

        #endregion

        #region Constructors
            public ViewModelGame()
                {
                    _nextPlayerCommand = new DelegateCommand(ExecuteNextPlayerCommand);
                    _clickRollCommand = new DelegateCommand(ExecuteClickRollCommand);
                    _clickDiceCommand = new DelegateCommand(ExecuteClickDiceCommand);
                    _clickScoreCommand = new DelegateCommand(ExecuteClickScoreCommand);
                    _iPlayer = 0;
                    _nbLaps = 0;
                    //_selectedPlayer = new Player();
                    ListPlayers = new Player[4];
                }
        #endregion

        #region Properties
            public Game Game
            {
                get { return _game; }
                set { Assign(ref _game, value); }
            }

            public Player SelectedPlayer
            {
                get { return _selectedPlayer; }
                set { Assign(ref _selectedPlayer, value); }
            }

            public int IPlayer
            {
                get { return _iPlayer; }
                set { Assign(ref _iPlayer, value); }
            }

            public int NbPlayer
            {
                get { return _nbPlayer; }
                set { Assign(ref _nbPlayer, value); }
            }

            public int NbLaps
            {
                get { return _nbLaps; }
                set { Assign(ref _nbLaps, value); }
            }

            public DelegateCommand NextPlayerCommand
            {
                get { return _nextPlayerCommand; }
            }

            public DelegateCommand ClickRollCommand
            {
                get { return _clickRollCommand; }
            }

            public DelegateCommand ClickScoreCommand
            {
                get { return _clickScoreCommand; }
            }

            public DelegateCommand ClickDiceCommand
            {
                get { return _clickDiceCommand; }
            }

            public Player[] ListPlayers
            {
                get { return _listPlayers; }
                set { Assign(ref _listPlayers, value); }
            }

            public Combination SelectedCombination
            {
                get { return _selectedCombination; }
                set { Assign(ref _selectedCombination, value); }
            }

            

        #endregion

        #region Methods
            /// <summary>
            ///     Action lors du clic sur le bouton "Joueur suivant"
            /// </summary>
            /// <param name="parametre"></param>
            protected virtual void ExecuteNextPlayerCommand(object parametre)
            {
            }

            /// <summary>
            ///     Action lors de la sélection d'une combinaison
            /// </summary>
            /// <param name="parametre">La combinaison sélectionnée</param>
            protected virtual void ExecuteClickScoreCommand(object parametre)
            {
                SelectedCombination = parametre as Combination;

                // On bloque la combinaison sélectionnée pour qu'elle ne puisse plus être jouée
                SelectedCombination.IsNotFilled = false;

                // Remise à zéro des autres combinaisons
                foreach (Combination c in _selectedPlayer.Combinations)
	                {
                        if (SelectedCombination.Name != c.Name && c.IsNotFilled == true)
                        {
                            c.Value = 0;
                        }
	                }

                // Passage au joueur suivant
                if (IPlayer < NbPlayer - 1 )
                {
                    IPlayer++;
                }
                else
                {
                    IPlayer = 0;

                    // Incrémentation du nombre de tour effectués
                    NbLaps++;
                }


                // Passage au joueur suivant tant que toutes les combinaisons ne sont pas jouées
                if (NbLaps < 13)
                {
                    calculateScorePlayer(SelectedPlayer);

                    SelectedPlayer = ListPlayers[IPlayer];

                    // On initialise un nouveau tour pour le prochain joueur
                    initSelectedPlayer();
                }
                else
                {
                    //TODO 1 : Affichage d'une nouvelle page avec le tableau des scores + les totaux
                    //TODO 2 : Ajout d'un bouton "Rejouer"

                    List<Player> winners = new List<Player>();  // Liste des gagnants (plusieurs si égalité)
                   
                    foreach (Player player in Game.Players)
                    {
                        calculateScorePlayer(player);

                        // Sauvegarde du/des gagnant(s)
                        if (winners.Count() == 0)
                        {
                            winners.Add(player);
                        }
                        else
                        {
                            if (winners[winners.Count() - 1].GrandTotal <= player.GrandTotal)
                            {
                                // On vide la liste si le nouveau total est supérieur à l'ancien
                                if (winners[winners.Count() - 1].GrandTotal < player.GrandTotal)
                                {
                                    winners.Clear();
                                }
                                winners.Add(player);
                            }
                        }
                    }

                    // Affichage d'un message donnant le(s) nom(s) du/des gagnant(s) et son/leur score
                    String message = "";
                    
                    if (winners.Count() == 1)
                    {
                        message = "\"" + winners[0].Name + "\" a gagné avec " + winners[0].GrandTotal + " points !";
                    }
                    else
                    {
                        message = "Egalité ! ";
                        for (int i = 0; i < winners.Count(); i++)
                        {
                            message += "\"" + winners[i].Name + "\", ";
                        }
                        message += " ont gagné avec " + winners[0].GrandTotal + " points !";
                    }

                    var result = MessageBox.Show(message, "Partie terminée", MessageBoxButton.OKCancel);

                    // Sauvegarde du Score en BDD

                    Score s = new Score(ListPlayers, _nbPlayer);

                    YahtzeeDataContext.Instance.Score.InsertOnSubmit(s);

                    try
                    {
                        YahtzeeDataContext.Instance.SubmitChanges();
                    }
                    catch (Exception e)
                    {
                        
                        throw;
                    }

                    int nb = YahtzeeDataContext.Instance.Score.Count();

                    // Si le bouton OK est sélectionné, on affiche la page des scores
                    if (result == MessageBoxResult.OK)
                    {
                        App.RootFrame.GoBack();
                    }

                }
                
            }

            private void calculateScorePlayer(Player player)
            {
                // Calcul des totaux
                player.TotalScore = player.calculateTotalScore();
                player.TotalUpperSection = player.calculateTotalUpperSection();
                player.TotalLowerSection = player.calculateTotalLowerSection();
                player.GrandTotal = player.calculateGrandTotal();

            }
            /// <summary>
            ///     Mise à jour de l'image du dé en fonction de la propriété "keep"
            /// </summary>
            /// <param name="dice"></param>
            protected virtual void UpdateDiceProperties(Dice dice)
            {
                if (dice.Keep == true)
                {
                    dice.Image = "/Resources/de" + dice.Number + "k.png";
                }
                else
                {
                    dice.Image = "/Resources/de" + dice.Number + ".png";
                }
            }

            /// <summary>
            ///     Action lors du clic sur un dé
            /// </summary>
            /// <param name="parametre">Id du dé cliqué</param>
            protected virtual void ExecuteClickDiceCommand(object parametre)
            {
                // Récupération du dé cliqué
                int indexDice = Int32.Parse((string)parametre);
                Dice dice = _selectedPlayer.Dices[indexDice];

                // Si le dé était gardé, on ne le garde plus, et inversement
                dice.Keep = !dice.Keep;

                // Mise à jour de l'image du dé
                this.UpdateDiceProperties(dice);
            }
        
            /// <summary>
            ///     Action lors du clic sur le bouton "Lancer"
            /// </summary>
            /// <param name="parametre"></param>
            protected virtual void ExecuteClickRollCommand(object parametre)
            {
                // Lancement des dés
                SelectedPlayer.roll();

                // Activation des dés
                SelectedPlayer.IsStart = true;

                // Activation des combinaisons si elles ne sont pas encore remplies
                foreach (Combination combination in SelectedPlayer.Combinations)
                {
                    if (combination.IsNotFilled)
                    {
                        combination.Playable = true;
                    }
                }

                // Décrémentation du nombre de coups restants
                if (SelectedPlayer.NbRoll > 0)
                {
                    SelectedPlayer.NbRoll--;

                    // S'il n'y a plus de lancers
                    if (SelectedPlayer.NbRoll == 0)
                    {
                        // Le bouton "Lancer" est grisé
                        SelectedPlayer.IsPlayable = false;

                        // Les dés deviennent inactifs
                        SelectedPlayer.IsStart = false;

                        // Les dés sont remis à leur l'état initial
                        foreach (Dice dice in SelectedPlayer.Dices)
                        {
                            // On garde obligatoirement tous les dés
                            dice.Keep = true;

                            // Mise à jour de l'image du dé
                            this.UpdateDiceProperties(dice);
                        }
                    }
                }

                // Calcule les scores des combinaisons d'après les 5 dés
                SelectedPlayer.checkCombinations();
            }

            /// <summary>
            ///     Méthode de chargement des données au début du jeu
            /// </summary>
            /// <param name="nbPlayer">Nombre de joueurs</param>
            /// <param name="pseudo1">Pseudo du joueur 1</param>
            /// <param name="pseudo2">Pseudo du joueur 2</param>
            /// <param name="pseudo3">Pseudo du joueur 3</param>
            /// <param name="pseudo4">Pseudo du joueur 4</param>
            public void LoadData(int nbPlayer, string pseudo1, string pseudo2, string pseudo3, string pseudo4)
            {
                NbPlayer = nbPlayer;
                ListPlayers[0] = new Player(pseudo1);
                ListPlayers[1] = new Player(pseudo2);

                if (nbPlayer > 2)
                {
                    ListPlayers[2] = new Player(pseudo3);

                    if (nbPlayer > 3)
                    {
                        ListPlayers[3] = new Player(pseudo4);
                    }
                }

                _selectedPlayer = ListPlayers[0];

                this.Game = new Models.Game();
                this.Game.Players.AddRange(_listPlayers.Where( p => p != null));

                this.initSelectedPlayer();
            }

            /// <summary>
            ///     Méthode d'initialisation d'un joueur (appelée à chaque tour)
            /// </summary>
            public void initSelectedPlayer()
            {
                // Parcours de chaque joueur
                foreach (Player player in this.Game.Players)
                {
                    // Les dés sont remis à leur l'état initial
                    foreach (Dice dice in player.Dices)
                    {
                        dice.Image = "/Resources/deInit.png";
                        dice.Keep = false;
                    }

                    // Si ce n'est pas à ce joueur de jouer, on bloque le bouton "Lancer"
                    if (SelectedPlayer != player)
                    {
                        player.IsPlayable = false;
                    }
                    else
                    {
                        player.IsPlayable = true;
                    }

                    // On bloque les combinaisons pour qu'elles ne puissent pas être sélectionnées avant le prochain tour
                    foreach (Combination combination in player.Combinations)
                    {
                        combination.Playable = false;
                    }

                    // Les propriétés sont remises à leur l'état initial
                    player.IsStart = false;
                    player.NbRoll = 3;
                }

            }

        #endregion

    }
}
