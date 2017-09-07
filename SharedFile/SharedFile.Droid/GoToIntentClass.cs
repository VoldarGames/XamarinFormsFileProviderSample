using Android.App;
using Android.Content;
using Android.Support.V4.Content;
using Xamarin.Forms;

namespace SharedFile.Droid
{
    public class GoToIntentClass : IGoToIntentApp
    {
        public void GoToClientApp()
        {
            Java.IO.File file = new Java.IO.File(((Activity)Forms.Context).FilesDir.AbsolutePath, "texto.txt");
            var uri = FileProvider.GetUriForFile(Forms.Context, "SharedFile.Droid.fileprovider", file);
            Forms.Context.GrantUriPermission("ClientSharedApp.ClientSharedApp",uri,ActivityFlags.GrantReadUriPermission);
            var intent = new Intent("ClientSharedApp.ClientSharedApp.START");
            intent.PutExtra("dbUri", uri);
            intent.SetFlags(ActivityFlags.GrantReadUriPermission);

            ((Activity)Forms.Context).StartActivity(intent);
        }
    }
}