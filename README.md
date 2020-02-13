# NetwrixTechnicalTest

## Overview

ASP.NET MVC project for the Netwrix technical test, which displays Customer and Invoice data.

The applications uses ASP.NET Core 3.1, EF Core, JQuery and Bootstrap. 

## Database

The included demo database is a SQLite database which is accessed using EF Core but this could be swapped for another data source simply by creating a new implementation of the IApplicationDatabaseContext interface and altering the ConfigureServices method in Startup.cs to use the new implementation. The DatabaseContext implementation is set up as a singleton so only one instance of it can be created (See the section on IOC container below).

## Project Structure

Following seperation of concerns principle the project components have been split up into the following folders:

- Controllers: contains all project controllers
- Database: contains the SQLite database and associated DatabaseContext and Interfaces
- Models: contains representations of the data classes
- ServiceLayer: contains the business logic pertaining to the data classes, and the associated interfaces
- ViewModels: contains composite models as required by Views
- Views: contains razor view files

## IOC

The application uses the .NET Core built in IOC container for dependency injection, this is configured in the ConfigureServices method of the Statup.cs file.

## Error Handling

There is an ErrorsController which is designed to handle application errors. The Configure method of the Startup.cs is configured to call the route assigned to this controller. Note that when running in Debug the development page is enabled for more debugging information.


## Unit Tests

The test project uses Xunit and Moq, and has a sample of the unit tests possible when using IOC and mocking.
