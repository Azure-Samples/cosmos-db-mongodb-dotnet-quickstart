<!--
---
page_type: sample
name: "Quickstart: Azure Cosmos DB for MongoDB and .NET"
description: This is a simple ASP.NET web application to illustrate common basic usage of Azure Cosmos DB for MongoDB and .NET.
urlFragment: template
languages:
- csharp
- azdeveloper
products:
- azure-cosmos-db
---
-->

# Quickstart: Azure Cosmos DB for MongoDB - .NET

This is a simple Blazor web application to illustrate common basic usage of Azure Cosmos DB for MongoDB with .NET

## Prerequisites

- [Docker](https://www.docker.com/)
- [Azure Developer CLI](https://aka.ms/azd-install)
- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0) 

## Quickstart

1. Log in to Azure Developer CLI. *This is only required once per-install.*

    ```bash
    azd auth login
    ```

1. Initialize this template (`cosmos-db-mongodb-dotnet-quickstart`) using `azd init`.

    ```bash
    azd init --template cosmos-db-mongodb-dotnet-quickstart
    ```

1. (Optional) Select either `vcore` or `request-unit` account type using `azd env set`.

    ```bash
    azd env set "MONGODB_DEPLOYMENT_TYPE" "vcore"
    ```

    ```bash
    azd env set "MONGODB_DEPLOYMENT_TYPE" "request-unit"
    ```

1. Ensure that **Docker** is running in your environment.

1. Use `azd up` to provision your Azure infrastructure and deploy the web application to Azure.

    ```bash
    azd up
    ```

1. Observed the deployed web application

    ![Screenshot of the deployed web application.](assets/web.png)

1. (Optionally) Run this web application locally in the `src/web` folder: 

    ```dotnetcli
    dotnet watch run
    ```
