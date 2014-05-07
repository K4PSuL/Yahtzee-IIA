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


        protected virtual void Assign<T>(ref T field, T newValue, [CallerMemberName]string propertyName = "")
        {

            if (field == null || !(field.Equals(newValue)))
            {
                // S'assure que la methode OnPropertyChanging est bien executer sur le thread UI !! (SINON CA PLANTE !!)
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => OnPropertyChanging(propertyName));


                field = newValue;

                // S'assure que la methode OnPropertyChanged est bien executer sur le thread UI !! (SINON CA PLANTE !!)
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => OnPropertyChanged(propertyName));
            }

        }


        #endregion

    }
}
