using System;
using WhatsForDinner.Services;
using WhatsForDinner.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WhatsForDinner.ViewModels;

namespace WhatsForDinner
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MasterPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
