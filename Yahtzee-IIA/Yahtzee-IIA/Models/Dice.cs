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
    public class Dice : ObservableObject
    {
        #region Fields

        private int _number; 
        private Boolean _keep; 
        private String _image;

        #endregion

        #region Properties

        [Column(DbType = "Int")]
        public int Number
        {
            get { return _number; }
            set { Assign(ref _number, value); }
        }

        [Column(DbType = "Bit", CanBeNull = false)]
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
            _keep = false;
            _image = null;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Génère un nombre aléatoire entre 1 et 5 pour le dé 
        /// </summary>
        public void random()
        {
            //Tirage d'un nombre au hasard entre 1 et 6 et mise à jour de la propriété « number » du dé
            //Random random = new Random();
            Random random = new Random(unchecked((int)DateTime.Now.Ticks));
            int randomNumber = random.Next(1, 7);
            this.Number = randomNumber;

            //Mise à jour de la propriété « image » du dé
            this.Image = "/Resources/de" + randomNumber + ".png";
        }

        #endregion

    }
}
