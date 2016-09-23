Write-Host "Setting environment variables..."
[System.Environment]::SetEnvironmentVariable("baseUrl", "http://*:9000")

Write-Host "Starting the current adapter..."
& "$Env:APPLICATION_PATH\Rewardle.Identity.Host.exe" install
Start-Sleep -s 5
& "$Env:APPLICATION_PATH\Rewardle.Identity.Host.exe" start
Start-Sleep -s 5
& "$Env:APPLICATION_PATH\Rewardle.Identity.Host.exe" start	
    
Start-Sleep -s 60

Invoke-RestMethod -Uri "https://api.runscope.com/radar/bucket/fc5cb601-8997-42e2-a445-d5413f6a756e/trigger" -Method Post
Invoke-RestMethod -Uri "https://api.runscope.com/radar/bucket/7646f37d-bbec-4fed-9244-5cb4e813d7f3/trigger" -Method Post
Invoke-RestMethod -Uri "https://api.runscope.com/radar/bucket/4917f39b-46f7-4257-b350-06fa92e7c37d/trigger" -Method Post

$environment = [System.Environment]::GetEnvironmentVariable("Environment")
$notificationPayload = @{ text="Rewardle.Identity " + $environment + " v" + $Env:APPVEYOR_BUILD_VERSION + " deployment complete."; username="Dev Server"; 
icon_url="http://bestofvin.es/images/amazon_icon_512x512.png"; channel= "#rewardle"; }
Invoke-RestMethod -Uri "https://hooks.slack.com/services/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" -Method Post -Body (ConvertTo-Json $notificationPayload)
