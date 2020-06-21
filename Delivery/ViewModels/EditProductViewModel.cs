using Delivery.Models;
using Delivery.Services;
using Delivery.Views;
using Plugin.Media;
using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;


namespace Delivery.ViewModels
{
    public class EditProductViewModel: BaseViewModel
    {
        Command _SaveCommand;
        public Command SaveCommand => _SaveCommand ?? (_SaveCommand = new Command(SaveAction));

        Command _CancelCommand;
        public Command CancelCommand => _CancelCommand ?? (_CancelCommand = new Command(CancelAction));

        Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        ProductModel productSelected;
        public ProductModel ProductSelected
        {
            get => productSelected;
            set => SetProperty(ref productSelected, value);
        }

        public EditProductViewModel()
        {
            productSelected = new ProductModel();
            IsBusy = false;
        }

        public EditProductViewModel(ProductModel productModel)
        {
            productSelected = productModel;
            ImageUrl = productSelected.PictureBase64;
            IsBusy = false;
        }

        string _ImageUrl;
        public string ImageUrl
        {
            get => _ImageUrl;
            set => SetProperty(ref _ImageUrl, value);
        }

        private async void SaveAction()
        {
            IsBusy = true;
            try
            {
                if (ProductSelected.ID == null)
                {
                    ApiResponse response = await new ApiService().PostDataAsync("product", new ProductModel
                    {
                        ID = 0,
                        Name = ProductSelected.Name,
                        Price = ProductSelected.Price,
                        PictureBase64 = ImageUrl ?? ""
                    });
                    if (!response.IsSuccess)
                    {
                        await Application.Current.MainPage.DisplayAlert("Failed Request", "Message: " + response.Message, "OK");
                    } else
                    {
                        await Application.Current.MainPage.DisplayAlert("Mensaje", "El Producto fue creado exitosamente", "Ok");
                    }
                }
                else
                {
                    ApiResponse response = await new ApiService().PutDataAsync("product", new ProductModel
                    {
                        ID = productSelected.ID,
                        Name = ProductSelected.Name,
                        Price = ProductSelected.Price,
                        PictureBase64 = ImageUrl ?? ""
                    }, ProductSelected.ID ?? 0);

                    await Application.Current.MainPage.DisplayAlert("Mensaje", "El Producto fue actualizado exitosamente", "Ok");
                    DetailViewModel.GetInstance().GetProductContent(ProductSelected.ID ?? 0);
                }
                ProductsListViewModel.GetInstance().GetListProducts();
                await MenuPage.menuPages.Detail.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }


        private async void TakePictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No hay cámara disponible en el dispositivo.", "Ok");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
            });

            if (file == null)
                return;

            ImageUrl = await new ImageService().ConvertImageFileToBase64(file.Path);
            await Application.Current.MainPage.DisplayAlert("File Location", file.Path, "Ok");
        }

        private async void SelectPictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Seleccionar fotografías no soportada", "Ok");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
            });

            if (file == null)
                return;

            ImageUrl = await new ImageService().ConvertImageFileToBase64(file.Path);
        }

        public async void CancelAction()
        {
            await MenuPage.menuPages.Detail.Navigation.PopAsync();
        }
    }
}
