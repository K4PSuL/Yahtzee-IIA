using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Core;

namespace Yahtzee_IIA.Models
{
    [Table]
    class Dice : ObservableObject
    {
        #region Fields

        private int _number; 
        private Boolean _keep; 
        private String _image;

        #endregion

        #region Properties

        [Column(DbType = "Integer")]
        public int Number
        {
            get { return _number; }
            set { Assign(ref _number, value); }
        }

        [Column(DbType = "Boolean", CanBeNull = false)]
        public Boolean Keep
        {
            get { return _keep; }
            set { Assign(ref _keep, value); }
        }

        [Column(DbType = "NVarChar(1024)", CanBeNull = false)]
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
