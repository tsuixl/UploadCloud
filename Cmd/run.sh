
dirname $0
cd `dirname $0`
dotnet ./upload.dll "{\"Type\":\"Oss\", \"OssBucketName\":\"clongame-slg-patches\", \"UploadPath\":\"TestFile\", \"LocalPath\":\"/Users/cc/github/UploadCloud/TestFile\" }"