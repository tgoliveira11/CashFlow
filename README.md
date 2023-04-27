# CashFlow


A merchant needs to control their daily cash flow with entries (debits and credits) and also requires a report that provides the daily consolidated balance.

CashFlow/
│
├── Domain/
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
├── Infrastructure/
│   ├── Data/
│   │   └── CashFlowDbContext.cs
│   │
│   ├── Repositories/
│   │   └── EntryRepository.cs
│   │
│   └── Services/
│       └── ReportService.cs
│
└── Presentation/
    └── ... (Controllers, Views, etc.)


Clients can interact with the API using the following endpoints:

**GET /api/entries**: Get all entries
**GET /api/entries/{id}**: Get an entry by its ID
**POST /api/entries**: Create a new entry
**PUT /api/entries/{id}**: Update an existing entry
**DELETE /api/entries/{id}**: Delete an entry
**GET /api/reports/dailyBalance?startDate={startDate}&endDate={endDate}**: Get a daily balance report for the specified date range

These endpoints should provide the necessary functionality to accomplish the project requirements.