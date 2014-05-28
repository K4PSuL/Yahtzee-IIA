using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            private Player _selectedPlayer;
            private Game _game;
            private Player[] _listPlayers;
            private Combination _selectedCombination;
            private bool _isStart;

        #endregion

        #region Constructors
            public ViewModelGame()
                {
                    _nextPlayerCommand = new DelegateCommand(ExecuteNextPlayerCommand);
                    _clickRollCommand = new DelegateCommand(ExecuteClickRollCommand);
                    _clickDiceCommand = new DelegateCommand(ExecuteClickDiceCommand);
                    _isStart = false;

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

            public bool IsStart
            {
                get { return _isStart; }
                set { Assign(ref _isStart, value); }
            }

            public DelegateCommand NextPlayerCommand
            {
                get { return _nextPlayerCommand; }
            }

            public DelegateCommand ClickRollCommand
            {
                get { return _clickRollCommand; }
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
            protected virtual void ExecuteNextPlayerCommand(object parametre)
            {
            }

            protected virtual void ExecuteClickDiceCommand(object parametre)
            {
                //TODO : Garder le dès ou pas
                int indexDice = Int32.Parse((string)parametre);

                Dice dice = Game.Dices[indexDice];

                dice.Keep = !dice.Keep;

                if (dice.Keep == true)
                {
                    dice.Image = "/Resources/de" + dice.Number + "k.png";
                }
                else
                {
                    dice.Image = "/Resources/de" + dice.Number + ".png";

                }

            }
        
            protected virtual void ExecuteClickRollCommand(object parametre)
            {
                //TODO : Lancer les dés

                Game.roll();

                if (Game.NbRoll > 0)
                {
                    Game.NbRoll--;

                    if (Game.NbRoll == 0)
                    {
                        Game.IsPlayable = false;
                    }
                }

                IsStart = true;

                //  Game.checkCombinaison(). Verifier toutes les combinaisons possible et afficher les valeurs (Combinaison.setValue())
            }

            public void LoadData(int nbPlayer, string pseudo1, string pseudo2, string pseudo3, string pseudo4)
            {
                
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

                this.Game = new Models.Game();
                this.Game.Players.AddRange(_listPlayers.Where( p => p != null));

                foreach (Dice dice in Game.Dices)
                {
                    dice.Image = "/Resources/deInit.png";
                    
                }

                YahtzeeDataContext.Instance.Game.InsertOnSubmit(Game);
            }


        #endregion

    }
}
