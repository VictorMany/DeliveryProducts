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
    public partial class ProductsListPage : ContentPage
    {
        public ProductsListPage()
        {
            InitializeComponent();
            BindingContext = new ProductsListViewModel();
        }
    }
}