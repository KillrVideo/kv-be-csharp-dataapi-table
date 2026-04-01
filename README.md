# KillrVideo v2 - Dotnet C-Sharp Table API Backend

Date: March 2026

A reference backend for the KillrVideo sample application rebuilt for 2026 using **Dotnet**, **C-Sharp** and **DataStax Astra DB**. This backend uses the Data API (Tables) to access existing CQL tables.

---

## Overview
This repo demonstrates modern API best-practices with:

* Restful, typed request/response models
* Role-based JWT auth
* DataStax's data API client via `DataStax.AstraDB.DataApi -v 2.1.0-beta`
* Micro-service friendly layout – or run everything as a monolith

---

## Prerequisites
1. **.NET 8.0** or later
2. A **DataStax Astra DB** serverless database – [grab a free account](https://astra.datastax.com).

## Setup & Configuration
```bash
# clone
git clone git@github.com:KillrVideo/kv-be-csharp-dataapi-table.git
cd kv-be-csharp-dataapi-table

# build and install dependencies
dotnet add package DataStax.AstraDB.DataApi --version 2.1.0-beta
dotnet build
```

_Note: You can also install the Data API C# client using NuGet._

Database schema:
1. Create a new keyspace named `killrvideo`.
2. Create the tables from the CQL file in the killrvideo-data repository: <https://github.com/KillrVideo/killrvideo-data/blob/master/schema-astra.cql>

Environment variables (via `export`):

| Variable | Description |
|----------|-------------|
| `ASTRA_DB_API_ENDPOINT` | The API endpoint for your Astra database |
| `ASTRA_DB_APPLICATION_TOKEN` | Token created in Astra UI |
| `ASTRA_DB_KEYSPACE` | `killrvideo` |

Edit `appsettings.json`:
 - Generate and change the `jwt.key` key (or use the default).

Command line
 - Trust the ASP.NET Core HTTPS dev certificate. [documentation](https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-9.0&tabs=visual-studio%2Clinux-sles#trust-the-aspnet-core-https-development-certificate)
```bash
dotnet dev-certs https trust
```
_Note: If you have trouble with the certificate in your browser or via `curl`, try "cleaning" the Dotnet certificate store, and "trust" again. `dotnet dev-certs https clean`_

---

## Running the Application
```bash
dotnet build
dotnet run
```
Or simply...
```bash
dotnet run
```

## Test the health check service
```bash
curl -X GET "https://localhost:7264/api/v1/health" \
--header "Content-Type: application/json" \
--http1.0
```
"Service is up and running!"