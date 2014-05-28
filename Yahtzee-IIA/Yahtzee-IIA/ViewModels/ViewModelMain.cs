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
    public class ViewModelMain : ObservableObject
    {

        #region Fields

        private DelegateCommand _goToNavigateCommand;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtien la commande _goToNavigateCommand
        /// </summary>
        public DelegateCommand GoToNavigateCommand
        {
            get { return _goToNavigateCommand; }
        }

        #endregion

        #region Constructors

        public ViewModelMain()
        {
            _goToNavigateCommand = new DelegateCommand(ExecuteGoToNavigateCommand);

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
