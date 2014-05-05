using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee_IIA.Models
{
    class Player
    {
        #region Fields

        private int _id;
        private String _name;
        private Score _score;

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;

                    //this.OnPropertyChanged();
                }
            }
        }

        public String Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;

                    //this.OnPropertyChanged();
                }
            }
        }
        public Score Score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;

                    //this.OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe Player
        /// </summary>
        /// <param name="id">Id du joueur</param>
        /// <param name="name">Nom du joueur</param>
        /// <param name="score">Score du joueur</param>
        public Player(int id, String name, Score score)
        {
            _id = id;
            _name = name;
            _score = score;
        }

        #endregion
    }
}
