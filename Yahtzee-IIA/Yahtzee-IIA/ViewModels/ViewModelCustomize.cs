using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WP.Core;
using Yahtzee_IIA;

namespace Yahtzee_IIA.ViewModels
{
    public class ViewModelCustomize : ObservableObject
    {

        #region Fields
            private DelegateCommand _goToGameCommand;
            private string _pseudo1;
            private string _pseudo2;
            private string _pseudo3;
            private string _pseudo4;

            private bool _is2Player;
            private bool _is3Player;
            private bool _is4Player;
        #endregion

        #region Properties

            /// <summary>
            ///     Obtien la commande _goToGameCommand
            /// </summary>
            public DelegateCommand GoToGameCommand
            {
                get { return _goToGameCommand; }
            }

            public string Pseudo1
            {
                get { return _pseudo1; }
                set
                {
                    if (_pseudo1 != value)
                    {
                        _pseudo1 = value;
                        this.OnPropertyChanged();
                    }
                }
            }

            public string Pseudo2
            {
                get { return _pseudo2; }
                set
                {
                    if (_pseudo2 != value)
                    {
                        _pseudo2 = value;
                        this.OnPropertyChanged();
                    }
                }
            }

            public string Pseudo3
            {
                get { return _pseudo3; }
                set
                {
                    if (_pseudo3 != value)
                    {
                        _pseudo3 = value;
                        this.OnPropertyChanged();
                    }
                }
            }

            public string Pseudo4
            {
                get { return _pseudo4; }
                set
                {
                    if (_pseudo4 != value)
                    {
                        _pseudo4 = value;
                        this.OnPropertyChanged();
                    }
                }
            }

            public bool Is2Player
            {
                get { return _is2Player; }
                set
                {
                    if (_is2Player != value)
                    {
                        _is2Player = value;
                        this.OnPropertyChanged();
                    }
                }
            }

            public bool Is3Player
            {
                get { return _is3Player; }
                set
                {
                    if (_is3Player != value)
                    {
                        _is3Player = value;
                        this.OnPropertyChanged();
                    }
                }
            }

            public bool Is4Player
            {
                get { return _is4Player; }
                set
                {
                    if (_is4Player != value)
                    {
                        _is4Player = value;
                        this.OnPropertyChanged();
                    }
                }
            }

        #endregion

        #region Constructors
            public ViewModelCustomize()
            {
                // On initialise la commande GoToDeviceStatusCommand qui utilisera la methode ExecuteGoToDeviceStatusCommand
                _goToGameCommand = new DelegateCommand(ExecuteGoToGameCommand, CanExecuteGoToGameCommand);
            
                _pseudo1 = "Joueur 1";
                _pseudo2 = "Joueur 2";
                _pseudo3 = "Joueur 3";
                _pseudo4 = "Joueur 4";

                _is2Player = true;
                _is3Player = false;
                _is4Player = false;

            }
        #endregion

        public virtual void ExecuteGoToGameCommand(object parametre)
        {
            string chaine = "";
            

            if (Is2Player) {
                chaine = "?nbPlayer=2";
                chaine += "&pseudo1=" + Pseudo1 + "&pseudo2=" + Pseudo2;
            }

            if (Is3Player)
            {
                chaine = "?nbPlayer=3";
                chaine += "&pseudo1=" + Pseudo1 + "&pseudo2=" + Pseudo2 + "&pseudo3=" + Pseudo3;
            }

            if (Is4Player)
            {
                chaine = "?nbPlayer=4";
                chaine += "&pseudo1=" + Pseudo1 + "&pseudo2=" + Pseudo2 + "&pseudo3=" + Pseudo3 + "&pseudo4=" + Pseudo4;
            }

            System.Diagnostics.Debug.WriteLine("/Views/ViewGame.xaml" + chaine);

            App.RootFrame.Navigate(new Uri("/Views/ViewGame.xaml" + chaine, UriKind.Relative));
        }

        public virtual bool CanExecuteGoToGameCommand(object parametre)
        {
            if (Is2Player && (!string.IsNullOrWhiteSpace(Pseudo1) && !string.IsNullOrWhiteSpace(Pseudo2)) ) {
                return true;
            }
            
            if (Is3Player && (!string.IsNullOrWhiteSpace(Pseudo1) && !string.IsNullOrWhiteSpace(Pseudo2) && !string.IsNullOrWhiteSpace(Pseudo3)) ) {
                return true;
            }

            if (Is4Player && (!string.IsNullOrWhiteSpace(Pseudo1) && !string.IsNullOrWhiteSpace(Pseudo2) && !string.IsNullOrWhiteSpace(Pseudo3) && !string.IsNullOrWhiteSpace(Pseudo4)) ) {
                return true;
            }

            return false;
        }


    }
}
