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

    public partial class AssessmentView : ContentPage
    {
        private readonly int _selectedAssessmentId;
        private Assessment _currentAssessment;
        public AssessmentView(Assessment assessment)
        {
            InitializeComponent();
            _currentAssessment = assessment;
            _selectedAssessmentId = assessment.Id;
            AssessName.Text = assessment.Name;
            AssessType.Text = assessment.Type;
            AssessStart.Text = assessment.AssessStart.Date.ToShortDateString();
            AssessEnd.Text = assessment.AssessEnd.Date.ToShortDateString();
        }

        async void EditAssess_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentEdit(_currentAssessment));
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

                await DatabaseService.RemoveAssessment(_selectedAssessmentId);

                await DisplayAlert("Assessment Deleted", "Assessment Deleted", "OK");
            }
            else
            {
                await DisplayAlert("Delete Canceled", "Nothing Deleted", "OK");
            }

            await Navigation.PopAsync();
        }
    }
}