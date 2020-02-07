param(
        [Parameter(Position=0,mandatory=$true)]
        [ValidateSet('clean','package','distribute', 'publish')]
        [string]
        $command
)

$origin = Get-Location
Set-Location $PSScriptRoot
$version = '0.0.10'
$libVersion = '0.5.0.1579093712'

function Clear-Output {
    Remove-Item obj -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item bin -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item lib -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item nupkgs -Recurse -Force -ErrorAction SilentlyContinue
}

function New-Package {
    Clear-Output
    ../scripts/dependencies.ps1 -libVersion $libVersion -location './lib' -ErrorAction Stop
    dotnet pack --configuration Release --output nupkgs -p:PackageVersion=$($version) -p:Version=$($version)
}

function Publish-Package {
    $package = (Get-ChildItem nupkgs | Where-Object {$_.extension -eq '.nupkg'} | Select-Object -First 1)
    dotnet nuget push  "nupkgs\$package" -k $env:NUGET_API_KEY -s https://api.nuget.org/v3/index.json
}

switch ($command) {
    'clean' {Clear-Output; break}
    'package' {New-Package; break}
    'distribute' {Clear-Output; break}
    'publish' {Publish-Package; break}
}

Set-Location $origin
