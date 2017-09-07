using System;
using System.IO;

namespace SharedFile.Droid
{
    public class AccessFile : IAccessFile
    {
        public string ReadFile()
        {
            var fileName = "texto.txt";

            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);
            if (!File.Exists(path)) return "FILE DOESN'T EXIST";
            
            using (var sw = new StreamReader(path))
            {
                return sw.ReadToEnd();
            }
        }
    }
}