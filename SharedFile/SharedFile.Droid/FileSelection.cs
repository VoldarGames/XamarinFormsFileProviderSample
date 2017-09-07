using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Lang;
using Environment = System.Environment;
using String = System.String;

namespace SharedFile.Droid
{
    [Activity(Label = "FileSelection")]
    public class FileSelection : Activity
    {
        // The path to the root of this app's internal storage
        public File mPrivateRootDir;
        // The path to the "images" subdirectory
        public File mImagesDir;
        // Array of files in the images subdirectory
        public File[] mImageFiles;
        // Array of filenames corresponding to mImageFiles
        String[] mImageFilenames;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FileSelection);

            

            // Set up an Intent to send back to apps that request a file
            var mResultIntent = new Intent("com.example.myapp.ACTION_RETURN_FILE");
            // Get the files/ subdirectory of internal storage
            mPrivateRootDir = new File(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            // Get the files/images subdirectory;
            mImagesDir = new File(mPrivateRootDir, "images");
            // Get the files in the images subdirectory
            mImageFiles = mImagesDir.ListFiles();
            // Set the Activity's result to null to begin with
            SetResult(Result.Canceled, null);
            /*
             * Display the file names in the ListView mFileListView.
             * Back the ListView with the array mImageFilenames, which
             * you can create by iterating through mImageFiles and
             * calling File.getAbsolutePath() for each File
             */
            var contents = mImageFiles.ToList();
            var listView = FindViewById<ListView>(Resource.Id.mylistview);
            listView.Adapter = new ArrayAdapter(this, Resource.Id.list_item, contents);

            listView.OnItemClickListener = new OnItemClick(mImageFilenames, this);
        }
    }

    public class OnItemClick : AdapterView.IOnItemClickListener
    {
        private readonly string[] _mImageFilenames;
        private readonly FileSelection _fileSelection;

        public OnItemClick(string[] mImageFilenames, FileSelection fileSelection)
        {
            _mImageFilenames = mImageFilenames;
            _fileSelection = fileSelection;
        }
        public void Dispose()
        {
            
        }

        public IntPtr Handle { get; }

        void AdapterView.IOnItemClickListener.OnItemClick(AdapterView parent, View view, int position, long id)
        {
            File requestFile = new File(_mImageFilenames[position]);
            /*
                 * Most file-related method calls need to be in
                 * try-catch blocks.
                 */
            // Use the FileProvider to get a content URI
            try
            {
                var fileUri = FileProvider.GetUriForFile(
                    _fileSelection,
                    "SharedFile.Droid.fileprovider",
                    requestFile);
            }
            catch (IllegalArgumentException e)
            {
                Log.Error("File Selector",
                   "The selected file can't be shared: " +
                   _mImageFilenames[position] + "Exception: "  + e);
            }
        }
    }
}

