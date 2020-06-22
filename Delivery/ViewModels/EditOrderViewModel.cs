using Delivery.Models;
using Delivery.Services;
using Delivery.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class EditOrderViewModel : BaseViewModel
    {
        Command _SaveCommand;
        public Command SaveCommand => _SaveCommand ?? (_SaveCommand = new Command(SaveAction));

        Command _MapCommand;
        public Command MapCommand => _MapCommand ?? (_MapCommand = new Command(MapAction));

        Command _CancelCommand;
        public Command CancelCommand => _CancelCommand ?? (_CancelCommand = new Command(CancelAction));

        OrderModel _Order;
        public OrderModel Order
        {
            get => _Order;
            set => SetProperty(ref _Order, value);
        }

        public EditOrderViewModel(OrderModel order)
        {
            Order = order;
        }

        public async void MapAction()
        {
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new MapPage(new OrderModel
            {
                Latitude = Order.Latitude,
                Longitude = Order.Longitude,
                Date = Order.Date,
                Address = Order.Address,
                Total = Order.Total
            }));
        }

        private async void SaveAction()
        {
            if(Order.Address != "")
            {
                IsBusy = true;
                try
                {
                    ApiResponse response = await new ApiService().PutDataAsync("order", new OrderModel
                    {
                        Total = Order.Total,
                        Latitude = Order.Latitude,
                        Longitude = Order.Longitude,
                        Address = Order.Address,
                        Date = Order.Date
                    }, Order.ID);
                    App.getOrdersList();
                    await Task.Delay(3000);
                    OrdersListViewModel.GetInstance().GetListOrders();
                    OrderDetailViewModel.GetInstance(Order.ID).getOrder(Order.ID);
                    await Application.Current.MainPage.DisplayAlert("Delivery", "El Pedido fue actualizado exitosamente", "Ok");
                    await MenuPage.menuPages.Detail.Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
                IsBusy = false;
            }
            else
            {

            }
            
        }

        public async void CancelAction()
        {
            await MenuPage.menuPages.Detail.Navigation.PopAsync();
        }
    }
}
