using Delivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Delivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage(OrderModel orderModel)
        {
            InitializeComponent();
            MapOrder.MoveToRegion(
                   MapSpan.FromCenterAndRadius(
                       new Position(orderModel.Latitude, orderModel.Longitude),
                       Distance.FromMiles(.5)
            ));

            MapOrder.Order = orderModel;

            MapOrder.Pins.Add(
                //PIN
                new Pin
                {
                    Type = PinType.Place,
                    Label = orderModel.Total.ToString(),
                    Position = new Position(orderModel.Latitude, orderModel.Longitude)
                }
            );

            //DATOS DEL BOX VIEW
            Address.Text = orderModel.Address;         
            Date.Text = orderModel.Date.ToString();
            TxtTotal.Text = orderModel.Total.ToString();
           
        }
    }
}