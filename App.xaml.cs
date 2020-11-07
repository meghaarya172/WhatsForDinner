using System;
using WhatsForDinner.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
