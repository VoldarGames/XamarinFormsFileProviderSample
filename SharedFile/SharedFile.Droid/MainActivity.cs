using System;
using System.Globalization;
using System.IO;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Environment = System.Environment;

namespace SharedFile.Droid
{
    [Activity(Label = "SharedFile", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CreateFile();
            RegisterDependencies();
            LoadApplication(new App());
        }

        private void RegisterDependencies()
        {
            DependencyService.Register<AccessFile>();
            DependencyService.Register<GoToIntentClass>();
        }

        private void CreateFile()
        {
            var fileName = "texto.txt";

            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            if (!File.Exists(path))
            {
                using(var sw = new StreamWriter(path))
                {
                    sw.Write(DateTime.Now.ToString(CultureInfo.CurrentCulture));
                }
            }
        }
    }
}

