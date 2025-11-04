# Ticketing System

A simple ticketing system built with ASP.NET Core, Entity Framework Core, and Blazor WebAssembly.
It allows users to create, assign, and manage tickets with different statuses.

This project was developed as a coding assessment for MDK Legal.

---

## Table of Contents

- [Features](#features)  
- [Technologies](#technologies)  
- [Getting Started](#getting-started)  
  - [Prerequisites](#prerequisites)  
  - [Installation](#installation)  
- [Database](#database)  
- [API](#api)  
- [FrontEnd](#frontend)  
- [Testing](#testing)  


---

## Features

- Create and manage tickets  
- Assign tickets to users  
- Track ticket status: Open, In Progress, Closed  
- Create Tickets
- Update Tickets
- Assign Tickets to Users

---

## Technologies

- ASP.NET Core  
- Entity Framework Core  
- SQL Server (or your preferred database)  
- NUnit unit testing
- Blazor Web Assembly front end
- MudBlazor component library

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)  
- SQL Server / LocalDB  
- Optional: Visual Studio 2022 / VS Code  





### Installation

Currently, the ticketing system is not deployed to Azure. It requires a locally installed SQL Server instance. Docker support is planned but not yet implemented.

Update the DefaultConnectionString in appSettings.json to point to a local clean database in order to run.  Once this is properly set navigate to ./Ticketing and run the following command

```bash
dotnet ef database update
```

## Database, ORM, and Data Models

SQL Server and Entity Framework Core are the basis of the data model for this project.

The project features two main data objects Ticket and User.  As described in the requirements Ticket features an Id, Title, Description, UpdatedAt and CreatedAt timestamps, and an assignedUser which is linked to the User Entity.

Additionally a ticketNo exists as an int for identity purposes and allowing the GUIDs to remain hidden in the backend while still giving an easy ticket identifier to the item.  String was considered for this and if a more robust ticketing system was built would have been used allowing tickets to be assigned to specific projects.

Status is represented by an enum in addition to the statuses in the requirements an ERROR status was added in the case that an exception gets thrown somewhere along the way.

Since there is no update or create functionality for Users Seed data was created for Users.

Seed ticket data was created for the purpose of integration testing.  In an actual application both of these would be removed.

## API

The Ticketing project is an ASP.NET Core Web API it features the following endpoints


| Url    | ACTION   | Notes                  |
|-------------|--------|------------------------------|
| /Tickets    | GET | Gets all tickets by default accepts status and user as query params that will filter down     |
| /Tickets/{id}    | GET | Gets a specific ticket.  Id can be a GUID or the TicketNo   |
| /Tickets  | POST | Create a ticket      |
| /Tickets/{id}        | POST    | Update an existing Ticket id can be a GUID or the TicketNo   |
| /Users        | Get    | Gets all users   |


Some notes on the methods.

Tickets are always created with OPEN status

POST is used here for the update method instead of PUT or PATCH because of personal preferrence.  It could easily be changed to PUT or PATCH.

If a field is not included in the Update method that field will not be set to null instead it will be left unchanged.

The User get exists purely to populate fields on the Front End to allow tickets to be reassigned.

CreatedAt and UpdatedAt cannot be set via api and instead are set at creation and on update.

-Create Ticket Body

```json
{
  "Title": "Cannot login to account",
  "Description": "User reports that login fails with 'invalid credentials' error.",
  "AssignedUserId": "d290f1ee-6c54-4b01-90e6-d701748f0851"
}
```

Update Ticket Body
```json
{
  "Title": "Cannot login to account",
  "Description": "User reports that login fails with 'invalid credentials' error.",
  "AssignedUserId": "d290f1ee-6c54-4b01-90e6-d701748f0851",
  "TicketStatus": 1
}
```

Both Create and Update return the ticket that was just created/updated on a successfull call.  This allows the FrontEnd to update fields and page type after submitting a POST action without having to call another GET.
## Front End

The front end application located at ./TicketingFE is a Web Assembly BLazor project written using the MudBlazor library for an easy set of UI components.

It features two pages.  A home page where you can view a table of tickets and filter by status or user and a Create/Update/View Ticket page where tickets can be viewed or edited.

Future enhancments include implementing pagination on the client side table.  Implementing column sort and a search bar that will further filter tickets.

The ticket page updates based on if an id is passed into the page if so it will call the API using that id set IsEditMode to true and populate the ticket details including UpdatedAt and CreatedAt.

The button at the bottom changes based on the mode.  As do several other fields (Status cannot be set on a new ticket)

When the ticket is created the page will update itself into Edit mode and allow further updates to the created ticket.

## Testing

For testing Nunit, Moq, and Playwright are installed.  Unit Tests were focused on the service/repository layer.  Moq was set up for any needed mocking but not actually used in favor of the InMemoryDatabase for EFCore.

Service Level unit tests were created to try and maximize code coverage.

A further enhancement would be to create unit tests around controller logic.

For integration testing Playwright was used.  Both ./Ticketing and ./TicketingFE must be running using dotnet run for the Integration tests to be successful.  At the moment the integration tests do not run any update or create functionality.  This is mostly due to repeatability particularly for create.  Since No Delete functionality exists having an integration test that runs create would quickly create a lot of dummy data.

## Next Steps

- Add Docker support for both the API and Blazor frontend.
- Error handling/Logging
- Caching
- Configure environment variables for connection strings and secrets.
- Automate deployment to Azure.
- Implement pagination, column sorting, and search in the frontend ticket table.
- Code warning cleanup.   
- Error/edge case handling.