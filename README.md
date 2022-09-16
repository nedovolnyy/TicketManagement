# Task-5

## How many items from requirements was completed?
- [ ] Part of refactoring

## Possible Drawbacks/Concerns (What should reviewers look out for?)
*

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
2. In Solution Explorer, select the solution (the top node)
3. Choose the solution node's context (right-click) menu and then choose Properties. The Solution Property Pages dialog box appears
4. Solution Property Pages
5. Expand the Common Properties node, and choose Startup Project
6. Choose the Multiple Startup Projects option and set this projects:
    - [X] [WebUI](/src/TicketManagement.WebUI/) :7114
    - [X] [UserAPI](/src/TicketManagement.UserAPI/) :5004
    - [X] [EventManagementAPI](/src/TicketManagement.EventManagementAPI/) :5003
7. To switch WebUI, change "UseReact" flag(appsettings.Develop.json):
	- `true` to run ReactJS
	- `false` to run ICC.
8. Ok => Run

## Steps how to check
Deployment of the database for tests is automatic.

# Credentials
For login with Indentity:

Email: admin@admin.com
Password: admin


![Jokes Card](https://readme-jokes.vercel.app/api)