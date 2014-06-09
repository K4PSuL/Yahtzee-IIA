using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Core;
using Yahtzee_IIA;
using Yahtzee_IIA.Models;

namespace Yahtzee_IIA.ViewModels
{
    public class ViewModelScore : ObservableObject
    {

        #region Fields
        //private DelegateCommand _goToGameCommand;
        private ObservableCollection<Score> _scores;

        #endregion

        #region Properties
        /*
        /// <summary>
        ///     Obtien la commande _goToGameCommand
        /// </summary>
        public DelegateCommand GoToGameCommand
        {
            get { return _goToGameCommand; }
        }
        */
        /// <summary>
        ///     Obtient la collection observable des games.
        /// </summary>
        public ObservableCollection<Score> Scores
        {
            get { return _scores; }
            set { Assign(ref _scores, value); }
        }

        #endregion

        #region Constructors
            public ViewModelScore()
            {
                //_goToGameCommand = new DelegateCommand(ExecuteGoToGameCommand, CanExecuteGoToGameCommand);
                
            }
        #endregion
        /*
        public virtual void ExecuteGoToGameCommand(object parametre)
        {
            
        }

        public virtual bool CanExecuteGoToGameCommand(object parametre)
        {
            return false;
        }
        */
        public void LoadData()
        {
            var queryGames = from g in YahtzeeDataContext.Instance.Score
                             select g;

            YahtzeeDataContext.Instance.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, YahtzeeDataContext.Instance.Score);

            Scores = new ObservableCollection<Score>(YahtzeeDataContext.Instance.Score);
        }
    }
}
