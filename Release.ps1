$CD = Get-Location;
$Out = "$CD\Out"
$TmpOut = "$CD\tmp"
$MsBuild = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe";
$DotNet = "C:\Program Files\dotnet\dotnet.exe";

if (Test-Path $Out)
{
	Remove-Item -Verbose -Force -Recurse $Out
}

if (Test-Path $TmpOut)
{
	Remove-Item -Verbose -Force -Recurse $TmpOut
}

nuget restore $CD\DesktopBlazor.Contracts\Contracts.csproj
nuget restore $CD\Wpf\DesktopBlazor.Wpf\Wpf.csproj
& $MsBuild /t:build "$CD\Wpf\DesktopBlazor.Wpf\Wpf.csproj" /p:OutDir=$Out;

nuget restore $CD\WebApp\DesktopBlazor.WebApp\WebApp.csproj
& $DotNet publish --configuration release "$CD\WebApp\DesktopBlazor.WebApp\WebApp.csproj" --output $TmpOut;
Copy-Item -Verbose -Recurse -Path "$TmpOut\DesktopBlazor.WebApp\dist" -Destination "$Out\dist";

Remove-Item -Verbose -Force -Recurse $TmpOut
