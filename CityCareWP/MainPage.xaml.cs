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
using CityCareWP.Resources;
using System.IO.IsolatedStorage;

namespace CityCareWP
{
    public partial class MainPage : PhoneApplicationPage
    {
        private UserController fUserController;
        private IsolatedStorageSettings fIsolatedStorageSettings;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            LoadInfoOfSystem();

            fUserController = new UserController();
           

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void LoadInfoOfSystem()
        {
            fIsolatedStorageSettings = IsolatedStorageSettings.ApplicationSettings;
            if (email.Text != "" && password.Password != "")
            {
                LoginSettings.Email = email.Text;
                LoginSettings.Password = password.Password;
                fIsolatedStorageSettings.Add(LoginSettings.email, email.Text);
                fIsolatedStorageSettings.Add(LoginSettings.password, password.Password);
            }
            fIsolatedStorageSettings.Save();
        }

        private void signin_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/Views/Home.xaml", UriKind.Relative));

            fUserController.OnPostValidateUserSucceed += fUserController_OnPostValidateUserSucceed;
            fUserController.OnPostValidateUserFailed += fUserController_OnPostValidateUserFailed;

            UserAuthDataModel _authDataModel = new UserAuthDataModel { Email = email.Text, Password = password.Password };
            fUserController.PostValidateUser(_authDataModel);
        }

        void fUserController_OnPostValidateUserFailed(object source, PostValidateUserFailedEventArgs e)
        {
            MessageBox.Show(e._ErrorMessage);
        }

        void fUserController_OnPostValidateUserSucceed(object source, PostValidateUserSucceedEventArgs e)
        {
            LoginSettings.UserId = e.fResponseAuthDataModel.fUserId;
            fIsolatedStorageSettings.Add(LoginSettings.userId, e.fResponseAuthDataModel.fUserId);
            NavigationService.Navigate(new Uri("/Views/Home.xaml", UriKind.Relative));
        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Register.xaml", UriKind.Relative));
        }
         

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
     



    }
}