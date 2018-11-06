using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  

namespace System
{
    public static class Utils
    {
        public static bool IsNullOrEmpty(this string pString)
        {
            return string.IsNullOrEmpty(pString);
        }

        public static string ToSlashesFileSystem(this string input)
        {
            return input.Replace(@"/", @"\");
        }

        public static string RemoveLastFolder(this string input)
        {
            var folders = input.Split('/');
            var folders_string = string.Join("/", folders.Take(folders.Length - 1));
            return folders_string;
        }
    }
}
