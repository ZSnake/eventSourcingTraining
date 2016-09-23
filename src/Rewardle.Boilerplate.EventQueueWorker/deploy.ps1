Write-Host "Starting the current adapter..."
& "$Env:APPLICATION_PATH\Rewardle.Identity.EventQueueWorker.exe" install
Start-Sleep -s 5
& "$Env:APPLICATION_PATH\Rewardle.Identity.EventQueueWorker.exe" start
Start-Sleep -s 5
& "$Env:APPLICATION_PATH\Rewardle.Identity.EventQueueWorker.exe" start