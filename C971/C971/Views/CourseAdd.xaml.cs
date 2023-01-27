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
    public partial class CourseAdd : ContentPage
    {
        private readonly int _selectedTermId;
        public CourseAdd(int termId)
        {
            InitializeComponent();
            _selectedTermId = termId;
        }

        async void CancelCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
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
                await DisplayAlert("Missing Phone", "Please enter your instructors phone number", "OK");
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

            await DatabaseService.AddCourse(_selectedTermId, CourseName.Text, CourseStatus.SelectedItem.ToString(), Notification.IsToggled,
                CourseStart.Date, CourseEnd.Date, Notes.Text, InstName.Text, InstEmail.Text, InstPhone.Text);

            await Navigation.PopAsync();
        }

        async void Home_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}