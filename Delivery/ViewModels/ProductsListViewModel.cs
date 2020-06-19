﻿using Delivery.Models;
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
    public class ProductsListViewModel: BaseViewModel
    {

        Command _selectCommand;
        public Command SelectCommand => _selectCommand ?? (_selectCommand = new Command(SelectAction));

        Command _seeCommand;
        public Command SeeCartCommand => _seeCommand ?? (_seeCommand = new Command(SeeAction));

        Command _NewProductCommand;
        public Command NewProductCommand => _NewProductCommand ?? (_NewProductCommand = new Command(NewAction));

        Command _refreshPageCommand;
        public Command RefershPageCommand => _refreshPageCommand ?? (_refreshPageCommand = new Command(GetListProducts));

        public ProductsListViewModel() 
        {
            ProductModel.staticParent = this;

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
            GetListProducts();
        }

        List<ProductModel> _ListProducts;
        public List<ProductModel> ListProducts
        {
            get => _ListProducts;
            set => SetProperty(ref _ListProducts, value);
        }

        public async void GetListProducts()
        {
            IsBusy = true;
            try
            {
                ApiResponse response = await new ApiService().GetDataAsync<ProductModel>("product");
                if (response != null || response.Result != null)
                {
                    ObservableCollection<ProductModel> productCollection = (ObservableCollection<ProductModel>)response.Result;
                    ListProducts = productCollection.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            IsBusy = false;
        }


        private async void SelectAction(object obj)  //Nos va a llevar al detail del producto
        {
            //Id del producto seleccionado 
            int a = (int)obj;
            //oBTENER EL OBJ a partir de l Id que ya tenemos 
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new DetailProduct());
        }

        private async void SeeAction()  //Nos va a llevar al pedido que estamos generando en este momento
        {
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new OrderPage());
        }

        public async void NewAction()
        {
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new EditProductPage());
        }
    }
}
