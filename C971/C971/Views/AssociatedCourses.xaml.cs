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
    public partial class AssociatedCourses : ContentPage
    {
        private readonly int _selectedTermId;
        private Term _currentTerm;
        public AssociatedCourses(Term term)
        {
            InitializeComponent();
            _selectedTermId = term.Id;
            _currentTerm = term;
            TermTitle.Text = term.Title;
        }

        async void CourseCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var course = (Course)e.CurrentSelection.FirstOrDefault();
            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync((new CourseView(course)));
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            CourseCollectionView.ItemsSource = await DatabaseService.GetCourses(_selectedTermId);

        }

        async void AddCourse_Clicked(object sender, EventArgs e)
        {
            var termId = _selectedTermId;

            await Navigation.PushAsync(new CourseAdd(termId));
        }
    }
}