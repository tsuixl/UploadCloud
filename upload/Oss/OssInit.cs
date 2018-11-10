using Common;
using System.IO;
using Newtonsoft.Json;

namespace Oss
{
    public class OssInit : ArgsInit
    {
        //  md5检测
        public bool Md5Checkout { get; set; }

        //  超时(毫秒)
        public int ConnectionTimeout { get; set; }

        public string Endpoint {get; set;}
        public string AccessKeyId {get; set;}
        public string AccessKeySecret {get; set;}
        public string BucketName {get; set;}

        public static OssInit GetDefault ()
        {
            return JsonConvert.DeserializeObject<OssInit> (File.ReadAllText("Config/OssConfig.json"));
        }


        public static OssInit GetDefault (ArgsData data)
        {
            var ossInit = JsonConvert.DeserializeObject<OssInit> (File.ReadAllText("Config/OssConfig.json"));
            
            ossInit.LocalRootPath = data.LocalPath;
            if (File.Exists(ossInit.LocalRootPath))
            {
                ossInit.LocalRootPath = string.Empty;
            }
            ossInit.UploadFolder = data.UploadPath;
            ossInit.BucketName = data.OssBucketName;
            return ossInit;
        }
    
    }
}