﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmOrderPage : ContentPage
    {
        public ConfirmOrderPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.OrderViewModel();
        }
    }
}