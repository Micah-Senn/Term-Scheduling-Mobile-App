using C971.Models;
using C971.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseView : ContentPage
    {
        private readonly int _selectedCourseId;
        private Course _currentCourse;
        public CourseView(Course selectedCourse)
        {
            InitializeComponent();
            _currentCourse = selectedCourse;
            _selectedCourseId = selectedCourse.Id;
            CourseName.Text = selectedCourse.Name;
            CourseStatus.Text = selectedCourse.Status;
            CourseStart.Text = selectedCourse.CourseStart.Date.ToShortDateString();
            CourseEnd.Text = selectedCourse.CourseEnd.Date.ToShortDateString();
            InstName.Text = selectedCourse.InstName;
            InstEmail.Text = selectedCourse.InstEmail;
            InstPhone.Text = selectedCourse.InstPhone;
            Notes.Text = selectedCourse.Notes;
            //Notification.IsToggled = selectedCourse.StartNotification;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            int countAssessments = await DatabaseService.GetAssessmentCountAsync(_selectedCourseId);

            CountLabel.Text = "Assessments: " + countAssessments.ToString();

            AssessmentCollectionView.ItemsSource = await DatabaseService.GetAssessments(_selectedCourseId);
        }

        async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Course?", "Delete this course?", "Yes", "No");

            if (answer == true)
            {

                await DatabaseService.RemoveCourse(_selectedCourseId);

                await DisplayAlert("Course Deleted", "Course Deleted", "OK");
            }
            else
            {
                await DisplayAlert("Delete Canceled", "Nothing Deleted", "OK");
            }

            await Navigation.PopAsync();
        }

        async void CancelCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void EditCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseEdit(_currentCourse));
        }

        private void CourseCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddAssessment_Clicked(object sender, EventArgs e)
        {

        }

        private void AssessmentCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}