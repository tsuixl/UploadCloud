#!/bin/sh
dirname $0 /dev/null
cd `dirname $0`
cd ../
echo `dirname $0`
# dotnet publish -c Release -r osx-x64 -o Publish/Osx64 /p:TrimUnusedDependencies=true
dotnet publish -c Release -o Publish/Osx64 /p:TrimUnusedDependencies=true
# dotnet publish -c Release -o Publish/Osx64 /p:TrimUnusedDependencies=true
cp -r -p Config/. upload/Publish/Osx64/Config
cp Cmd/run.sh upload/Publish/Osx64/run.sh
chmod 777 upload/Publish/Osx64/run.sh