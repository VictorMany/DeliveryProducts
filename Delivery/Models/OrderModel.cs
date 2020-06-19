using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Delivery.Models
{
    public class OrderModel
    {
        public int OrderID { get;  set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Total { get; set; }

    }
}
