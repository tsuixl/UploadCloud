using System.IO;
using System.Collections.Generic;

namespace Util
{
    public static class UploadUtil
    {
        
        public static string NormalPath (string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return str.Replace("\\", "/");
        }


        public static void NormalPaths (List<string> paths)
        {
            for (int i = 0; i < paths.Count; i++)
            {
                paths[i] = NormalPath (paths[i]);
            }
        }


        public static string AddPathDiagonal (string path)
        {
            if (Path.GetFileName(path).Contains("."))
            {
                return path;
            }

            if (!path.EndsWith("/"))
            {
                return path + "/";
            }
            return path;
        }

    }
}