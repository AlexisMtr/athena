variable "rg_name" {
  type        = string
  description = "name of the resource group"
}

variable "rg_location" {
  type        = string
  description = "location of the resource group"
}

variable "release_name" {
  type        = string
  description = "name of the release (used to name resources)"
}

variable "app_insight_instrumentation_key" {
  type        = string
  description = "Existing instrumentationkey"
}

variable "create_app_insights" {
  type        = bool
  default     = false
  description = "Create or use given instrumentationkey"
}

variable "event_subscribe_connection_string" {
  type        = string
  description = "ConnectionString to EventHub to listen on (use KV notation to use secret on KV)"
}

variable "event_subscribe_topic" {
  type        = string
  description = "EventHub/Kafka topic to listen on"
}

variable "event_publish_connection_string" {
  type        = string
  description = "ConnectionString to EventHub to publish on (use KV notation to use secret on KV)"
}

variable "event_publish_topic" {
  type        = string
  description = "EventHub topic to publish on"
}

variable "db_connection_string" {
  type        = string
  description = "ConnectionString to the poseidon database (use KV notation to use secret on KV)"
}

variable "athena_package_source" {
  type        = string
  description = "URI of the Athena Package available on Internet"
}

variable "disable_http_trigger" {
  type        = bool
  default     = false
  description = "Disable HTTP Trigger"
}

variable "disable_eventhub_trigger" {
  type        = bool
  default     = false
  description = "Disable Eventhub Trigger"
}