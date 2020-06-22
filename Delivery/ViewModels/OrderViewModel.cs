using Delivery.Models;
using Delivery.Services;
using Delivery.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        Command _selectCommand;
        public Command SelectCommand => _selectCommand ?? (_selectCommand = new Command(DeleteProductFromCart));

        Command _NextCommand;
        public Command NextCommand => _NextCommand ?? (_NextCommand = new Command(NextAction));

        Command _MapCommand;
        public Command MapCommand => _MapCommand ?? (_MapCommand = new Command(MapAction));

        Command _ConfirmCommand;
        public Command ConfirmCommand => _ConfirmCommand ?? (_ConfirmCommand = new Command(ConfirmAction));

        ObservableCollection<ProductModel> OrderProductsList;

        List<ProductModel> _ListProducts;
        public List<ProductModel> ListProductsOnCart
        {
            get => _ListProducts;
            set => SetProperty(ref _ListProducts, value);
        }

        OrderModel _Order;
        public OrderModel Order
        {
            get => _Order;
            set => SetProperty(ref _Order, value);
        }

        float _Total;
        public float Total
        {
            get => _Total;
            set => SetProperty(ref _Total, value);
        }

        public OrderViewModel(){

            ProductModel.staticParent2 = this;

            Total = 0;
            Order = new OrderModel
            {
                Date = DateTime.Now.ToString()
            };
            GetListOrderProducts();
            IsBusy = false;

        }

        public async void DeleteProductFromCart(object obj)
        {
            bool proceed = await Application.Current.MainPage.DisplayAlert("Delivery", "¿Desea eliminar el producto del carrito?", "SI", "NO");
            if (proceed)
            {
                IsBusy = true;
                int a = (int)obj;
                try
                {
                    IsBusy = true;
                    ApiResponse response = await new ApiService().DeleteCartDataAsync("orderProduct", new OrderProductModel
                    {
                        IDProduct = a,
                        IDOrder = App.car.ID
                    });
                    if (response.IsSuccess)
                    {
                        for (int b =0; b< App.listProducts.Count; b++)
                        {
                            if(App.listProducts[b].ID == a)
                            {
                                Total = Total - App.listProducts[b].Price;
                            }
                        }

                        await Task.Delay(2000);
                        GetListOrderProducts();
                    }
                }
                catch(Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
                IsBusy = false;
            }
        }

        public async void GetListOrderProducts()
        {
            IsBusy = true;
            try
            {
                ApiResponse response = await new ApiService().GetListDataAsyncByID<ProductModel>("orderProduct", App.car.ID);
                if (response != null || response.Result != null)
                {
                    OrderProductsList = (ObservableCollection<ProductModel>)response.Result;
                    if(response.Result != null)
                    {
                        for (int productId = 0; productId < OrderProductsList.Count; productId++)
                        {
                            Total += OrderProductsList[productId].Price;
                        }
                        ListProductsOnCart = OrderProductsList.ToList();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Delivery", "Todavia no cuenta con productos agregados", "Ok");
                        await MenuPage.menuPages.Detail.Navigation.PopAsync();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            // ListProductsOnCart
            IsBusy = false;
        }

        public async void NextAction()
        {
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new ConfirmOrderPage());
        }

        public async void MapAction()
        {
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new MapPage(new OrderModel
            {
                Latitude = Order.Latitude,
                Longitude = Order.Longitude,
                Date = DateTime.Now.ToString(), 
                Address = Order.Address,
                Total = this.Total
            }));
        }

        public async void ConfirmAction()
        {
            IsBusy = true;
            try
            {
                ApiResponse response = await new ApiService().PutDataAsync("order", new OrderModel
                {
                    Date = DateTime.Now.ToString(),
                    Address = Order.Address,
                    Total = this.Total,
                    Latitude = Order.Latitude,
                    Longitude = Order.Longitude

                }, App.car.ID);

                if (response.IsSuccess != false)
                {
                    await Application.Current.MainPage.DisplayAlert("Delivery", "Gracias por confiar en nosotros!!!, su pedido se ha realizado", "Ok");
                    App.getOrdersList();
                    App.menuPage.Detail = new NavigationPage(new ProductsListPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Delivery", "Algo sucedio, intentelo en unos momentos", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }
    }
}
