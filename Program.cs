using System.Diagnostics.Contracts;
using TicketManager;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        var storage = new TicketStorage();
        var processor = new TicketProcessor(storage, "ticket.json");
        bool exit = true;
        while (exit)
        {
            Console.WriteLine();
            Console.WriteLine("1. Add ticket");
            Console.WriteLine("2. Show All Tickets");
            Console.WriteLine("3. Show Open Tickets");
            Console.WriteLine("4. Show Closed Tickets");
            Console.WriteLine("5. Change Ticket Status");
            Console.WriteLine("6. Remove Ticket");
            Console.WriteLine("0. Exit");

            Console.WriteLine("select option: ");
            var choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    try
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("Enter ticket description: ");
                        string description = Console.ReadLine();

                        Console.WriteLine();
                        Console.WriteLine("Enter reporter name: ");
                        {
                            string reporter = Console.ReadLine();

                            Console.WriteLine();
                            Console.WriteLine("Choose priority: ");
                            Console.WriteLine("1 - Low");
                            Console.WriteLine("2 - Medium");
                            Console.WriteLine("3 - High");
                            string priorityInput = Console.ReadLine();

                            if (!int.TryParse(priorityInput, out int priorityNumber) || priorityNumber < 1 || priorityNumber > 3)
                            {
                                Console.WriteLine("Invalid priority number.");
                                break;
                            }
                            Priority priority = (Priority)(priorityNumber - 1);

                            processor.AddTicket(description, reporter, priority);
                            Console.WriteLine();
                            Console.WriteLine("Ticket added successfully. \n");
                        }
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Missing input: {ex.Message}");
                        Console.ResetColor();
                    }
                    break;

                case "2":
                    List<ServiceTicket> services = processor.GetAllTickets();

                    if (services.Count == 0)
                    {
                        Console.WriteLine("No tickets available");
                        Console.WriteLine();
                    }
                    else
                    {
                        foreach (ServiceTicket ticket in services)
                        {
                            Console.WriteLine(ticket);
                        }
                    }
                    break;

                case "3":
                    List<ServiceTicket> openTickets = processor.GetOpenTickets();

                    if (openTickets.Count == 0)
                    {
                        Console.WriteLine("No tickets available.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Open tickets: ");
                        foreach (ServiceTicket ticket in openTickets)
                        {
                            Console.WriteLine(ticket);
                        }
                        Console.WriteLine();
                    }
                    break;

                case "4":
                    List<ServiceTicket> closedTickets = processor.GetClosedTickets();

                    if (closedTickets.Count == 0)
                    {
                        Console.WriteLine("No tickets available.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Closed tickets: ");
                        foreach (ServiceTicket ticket in closedTickets)
                        {
                            Console.WriteLine(ticket);
                        }
                        Console.WriteLine();
                    }
                    break;

                case "5":
                    List<ServiceTicket> openTicketsToShow = processor.GetOpenTickets();

                    if (openTicketsToShow.Count == 0)
                    {
                        Console.WriteLine("No tickets available.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Open tickets: ");
                        foreach (ServiceTicket ticket in openTicketsToShow)
                        {
                            Console.WriteLine(ticket);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("Change status");
                    Console.WriteLine("Enter ID");
                    string idInput = Console.ReadLine();

                    if (!int.TryParse(idInput, out int ticketId))
                    {
                        Console.WriteLine("Invalid ID format.");
                        break;
                    }
                    if (!processor.TicketExists(ticketId))
                    {
                        Console.WriteLine($"Ticket with ID {ticketId} does not exist or is closed.");
                        break;
                    }
                    Console.WriteLine("Select new status:");
                    Console.WriteLine("1 - New");
                    Console.WriteLine("2 - In Progress");
                    Console.WriteLine("3 - Completed");
                    string statusNumberInput = Console.ReadLine();

                    if (!int.TryParse(statusNumberInput, out int statusNumber) || statusNumber < 1 || statusNumber > 3)
                    {
                        Console.WriteLine("Invalid status number.");
                        break;
                    }
                    TicketStatus newStatus = (TicketStatus)(statusNumber - 1);
                    processor.ChangeTicketStatus(ticketId, newStatus);
                    break;

                case "6":
                    List<ServiceTicket> showTickets = processor.GetOpenTickets();

                    if (showTickets.Count == 0)
                    {
                        Console.WriteLine("No tickets available.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Open tickets: ");
                        foreach (ServiceTicket ticket in showTickets)
                        {
                            Console.WriteLine(ticket);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("Remove ticket.");
                    Console.WriteLine("Enter ID");
                    string removeIdInput = Console.ReadLine();

                    if (!int.TryParse(removeIdInput, out int removeId))
                    {
                        Console.WriteLine("Invalid ID format.");
                        break;
                    }
                    if (!processor.TicketExists(removeId))
                    {
                        Console.WriteLine($"Ticket with ID {removeId} does not exist or is closed.");
                        break;
                    }

                    Console.WriteLine($"Are you sure? Press (y) or (n).");
                    string yesOrNo = Console.ReadLine();

                    if (yesOrNo.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        processor.RemoveTicket(removeId);
                        Console.WriteLine($"Ticket {removeId} was removed.");
                    }
                    else if (yesOrNo.Equals("n", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Operation cancelled.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Operation cancelled");
                    }
                    break;

                case "0":
                    Console.WriteLine("Do you want to exit the program?");
                    Console.WriteLine("(y)es or (n)o?");
                    string ask = Console.ReadLine();

                    if (ask.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Are you sure?");
                        string repeat = Console.ReadLine();
                        if (repeat.Equals("y", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Thank you for using the program. Goodbye.");
                            exit = false;
                        }
                    }
                    else if (ask.Equals("n", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Invalid inpuit.");
                    }
                    break;

            }
        }
    }
}