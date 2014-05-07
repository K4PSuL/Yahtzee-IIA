using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Core;

namespace Yahtzee_IIA.Models
{
    class Player : ObservableObject
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
            set { Assign(ref _id, value); }
        }

        public String Name
        {
            get { return _name; }
            set { Assign(ref _name, value); }
        }
        public Score Score
        {
            get { return _score; }
            set { Assign(ref _score, value); }
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
