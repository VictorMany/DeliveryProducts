using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using Delivery.Droid.Renders;
using Delivery.Models;
using Delivery.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRender))]
namespace Delivery.Droid.Renders
{
    public class CustomMapRender : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        OrderModel Order;

        public CustomMapRender(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Order = ((CustomMap)e.NewElement).Order;
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            //return base.CreateMarker(pin);
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(Order.Latitude, Order.Longitude));
            marker.SetTitle(Order.Address);
            marker.SetSnippet(Order.Date.ToString());
            return marker;
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;
                view = inflater.Inflate(Resource.Layout.MarkerWindow, null);
               
                var infoNombre = view.FindViewById<TextView>(Resource.Id.MarkerWindowAddress);

               
                if (infoNombre != null) infoNombre.Text = marker.Title;

                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }
    }
}