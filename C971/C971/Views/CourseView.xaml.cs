using C971.Models;
using C971.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            int countAssessments = await DatabaseService.GetAssessmentCountAsync(_selectedCourseId);

            CountLabel.Text = "Assessments: " + countAssessments.ToString();

            if (countAssessments == 0)
            {
                AddAssessment.IsVisible = true;
                ViewAssessment.IsVisible = false;
            }
            else
            {
                AddAssessment.IsVisible = false;
                ViewAssessment.IsVisible = true;
            }
            CourseName.Text = _currentCourse.Name;
            CourseStatus.Text = _currentCourse.Status;
            CourseStart.Text = _currentCourse.CourseStart.Date.ToShortDateString();
            CourseEnd.Text = _currentCourse.CourseEnd.Date.ToShortDateString();
            InstName.Text = _currentCourse.InstName;
            InstEmail.Text = _currentCourse.InstEmail;
            InstPhone.Text = _currentCourse.InstPhone;
            Notes.Text = _currentCourse.Notes;

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


        async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            var courseId = _selectedCourseId;

            await Navigation.PushAsync(new AssessmentAdd(courseId));
        }

        async void ShareButton_Clicked(object sender, EventArgs e)
        {
            var text = Notes.Text;
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }

        async void ViewAssessment_Clicked(object sender, EventArgs e)
        {
            var course = _currentCourse;

            await Navigation.PushAsync(new AssociatedAssessments(course));
        }
    }
}