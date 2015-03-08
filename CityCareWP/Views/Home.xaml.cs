using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Common.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CityCareWP.Views
{
    public partial class Home : PhoneApplicationPage
    {
        public Home()
        {
            InitializeComponent();
        }

        private void createNewReport_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Report.xaml", UriKind.Relative));
        }

        private void checkPending_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PendingReports.xaml", UriKind.Relative));
        }

        private void signout_Click(object sender, RoutedEventArgs e)
        {
            LoginSettings.Email = null;
            LoginSettings.Password = null;
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }



    }
}