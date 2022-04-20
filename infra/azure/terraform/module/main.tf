resource "azurerm_resource_group" "athena" {
  name     = var.rg_name
  location = var.rg_location
}

resource "azurerm_storage_account" "athena" {
  name                     = "${var.release_name}sta"
  resource_group_name      = azurerm_resource_group.athena.name
  location                 = azurerm_resource_group.athena.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_app_service_plan" "athena" {
  name                = "${var.release_name}-service-plan"
  location            = azurerm_resource_group.athena.location
  resource_group_name = azurerm_resource_group.athena.name
  kind                = "FunctionApp"
  reserved            = true

  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}

resource "azurerm_function_app" "athena" {
  name                       = "${var.release_name}-function"
  location                   = azurerm_resource_group.athena.location
  resource_group_name        = azurerm_resource_group.athena.name
  app_service_plan_id        = azurerm_app_service_plan.athena.id
  storage_account_name       = azurerm_storage_account.athena.name
  storage_account_access_key = azurerm_storage_account.athena.primary_access_key
  os_type                    = "linux"

  app_settings = {
    FUNCTIONS_WORKER_RUNTIME               = "dotnet"
    FUNCTIONS_EXTENSION_VERSION            = "~4"
    WEBSITE_RUN_FROM_PACKAGE               = var.athena_package_source
    APPINSIGHTS_INSTRUMENTATIONKEY         = var.create_app_insights ? azurerm_application_insights.athena[0].instrumentation_key : var.app_insight_instrumentation_key
    EventPublishConnectionString           = var.event_publish_connection_string
    EventPublishTopic                      = var.event_publish_topic
    EventSubscribeConnectionString         = var.event_subscribe_connection_string
    EventSubscribeTopic                    = var.event_subscribe_topic
    AzureWebJobs.TelemetriesHttp.Disabled  = var.disable_http_trigger
    AzureWebJobs.TelemetriesEvent.Disabled = var.disable_eventhub_trigger
    AzureWebJobs.DaprEvent.Disabled        = "true"
  }

  connection_string {
    name  = "DefaultConnection"
    value = var.db_connection_string
    type  = "SQLServer"
  }

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_application_insights" "athena" {
  count = var.create_app_insights ? 1 : 0

  name                = "${var.release_name}-appinsights"
  location            = azurerm_resource_group.athena.location
  resource_group_name = azurerm_resource_group.athena.name
  application_type    = "web"
}