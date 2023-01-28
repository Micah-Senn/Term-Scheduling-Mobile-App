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
    public partial class TermEdit : ContentPage
    {
        private readonly int _selectedTermId;
        public TermEdit(Term term)
        {
            InitializeComponent();
            _selectedTermId = term.Id;

            TermTitle.Text = term.Title;
            TermStart.Date = term.TermStart;
            TermEnd.Date = term.TermEnd;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void SaveTerm_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TermTitle.Text))
            {
                await DisplayAlert("Missing Title", "Please enter a Title.", "OK");
                return;
            }

            if (TermStart.Date > TermEnd.Date)
            {
                await DisplayAlert("Date Error", "The start date cannot be after the end date", "OK");
                return;
            }


            await DatabaseService.UpdateTerm(_selectedTermId, TermTitle.Text, TermStart.Date, TermEnd.Date);
            await Navigation.PopToRootAsync();
            
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

        async void CourseCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var course = (Course)e.CurrentSelection.FirstOrDefault();
            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync((new CourseEdit(course)));
            }
        }
    }
}