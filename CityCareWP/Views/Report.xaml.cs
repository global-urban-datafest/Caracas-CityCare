using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Common.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using CityCareWP.Controllers;
using System.Device.Location;

namespace CityCareWP.Views
{
    public partial class Report : PhoneApplicationPage
    {
        private ReportController fReportController;
        CameraCaptureTask fCameraCaptureTask;
        GeoCoordinateWatcher fWatcher;
        private double fLongitude = 0;
        private double fLatitude = 0;
        private string fReportType = "";

        public Report()
        {
            InitializeComponent();
             
            fReportController = new ReportController();

            fCameraCaptureTask = new CameraCaptureTask();
            fCameraCaptureTask.Completed += new EventHandler<PhotoResult>(cameraCaptureTask_Completed);
        }

        void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {

            if (e.TaskResult == TaskResult.OK)
            {
                //MessageBox.Show(e.ChosenPhoto.Length.ToString());

                //Code to display the photo on the page in an image control named myImage.
                BitmapImage _bmp = new BitmapImage();
                _bmp.DecodePixelHeight = 154;
                _bmp.DecodePixelWidth = 351;
                _bmp.SetSource(e.ChosenPhoto);
                takeAPicText.Visibility = Visibility.Collapsed;
                cameraIcon.Visibility = Visibility.Collapsed;
                //imageFrame.Height = _bmp.DecodePixelHeight;
                //imageFrame.Width = _bmp.DecodePixelWidth;
                reportImage.Height = 154;
                reportImage.Width = 351;
                reportImage.Source = _bmp;
                //reportImage.HorizontalAlignment = HorizontalAlignment.Center;
            }

        }

        private void takePicture_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            fCameraCaptureTask.Show();
        }


        private void createReport_Click(object sender, RoutedEventArgs e)
        {
            //if (fWalletId == null)
            //{
            //    fWalletId = (((Wallet)wallets.SelectedItem).Id);
            //}
            if (fWatcher == null)
            {
                fWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
                fWatcher.MovementThreshold = 20;
                fWatcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
                fWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            }
            fWatcher.Start();
            
        }

        void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    MessageBox.Show("Location Service is not enabled on the device");
                    break;

                case GeoPositionStatus.NoData:
                    MessageBox.Show(" The Location Service is working, but it cannot get location data.");
                    break;
            }
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (e.Position.Location.IsUnknown)
            {
                MessageBox.Show("Please wait while your prosition is determined....");
            }

            fLatitude = e.Position.Location.Latitude;
            fLongitude= e.Position.Location.Longitude;

            fReportController.OnPostAddReportsSucceed += fReportController_OnPostAddReportsSucceed;
            fReportController.OnPostAddReportsFailed += fReportController_OnPostAddReportsFailed;
            ReportDataModel _reportDataModel = new ReportDataModel();
            _reportDataModel.Latitud = fLatitude;
            _reportDataModel.Longitud = fLongitude;
            _reportDataModel.ProposedReportType = proposeReport.Text;
            _reportDataModel.ProposedReportType = fReportType;
            _reportDataModel.Description = description.Text;
            _reportDataModel.CCUserId = LoginSettings.UserId;
            fReportController.PostAddReports(_reportDataModel);
        }



        void fReportController_OnPostAddReportsFailed(object source, PostAddReportsFailedEventArgs e)
        {
            MessageBox.Show(e._ErrorMessage);
        }

        void fReportController_OnPostAddReportsSucceed(object source, PostAddReportsSucceedEventArgs e)
        {
            MessageBox.Show("The report has been successfully sent");
        }

        private void cancel_Copy_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Home.xaml", UriKind.Relative));
        }
    }
}