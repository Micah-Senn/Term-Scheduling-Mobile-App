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
    public partial class TermView : ContentPage
    {
        private readonly int _selectedTermId;
        private Term _currentTerm;
        public TermView(Term term)
        {
            InitializeComponent();
            _selectedTermId = term.Id;
            _currentTerm = term;
            TermTitle.Text = term.Title;
            TermStart.Text = term.TermStart.ToShortDateString();
            TermEnd.Text = term.TermEnd.ToShortDateString();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            int countCourses = await DatabaseService.GetCourseCountAsync(_selectedTermId);

            CountLabel.Text = "Courses: " + countCourses.ToString();


            if (countCourses == 0)
            {
                AddCourse.IsVisible = true;
                ViewCourse.IsVisible = false;
            }
            else
            {
                AddCourse.IsVisible = false;
                ViewCourse.IsVisible = true;
            }
        }


        async void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Term and Related Courses?", "Delete this Term?", "Yes", "No");

            if (answer == true)
            {
                await DatabaseService.RemoveTerm(_selectedTermId);

                await DisplayAlert("Term Deleted", "Term deleted", "OK");
            }
            else
            {
                await DisplayAlert("Delete Canceled", "Nothing Deleted", "OK");
            }

            await Navigation.PopAsync();
        }

        async void CancelTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void AddCourse_Clicked(object sender, EventArgs e)
        {
            var termId = _selectedTermId;

            await Navigation.PushAsync(new CourseAdd(termId));
        }

        async void EditTerm_Clicked(object sender, EventArgs e)
        {

                await Navigation.PushAsync(new TermEdit(_currentTerm));
            }

        async void ViewCourse_Clicked(object sender, EventArgs e)
        {
            var term = _currentTerm;
            await Navigation.PushAsync(new AssociatedCourses(term));
        }
    }
    }
