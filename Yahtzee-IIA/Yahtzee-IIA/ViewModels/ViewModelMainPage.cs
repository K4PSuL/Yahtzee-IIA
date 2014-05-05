using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WP.Core;
using Yahtzee_IIA;

namespace WP8.DevicStatus.ViewModels
{
    public class ViewModelMainPage : ObservableObject
    {

        #region Fields

        private DelegateCommand _goToGameCommand;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtien la commande GoToDeviceStatusCommand
        /// </summary>
        public DelegateCommand GoToGameCommand
        {
            get { return _goToGameCommand; }
        }


        #endregion

        #region Constructors

        public ViewModelMainPage()
        {
            // On initialise la commande GoToDeviceStatusCommand qui utilisera la methode ExecuteGoToDeviceStatusCommand
            _goToGameCommand = new DelegateCommand(ExecuteGoToNavigateCommand);

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
