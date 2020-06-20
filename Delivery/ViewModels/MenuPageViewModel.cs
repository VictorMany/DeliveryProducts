using Delivery.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class MenuPageViewModel
    {
        MasterDetailPage masterDetailPage;

        Command selectedCommand;
        public Command SelectedCommand => selectedCommand ?? (selectedCommand = new Command(SelectedAction));

        private async void SelectedAction(object sender)
        {
            var frame = sender as Frame;
            await frame.FadeTo(0.5, 150);
            await frame.FadeTo(1, 50);
            string name = frame.ClassId;

            ContentPage nextpage;

            switch (name)
            {
                case "Products":
                    nextpage = new ProductsListPage();
                    break;
                case "Orders":
                    nextpage = new OrdersListPage();
                    break;
                default:
                    nextpage = new ProductsListPage();
                    break;
            }

            masterDetailPage.IsPresented = false;
            masterDetailPage.Detail = new NavigationPage(nextpage);
        }

        public MenuPageViewModel(MasterDetailPage masterDetailPage)
        {
            this.masterDetailPage = masterDetailPage;
        }

    }
}
