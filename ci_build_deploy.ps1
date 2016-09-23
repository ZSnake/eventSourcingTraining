$ErrorActionPreference = "Stop"

Write-Host "Setting up nuget repository..."
nuget sources add -Name https://www.myget.org/F/rewardle/api/v2 -Source https://www.myget.org/F/rewardle/api/v2 -Username rewardle-deploy -Password '@#!4$*hWIhx3'

# Rewrite the AssemblyInfo.cs with our version information
# There may be a better way to do this than string replace, but this works...
$version = "1.0.$env:BUILDKITE_BUILD_NUMBER.1"
Write-Host "Patching AssemblyInfo"
Get-ChildItem -Recurse -Filter AssemblyInfo.cs | % {
  Write-Host "Patching $($_.FullName)..."
  $content = [System.IO.File]::ReadAllText($_.FullName).Replace('[assembly: AssemblyVersion("1.0.0.0")]',"[assembly: AssemblyVersion(`"$version`")]")
  $content = $content.Replace('[assembly: AssemblyFileVersion("1.0.0.0")]', "[assembly: AssemblyFileVersion(`"$version`")]")
  $content = $content.Replace('[assembly: AssemblyInformationalVersion("1.0.0.0")]', "[assembly: AssemblyInformationalVersion(`"$version`")]")
  [System.IO.File]::WriteAllText($_.FullName, $content)
  Write-Host "OK"
}

Write-Host "Restoring nuget packages..."
Get-ChildItem src -Recurse -Filter *.sln | % {
  Write-Host "Restoring $($_.FullName)..."
  & nuget restore -Source https://www.myget.org/F/rewardle/api/v2 -Verbosity detailed $_.Directory
}

Write-Host "Copying config.default files..."
if ((!(Test-Path src/connectionStrings.config)) -and (Test-Path connectionStrings.config.default)) { Copy-Item connectionStrings.config.default src/connectionStrings.config }
if ((!(Test-Path src/logging.config)) -and (Test-Path logging.config.default))  { Copy-Item logging.config.default src/logging.config }

Write-Host "Building packages..."
Get-ChildItem src -Recurse -Filter deploy.ps1 | % {
  Get-ChildItem $_.Directory -Filter *.csproj | % {
    & 'C:\Program Files (x86)\MSBuild\12.0\bin\MSBuild.exe' /p:Configuration=Release $_.FullName
  }
}

Write-Host "Building Specs..."
Get-ChildItem src -Recurse -Filter *.Specs.csproj | % {
  & 'C:\Program Files (x86)\MSBuild\12.0\bin\MSBuild.exe' /p:Configuration=Release /p:OutDir=specs $_.FullName
}

Write-Host "Running Specs..."
Get-ChildItem specs -Recurse -Filter *.Specs.dll | % { & 'C:\Machine.Specifications.Runner.Console.0.9.1\tools\mspec-clr4.exe' $_.FullName }

Write-Host "Creating nuget packages..."
Get-ChildItem src -Filter *.nuspec | % {
  nuget pack -Properties "Configuration=Debug;Platform=AnyCPU" $_
}

Write-Host "Pushing nuget packages..."
Write-Host "psych - not working yet XXX"

Write-Host "Zipping Applications and uploading to S3..."
Get-ChildItem src -Directory -Exclude *.Specs, .nuget | % {
  $packageName = $_.Name
  Get-ChildItem $_ -Recurse -Directory -Filter Release | % {
     & 'C:\Program Files\7-Zip\7z.exe' a -tzip "$packageName.zip" -r "$($_.FullName)\*.*"
    Write-S3Object -BucketName rewardlecompiledsource -File "$packageName.zip" -Key "services/$packageName/$env:BUILDKITE_BUILD_NUMBER/$packageName.zip" -CannedACLName Private
  }
}
