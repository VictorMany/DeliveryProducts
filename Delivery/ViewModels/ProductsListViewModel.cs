using Delivery.Models;
using Delivery.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class ProductsListViewModel: BaseViewModel
    {

        Command _selectCommand;
        public Command SelectCommand => _selectCommand ?? (_selectCommand = new Command(SelectAction));

        List<ProductModel> listProducts;

        public ProductsListViewModel() 
        {
            ProductModel.staticParent = this;

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
                }


            };
            ListProducts = listProducts;
            IsBusy = false;
        }

        List<ProductModel> _ListProducts;
        public List<ProductModel> ListProducts
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

        private async void SelectAction(object obj)
        {
            //Id 
            int a = (int)obj;
            //oBTENER EL OBJ a partir de l Id que ya tenemos 

            // await Application.Current.MainPage.DisplayAlert("Hola", a.ToString(), "OK");
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new DetailProduct());
        }
    }
}
