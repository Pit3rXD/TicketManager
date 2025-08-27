using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TicketManager
{
    internal class TicketStorage
    {
        public void SaveTicketToFile(List<ServiceTicket> ticket, string filePath)
        {
            string updateJson = JsonSerializer.Serialize(ticket, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, updateJson);
        }
        public List<ServiceTicket> LoadTicketFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<ServiceTicket>();
            }
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<ServiceTicket>>(json) ?? new List<ServiceTicket>();
        }
    }
}
