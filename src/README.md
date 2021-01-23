>NOTE: The project is built on .NET Core 3.1, be sure to have the SDK on your machine, or [install it](https://dotnet.microsoft.com/download/dotnet-core/3.1)
# Configure your dev machine

## Environment variables
| name | description |
| ---- | ----------- |
| AzureWebJobsStorage | Connection string to Azure Storage Account |
| AzureWebJobsDashboard | Connection string to Azure Storage Account |
| FUNCTIONS_WORKER_RUNTIME | should be `dotnet` |
| FUNCTIONS_EXTENSION_VERSION | should be `~3` |
| EventSubscribeConnectionString | Connection string for broker |
| EventSubscribe | Event path to subscribe (eg: topic) |
| EventPublishConnectionString | Connection string for broker |
| EventPublish | Event path to publish (eg: topic) |
| ConnectionStrings__DefaultConnection | DB ConnectionString |

## VisualStudio / VSCode
VisualStudio add local.settings.json file to the .gitignore for the FunctionApp. So, add local.settings.json to the project and configure the values (see [Environment Variables](#Environment-variables))
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
    "EventPublish": ""
  },
  "ConnectionStrings": {
    "DefaultConnection": ""
  }
}
```
# Build
## Using CLI
```sh
$ dotnet build -c <Debug|Release> -o build Athena.csproj
```
## Using Docker
```sh
$ docker build -t athena .
```
# Run
## using `CLI`
(reference: [azure function documentation](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=linux%2Ccsharp%2Cbash#start))
```sh
$ func run --build
```
## using Docker
```sh
$ docker run -it --rm --name athena \
  -e "ConnectionStrings__DefaultConnection=<your_db_connection_string>" \
  -e "AzureFunctionsJobHost__Logging__Console__IsEnabled=true" \
  -e "AzureWebJobsSecretStorageType='blob'" \
  -e "AzureWebJobsStorage=<your_storage_account>" \
  -e "AzureWebJobsDashboard=<your_storage_account>" \
  -e "FUNCTIONS_WORKER_RUNTIME=dotnet" \
  -e "FUNCTIONS_EXTENSION_VERSION=~3" \
  -e "EventSubscribeConnectionString=<your_broker_connection_string>" \
  -e "EventSubscribe=<your_subsribe_topic>" \
  -e "EventPublishConnectionString=<your_broker_connection_string>" \
  -e "EventPublish=<your_publish_topic>" \
  -p 8080:80
  athena
```