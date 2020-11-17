using System;
using WhatsForDinner.Services;
using WhatsForDinner.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WhatsForDinner.ViewModels;
using WhatsForDinner.Data;
using System.IO;

namespace WhatsForDinner
{
    public partial class App : Application
    {
        static RestaurantDatabase database;

        public static RestaurantDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new RestaurantDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "History.db3"));
                }
                return database;
            }
        }

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
