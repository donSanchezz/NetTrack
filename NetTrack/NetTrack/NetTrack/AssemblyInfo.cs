using Android;
using Android.App;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: ExportFont("Brands-Regular-400.otf", Alias = "FAB")] 
[assembly: ExportFont("Free-Regular-400.otf", Alias = "FAR")] 
[assembly: ExportFont("Free-Solid-900.otf", Alias = "FAB")]
[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesFeature("android.hardware.location", Required = false)]
[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
[assembly: UsesFeature("android.hardware.location.network", Required = false)]
[assembly: UsesPermission(Manifest.Permission.AccessBackgroundLocation)] // for background location
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
[assembly: UsesPermission(Android.Manifest.Permission.Camera)]
[assembly: UsesFeature("android.hardware.camera", Required = true)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = true)] 