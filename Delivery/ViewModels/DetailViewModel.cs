using Delivery.Models;
using Delivery.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        ProductModel _Product;
        public ProductModel Product
        {
            get => _Product;
            set => SetProperty(ref _Product, value);
        }

        Command _CommandEdit;
        public Command CommandEdit => _CommandEdit ?? (_CommandEdit = new Command(EditAction));

        public async void EditAction()
        {
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new EditProductPage());
        }

        public DetailViewModel()
        {
            Product = new ProductModel
            {
                PictureBase64 = "",
                Name = "YO",
                Price = 78
            };
        }
    }
}
