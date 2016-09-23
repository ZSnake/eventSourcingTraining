Write-Host "Stopping the current adapter..."
& "$Env:APPLICATION_PATH\Rewardle.Identity.EventQueueWorker.exe" stop
& "$Env:APPLICATION_PATH\Rewardle.Identity.EventQueueWorker.exe" uninstall