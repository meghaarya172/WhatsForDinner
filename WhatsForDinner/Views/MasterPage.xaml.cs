using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WhatsForDinner.Views
{
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            Detail = new NavigationPage(new WhatsForDinnerPage());
        }

        void Dinner_Clicked(System.Object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new WhatsForDinnerPage());
        }

        void Blacklist_Clicked(System.Object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new BlacklistPage());
        }

        void About_Clicked(System.Object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new AboutPage());
        }
    }
}
