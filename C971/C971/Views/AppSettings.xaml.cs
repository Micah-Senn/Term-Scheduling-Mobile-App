using C971.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppSettings : ContentPage
    {
        public AppSettings()
        {
            InitializeComponent();
        }

        private void ClearPreferences_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
        }

        private void LoadSampleData_Clicked(object sender, EventArgs e)
        {
            if (Settings.FirstRun)
            {
               DatabaseService.LoadSampleData();
                Settings.FirstRun = false;

            }
        }

        async void ClearSampleData_Clicked(object sender, EventArgs e)
        {
            await DatabaseService.ClearSampleData();
        }
    }
}