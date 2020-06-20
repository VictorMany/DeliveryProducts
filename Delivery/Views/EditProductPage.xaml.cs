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
    public partial class EditProductPage : ContentPage
    {
        public EditProductPage()
        {
            InitializeComponent();

            BindingContext = new EditProductViewModel();
        }

        public EditProductPage(ProductModel prod)
        {
            InitializeComponent();

            BindingContext = new EditProductViewModel(prod);
        }
    }
}