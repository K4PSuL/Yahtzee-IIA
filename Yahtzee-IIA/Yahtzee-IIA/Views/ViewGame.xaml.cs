using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Yahtzee_IIA.Models;
using System.Windows.Media;

namespace Yahtzee_IIA.Views
{
    public partial class GamePage : PhoneApplicationPage
    {
        
        public GamePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            #region Recuperer les pseudos

            string pseudo1Handle;
            string pseudo2Handle;
            string pseudo3Handle;
            string pseudo4Handle;
            string nbPlayerHandle;

            string pseudo1 = "Joueur 1";
            string pseudo2 = "Joueur 2";
            string pseudo3 = "Joueur 3";
            string pseudo4 = "Joueur 4";
            int nbPlayer = 2;
            
            base.OnNavigatedTo(e);

            if (NavigationContext.QueryString.TryGetValue("nbPlayer", out nbPlayerHandle))
            {
                nbPlayer = int.Parse(nbPlayerHandle);
            }

            if (NavigationContext.QueryString.TryGetValue("pseudo1", out pseudo1Handle))
            {
                pseudo1 = pseudo1Handle;
            }

            if (NavigationContext.QueryString.TryGetValue("pseudo2", out pseudo2Handle))
            {
                pseudo2 = pseudo2Handle;
            }

            if (NavigationContext.QueryString.TryGetValue("pseudo3", out pseudo3Handle))
            {
                pseudo3 = pseudo3Handle;
            }

            if (NavigationContext.QueryString.TryGetValue("pseudo4", out pseudo4Handle))
            {
                pseudo4 = pseudo4Handle;
            }  

            if (this.DataContext is ViewModels.ViewModelGame)
            {
                ((ViewModels.ViewModelGame)this.DataContext).LoadData(nbPlayer, pseudo1, pseudo2, pseudo3, pseudo4);
            }

            #endregion

            #region Creation des pivots

            Player[] listPlayers = ((ViewModels.ViewModelGame)this.DataContext).ListPlayers;

            #endregion
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(pivot.SelectedItem);
        }

        private void OnClickBtnRoll(object sender, RoutedEventArgs e)
        {

            Game game = ((ViewModels.ViewModelGame)this.DataContext).Game;

            if (game.NbRoll > 0)
            {
                game.NbRoll--;

                if (game.NbRoll == 0)
                {
                    game.IsPlayable = false;
                }
            }
        

            // TODO : 
            //  Decrementer le nombre de coups restants
            //  Si nbCoupsRestants = 0; Desactiver le bouton jouer
            //
            //  Game.roll();
            //  Game.checkCombinaison(). Verifier toutes les combinaisons possible et afficher les valeurs (Combinaison.setValue())
            //  
            // 
            // 
        }

        //private void OnClickDice(Button sender, RoutedEventArgs e)
        //{


        //    ImageBrush background = new ImageBrush();
        //    background.ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Resources/de6.png", UriKind.Relative));
        //    sender.Background = background;
        //}
    }




}