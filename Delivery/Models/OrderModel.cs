using Delivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Delivery.Models
{
    public class OrderModel
    {
        public int ID { get;  set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Total { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public static OrdersListViewModel staticParent { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public OrdersListViewModel parent { get; set; }

        public OrderModel()
        {
            parent = staticParent;
        }

    }
}
