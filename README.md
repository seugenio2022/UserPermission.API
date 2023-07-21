## Description

API that allows assigning permissions to employees. An employee and 2 types of permissions are generated when the App is executed for the first time.

## Execution Requirements

* Visual Studio + Net 7  
* Docker Compose  
* SQLServer 

## Execution

1. Run "docker-compose up -d"
2. Modify the "Server" value used locally in src/WebUI/appsettings.json and tests/Application.IntegrationTests/appsettings.json
3. Open http://localhost:5000/swagger/index.html

## Features

* Information logs in every API endpoint with Serilog.
* Persistence of each operation in Kafka.
* Read operations with ElasticSearch.
* CQRS: SQL Server is used as the write model and ElasticSearch as the read model. The write model publishes events to Kafka, which are consumed by the read model to keep ElasticSearch updated.
* EntityFramework, Unit Of Work, and Repository Pattern.
* Unit Tests / Integration Tests
* Clean Architecture.
* Domain Driven Design.
* Solution prepared to run in Docker.
* Solution prepared to run in Kubernetes
