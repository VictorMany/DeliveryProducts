using Delivery.Models;
using Delivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailProduct : ContentPage
    {
        public DetailProduct()
        {
            InitializeComponent();

            BindingContext = new DetailViewModel();
        }
        public DetailProduct(int prodID)
        {
            InitializeComponent();
            BindingContext = new DetailViewModel(prodID);
        }
    }
}