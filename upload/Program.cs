using System;
using System.Collections.Generic;
using System.IO;
using Aliyun.OSS;
using Oss;
using Util;
using Newtonsoft.Json;

namespace upload
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            // OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);    
            // var buckets = client.ListBuckets();
            
            // foreach (var bucket in buckets) 
            // {
            //     Console.WriteLine(bucket.Name + ", " + bucket.Location + ", " + bucket.Owner);
            // }

            // OssTest();

            // string jsonStr = "{\"Type\":\"Oss\", \"OssBucketName\":\"clongame-slg-patches\", \"UploadPath\":\"TestFile\", \"LocalPath\":\"/Users/cc/github/UploadCloud/TestFile\" }";
            // if (args.Length <= 0 )
            // {
            //     args = new string[] {jsonStr};
            // }


            System.Console.WriteLine (JsonConvert.SerializeObject(args));


            if (args.Length == 1)
            {
                ArgsData data = JsonConvert.DeserializeObject<ArgsData> (args[0]);
                Start (data);
            }
            else if (args.Length > 1)
            {
                ArgsData argsData = new ArgsData();
                if (argsData.TryParseLine (args))
                {
                    System.Console.WriteLine (JsonConvert.SerializeObject(argsData));
                    Start (argsData);
                }
            }
            else
            {
                Log.Content = new ArgsData();
                Log.l (ArgsData.GetTips());
            }
        }


        static void Start(ArgsData data)
        {
            Log.Content = data;
            if (data.CheckContent())
            {
                data.NormalPath();

                //  阿里云
                if (data.Type == UploadType.Oss)
                {
                    var ossInit = OssInit.GetDefault(data);
                    var ossAgent = new OssAgent(ossInit);
                    ossAgent.Connect();
                    ossAgent.StartUpload(data.GetUploadPaths());
                }
            }
            else
            {
                Log.Kill("参数异常.");
            }
        }


        // public static void OssTest ()
        // {
        //     var tsetRootPath = "/Users/cc/github/UploadCloud/TestFile";

        //     var uploadInfos = new List<string> {
        //         "/Users/cc/github/UploadCloud/TestFile/up",
        //         "/Users/cc/github/UploadCloud/TestFile/version.moe"
        //     };

        //     var initData = new OssInit();
        //     initData.Endpoint = "http://oss-cn-shenzhen.aliyuncs.com";
        //     initData.AccessKeyId = "LTAIg5ezba9oiusk";
        //     initData.AccessKeySecret = "YvQIGvkn1qCkCL5SmxcV11gIFcA1B6";
        //     initData.BucketName = "clongame-slg-patches";
        //     initData.UploadFolder = "test";
        //     initData.LocalRootPath = UploadUtil.AddPathDiagonal(tsetRootPath);

        //     var ossAgent = new OssAgent(initData);
        //     ossAgent.Connect();
        //     ossAgent.StartUpload (uploadInfos);
        // }


    }
}
