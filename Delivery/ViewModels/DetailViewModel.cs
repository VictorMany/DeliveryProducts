using Delivery.Models;
using Delivery.Services;
using Delivery.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {

        Command _DeleteCommand;
        public Command DeleteCommand => _DeleteCommand ?? (_DeleteCommand = new Command(DeleteAction));

        static DetailViewModel instance;
        ProductModel _Product;
        public ProductModel Product
        {
            get => _Product;
            set => SetProperty(ref _Product, value);
        }

        string _ImageUrl;
        public string ImageUrl
        {
            get => _ImageUrl;
            set => SetProperty(ref _ImageUrl, value);
        }

        Command _CommandEdit;
        public Command CommandEdit => _CommandEdit ?? (_CommandEdit = new Command(EditAction));

        public async void EditAction()
        {
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new EditProductPage(Product));
        }

        public DetailViewModel()
        {
            Product = new ProductModel
            {
                PictureBase64 = "",
                Name = "Producto genérico",
                Price = 78
            };
        }

        public DetailViewModel(int prodID)
        {
            GetProductContent(prodID);
            instance = this;
        }

        public static DetailViewModel GetInstance()
        {
            if (instance == null) instance = new DetailViewModel();
            return instance;
        }

        public async void GetProductContent(int id)
        {
            IsBusy = true;
            try
            {
                ApiResponse response = await new ApiService().GetDataAsyncByID<ProductModel>("product", id);
                if (response != null || response.Result != null)
                {
                    ProductModel productCollection = (ProductModel)response.Result;
                    Product = productCollection;
                    ImageUrl = Product.PictureBase64;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            IsBusy = false;
        }

        private async void DeleteAction()
        {
            if (Product.ID != 0)
            {
                try
                {
                    ApiResponse response = await new ApiService().DeleteDataAsync("product", Product.ID);
                    ProductsListViewModel.GetInstance().GetListProducts();
                    await Application.Current.MainPage.DisplayAlert("Delivery", "El Producto fue borrado exitosamente!!", "Ok");
                    await MenuPage.menuPages.Detail.Navigation.PopAsync();
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
