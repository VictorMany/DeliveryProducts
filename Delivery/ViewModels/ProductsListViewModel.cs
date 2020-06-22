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
    public class ProductsListViewModel: BaseViewModel
    {
        public static ProductsListViewModel instance;

        Command _selectCommand;
        public Command SelectCommand => _selectCommand ?? (_selectCommand = new Command(SelectAction));

        Command _seeCommand;
        public Command SeeCartCommand => _seeCommand ?? (_seeCommand = new Command(SeeAction));

        Command _NewProductCommand;
        public Command NewProductCommand => _NewProductCommand ?? (_NewProductCommand = new Command(NewAction));

        Command _refreshPageCommand;
        public Command RefershPageCommand => _refreshPageCommand ?? (_refreshPageCommand = new Command(RefreshProducts));

        public ProductsListViewModel() 
        {
            instance = this;
            ProductModel.staticParent = this;
            GetListProducts();
        }

        List<ProductModel> _ListProducts;
        public List<ProductModel> ListProducts
        {
            get => _ListProducts;
            set => SetProperty(ref _ListProducts, value);
        }

        public void RefreshProducts()
        {
            IsBusy = true;
            App.getProductsList();
            ListProducts = App.listProducts;
            IsBusy = false;
        }

        public async void GetListProducts()
        {
            IsBusy = true;
            await Task.Delay(4500);
            ListProducts = App.listProducts;
            IsBusy = false;
        }

        public static ProductsListViewModel GetInstance()
        {
            if (instance == null) instance = new ProductsListViewModel();
            return instance;
        }


        private async void SelectAction(object obj)  //Nos va a llevar al detail del producto
        {
            //Id del producto seleccionado 
            int a = (int)obj;
            //Obtener el producto a partir del Id que ya tenemos
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new DetailProduct(a));
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
