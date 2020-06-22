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
    public class OrderDetailViewModel : BaseViewModel
    {
        static OrderDetailViewModel instance;
        Command _DeleteCommand;
        public Command DeleteCommand => _DeleteCommand ?? (_DeleteCommand = new Command(DeleteAction));

        Command _MapCommand;
        public Command MapCommand => _MapCommand ?? (_MapCommand = new Command(MapAction));

        Command _EditCommand;
        public Command EditCommand => _EditCommand ?? (_EditCommand = new Command(EditAction));

        OrderModel _Order;

        public OrderModel Order
        {
            get => _Order;
            set => SetProperty(ref _Order, value);
        }

        public OrderDetailViewModel(int idOrder)
        {
            instance = this;
            getOrder(idOrder);
        }

        public void getOrder(int idOrder)
        {
            for (int a = 0; a < App.listOrders.Count; a++)
            {
                if (idOrder == App.listOrders[a].ID)
                {
                    Order = App.listOrders[a];
                }
            }
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

        public static OrderDetailViewModel GetInstance(int id)
        {
            if (instance == null) instance = new OrderDetailViewModel(id);
            return instance;
        }

        public async void DeleteAction()
        {
            bool isSure = await Application.Current.MainPage.DisplayAlert("Delivery", "¿Deseas eliminar el pedido?", "SI", "NO");
            if (isSure)
            {
                if (Order.ID != null)
                {
                    IsBusy = true;
                    try
                    {
                        ApiResponse response = await new ApiService().DeleteDataAsync("order", Order.ID);
                        App.getOrdersList();
                        await Task.Delay(4500);
                        OrdersListViewModel.GetInstance().GetListOrders();
                        await Application.Current.MainPage.DisplayAlert("Delivery", "El Pedido fue borrado exitosamente!!", "Ok");
                        await MenuPage.menuPages.Detail.Navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                    }
                    IsBusy = false;
                }
            }
        }

        public async void EditAction()
        {
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new EditOrderPage(Order));
        }
    }
}
