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
    public partial class CourseEdit : ContentPage
    {
        private readonly int _selectedCourseId;
        private readonly int _selectedTermId;
        public CourseEdit(Course selectedCourse)
        {
            InitializeComponent();
            _selectedTermId = selectedCourse.TermId;
            _selectedCourseId = selectedCourse.Id;
            CourseName.Text = selectedCourse.Name;
            CourseStatus.SelectedItem = selectedCourse.Status;
            CourseStart.Date = selectedCourse.CourseStart.Date;
            CourseEnd.Date = selectedCourse.CourseEnd.Date;
            InstName.Text = selectedCourse.InstName;
            InstEmail.Text = selectedCourse.InstEmail;
            InstPhone.Text = selectedCourse.InstPhone;
            Notes.Text = selectedCourse.Notes;
            Notification.IsToggled = selectedCourse.StartNotification;
        }

        async void SaveCourse_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CourseName.Text))
            {
                await DisplayAlert("Missing Name", "Please enter the name of the course.", "OK");
                return;
            }

            if (CourseStatus.SelectedItem == null)
            {
                await DisplayAlert("Missing Status", "Please enter the status of the course.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(InstName.Text))
            {
                await DisplayAlert("Missing Name", "Please enter your instructors name", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(InstPhone.Text))
            {
                await DisplayAlert("Missing Name", "Please enter your instructors phone number", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(InstEmail.Text))
            {
                await DisplayAlert("Missing Email", "Please enter your instructors email", "OK");
                return;
            }

            if (CourseStart.Date > CourseEnd.Date)
            {
                await DisplayAlert("Date Error", "The start date cannot be after the end date", "OK");
                return;
            }

            await DatabaseService.UpdateCourse(_selectedCourseId, _selectedTermId, CourseName.Text, CourseStatus.SelectedItem.ToString(), Notification.IsToggled,
                CourseStart.Date, CourseEnd.Date, Notes.Text, InstName.Text, InstEmail.Text, InstPhone.Text);
            await  Navigation.PopAsync(); 
        }

        async void CancelCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Home_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
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
    }
}