Write-Host "Stopping the current adapter..."
& "$Env:APPLICATION_PATH\Rewardle.Identity.CommandQueueWorker.exe" stop
& "$Env:APPLICATION_PATH\Rewardle.Identity.CommandQueueWorker.exe" uninstall