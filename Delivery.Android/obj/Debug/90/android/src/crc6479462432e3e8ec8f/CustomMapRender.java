package crc6479462432e3e8ec8f;


public class CustomMapRender
	extends crc648aad9efe354a1d8f.MapRenderer
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.maps.GoogleMap.InfoWindowAdapter
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getInfoContents:(Lcom/google/android/gms/maps/model/Marker;)Landroid/view/View;:GetGetInfoContents_Lcom_google_android_gms_maps_model_Marker_Handler:Android.Gms.Maps.GoogleMap/IInfoWindowAdapterInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"n_getInfoWindow:(Lcom/google/android/gms/maps/model/Marker;)Landroid/view/View;:GetGetInfoWindow_Lcom_google_android_gms_maps_model_Marker_Handler:Android.Gms.Maps.GoogleMap/IInfoWindowAdapterInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"";
		mono.android.Runtime.register ("Delivery.Droid.Renders.CustomMapRender, Delivery.Android", CustomMapRender.class, __md_methods);
	}


	public CustomMapRender (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CustomMapRender.class)
			mono.android.TypeManager.Activate ("Delivery.Droid.Renders.CustomMapRender, Delivery.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public CustomMapRender (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CustomMapRender.class)
			mono.android.TypeManager.Activate ("Delivery.Droid.Renders.CustomMapRender, Delivery.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public CustomMapRender (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CustomMapRender.class)
			mono.android.TypeManager.Activate ("Delivery.Droid.Renders.CustomMapRender, Delivery.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public android.view.View getInfoContents (com.google.android.gms.maps.model.Marker p0)
	{
		return n_getInfoContents (p0);
	}

	private native android.view.View n_getInfoContents (com.google.android.gms.maps.model.Marker p0);


	public android.view.View getInfoWindow (com.google.android.gms.maps.model.Marker p0)
	{
		return n_getInfoWindow (p0);
	}

	private native android.view.View n_getInfoWindow (com.google.android.gms.maps.model.Marker p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
