# Athena
## About
Athena is a component of project Poseidon (a connected swimming pool system)  
It's responsible to store and dispatch every incoming telemetries to other services of the solution in addition to forwarding device's configuration to the telemetry's sender

## Settings
In `local.settings.json`
```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "AzureWebJobsDashboard": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "FUNCTIONS_EXTENSION_VERSION": "~3",
    "EventSubscribeConnectionString": "",
    "EventSubscribe": "",
    "EventPublishConnectionString": "",
    "EventPublish":  ""
  },
  "ConnectionStrings": {
    "DefaultConnection": ""
  }
}
```