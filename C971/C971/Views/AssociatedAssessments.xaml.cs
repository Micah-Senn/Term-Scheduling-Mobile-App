using C971.Models;
using C971.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssociatedAssessments : ContentPage
    {
        private readonly int _selectedCourseId;
        private Course _currentCourse;
        public AssociatedAssessments(Course selectedCourse)
        {
            InitializeComponent();
            _currentCourse = selectedCourse;
            _selectedCourseId = selectedCourse.Id;
            CourseName.Text = selectedCourse.Name;
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
            int countAssessments = await DatabaseService.GetAssessmentCountAsync(_selectedCourseId);

            //CountLabel.Text = "Assessments: " + countAssessments.ToString();

            AssessmentCollectionView.ItemsSource = await DatabaseService.GetAssessments(_selectedCourseId);
        }

        async void AssessmentCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var assess = (Assessment)e.CurrentSelection.FirstOrDefault();
            if (e.CurrentSelection != null)
            {
               await Navigation.PushAsync(new AssessmentView(assess));
            }
        }

        async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            var courseId = _selectedCourseId;

            await Navigation.PushAsync(new AssessmentAdd(courseId));
        }
    }
}
