What is the Saloot Project?
=====================
The Saloot Project is an open-source project written in .NET Core and Vue TypeScript.

The goal of this repsoitory is to implement the most common used technologies in .Net Core backend and Vue Ts frontend.

This project is a template for creating a Single Page App (SPA) with Vue TypeScript and ASP.NET Core.

## Give a Star! :star:
If you liked the repo or if it helped you, a star would be appreciated.

## Features
* Multi Tenancy
* Multi Database providers (PostgreSql , SqlServer)
* File uploading on file system or database
* Global Exception Handling
* Logging :
    1. All api actions are logged.
    2. All exceptions are logged.
* JWT Authentication
* .Net Core Identity
* Email Service

## Technologies

Backend :

* [ASP.NET Core 5](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0)
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [MailKit](https://www.nuget.org/packages/MailKit/)
* [Serilog](https://serilog.net/)
* [SqlServer](https://www.microsoft.com/en-us/sql-server/sql-server-2019)
* [PostgreSQL](https://www.postgresql.org/)
* [XUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq)

Frontend :

* [VueJs 2.6.12](https://vuejs.org/) 
* [Type Script](https://www.typescriptlang.org/)
* [Vue Router](https://router.vuejs.org/)
* [vue-axios TS](https://www.npmjs.com/package/vue-axios) , [Axios](https://github.com/axios/axios)
* [vue-notifications TS](https://www.npmjs.com/package/vue-notification)
* [vuex-module-decorators](https://www.npmjs.com/package/vuex-module-decorators)
* [Admin_LTE](https://adminlte.io/)
* [bootstrap](https://getbootstrap.com/)

## Database Configuration
* Database provider :

    You can use SqlServer or PostgreSql in this template by update DatabaseProvider in **Api/appsettings.json** 
    ```json
      "DatabaseProvider": 1 /* Postgres = 1 , SqlServer = 2 */
    ```
    Then update ConnectionString 
    ```json
     "ConnectionStrings": {
        "SqlServer": "Server=.;Database=SalootProject;User Id=YourDatabaseId;Password=YourDatabasePassword",
        "Postgres": "Host=localhost;Database=SalootProject;Username=YourDatabaseUsername;Password=YourDatabasePassword"
      },
    ```

* Files Storing :
    There are two ways for storing files in this template :
    1. Store in Database
    2. Store in files

    ```json
      "StoreFilesOnDatabase": false,
    ```

## Backend Architecture Overview

This application uses Onion architecture.

### Core Layer
This will contains Enums , Exception classes , Setting classes , and Utilities will be used in above layers .

### Data Layer
This layer contains Entities , Database config , Migrations and DataProviders (Service layer uses DataProviders for getting proper data) .

### Service Layer
Service layer contains all application services such as Domain services (Business logic) and other services like Jwt , Email , Sms and etc .

### Presentation Layer (Backend)

Api :

Api endpoints , Middlewares , FilterActions , Service Registrations are placed in this layer.
In addition, this layer depends on service layer.

## Frontend Overview
UserPanel and AdminPanel of this template uses [Admin_LTE](https://adminlte.io/) in UserPanel and AdminPanel and you can change and modify it.

See the [docs](https://adminlte.io/docs/2.4/installation#) .


Below image demonstrates the essence of the frontend and shows how it works :
![Frontend_Overview](https://user-images.githubusercontent.com/39926422/121818798-97e1f880-cc9e-11eb-944f-d20df0853c18.png)

### Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Lints and fixes files
```
npm run lint
```


## References

EnumExtensions , IdentitySetting extension , ApiResponse ,Exceptions and Jwt structure

are inspired by this repository
https://github.com/dotnetzoom/AspNetCore-WebApi-Course
