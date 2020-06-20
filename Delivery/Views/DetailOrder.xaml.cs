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
    public partial class DetailOrder : ContentPage
    {
        public DetailOrder(int idProd)
        {
            InitializeComponent();

            BindingContext = new OrderDetailViewModel(idProd);
        }
    }
}