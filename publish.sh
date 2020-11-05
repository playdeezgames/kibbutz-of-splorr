#!/bin/bash

rm -rf ./deploy/win/*
rm -rf ./deploy/mac/*
rm -rf ./deploy/linux/*
rm -rf ./deploy/pi/*
rm -rf ./deploy/*.zip

dotnet publish -c Release --runtime win-x64 -o ./deploy/win ./Splorr.Kibbutz.MonoGame/Splorr.Kibbutz.MonoGame.fsproj -p:PublishReadyToRun=true
dotnet publish -c Release --runtime osx-x64 --framework netcoreapp3.1 --self-contained true  -o ./deploy/mac ./Splorr.Kibbutz/Splorr.Kibbutz.fsproj
dotnet publish -c Release --runtime linux-x64 --framework netcoreapp3.1 --self-contained true  -o ./deploy/linux ./Splorr.Kibbutz/Splorr.Kibbutz.fsproj
dotnet publish -c Release --runtime linux-arm64 --framework netcoreapp3.1 --self-contained true  -o ./deploy/pi ./Splorr.Kibbutz/Splorr.Kibbutz.fsproj

rm ./deploy/win/*.pdb
rm ./deploy/mac/*.pdb
rm ./deploy/linux/*.pdb
rm ./deploy/pi/*.pdb
"C:\Program Files\7-Zip\7z" a -tzip ./deploy/Kibbutz-win.zip ./deploy/win/*
"C:\Program Files\7-Zip\7z" a -tzip ./deploy/Kibbutz-mac.zip ./deploy/mac/*
"C:\Program Files\7-Zip\7z" a -tzip ./deploy/Kibbutz-linux.zip ./deploy/linux/*
"C:\Program Files\7-Zip\7z" a -tzip ./deploy/Kibbutz-pi.zip ./deploy/pi/*
