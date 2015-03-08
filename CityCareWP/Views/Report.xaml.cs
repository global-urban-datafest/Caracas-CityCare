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
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace CityCareWP.Views
{
    public partial class Report : PhoneApplicationPage
    {
        CameraCaptureTask fCameraCaptureTask;

        public Report()
        {
            InitializeComponent();
            //imageFrame.Visibility = Visibility.Collapsed;

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

        private void cancelClick(object sender, RoutedEventArgs e)
        {

        }


        private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}