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
        ObservableCollection<ProductModel> OrderProductsList;
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

        Command _CommandAddToCart;
        public Command CommandAddToCart => _CommandAddToCart ?? (_CommandAddToCart = new Command(AddToCartAction));

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
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }

        private async void DeleteAction()
        {
            bool isSure = await Application.Current.MainPage.DisplayAlert("Eliminar", "¿Deseas eliminar el producto?", "SI", "NO");
            if (isSure)
            {
                if (Product.ID != null)
                {
                    IsBusy = true;
                    try
                    {
                        ApiResponse response = await new ApiService().DeleteDataAsync("product", Product.ID ?? 0);
                        App.getProductsList();
                        ProductsListViewModel.GetInstance().GetListProducts();
                        await Application.Current.MainPage.DisplayAlert("Delivery", "El Producto fue borrado exitosamente!!", "Ok");
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

        public async void AddToCartAction()
        {
            try
            {
                ApiResponse response = await new ApiService().GetListDataAsyncByID<ProductModel>("orderProduct", App.car.ID);
                if (response != null || response.Result != null)
                {
                    bool found = false;
                    OrderProductsList = (ObservableCollection<ProductModel>)response.Result;
                    if (response.Result != null)
                    {
                        for (int productId = 0; productId < OrderProductsList.Count; productId++)
                        {
                            if (OrderProductsList[productId] == Product)
                            {
                                found = true;
                            }
                        }

                        if(found != true)
                        {
                            AddToCart();
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Delivery", "Este producto ya se encuentra en el carrito", "Ok");
                        }
                    }
                    else
                    {
                        AddToCart();
                    }

                }
            } catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async void AddToCart()
        {
            try
            {
                ApiResponse response = await new ApiService().PostDataAsync("orderProduct", new OrderProductModel
                {
                    IDOrder = App.car.ID,
                    IDProduct = (int)Product.ID
                });
                if (response != null || response.Result != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Delivery", "El Producto se agrego al carrito exitosamente!!", "Ok");
                    await MenuPage.menuPages.Detail.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }
    }
}
