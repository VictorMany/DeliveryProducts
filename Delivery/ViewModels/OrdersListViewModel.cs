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

        public void GetListOrders()
        {
            IsBusy = true;
            ListOrders = App.listOrders;
            IsBusy = false;
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

