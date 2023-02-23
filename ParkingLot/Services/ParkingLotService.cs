using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task1
{
    class ParkingLotService
    {
        public ParkingSlot[] ParkingSlots;

        public List<ParkingTicket> Tickets = new List<ParkingTicket>();

        public Dictionary<VehicleType, int> VehicleSlots = new Dictionary<VehicleType, int>();
        public Dictionary<VehicleType, int> OccupiedSlots = new Dictionary<VehicleType, int>();

        public ParkingLotService(int twoWheelerSlot, int fourWheelerSlot, int heavyVehicles)
        {
            VehicleSlots.Add(VehicleType.TwoWheeler, twoWheelerSlot);
            VehicleSlots.Add(VehicleType.FourWheeler, fourWheelerSlot);
            VehicleSlots.Add(VehicleType.HeavyVehicle, heavyVehicles);

            OccupiedSlots.Add(VehicleType.TwoWheeler, 0);
            OccupiedSlots.Add(VehicleType.FourWheeler, 0);
            OccupiedSlots.Add(VehicleType.HeavyVehicle, 0);


        ParkingSlots = new ParkingSlot[twoWheelerSlot + fourWheelerSlot + heavyVehicles];

            for (int i = 0; i < VehicleSlots[VehicleType.TwoWheeler]; i++)
            {
                ParkingSlot parkingSlot = new ParkingSlot(i + 1, VehicleType.TwoWheeler, false);
                ParkingSlots[i] = parkingSlot;
            }
            for (int i = VehicleSlots[VehicleType.TwoWheeler]; i < VehicleSlots[VehicleType.TwoWheeler] + VehicleSlots[VehicleType.FourWheeler]; i++)
            {
                ParkingSlot parkingSlot = new ParkingSlot(i + 1, VehicleType.FourWheeler, false);
                ParkingSlots[i] = parkingSlot;
            }
            for (int i = VehicleSlots[VehicleType.TwoWheeler] + VehicleSlots[VehicleType.FourWheeler]; i < VehicleSlots[VehicleType.TwoWheeler] + VehicleSlots[VehicleType.FourWheeler] + VehicleSlots[VehicleType.HeavyVehicle]; i++)
            {
                ParkingSlot parkingSlot = new ParkingSlot(i + 1, VehicleType.HeavyVehicle, false);
                ParkingSlots[i] = parkingSlot;
            }
        }
        public int IsSlotAvailable(VehicleType VehicleType)
        {
            foreach (ParkingSlot slot in ParkingSlots)
            {
                if (slot.VehicleType == VehicleType && !slot.IsOccupied)
                {
                    return slot.SlotNumber;
                }
            }
            return -1;
        }
        public bool IsValidVehicleNumber(string VehicleNumber)
        {
            Regex regex = new Regex(@"^[a-zA-Z]{2}\d{8}$");
            return regex.IsMatch(VehicleNumber);
        }
        public ParkingTicket ParkingVehicle(int SlotNumber, string VehicleNumber, VehicleType VehicleType)
        {
            if (VehicleType == VehicleType.TwoWheeler)
            {
                OccupiedSlots[VehicleType.TwoWheeler]++;
            }

            else if (VehicleType == VehicleType.FourWheeler)
            {
                OccupiedSlots[VehicleType.FourWheeler]++;
            }

            else if (VehicleType == VehicleType.HeavyVehicle)
            {
                OccupiedSlots[VehicleType.HeavyVehicle]++;
            }

            ParkingTicket ticket = GenerateTicket(SlotNumber, VehicleNumber, VehicleType);
            
            ParkingSlots[SlotNumber - 1].VehicleNumber = VehicleNumber;
            ParkingSlots[SlotNumber - 1].IsOccupied = true;
            ticket.VehicleType= VehicleType;
            Tickets.Add(ticket);
            return ticket;
        }
        public ParkingTicket GenerateTicket(int SlotNumber, string VehicleNumber, VehicleType VehicleType)
        {
            DateTime CurrentTime = DateTime.Now;
            string TicketId = "";

            if (VehicleType == VehicleType.TwoWheeler)
            {
                TicketId = "TW" + VehicleNumber.Substring(0, 4) + CurrentTime.ToString("yyyyMMddHHmmss");
            }
            else if (VehicleType == VehicleType.FourWheeler)
            {
                TicketId = "FW" + VehicleNumber.Substring(0, 4) + CurrentTime.ToString("yyyyMMddHHmmss");
            }
            else
            {
                TicketId = "HV" + VehicleNumber.Substring(0, 4) + CurrentTime.ToString("yyyyMMddHHmmss");
            }
            ParkingTicket ticket = new ParkingTicket(SlotNumber, VehicleNumber, DateTime.Now, TicketId);
            return ticket;
        }
        public ParkingTicket UnParkVehicle(string TicketId)
        {
            var ticket = Tickets.FirstOrDefault(t => t.TicketId == TicketId);

            if (ticket != null)
            {
                var slot = ticket.SlotNumber;

                switch (ParkingSlots[slot - 1].VehicleType)
                {
                    case VehicleType.TwoWheeler:
                        OccupiedSlots[VehicleType.TwoWheeler]--;
                        break;
                    case VehicleType.FourWheeler:
                        OccupiedSlots[VehicleType.FourWheeler]--;
                        break;
                    case VehicleType.HeavyVehicle:
                        OccupiedSlots[VehicleType.HeavyVehicle]--;
                        break;
                }
                ParkingSlots[slot - 1].IsOccupied = false;
                ParkingFee(ticket);

                return ticket;
            }
            return null;
        }
        public double ParkingFee(ParkingTicket Ticket)
{
    Ticket.OutTime = DateTime.Now;

    double parkingRate = 0;
    double parkingDuration = (Ticket.OutTime - Ticket.InTime).TotalMinutes;
            switch (Ticket.VehicleType)
            {
                case VehicleType.TwoWheeler:
                    parkingRate = 0.5;
                    break;

                case VehicleType.FourWheeler:
                    parkingRate = 1.0;
                    break;

                case VehicleType.HeavyVehicle:
                    parkingRate = 2.0;
                    break;
                default:
                    break;
            }
    double parkingFee = parkingRate * parkingDuration;

    return parkingFee;
}
    }
}

