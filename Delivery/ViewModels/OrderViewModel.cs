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
    public class OrderViewModel : BaseViewModel
    {
        Command _NextCommand;
        public Command NextCommand => _NextCommand ?? (_NextCommand = new Command(NextAction));


        Command _MapCommand;
        public Command MapCommand => _MapCommand ?? (_MapCommand = new Command(MapAction));

        Command _ConfirmCommand;
        public Command ConfirmCommand => _ConfirmCommand ?? (_ConfirmCommand = new Command(ConfirmAction));

        OrderModel lastOrder;

        ObservableCollection<OrderModel> OrdersList;

        ObservableCollection<ProductModel> OrderProductsList;

        List<ProductModel> _ListProducts;
        public List<ProductModel> ListProductsOnCart
        {
            get => _ListProducts;
            set => SetProperty(ref _ListProducts, value);
        }

        float _Total;
        public float Total
        {
            get => _Total;
            set => SetProperty(ref _Total, value);
        }

        List<ProductModel> listProducts;
        public OrderViewModel(){

            ProductModel.staticParent2 = this;

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
                    ID = 3,
                    Name= "Vic",
                    PictureBase64= "",
                    Price = 34
                },
                 new ProductModel
                {
                    ID = 4,
                    Name= "Chris",
                    PictureBase64= "",
                    Price = -12
                }
            };
            ListProductsOnCart = listProducts;*/
            Total = 0;
            GetListOrderProducts();
            IsBusy = false;

        }

        public async void GetListOrderProducts()
        {
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
                            // GetListProducts(OrderProductsList[productId].IDProduct);
                        }
                        ListProductsOnCart = OrderProductsList.ToList();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Lista de productos", "Todavia no cuenta con productos agregados", "Ok");
                        await MenuPage.menuPages.Detail.Navigation.PopAsync();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            // ListProductsOnCart
        }

        public void GetListProducts(int id)
        {

            for (int position = 0; position < App.listProducts.Count; position++)
            {
                if (id == App.listProducts[position].ID)
                {
                    
                }
            }
        }

        public async void NextAction()
        {
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new ConfirmOrderPage());
        }
        public async void MapAction()
        {
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new MapPage());
        }
        public async void ConfirmAction()
        {
         
        }
    }
}
