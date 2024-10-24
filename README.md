# README #

Professional REST API design with ASP.NET Core and WebAPI following the principles of Clean Architecture.

## Technologies
* .NET 8
* ASP .NET Core 8
* Entity Framework Core 8
* AutoMapper
* FluentValidation

### Database Migrations

To use `dotnet-ef` for your migrations please add the following flags to your command (values assume you are executing from repository root)

- `--project src/Infrastructure` (optional if in this folder)
- `--startup-project src/WebUI`
- `--output-dir Migrations`

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.


### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.


### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebApplication1

This layer is a Web API template application based on ASP.NET Core 8. This layer depends on both the Application and Infrastructure layers.
