using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingLot.Models;

namespace ParkingLot.Repositories
{
    public interface IParkingTicketRepository
    {
        public void AddTicket(ParkingTicket parkingTicket);

        public ParkingTicket GetTicketById(string TicketId);

        public List<ParkingTicket> GetAll();

        public void UpdateTicket(ParkingTicket parkingTicket);
    }

}
