# Deploy on k8s
Released charts will be available on [https://alexismtr.github.io/poseidon-helm-chart](https://github.com/alexismtr/poseidon-helm-chart)
```sh
$ helm repo add poseidon https://alexismtr.github.io/poseidon-helm-chart
```

## Install/Upgrade/Uninstall the Chart
Install the chart with
```sh
$ helm install athena poseidon/athena
```
Upgrade the chart with
```sh
$ helm upgrade athena poseidon/athena
```
Uninstall the chart with
```sh
$ helm uninstall athena
```

## Configuration
| parameter | default |
| --------- | ------- |
| replicaCount | 1 |
| image.repository | alexismtr/athena |
| image.pullPolicy | `IfNotPresent` |
| image.tag | `$.Chart.AppVersion` |
| imagePullSecrets | `[]` |
| nameOverride | ""
| fulleNameOverride | "" |
| serivceAccount.create | `false` |
| serviceAccount.annotations | `{}` |
| serviceAccount.name | "" |
| podAnnotations | `{}` |
| podSecurityContext | `{}` |
| securityContext | `{}` |
| service.type | `CLusterIP` |
| service.port | `80` |
| ingress.enable | `false` |
| ingress.annotations | `{}` |
| ingress.hosts | `[]`|
| ingress.tls | `[]` |
| resources | `{}` |
| autoscaling.enable | `false`|
| autoscaling.minReplicas | `1` |
| autoscaling.maxReplicas | `100` |
| autoscaling.targetCPUUtilizationPercentage | `80` |
| nodeSelector | `{}` |
| toleration | `[]` |
| affinity | `{}` |
| environments.FUNCTIONS_WORKER_RUNTIME | `dotnet ` |
| environments.FUNCTIONS_EXTENSION_VERSION | `~3 ` |
| environments.AZURE_FUNCTIONS_ENVIRONMENT | `DEVELOPMENT` |
| environments.EventPublish | `new_computed_telemetries` |
| environments.EventSubscribe | `new_telemetries` |
| environmentSecrets.AZ_STORAGE | "" |
| environmentSecrets.EVT_SUB_CONNECTION_STRING | "" |
| environmentSecrets.EVT_PUB_CONNECTION_STRING | "" |
| environmentSecrets.DB_CONNECTION_STRING | "" |