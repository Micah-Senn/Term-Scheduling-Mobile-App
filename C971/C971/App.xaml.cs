using C971.Models;
using C971.Services;
using C971.Views;
using Plugin.LocalNotifications;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Settings.FirstRun)
            {
                DatabaseService.LoadSampleData();

                Settings.FirstRun = false;
            }
            var dashBoard = new Dashboard();
            var navPage = new NavigationPage(dashBoard);
            MainPage = navPage;
        }

        protected override async void OnStart()
        {
            var courseList = await DatabaseService.GetCourses();
            var notifyRandom = new Random();
            var notifyId = notifyRandom.Next(1000);
            foreach (Course courseRecord in courseList)
            {
                if (courseRecord.StartNotification == true)
                {
                    if (courseRecord.CourseStart == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"{ courseRecord.Name} starts today!", notifyId);

                    }
                    if (courseRecord.CourseEnd == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"{ courseRecord.Name} ends today!", notifyId);

                    }
                }
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
