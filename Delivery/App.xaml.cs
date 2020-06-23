using Delivery.Models;
using Delivery.Services;
using Delivery.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery
{
    public partial class App : Application
    {
        public static MasterDetailPage menuPage;
        public static List<ProductModel> listProducts;
        public static List<OrderModel> listOrders;
        public static OrderModel car;
        public App()
        {
            InitializeComponent();

            menuPage = new MenuPage();
            MainPage = menuPage;
            getProductsList();
            getOrdersList();
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

        public static async void getProductsList()
        {
            try
            {
                ApiResponse response = await new ApiService().GetDataAsync<ProductModel>("product");
                if (response != null || response.Result != null)
                {
                    ObservableCollection<ProductModel> productCollection = (ObservableCollection<ProductModel>)response.Result;
                    listProducts = productCollection.ToList();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public static async void GetCart()
        {
            try
            {
                ApiResponse response = await new ApiService().GetDataAsync<OrderModel>("order");
                if (response != null || response.Result != null)
                {
                    ObservableCollection<OrderModel> orderCollection = (ObservableCollection<OrderModel>)response.Result;
                    if (orderCollection[orderCollection.Count - 1].Latitude == 0 && orderCollection[orderCollection.Count - 1].Longitude == 0)
                    {
                        car = orderCollection[orderCollection.Count - 1];
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static async void getOrdersList()
        {
            try
            {
                ApiResponse response = await new ApiService().GetDataAsync<OrderModel>("order");
                if (response != null || response.Result != null)
                {
                    ObservableCollection<OrderModel> orderCollection = (ObservableCollection<OrderModel>)response.Result;
                    if (orderCollection[orderCollection.Count - 1].Latitude != 0 && orderCollection[orderCollection.Count - 1].Longitude != 0)
                    {
                        createOrder();
                    }
                    else
                    {
                        car = orderCollection[orderCollection.Count - 1];
                        orderCollection.RemoveAt(orderCollection.Count - 1);
                    }
                    listOrders = orderCollection.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static async void createOrder()
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
                    car = (OrderModel)response.Result;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async void deleteOrder(int id)
        {
            try
            {
                ApiResponse response = await new ApiService().DeleteDataAsync("order", id);

                if(!response.IsSuccess)
                {
                    await Current.MainPage.DisplayAlert("Failed", response.Message, "OK");
                }

            } 
            catch (Exception ex)
            {
                await Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
