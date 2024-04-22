# dotnet-interview / TodoApi

[![Open in Coder](https://dev.crunchloop.io/open-in-coder.svg)](https://dev.crunchloop.io/templates/fly-containers/workspace?param.Git%20Repository=git@github.com:crunchloop/dotnet-interview.git)

This is a simple Todo List API built in .NET 7. This project is currently being used for .NET full-stack candidates.

## Build

To build the application:

`dotnet build`

## Run the API

Before running the API, you need to connect the project to your database and build its structure.

To do this:
1. Navigate to TodoApi -> appsettings.json and find the ConnectionStrings -> TodoContext property. Set your connection string to your database there.
2. Open the Package Manager Console and execute the following commands:
   1. Add-Migration [migration_name] (Replace [migration_name] with the desired name for your migration.)
   2. Update-Database

To run the TodoApi in your local environment:

`dotnet run --project TodoApi`

## Test

To run tests:

`dotnet test`

Check integration tests at: (https://github.com/crunchloop/interview-tests)

## Contact

- Martín Fernández (mfernandez@crunchloop.io)

## About Crunchloop

![crunchloop](https://crunchloop.io/logo-blue.png)

We strongly believe in giving back :rocket:. Let's work together [`Get in touch`](https://crunchloop.io/contact).
