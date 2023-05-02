# CashFlow

A merchant needs to control their daily cash flow with entries (debits and credits) and also requires a report that provides the daily consolidated balance.

CashFlow/
│
├── Domain/ (CashFlow.Domain)
│   ├── Entities/
│   │   ├── Entry.cs
│   │   ├── DailyBalance.cs
│   │   └── Report.cs
│   │
│   ├── Repositories/
│   │   └── IEntryRepository.cs
│   │
│   └── Services/
│       └── IReportService.cs
│
├── Infrastructure/ (CashFlow.Infrastructure.DataAccess
│   ├── /
│   │   └── CashFlowDbContext.cs
│   │
│   ├── Repositories/
│   │   └── EntryRepository.cs
│   │
│   └── Migrations/
│       └── ReportService.cs
│
├── Services/ (CashFlow.Infrastructure.Services
│   ├── /
│       └── ... (Controllers)
│
├── APIs/ (CashFlow.Api)
│
└── Presentation/ (CashFlow.Web)
    └── ... (Controllers, Views, etc.)


Clients can interact with the API using the following endpoints:

**GET /api/entries**: Get all entries
**GET /api/entries/{id}**: Get an entry by its ID
**POST /api/entries**: Create a new entry
**PUT /api/entries/{id}**: Update an existing entry
**DELETE /api/entries/{id}**: Delete an entry
**GET /api/reports/dailyBalance?startDate={startDate}&endDate={endDate}**: Get a daily balance report for the specified date range

These endpoints should provide the necessary functionality to accomplish the project requirements.
 
To install it, create a database on a SQL Server and ajust the entry on the section ConnectionStrings of appsettings.json file.

![Swagger](/img/Swagger.png "Swagger")
![Entries](/img/Entries-Web.png "Entries")
![Report](/img/Report-Web.png "Report")