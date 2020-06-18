using Delivery.Models;
using Delivery.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Delivery.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        List<ProductModel> _ListProducts;
        public List<ProductModel> ListProductsOnCart
        {
            get => _ListProducts;
            set => SetProperty(ref _ListProducts, value);
        }


        bool _IsBusy;
        public bool IsBusy
        {
            get => _IsBusy;
            set => SetProperty(ref _IsBusy, value);
        }

        List<ProductModel> listProducts;
        public OrderViewModel(){

            ProductModel.staticParent2 = this;

            listProducts = new List<ProductModel>()
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
            ListProductsOnCart = listProducts;
            IsBusy = false;
        }

        public async void GetListOrders()
        {
            try
            {
                ApiResponse response = await new ApiService().GetDataAsync<ProductModel>("order");
                if (response != null || response.Result != null)
                {
                    ListProductsOnCart = (List<ProductModel>)response.Result;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
