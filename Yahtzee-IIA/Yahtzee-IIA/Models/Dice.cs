using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee_IIA.Models
{
    class Dice
    {
        private int _number; 
        private Boolean _keep; 
        private String _image;

        public int Number
        {
            get { return _number; }
            set
            {
                if (_number != value)
                {
                    _number = value;

                    //this.OnPropertyChanged();
                }
            }
        }

        public Boolean Keep
        {
            get { return _keep; }
            set
            {
                if (_keep != value)
                {
                    _keep = value;

                    //this.OnPropertyChanged();
                }
            }
        }

        public String Image
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    _image = value;

                    //this.OnPropertyChanged();
                }
            }
        }

    }
}
