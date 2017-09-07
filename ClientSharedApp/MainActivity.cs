using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Uri = Android.Net.Uri;


namespace ClientSharedApp
{
    [Activity(Label = "ClientSharedApp", MainLauncher = true, Icon = "@drawable/icon")]
    [IntentFilter(new[] { "ClientSharedApp.ClientSharedApp.START" }, Categories = new[] { Intent.CategoryDefault })]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            //SetContentView (Resource.Layout.Main);
            var dbUri = Intent.GetParcelableExtra("dbUri") as Uri;
            if (dbUri != null)
            {
                var mimeType = ContentResolver.GetType(dbUri);
                var returnCursor = ContentResolver.Query(dbUri, null, null, null, null);

                var nameIndex = returnCursor.GetColumnIndex(OpenableColumns.DisplayName);
                var sizeIndex = returnCursor.GetColumnIndex(OpenableColumns.Size);
                returnCursor.MoveToFirst();
                var filename = returnCursor.GetString(nameIndex);
                var fileSize = returnCursor.GetString(sizeIndex);

                using (var stream = ContentResolver.OpenInputStream(dbUri))
                {
                    var length = int.Parse(fileSize);
                    var buffer = new byte[length];
                    var contents = stream.Read(buffer, 0, length);
                    var contentParsed = System.Text.Encoding.ASCII.GetString(buffer);
                }
            }

        }
    }
}

