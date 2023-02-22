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
        public int TwoWheelerSlots { get; set; }
        public int FourWheelerSlots { get; set; }
        public int HeavyVehicleSlots { get; set; }

        public int OccupiedTwoWheelerSlots = 0;

        public int OccupiedFourWheelerSlots = 0;

        public int OccupiedHeavyVehicleSlots = 0;

        public ParkingSlot[] ParkingSlots;

        public List<ParkingTicket> Tickets = new List<ParkingTicket>();
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
        public ParkingTicket Parking(int SlotNumber, string VehicleNumber, VehicleType VehicleType)
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

            ParkingTicket ticket = GetTicket(SlotNumber, VehicleNumber, VehicleType);
            
            ParkingSlots[SlotNumber - 1].VehicleNumber = VehicleNumber;
            ParkingSlots[SlotNumber - 1].IsOccupied = true;
            Tickets.Add(ticket);
            return ticket;
        }
        public ParkingTicket GetTicket(int SlotNumber, string VehicleNumber, VehicleType VehicleType)
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
        public ParkingTicket UnPark(string TicketId)
        {
            foreach (ParkingTicket ticket in Tickets)
            {
                if (ticket.TicketId == TicketId)
                {
                    int slot = ticket.SlotNumber;

                    if (ParkingSlots[slot - 1].VehicleType == VehicleType.TwoWheeler)
                    {
                        OccupiedTwoWheelerSlots--;
  
                    }
                    if (ParkingSlots[slot - 1].VehicleType == VehicleType.FourWheeler)
                    {
                        OccupiedFourWheelerSlots--;
                    }
                    if (ParkingSlots[slot - 1].VehicleType == VehicleType.HeavyVehicle)
                    {
                        OccupiedHeavyVehicleSlots--;
                    }
                    ParkingSlots[slot - 1].IsOccupied = false;
                    UpdateTicket(ticket);
                    ParkingFee(ticket);
                  
                    return ticket;
                }

            }
            return null;
        }
        public void UpdateTicket(ParkingTicket Ticket)
        {
            Ticket.OutTime = DateTime.Now;
        }
        public double ParkingFee(ParkingTicket Ticket)
        {
            double parkingRate = 0.05;

            DateTime InTime = Ticket.InTime;

            DateTime OutTime = Ticket.OutTime;

            TimeSpan elapsed = OutTime - InTime;

            double TotalPrice = elapsed.TotalMinutes * parkingRate;
            return TotalPrice;
        }
    }
}
