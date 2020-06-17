using Delivery.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery
{
    public partial class App : Application
    {
        public static MasterDetailPage menuPage;
        public App()
        {
            InitializeComponent();

            menuPage = new MenuPage();
            MainPage = menuPage;
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
