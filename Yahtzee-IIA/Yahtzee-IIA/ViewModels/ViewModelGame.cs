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
            private Player _selectedPlayer;
            private Game _game;
            private Player[] _listPlayers;
        #endregion

        #region Constructors
            public ViewModelGame()
                {
                    _nextPlayerCommand = new DelegateCommand(ExecuteNextPlayerCommand);
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

            public DelegateCommand NextPlayerCommand
            {
                get { return _nextPlayerCommand; }
            }

            public Player[] ListPlayers
            {
                get { return _listPlayers; }
                set { Assign(ref _listPlayers, value); }
            }

        #endregion

        #region Methods
            public virtual void ExecuteNextPlayerCommand(object parametre)
            {
                //TODO : Joueur suivant. enregistrement du score du joueur actuel + set le new player
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

                YahtzeeDataContext.Instance.Game.InsertOnSubmit(Game);
            }


        #endregion

    }
}
