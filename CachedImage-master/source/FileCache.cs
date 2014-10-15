using System;
using System.Diagnostics;
using System.IO;

namespace CachedImage
{
    public class FileCache
    {
        static FileCache()
        {
            // default cache directory - can be changed if needed from App.xaml
            AppCacheDirectory = string.Format("{0}\\{1}\\Cache\\",
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "/Pirate3D/Buccaneer/");
        }

        /// <summary>
        /// Gets or sets the path to the folder that stores the filesystem-based cache
        /// </summary>
        public static string AppCacheDirectory { get; set; }

        public static string FromUrl(string url)
        {
            // Check to see if the cache directory has been created
            if (!Directory.Exists(AppCacheDirectory))
            {
                // create it
                Directory.CreateDirectory(AppCacheDirectory);
            }

            // Cast the string into a Uri so we can access the image name without regex
            var uri = new Uri(url);
            string temp = uri.Segments[uri.Segments.Length - 2] + uri.Segments[uri.Segments.Length - 1];
            string name = temp.Replace(@"/", "A");
            string localFile = string.Format("{0}\\{1}", AppCacheDirectory, name);

            if (!File.Exists(localFile))
            {
                HttpHelper.GetAndSaveToFile(url, localFile);
            }

            // The full path of the image on the local computer
            return localFile;
        }
    }
}