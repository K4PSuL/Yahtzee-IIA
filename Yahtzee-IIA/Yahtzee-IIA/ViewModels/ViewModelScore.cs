using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Core;
using Yahtzee_IIA;

namespace Yahtzee_IIA.ViewModels
{
    public class ViewModelScore : ObservableObject
    {

        #region Fields
        private DelegateCommand _goToGameCommand;
        private string _pseudo1;

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

      

        #endregion

        #region Constructors
            public ViewModelScore()
            {
                _goToGameCommand = new DelegateCommand(ExecuteGoToGameCommand, CanExecuteGoToGameCommand);
                _pseudo1 = "Joueur 1";
            }
        #endregion

        public virtual void ExecuteGoToGameCommand(object parametre)
        {
            string chaine = "";
        }

        public virtual bool CanExecuteGoToGameCommand(object parametre)
        {
            return false;
        }


    }
}
