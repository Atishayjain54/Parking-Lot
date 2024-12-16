using System.Collections.Generic;
using ParkingLot.Models;

namespace ParkingLot.Repositories
{
    public class ParkingTicketRepository : IParkingTicketRepository
    {
        private static List<ParkingTicket> ParkingTickets = new List<ParkingTicket>();

        public void AddTicket(ParkingTicket parkingTicket)
        {
            ParkingTickets.Add(parkingTicket);
        }
        public List<ParkingTicket> GetAll()
        {
            return ParkingTickets;
        }
        public ParkingTicket GetTicketById(string ticketId)
        {
            throw new NotImplementedException();
        }

        public void UpdateTicket(ParkingTicket parkingTicket)
        {
            var tickets = GetAll();
            var existingTicket = tickets.FirstOrDefault(t => t.TicketId == parkingTicket.TicketId);
            if (existingTicket != null)
            {
                existingTicket.OutTime = parkingTicket.OutTime;
                existingTicket.ParkingFee = parkingTicket.ParkingFee;
            }
        }
    }
}
