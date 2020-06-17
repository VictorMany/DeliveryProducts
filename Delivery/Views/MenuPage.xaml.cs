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

    public partial class MenuPage : MasterDetailPage
    {
        public static MenuPage menuPages;
        public MenuPage()
        {
            InitializeComponent();
            BindingContext = new MenuPageViewModel(this);
            Detail = new NavigationPage(new ProductsListPage());
            menuPages = this;
        }
    }
}