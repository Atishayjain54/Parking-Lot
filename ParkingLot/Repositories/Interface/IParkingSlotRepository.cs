using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingLot.Models;

namespace ParkingLot.Repositories
{
    public interface IParkingSlotRepository
    {
        void AddSlot(ParkingSlot parkingSlot);
        void RemoveSlot(ParkingSlot parkingSlot);
        ParkingSlot GetSlotBySlotNumber(int slotNumber);
        List<ParkingSlot> GetAll();

        void UpdateSlot(int slotNumber, ParkingSlot slot);

    }

}
