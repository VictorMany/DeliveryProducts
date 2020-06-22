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
        public Command RefershPageCommand => _refreshPageCommand ?? (_refreshPageCommand = new Command(RefreshOrders));

        public OrdersListViewModel()
        {
            instance = this;
            OrderModel.staticParent = this;
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
            for(int i = 0; i < ListOrders.Count; i++)
            {
                ListOrders[i].UpdateParent();
            }
            IsBusy = false;
        }

        public async void RefreshOrders()
        {
            IsBusy = true;
            App.getOrdersList();
            ListOrders = App.listOrders;
            IsBusy = false;
        }



        public static OrdersListViewModel GetInstance()
        {
            if (instance == null) instance = new OrdersListViewModel();
            return instance;
        }


        public async void SelectAction(object obj)  //Nos va a llevar al detail del producto
        {
            //Id del producto seleccionado 
            int a = (int)obj;
            //Obtener el producto a partir del Id que ya tenemos
            await MenuPage.menuPages.Detail.Navigation.PushAsync(new DetailOrder(a));
        }
    }
}

