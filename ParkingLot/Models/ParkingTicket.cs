using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using Task1;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace Task1
{
    class ParkingTicket
    {
        public int SlotNumber { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public string TicketId { get; set; }
        public VehicleType VehicleType { get; set; }
        public double ParkingFee { get; set; }
        public ParkingTicket(int SlotNumber, string VehicleNumber, DateTime InTime,string TicketId)
        {
            this.SlotNumber = SlotNumber;
            this.VehicleNumber = VehicleNumber;
            this.InTime = InTime;
            this.TicketId = TicketId;
        }
    }
}