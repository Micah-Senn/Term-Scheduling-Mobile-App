using C971.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C971.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermAdd : ContentPage
    {
        public TermAdd()
        {
            InitializeComponent();
        }

        async void CancelTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
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


            await DatabaseService.AddTerm(TermTitle.Text, TermStart.Date, TermEnd.Date);

            await Navigation.PopAsync();
        }
    }
    }
