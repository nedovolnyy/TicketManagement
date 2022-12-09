# TicketManagement

[![TicketManagement](https://github.com/nedovolnyy/TicketManagement/actions/workflows/CICD.yml/badge.svg?event=push)](https://github.com/nedovolnyy/TicketManagement/actions/workflows/CICD.yml)

## Possible Drawbacks/Concerns (What should reviewers look out for?)
	- Skipped tests for ReactJS case, because proxy issue.

## Testing Notes (How do we know this works & doesn't break other things)
* Created automated db creation for integration tests.

## Structure
* [ThirdPartyEventEditor](/src/ThirdPartyEventEditor/) - Third-party event editor(.NET Framework)
* [WebUI](/src/TicketManagement.WebUI/) - Constains ASP.NET Core MVC and SPA(single-page application) WebApp projects.
* [UserAPI](/src/TicketManagement.UserAPI/) - Constains ASP.NET Core WebAPI project.
* [EventManagementAPI](/src/TicketManagement.EventManagementAPI/) - Constains ASP.NET Core WebAPI project.
* [Common](src/TicketManagement.Common) - Contains entity classes and validation of exception class.
* [DataAccess Layer](src/TicketManagement.DataAccess/) - Contains repository for each entity.
* [Database](src/TicketManagement.Database/) - Project database.
* [Unit Tests](test/TicketManagement.UnitTests/) Unit tests for business logic's of Controllers.
* [Integration Tests](test/TicketManagement.IntegrationTests/) for DataAccess Layer.

# How to build and run the whole solution
1. Need to create and deploy(dacpac, MSSS or any) a [Database](src/TicketManagement.Database/) from this solution first
2. Install Node.js from https://nodejs.org/, and then restart your command prompt or IDE.
3. In Solution Explorer, select the solution (the top node)
4. Choose the solution node's context (right-click) menu and then choose Properties. The Solution Property Pages dialog box appears
5. Solution Property Pages
6. Expand the Common Properties node, and choose Startup Project
7. Choose the Multiple Startup Projects option and set this projects:
    - [X] [WebUI](/src/TicketManagement.WebUI/) :7114 /// :7115 - port for ReactJS application
    - [X] [UserAPI](/src/TicketManagement.UserAPI/) :5004
    - [X] [EventManagementAPI](/src/TicketManagement.EventManagementAPI/) :5003
8. To switch WebUI, change "UseReact" flag([appsettings.Develop.json](/src/TicketManagement.WebUI/appsettings.Develop.json)):
	- `true` to run ReactJS
	- `false` to run ASP.NET MVC.
9. Ok => Run

## Steps how to check
Deployment of the database for tests is automatic.
To specify another test database, enter its path in [testconfig.json](test/TicketManagement.IntegrationTests/testconfig.json):
```
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TestTicketManagement.Database;Integrated Security=True;Encrypt=False"
  },
  "Database": {
    "DefaultDatabaseName": "TestTicketManagement.Database",
    "DefaultDatabaseFileName": "TestTicketManagement.Database.dacpac"
  }
```
It is possible to specify absolute and relative (from the compilation folder) paths.

If need, for projects [UserAPI](/src/TicketManagement.UserAPI/) and [EventManagementAPI](/src/TicketManagement.EventManagementAPI/):
```
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TicketManagement.Database;Integrated Security=True"
  },
```
In configuration files: [UserAPI/appsettings.?.json](/src/TicketManagement.UserAPI/appsettings.Development.json) and [EventManagementAPI/appsettings.?.json](/src/TicketManagement.EventManagementAPI/appsettings.Development.json)

# Credentials
For login with Indentity:

Email: admin@admin.com
Password: admin


![Jokes Card](https://readme-jokes.vercel.app/api)
