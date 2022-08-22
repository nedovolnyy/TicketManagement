# Task-4

## How many items from requirements was completed?
*  all

## Possible Drawbacks/Concerns (What should reviewers look out for?)
* 

## Testing Notes (How do we know this works & doesn't break other things)
* Created automated db creation for integration tests.

## Structure
* [ThirdPartyEventEditor](/src/ThirdPartyEventEditor/) - Third-party event editor(.NET Framework)
* [WebUI](/src/TicketManagement.WebUI/) - Constains ASP.NET Core WebApp project.
* [UserAPI](/src/TicketManagement.UserAPI/) - Constains ASP.NET Core WebAPI project.
* [EventManagementAPI](/src/TicketManagement.EventManagementAPI/) - Constains ASP.NET Core WebAPI project.
* [BusinessLogic Layer](/src/TicketManagement.BusinessLogic/) - Constains services and validations.
* [Common](src/TicketManagement.Common) - Contains entity classes and validation of exception class.
* [DataAccess Layer](src/TicketManagement.DataAccess/) - Contains repository for each entity.
* [Database](src/TicketManagement.Database/) - Project database.
* [Unit Tests](test/TicketManagement.UnitTests/) for BusinessLogic Layer.
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
7. Ok => Run

## Steps how to check
Deployment of the database for tests is automatic.

# Credentials
For login with Indentity:

Email: admin@admin.com
Password: admin


![Jokes Card](https://readme-jokes.vercel.app/api)