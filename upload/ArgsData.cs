using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Util;
using System.IO;


public enum UploadType 
{
    None,
    Oss
}


public class ArgsData
{

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public UploadType Type { get; set; }

    //  云端目录
    public string UploadPath { get; set; }

    //  本地上传目录
    public string LocalPath { get; set; }


    public bool Jenkins { get; set; }


    #region Oss
    
    public string OssBucketName { get; set; }

    #endregion


    public ArgsData ()
    {
        UploadPath = string.Empty;
        LocalPath = string.Empty;
    }


    public bool TryParseLine (string [] args)
    {
        if (args.Length < 3)
        {
            return false;
        }

        Type = UploadType.None;
        object enumTemp = null; 
        if (System.Enum.TryParse (typeof(UploadType), args[0], out enumTemp))
        {
            Type = (UploadType)enumTemp;
        }

        if (Type == UploadType.None)
            return false;

        if (Type == UploadType.Oss)
        {
            if (args.Length < 4)
            {
                Log.Kill ("Oss 上传需要4个参数! Type, OssBucketName, LocalPath, UploadPath");
                return false;
            }

            OssBucketName = args[1];
            LocalPath = args[2];
            UploadPath = args[3];
        }

        return true;
    }

    public void NormalPath ()
    {
        UploadUtil.NormalPath (LocalPath);
        UploadUtil.NormalPath (UploadPath);
        LocalPath = UploadUtil.AddPathDiagonal(LocalPath);
    }

    public bool CheckContent ()
    {
        if (Type == UploadType.None)
        {
            Log.e ("UploadType == None.");
            return false;
        }

        if (string.IsNullOrEmpty(LocalPath))
        {
            Log.e ("LocalPath == null.");
            return false;
        }

        if (Type == UploadType.Oss && string.IsNullOrEmpty(OssBucketName))
        {
            Log.e("Type == Oss, OssBucketName == null.");
            return false;
        }

        return true;
    }


    public List<string> GetUploadPaths ()
    {
        List<string> result = new List<string>();
        if (Directory.Exists(LocalPath))
        {
            var paths = Directory.GetDirectories (LocalPath);
            foreach (var p in paths)
            {
                result.Add(p);
            }

            var files = Directory.GetFiles (LocalPath);
            foreach (var f in files)
            {
                result.Add(f);
            }
        }

        if (File.Exists(LocalPath))
        {
            result.Add(LocalPath);
        }

        return result;
    }


    public static string GetTips ()
    {
        StringBuilder sb = new StringBuilder(256);
        sb.AppendLine ("通用参数详情:");
        sb.AppendLine ("\tUploadType : 上传类型( Oss[阿里云] ).");
        sb.AppendLine ("\tUploadPath : 云端根目录.");
        sb.AppendLine ("\tLocalPath : 本地上传目录(根目录的文件优先级最低).");

        sb.AppendLine ("Oss 参数:");
        sb.AppendLine ("\tOssBucketName : Oss Bucket 名称.");

        return sb.ToString();
    }
}