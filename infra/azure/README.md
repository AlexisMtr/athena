# Deploy on Azure
## Overview
Whatever you decide, both methods will deploy the following resources on Azure
* StorageAccount x1
* AppServicePlan x1 *(on consumption plan)*
* FunctionApp x1
* ApplicationInsights x1 *(depending of inputs)*

Both methods will outputs
* FunctionApp Indentity PrincipalId
* ApplicationInsights instrumentationKey
## ARM
You can find in `templates` folder the ARM Template that you can use to deploy the required resources on an Azure ResourceGroup
### Bicep
**WIP**, see [Azure Bicep](https://github.com/azure/bicep)
## Terraform
