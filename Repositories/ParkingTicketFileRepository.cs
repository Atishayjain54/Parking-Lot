using Newtonsoft.Json;
using ParkingLot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace ParkingLot.Repositories
{
    public class ParkingTicketFileRepository : IParkingTicketRepository
    {
        private static string _filePath = @"C:\workspace\PT.txt.txt";

        //public ParkingTicketFileRepository(string filePath)
        //{
        //    _filePath = filePath;
        //}

        public void AddTicket(ParkingTicket parkingTicket)
        {
            var tickets = GetAll();
            if (tickets == null)
            {
                tickets = new List<ParkingTicket>();
            }
           
            tickets.Add(parkingTicket);

            var serializedTickets = JsonConvert.SerializeObject(tickets, Formatting.Indented);
            File.WriteAllText(_filePath, serializedTickets);
        }

        public List<ParkingTicket> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<ParkingTicket>();
            }

            var serializedTickets = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<ParkingTicket>>(serializedTickets);
        }

        public ParkingTicket GetTicketById(string ticketId)
        {
            var tickets = GetAll();
            return tickets.FirstOrDefault(t => t.TicketId == ticketId);
        }

        public void UpdateTicket(ParkingTicket parkingTicket)
        {
            var tickets = GetAll();
            var existingTicket = tickets.FirstOrDefault(t => t.TicketId == parkingTicket.TicketId);
            if (existingTicket != null)
            {
                existingTicket.OutTime = parkingTicket.OutTime;
                existingTicket.ParkingFee= parkingTicket.ParkingFee;

                var serializedTickets = JsonConvert.SerializeObject(tickets, Formatting.Indented);
                File.WriteAllText(_filePath, serializedTickets);
            }
        }

    }
}
