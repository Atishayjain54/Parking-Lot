using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;

namespace Task1
{

    class ParkingSlot
    {

        public int SlotNumber { get; set; }
        public string VehicleNumber { get; set; }
        public bool IsOccupied { get; set; }
        public VehicleType VehicleType { get; set; }

        public ParkingSlot(int slotNumber, VehicleType vehicleType , bool isOccupied)
        {
            SlotNumber = slotNumber;
            VehicleType = vehicleType;
            IsOccupied = isOccupied;
           
        }
    }





}