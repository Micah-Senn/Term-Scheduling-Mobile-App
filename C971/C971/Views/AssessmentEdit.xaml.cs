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
    public partial class AssessmentEdit : ContentPage
    {
        private readonly int _selectedAssessId;
        private readonly int _selectedCourseId;
        public AssessmentEdit(Assessment assessment)
        {
            InitializeComponent();
            _selectedCourseId = assessment.CourseId;
            _selectedAssessId = assessment.Id;
            AssessName.Text = assessment.Name;
            AssessType.SelectedItem = assessment.Type;
            AssessStart.Date = assessment.AssessStart.Date;
            AssessEnd.Date = assessment.AssessEnd.Date;
            Notification.IsToggled = assessment.StartNotification;
        }

        async void SaveAssess_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AssessName.Text))
            {
                await DisplayAlert("Missing Name", "Please enter the name of the assessment.", "OK");
                return;
            }

            if (AssessType.SelectedItem == null)
            {
                await DisplayAlert("Missing Type", "Please enter the assessment type.", "OK");
                return;
            }

            if (AssessStart.Date > AssessEnd.Date)
            {
                await DisplayAlert("Date Error", "The start date cannot be after the end date", "OK");
                return;
            }


            await DatabaseService.UpdateAssessment(_selectedAssessId, _selectedCourseId, AssessName.Text, AssessType.SelectedItem.ToString(), Notification.IsToggled,
                AssessStart.Date, AssessEnd.Date);

            await Navigation.PopToRootAsync();
        }

        async void CancelAssess_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void DeleteAssess_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Assessment?", "Delete this assessment?", "Yes", "No");

            if (answer == true)
            {

                await DatabaseService.RemoveAssessment(_selectedAssessId);

                await DisplayAlert("Course Deleted", "Assessment Deleted", "OK");
            }
            else
            {
                await DisplayAlert("Delete Canceled", "Nothing Deleted", "OK");
            }

            await Navigation.PopAsync();
        }
    }
}
