@echo off
set VERSIONSUFFIX=meshfeatures0001

echo Building %VERSIONSUFFIX%

dotnet build -c:Release --version-suffix %VERSIONSUFFIX% SharpGLTF.Core\SharpGLTF.Core.csproj
dotnet build -c:Release --version-suffix %VERSIONSUFFIX% SharpGLTF.Runtime\SharpGLTF.Runtime.csproj
dotnet build -c:Release --version-suffix %VERSIONSUFFIX% SharpGLTF.Toolkit\SharpGLTF.Toolkit.csproj

set DSTPATH=C:\Users\timle\source\repos\KeyframeAI\Keyframe.SpeckleTiler\SpeckleTiler\Packages

copy SharpGLTF.Core\bin\Release\*.*nupkg    %DSTPATH%
copy SharpGLTF.Runtime\bin\Release\*.*nupkg %DSTPATH%
copy SharpGLTF.Toolkit\bin\Release\*.*nupkg %DSTPATH%

pause