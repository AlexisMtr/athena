# Deploy on Azure
## Overview
Whatever you decide, both methods will deploy the following resources on Azure
* StorageAccount x1
* AppServicePlan x1 *(on consumption plan)*
* FunctionApp x1
* ApplicationInsights x1 *(depending of inputs)*

Both methods will outputs
* FunctionApp Indentity PrincipalId
## ARM/Bicep
Deploy required resource on Azure using [Bicep DSL](https://github.com/azure/bicep)
```console
$ export RG_NAME=
$ export DEPLOYMENT_NAME=
$ az deployment group create -g $RG_NAME -n $DEPLOYMENT_NAME -f bicep/main.bicep
```
## Terraform
This folder contain Terraform module. To use it, create a workspace and import this module