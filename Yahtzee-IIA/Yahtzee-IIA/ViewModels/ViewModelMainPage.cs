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
    public class ViewModelMainPage : ObservableObject
    {

        #region Fields

        private DelegateCommand _goToGameCommand;
        private DelegateCommand _goToSettingCommand;
        private DelegateCommand _goToRankCommand;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtien la commande _goToGameCommand
        /// </summary>
        public DelegateCommand GoToGameCommand
        {
            get { return _goToGameCommand; }
        }

        /// <summary>
        ///     Obtien la commande _goToSettingCommand
        /// </summary>
        public DelegateCommand GoToSettingCommand
        {
            get { return _goToSettingCommand; }
        }

        /// <summary>
        ///     Obtien la commande _goToRankCommand
        /// </summary>
        public DelegateCommand GoToRankCommand
        {
            get { return _goToRankCommand; }
        }


        #endregion

        #region Constructors

        public ViewModelMainPage()
        {
            // On initialise la commande GoToDeviceStatusCommand qui utilisera la methode ExecuteGoToDeviceStatusCommand
            _goToGameCommand = new DelegateCommand(ExecuteGoToNavigateCommand);
            _goToSettingCommand = new DelegateCommand(ExecuteGoToNavigateCommand);
            _goToRankCommand = new DelegateCommand(ExecuteGoToNavigateCommand);

            // Permet d'executer une commande depuis le code C#
            // _GoToDeviceStatusCommand.Execute(null);
        }

        #endregion

        public virtual void ExecuteGoToNavigateCommand(object parametre)
        {

            App.RootFrame.Navigate(new Uri(parametre.ToString(), UriKind.Relative));
   
        }


    }
}
