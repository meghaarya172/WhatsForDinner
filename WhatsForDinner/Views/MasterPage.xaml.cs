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
            IsPresented = false;
        }

        void History_Clicked(System.Object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new HistoryPage());
            IsPresented = false;
        }

        void Blacklist_Clicked(System.Object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new BlacklistPage());
            IsPresented = false;
        }

        void About_Clicked(System.Object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new AboutPage());
            IsPresented = false;
        }
    }
}
