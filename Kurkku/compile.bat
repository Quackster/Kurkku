dotnet publish --self-contained -c Release -r linux-x64 -p:IncludeAllContentForSelfExtract=True /p:PublishSingleFile=true /p:PublishTrimmed=true
dotnet publish --self-contained -c Release -r win-x64 -p:IncludeAllContentForSelfExtract=True /p:PublishSingleFile=true /p:PublishTrimmed=true
