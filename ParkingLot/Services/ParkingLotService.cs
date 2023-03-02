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

        public int TwoWheelerSlots { get; set; }
        public int FourWheelerSlots { get; set; }
        public int HeavyVehicleSlots { get; set; }
        public int OccupiedTwoWheelerSlots { get; set;} = 0;
        public int OccupiedFourWheelerSlots { get; set; } = 0;
        public int OccupiedHeavyVehicleSlots { get; set; } = 0;
        public ParkingLotService(int twoWheelerSlot, int fourWheelerSlot, int heavyVehicles)
        {
            TwoWheelerSlots = twoWheelerSlot;
            FourWheelerSlots = fourWheelerSlot;
            HeavyVehicleSlots = heavyVehicles;

            ParkingSlots = new ParkingSlot[twoWheelerSlot + fourWheelerSlot + heavyVehicles];

            for (int i = 0; i < TwoWheelerSlots; i++)
            {
                ParkingSlot parkingSlot = new ParkingSlot(i + 1, VehicleType.TwoWheeler, false);
                ParkingSlots[i] = parkingSlot;
            }
            for (int i = TwoWheelerSlots; i < TwoWheelerSlots + FourWheelerSlots; i++)
            {
                ParkingSlot parkingSlot = new ParkingSlot(i + 1, VehicleType.FourWheeler, false);
                ParkingSlots[i] = parkingSlot;
            }
            for (int i = TwoWheelerSlots + FourWheelerSlots; i < TwoWheelerSlots + FourWheelerSlots + HeavyVehicleSlots; i++)
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
                OccupiedTwoWheelerSlots++;
            }

            else if (VehicleType == VehicleType.FourWheeler)
            {
                OccupiedFourWheelerSlots++;
            }

            else if (VehicleType == VehicleType.HeavyVehicle)
            {
                OccupiedHeavyVehicleSlots++;
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
            string ticketId = "";

            if (VehicleType == VehicleType.TwoWheeler)
            {
                ticketId = "TW" + VehicleNumber.Substring(0, 4) + CurrentTime.ToString("yyyyMMddHHmmss");
            }
            else if (VehicleType == VehicleType.FourWheeler)
            {
                ticketId = "FW" + VehicleNumber.Substring(0, 4) + CurrentTime.ToString("yyyyMMddHHmmss");
            }
            else
            {
                ticketId = "HV" + VehicleNumber.Substring(0, 4) + CurrentTime.ToString("yyyyMMddHHmmss");
            }
            ParkingTicket ticket = new ParkingTicket(SlotNumber, VehicleNumber, DateTime.Now, ticketId);
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
                        OccupiedTwoWheelerSlots--;
                        break;
                    case VehicleType.FourWheeler:
                        OccupiedFourWheelerSlots--;
                        break;
                    case VehicleType.HeavyVehicle:
                        OccupiedHeavyVehicleSlots--;
                        break;
                }
                ParkingSlots[slot - 1].IsOccupied = false;
                ticket.OutTime = DateTime.Now;
                CalculateParkingFee(ticket);

                return ticket;
            }
            return null;
        }
        public void CalculateParkingFee(ParkingTicket Ticket)
        {
           
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

         Ticket.ParkingFee = parkingFee;
        }
        public Dictionary<VehicleType, int[]> GetOccupancyDetails()
        {
            Dictionary<VehicleType, int[]> VehicleSlots = new Dictionary<VehicleType, int[]>()
            {
             {VehicleType.TwoWheeler, new int[] {TwoWheelerSlots,OccupiedTwoWheelerSlots} },
             {VehicleType.FourWheeler, new int[] {FourWheelerSlots,OccupiedFourWheelerSlots} },
             {VehicleType.HeavyVehicle, new int[] {HeavyVehicleSlots,OccupiedHeavyVehicleSlots}}
            };
           return VehicleSlots;
        }
    }
}

