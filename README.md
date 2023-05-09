# CashFlow

CashFlow is a .NET Core 6.0 application that helps merchants manage and control their daily cash flow with debits, credits, and consolidated daily balance reports.

## Project Structure

The project is organized into the following components:

- CashFlow.Web: An ASP.NET Core MVC frontend that consumes the CashFlow.Api to provide a user interface.
- CashFlow.Api: A RESTful Web API layer that exposes the application's functionality.
- CashFlow.Domain: Contains the domain models and interfaces for repositories and services.
- CashFlow.Infrastructure.Services: Handles business logic and service operations.
- CashFlow.Infrastructure.Database: Handles database operations and implements the repository interfaces using Entity Framework Core.
- CashFlow.UnitTests: Contains unit tests for various components of the application.

## Prerequisites

- .NET Core 6.0 SDK
- SQL Server (or another supported database system)

## Architectural Diagram

                                         +----------------+
                                         |      User      |
                                         +----------------+
                                                 |
                                                 v
                                    +--------------------------+
                                    |   CashFlow.Web (MVC)     |
                                    +--------------------------+
                                                 |
                                                 v
                                    +--------------------------+
                                    |     CashFlow.Api (API)   |
                                    +--------------------------+
                                                 |
                                                 v
    +--------------------------+    +--------------------------+    +--------------------------+
    | CashFlow.Domain (Domain) |<-->| CashFlow.Infrastructure  |<-->|    Database (EF Core)    |
    +--------------------------+    +--------------------------+    +--------------------------+
                                                 |
                                                 v
                                    +--------------------------+
                                    |   CashFlow.UnitTests     |
                                    +--------------------------+



## Installation

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/CashFlow.git
   ```
2. Navigate to the root folder of the project:
   ```
   cd CashFlow
   ```
3. Set up the database connection string:

   - Open `appsettings.json` in the `CashFlow.Api` project.
   - Modify the `DefaultConnection` in the `ConnectionStrings` section to point to your database system.

   Example for SQL Server:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=CashFlowDb;User Id=your_user;Password=your_password;"
   }
   ```

4. Apply the database migrations:

   - Open a terminal or command prompt in the `CashFlow.Api` project directory.
   - Run the following command:

     ```
     dotnet ef database update
     ```

   This will create the required tables and schema in your database.

5. Build the solution:

   ```
   dotnet build
   ```

6. Run the CashFlow.Api project:

   ```
   dotnet run --project CashFlow.Api
   ```

   The API will start running on `http://localhost:5000` or `https://localhost:5001`.

7. Run the CashFlow.Web project:

   ```
   dotnet run --project CashFlow.Web
   ```

   The frontend will start running on `http://localhost:5002` or `https://localhost:5003`.

## Usage

Once both the CashFlow.Api and CashFlow.Web projects are running, you can use the web application to manage your cash flow by adding, editing, and deleting entries, as well as viewing daily balance reports.

---

You can customize this README file to include more information about your project, such as a detailed overview of its features, contribution guidelines, and other necessary information that users or developers might need.


## Endpoints

Clients can interact with the API using the following endpoints:

**GET /api/entries**: Get all entries

**GET /api/entries/{id}**: Get an entry by its ID

**POST /api/entries**: Create a new entry

**PUT /api/entries/{id}**: Update an existing entry

**DELETE /api/entries/{id}**: Delete an entry

**GET /api/reports/dailyBalance?startDate={startDate}&endDate={endDate}**: Get a daily balance report for the specified date range
 
## Images

![Swagger](/img/Swagger.png "Swagger")
![Entries](/img/Entries-Web.png "Entries")
![Report](/img/Report-web.png "Report")