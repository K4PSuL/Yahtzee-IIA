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
        #endregion

        #region Constructors
            public ViewModelGame()
                {
                    _nextPlayerCommand = new DelegateCommand(ExecuteNextPlayerCommand);
                    _game = new Game(3); // 3 Players
                    //_selectedPlayer = new Player();
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
        #endregion

        #region Methods
            public virtual void ExecuteNextPlayerCommand(object parametre)
            {
                //TODO : Joueur suivant. enregistrement du score du joueur actuel + set le new player
            }

            public void LoadData(int nbPlayer, string pseudo1, string pseudo2, string pseudo3, string pseudo4)
            {
                Player[] listPlayers;

                listPlayers[0] = new Player(pseudo1);
                listPlayers[1] = new Player(pseudo2);
                listPlayers[2] = new Player(pseudo3);
                listPlayers[3] = new Player(pseudo4);


                _game = new Game(listPlayers);
                
                
                Console.WriteLine(nbPlayer);
                Console.WriteLine(pseudo1);
                Console.WriteLine(pseudo2);
                Console.WriteLine(pseudo3);
                Console.WriteLine(pseudo4);


            }


        #endregion

    }
}
