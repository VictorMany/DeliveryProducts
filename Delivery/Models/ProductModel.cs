using Delivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Models
{
    public class ProductModel
    {
        
        public int? ID { get; set; }
        public string PictureBase64 { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public static ProductsListViewModel staticParent { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public ProductsListViewModel parent { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public static OrderViewModel staticParent2 { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public OrderViewModel parent2 { get; set; }

        public ProductModel()
        {
            parent = staticParent;
            parent2 = staticParent2;
        }
    }
}
