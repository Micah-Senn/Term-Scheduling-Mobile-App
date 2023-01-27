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
    public partial class AssessmentAdd : ContentPage
    {
        private readonly int _selectedCourseId;
        public AssessmentAdd(int courseId)
        {
            _selectedCourseId = courseId;
            InitializeComponent();
        }

        async void CancelAssess_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
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

            await DatabaseService.AddAssessment(_selectedCourseId, AssessName.Text, AssessType.SelectedItem.ToString(), Notification.IsToggled,
                AssessStart.Date, AssessEnd.Date);

            await Navigation.PopAsync();
        }
    }
}