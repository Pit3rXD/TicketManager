using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace TicketManager
{
    internal class TicketProcessor
    {
        private List<ServiceTicket> _tickets = new List<ServiceTicket>();
        //private List<ServiceTicket> _closedTickets = new List<ServiceTicket>();
        private int _nextId = 1;
        private readonly TicketStorage _storage;
        private readonly string _filePath;
        public TicketProcessor(TicketStorage storage, string filePath)
        {
            _storage = storage;
            _filePath = filePath;
            _tickets = _storage.LoadTicketFromFile(filePath);

            if (_tickets.Any())
            {
                int maxId = 0;

                foreach (var ticket in _tickets)
                {
                    if (ticket.Id > maxId)
                        maxId = ticket.Id;
                }
                _nextId = maxId + 1;
            }
        }
        private bool IsFinalStatus(TicketStatus status)
        {
            return status == TicketStatus.Completed;
        }
        public void AddTicket(string description, string reporter, Priority priority)
        {
            var ticket = new ServiceTicket(_nextId, description, reporter, priority);
            _tickets.Add(ticket);
            _nextId++;
            _storage.SaveTicketToFile(_tickets, _filePath);
        }
        public List<ServiceTicket> GetAllTickets()
        {
            return _storage.LoadTicketFromFile(_filePath);
        }
        public List<ServiceTicket> GetOpenTickets()
        {
            List<ServiceTicket> openTickets = new List<ServiceTicket>();

            foreach (var ticket in _tickets)
            {
                if (ticket.Status == TicketStatus.New || ticket.Status == TicketStatus.InProgress)
                {
                    openTickets.Add(ticket);
                }
            }
            return openTickets;
        }
        public List<ServiceTicket> GetClosedTickets()
        {
            string filePath = "closedTickets.json";
            TicketStorage storage = new TicketStorage();
            var ClosedTickets = storage.LoadTicketFromFile(_filePath);

            return storage.LoadTicketFromFile(filePath);
        }
        public bool ChangeTicketStatus(int ticketId, TicketStatus newStatus)
        {
            for (int i = _tickets.Count - 1; i >= 0; i--)
            {
                if (_tickets[i].Id == ticketId)
                {
                    _tickets[i].Status = newStatus;
                    Console.WriteLine($"The status of the ticket with ID {ticketId} has been changed to {newStatus}");

                    if (IsFinalStatus(newStatus))
                    {
                        //_closedTickets.Add(_tickets[i]);
                        //_tickets.RemoveAt(i);
                        string closedFilePath = "closedTickets.json";
                        _storage.SaveTicketToFile(_tickets, closedFilePath);
                    }
                    _storage.SaveTicketToFile(_tickets, _filePath);
                    return true;
                }
            }
            Console.WriteLine($"No ticket found with ID {ticketId}.");
            return false;
        }
        public List<ServiceTicket> RemoveTicket(int ticketId)
        {
            List<ServiceTicket> removedTickets = new List<ServiceTicket>();

            for (int i = _tickets.Count - 1; i >= 0; i--)
            {
                if (_tickets[i].Id == ticketId)
                {
                    removedTickets.Add(_tickets[i]);
                    _tickets.RemoveAt(i);
                    break;
                }
            }
            _storage.SaveTicketToFile(_tickets, _filePath);
            return removedTickets;
        }
        public bool TicketExists(int ticketId)
        {
            foreach (var ticket in _tickets)
            {
                if (ticket.Id == ticketId)
                {
                    return true;
                }
            }
           
            return false;
        }
    }
}
