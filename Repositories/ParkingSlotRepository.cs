using ParkingLot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Repositories
{
    public class ParkingSlotRepository : IParkingSlotRepository
    {
        private static List<ParkingSlot> _parkingSlots;

        public ParkingSlotRepository()
        {
            _parkingSlots = new List<ParkingSlot>();
        }
        public void AddSlot(ParkingSlot slot)
        {
            _parkingSlots.Add(slot);
        }
        public void RemoveSlot(ParkingSlot parkingSlot)
        {
            throw new NotImplementedException();
        }
        public ParkingSlot GetSlotBySlotNumber(int slotNumber)
        {
            return _parkingSlots.FirstOrDefault(s => s.SlotNumber == slotNumber);
        }
        public List<ParkingSlot> GetAll()
        {
            return _parkingSlots;
        }
        public void UpdateSlot(int slotNumber, ParkingSlot slot)
        {
            var slots = GetAll();
            var existingSlot = slots.FirstOrDefault(s => s.SlotNumber == slotNumber);
            if (existingSlot != null)
            {
                existingSlot.IsOccupied = slot.IsOccupied;
                existingSlot.VehicleNumber = slot.VehicleNumber;
                existingSlot.VehicleType = slot.VehicleType;
            }
        }
    }
}
