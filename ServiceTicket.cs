using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManager
{
    internal class ServiceTicket
    {
        public int Id {  get; set; }
        public string Description { get; set; }
        public string Reporter { get; set; }
        public Priority Priority { get; set; }
        public TicketStatus Status { get; set;}
        public DateTime ReportedAt { get; set; }
        public ServiceTicket(int id, string description, string reporter, Priority priority)
        {
            if(string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException(nameof(description), "Description cannot be empty.");
            }
            
            if(string.IsNullOrWhiteSpace(reporter))
            {
                throw new ArgumentNullException(nameof(reporter), "Reporter cannot be empty.");
            }

            this.Id = id;
            this.Description = description;
            this.Reporter = reporter;
            this.Priority = priority;
            this.Status = TicketStatus.New;
            this.ReportedAt = DateTime.Now;
        }
        public override string ToString()
        {
            return $"ID: {Id}, Description: {Description}, Reporter: {Reporter}, Priority: {Priority}, Status: {Status}, Reported At: {ReportedAt}.";
        }
    }
    enum Priority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }
    enum TicketStatus
    {
        New = 0,
        InProgress = 1,
        Completed = 2
    }
}
