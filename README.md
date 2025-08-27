TicketManager

TicketManager is a simple service ticket management system written in C#. The project was created as a way to practice working with JSON files, classes, enums, and basic business logic. It's a console application that doesn't require a database — everything is stored locally.

Features

- Add new tickets with priority and reporter information
- Change ticket status (e.g., to “In Progress” or “Completed”)
- View all tickets, only open tickets, or only closed ones
- Remove tickets by ID
- Save and load data from JSON files
- Automatically assign unique IDs to new tickets

Project Structure

The project consists of several classes:

- `ServiceTicket` – represents a single ticket (ID, description, reporter, priority, status, report date)
- `Priority` – enum with priorities: Low, Medium, High
- `TicketStatus` – enum with statuses: New, InProgress, Completed
- `TicketStorage` – handles saving and loading tickets from JSON files
- `TicketProcessor` – main application logic: adding, filtering, updating status, removing
- `Program.cs` – entry point of the application (not shown here but included in the project)
  
Data Storage

- All tickets are saved to a JSON file (e.g., `tickets.json`)
- Completed tickets are also saved separately to `closedTickets.json`

How to Run

1. Clone the repository:
   https://github.com/your-username/TicketManager.git
   - Open the project in Visual Studio or any C#-compatible IDE
2. Build and run the application
3. Use the console interface to add and manage tickets

Created as a learning exercise and a base for future improvements.
