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

        #endregion

        #region Properties

        /// <summary>
        ///     Obtien la commande _goToGameCommand
        /// </summary>
        public DelegateCommand GoToGameCommand
        {
            get { return _goToGameCommand; }
        }

        #endregion

        #region Constructors

        public ViewModelCustomize()
        {
            // On initialise la commande GoToDeviceStatusCommand qui utilisera la methode ExecuteGoToDeviceStatusCommand
            _goToGameCommand = new DelegateCommand(ExecuteGoToNavigateCommand);

        }

        #endregion

        public virtual void ExecuteGoToNavigateCommand(object parametre)
        {

            App.RootFrame.Navigate(new Uri(parametre.ToString(), UriKind.Relative));
  
        }


    }
}
