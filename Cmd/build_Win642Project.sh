#!/bin/sh
dirname $0 /dev/null
cd `dirname $0`
cd ../
echo `dirname $0`
# dotnet publish -c Release -r osx-x64 -o Publish/Win64 /p:TrimUnusedDependencies=true
dotnet publish -c Release -r win7-x64 -o Publish/Win64 /p:TrimUnusedDependencies=true
# dotnet publish -c Release -o Publish/Win64 /p:TrimUnusedDependencies=true
cp -r -p Config/. upload/Publish/Win64/Config
# cp Cmd/run.sh upload/Publish/Win64/run.sh
# chmod 777 upload/Publish/Win64/run.sh


cp -r -p upload/Publish/Win64 /Users/cc/Documents/eyu/slg/xfiles/client/tools/upload/Win64