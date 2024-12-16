using ParkingLot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Services
{
    public interface IParkingLotService
    {
        public int IsSlotAvailable(VehicleType VehicleType);

        public bool IsValidVehicleNumber(string VehicleNumber);

        public ParkingTicket ParkVehicle(int SlotNumber, string VehicleNumber, VehicleType VehicleType);

        public ParkingTicket UnParkVehicle(string TicketId);

        public Dictionary<VehicleType, int[]> GetOccupancyDetails();

        public void Initialize(int twoWheelerSlot, int fourWheelerSlot, int heavyVehicles);
    }
}
