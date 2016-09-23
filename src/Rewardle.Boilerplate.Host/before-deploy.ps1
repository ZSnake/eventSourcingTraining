Write-Host "Stopping the current adapter..."
& "$Env:APPLICATION_PATH\Rewardle.Identity.Host.exe" stop
& "$Env:APPLICATION_PATH\Rewardle.Identity.Host.exe" uninstall