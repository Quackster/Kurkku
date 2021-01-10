dotnet publish --self-contained -c Release -r ubuntu-x64 -p:IncludeAllContentForSelfExtract=True /p:PublishSingleFile=true
dotnet publish --self-contained -c Release -r win-x64 -p:IncludeAllContentForSelfExtract=True /p:PublishSingleFile=true /p:PublishTrimmed=true
