# [helm-chart-v2.0.0](https://github.com/AlexisMtr/athena/compare/helm-chart-v1.1.1...helm-chart-v2.0.0) (2022-04-20)


### chore

* **helm:** use ingress v1 ([7b702d8](https://github.com/AlexisMtr/athena/commit/7b702d8b7db536b5bd5220b3332ebd1f92261730))


### Features

* **athena:** bump appVersion to 1.1.0 ([728ce3a](https://github.com/AlexisMtr/athena/commit/728ce3a2f175cfda0d36779e9d9785da3e410473))
* **helm:** add support for DAPR ([5a1db97](https://github.com/AlexisMtr/athena/commit/5a1db97899cfd0fe8c12f96becc363926884c2b2))


### BREAKING CHANGES

* **helm:** drop support for ingress v1beta1 (require k8s 1.19+)
* **helm:** values structure has changed

# [helm-chart-v1.1.1](https://github.com/AlexisMtr/athena/compare/helm-chart-v1.1.0...helm-chart-v1.1.1) (2021-02-27)


### Bug Fixes

* **secret:** generate secret only if existingEnvSecret is not defined ([378fbc0](https://github.com/AlexisMtr/athena/commit/378fbc01c83453b7a3e6361b4b62854b46b66df9))
* quote secrets and configmap ([86c19a6](https://github.com/AlexisMtr/athena/commit/86c19a6af19aa6e8ae5d681b013514f87d29fa60))

# [helm-chart-v1.1.0](https://github.com/AlexisMtr/athena/compare/helm-chart-v1.0.0...helm-chart-v1.1.0) (2021-02-27)


### Features

* **env:** allow existing secret to fill env variables ([f69b0df](https://github.com/AlexisMtr/athena/commit/f69b0df2c9bc9eb70297f5ce3862bfbcae1faed5))

# helm-chart-v1.0.0 (2021-01-27)


### Bug Fixes

* **athena:** bump appVersion to 1.0.0 ([8213031](https://github.com/AlexisMtr/athena/commit/82130319352b37297d34500374a954bd185ec724))
* force release 1.0.0 ([e05936f](https://github.com/AlexisMtr/athena/commit/e05936feebe677d0034fd0e06c97b1537e3ea65b))

This file is generated using [SemanticRelease](https://github.com/semantic-release/changelog)
