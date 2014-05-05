using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WP.Core
{
    public class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Events

        /// <summary>
        ///     Se déclenche lorsqu'une propriété de l'objet a changée de valeur
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Se déclenche lorsqu'une propriété de l'objet VA changée de valeur
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        #region Methodes

        /// <summary>
        ///  Permet de déclencher l'évenement de PropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler tempHandler = PropertyChanged;

            if (tempHandler != null)
            {
                tempHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        ///  Permet de déclencher l'évenement de PropertyChanging
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanging(string propertyName)
        {
            PropertyChangingEventHandler tempHandler = PropertyChanging;

            if (tempHandler != null)
            {
                tempHandler(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
  
    }
}
