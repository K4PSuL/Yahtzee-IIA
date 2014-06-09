using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Yahtzee_IIA.Models;
using System.Collections.ObjectModel;

namespace Yahtzee_IIA.Views
{
    public partial class ViewScores : PhoneApplicationPage
    {
        public ViewScores()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (this.DataContext is ViewModels.ViewModelScore)
            {
                ((ViewModels.ViewModelScore)this.DataContext).LoadData();
            }
        }
    }
}