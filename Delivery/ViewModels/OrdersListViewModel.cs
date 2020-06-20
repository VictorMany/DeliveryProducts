using Delivery.Models;
using Delivery.Services;
using Delivery.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class OrdersListViewModel : BaseViewModel
    {
        public int IDOrderActual;
        public static OrdersListViewModel instance;

        Command _selectCommand;
        public Command SelectCommand => _selectCommand ?? (_selectCommand = new Command(SelectAction));

        Command _refreshPageCommand;
        public Command RefershPageCommand => _refreshPageCommand ?? (_refreshPageCommand = new Command(GetListOrders));

        public OrdersListViewModel()
        {
            instance = this;
            OrderModel.staticParent = this;

            /*listProducts = new List<ProductModel>()
            {
                new ProductModel
                {
                    ID = 1,
                    Name= "Javier", 
                    PictureBase64= "",
                    Price = 34
                },
                 new ProductModel
                {
                    ID = 2,
                    Name= "Anahi",
                    PictureBase64= "",
                    Price = 12
                },
                 new ProductModel
                {
                    ID = 1,
                    Name= "Javier",
                    PictureBase64= "",
                    Price = 34
                },
                 new ProductModel
                {
                    ID = 2,
                    Name= "Anahi",
                    PictureBase64= "",
                    Price = 12
                }
            };
            ListProducts = listProducts;*/
            GetListOrders();
        }

        List<OrderModel> _ListOrders;
        public List<OrderModel> ListOrders
        {
            get => _ListOrders;
            set => SetProperty(ref _ListOrders, value);
        }

        public async void GetListOrders()
        {
            IsBusy = true;
            try
            {
                ApiResponse response = await new ApiService().GetDataAsync<OrderModel>("order");
                if (response != null || response.Result != null)
                {
                    ObservableCollection<OrderModel> orderCollection = (ObservableCollection<OrderModel>)response.Result;
                    if (orderCollection[orderCollection.Count-1].Latitude != 0 && orderCollection[orderCollection.Count - 1].Longitude != 0)
                    {
                        createOrder();
                        IDOrderActual = orderCollection[orderCollection.Count - 1].OrderID + 1;
                    }
                    else
                    {
                        IDOrderActual=orderCollection[orderCollection.Count - 1].OrderID;
                        orderCollection.RemoveAt(orderCollection.Count - 1);
                    }
                    ListOrders = orderCollection.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            IsBusy = false;
        }

        public async void createOrder()
        {
            try
            {
                ApiResponse response = await new ApiService().PostDataAsync("order", new OrderModel
                {
                    Date = "not finished",
                    Address = "no finished",
                    Latitude = 0,
                    Longitude = 0,
                    Total = 0
                });
                if (response != null || response.Result != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Mensaje", "Error", "Ok");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static OrdersListViewModel GetInstance()
        {
            if (instance == null) instance = new OrdersListViewModel();
            return instance;
        }


        private async void SelectAction(object obj)  //Nos va a llevar al detail del producto
        {
            // Se mamo el Vic :D
            //Id del producto seleccionado 
            int a = (int)obj;
            //Obtener el producto a partir del Id que ya tenemos
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new DetailOrder(a));
        }
    }
}

