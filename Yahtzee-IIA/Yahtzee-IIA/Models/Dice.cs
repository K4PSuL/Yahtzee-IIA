using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Core;

namespace Yahtzee_IIA.Models
{
    class Dice : ObservableObject
    {
        #region Fields

        private int _number; 
        private Boolean _keep; 
        private String _image;

        #endregion

        #region Properties

        public int Number
        {
            get { return _number; }
            set { Assign(ref _number, value); }
        }

        public Boolean Keep
        {
            get { return _keep; }
            set { Assign(ref _keep, value); }
        }

        public String Image
        {
            get { return _image; }
            set { Assign(ref _image, value); }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe Dice
        /// </summary>
        public Dice()
        {
            _number = 0;
            _keep = true;
            _image = null;
        }

        #endregion
    }
}
