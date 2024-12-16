using Newtonsoft.Json;
using ParkingLot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ParkingLot.Repositories
{
    public class ParkingSlotFileRepository : IParkingSlotRepository
    {
        private readonly string _filePath = @"C:\workspace\PS.txt.txt";

        //public ParkingSlotFileRepository(string filePath)
        //{
        //    _filePath = filePath;
        //}

        public void AddSlot(ParkingSlot parkingSlot)
        {
            var slots = GetAll();
            if (slots == null)
            {
                slots = new List<ParkingSlot>();
            }
            slots.Add(parkingSlot);

            var serializedSlots = JsonConvert.SerializeObject(slots, Formatting.Indented);
            File.WriteAllText(_filePath, serializedSlots);
        }
        public List<ParkingSlot> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<ParkingSlot>();
            }

            var serializedSlots = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<ParkingSlot>>(serializedSlots);
        }

        public ParkingSlot GetSlotBySlotNumber(int slotNumber)
        {
            var slots = GetAll();
            return slots.FirstOrDefault(s => s.SlotNumber == slotNumber);
        }

        public void RemoveSlot(ParkingSlot parkingSlot)
        {
            var slots = GetAll();
            var slotToRemove = slots.FirstOrDefault(s => s.SlotNumber == parkingSlot.SlotNumber);
            if (slotToRemove != null)
            {
                slots.Remove(slotToRemove);

                var serializedSlots = JsonConvert.SerializeObject(slots, Formatting.Indented);
                File.WriteAllText(_filePath, serializedSlots);
            }
        }

        public void UpdateSlot(int slotNumber, ParkingSlot slot)
        {
            var slots = GetAll();
            var existingSlot = slots.FirstOrDefault(s => s.SlotNumber == slotNumber);
            if (existingSlot != null)
            {
                existingSlot.IsOccupied = slot.IsOccupied;
                existingSlot.VehicleNumber= slot.VehicleNumber;
                existingSlot.VehicleType= slot.VehicleType;

                var serializedSlots = JsonConvert.SerializeObject(slots, Formatting.Indented);
                File.WriteAllText(_filePath, serializedSlots);
            }
        }

    }

}
