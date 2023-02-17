using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
        public int ChechAvailability(VehicleType vehicleType)
        {
            foreach (ParkingSlot slot in ParkingSlots)
            {
                if (slot.VehicleType == vehicleType && !slot.IsOccupied)
                {
                    return slot.SlotNumber;
                }
            }
            return -1;
        }
        public static bool CheckVehicleNumber(string VehickleNumber)
        {
            if (VehickleNumber.Length == 10)
            {
                return true;
            }

            return false;
        }
        public void ParkVehicle(VehicleType VehicleType, string VehicleNumber)
        {
            int SlotNumber = 0;
            if (VehicleType == VehicleType.TwoWheeler)
            { 
                SlotNumber = ChechAvailability(VehicleType.TwoWheeler);
                if (SlotNumber != -1)
                {
                    VehicleNumber = Methods.ParkingVehicle(VehicleNumber, SlotNumber);

                    if (CheckVehicleNumber(VehicleNumber))
                    {

                        ParkingTicket ticket = Methods.GetTicket(SlotNumber, VehicleNumber, VehicleType);
                        Tickets.Add(ticket);
                        ParkingSlots[SlotNumber - 1].VehicleNumber = VehicleNumber;
                        ParkingSlots[SlotNumber - 1].IsOccupied = true;
                        OccupiedTwoWheelerSlots++;
                    }
                    else
                    {
                        Console.WriteLine("Please Enter Valid Vehicle Number");

                    }
                }
                else
                {
                    Console.WriteLine("Slot is Not Available for two Wheeler");
                    Console.WriteLine("\n");
                }
           }
            else if (VehicleType == VehicleType.FourWheeler)
            {
                SlotNumber = ChechAvailability(VehicleType.FourWheeler);
                if (SlotNumber != -1)
                {
                    VehicleNumber = Methods.ParkingVehicle(VehicleNumber, SlotNumber);

                    ParkingTicket ticket = Methods.GetTicket(SlotNumber, VehicleNumber, VehicleType);
                    Tickets.Add(ticket);
                    ParkingSlots[SlotNumber - 1].VehicleNumber = VehicleNumber;
                    ParkingSlots[SlotNumber - 1].IsOccupied = true;
                    OccupiedFourWheelerSlots++;
                }
                else
                {
                    Console.WriteLine("Slot is Not Available for Four Wheeler");
                }
                Console.WriteLine("\n");
            }
            else if (VehicleType == VehicleType.HeavyVehicle)
            {
                SlotNumber = ChechAvailability(VehicleType.HeavyVehicle);
                if (SlotNumber != -1)
                {
                    VehicleNumber = Methods.ParkingVehicle(VehicleNumber, SlotNumber);

                    ParkingTicket ticket = Methods.GetTicket(SlotNumber, VehicleNumber, VehicleType);
                    Tickets.Add(ticket);
                    ParkingSlots[SlotNumber - 1].VehicleNumber = VehicleNumber;
                    ParkingSlots[SlotNumber - 1].IsOccupied = true;
                    OccupiedHeavyVehicleSlots++;
                }
                else
                {
                    Console.WriteLine("Slot is Not Available for Heavy Vehicle");
                }
                Console.WriteLine("\n");
            }
        }
        public void UnParking()
        {
            Console.WriteLine("Please Enter the Ticket ID");
            string TicketId = Console.ReadLine();
            foreach (ParkingTicket ticket in Tickets)
            {
                if (ticket.TicketId == TicketId)
                {
                    int slot = ticket.SlotNumber;

                    if (ParkingSlots[slot - 1].VehicleType == VehicleType.TwoWheeler)
                    {
                        OccupiedTwoWheelerSlots--;
                        ParkingSlots[slot - 1].IsOccupied = false;
                    }
                    if (ParkingSlots[slot - 1].VehicleType == VehicleType.FourWheeler)
                    {
                        OccupiedFourWheelerSlots--;
                        ParkingSlots[slot - 1].IsOccupied = false;
                    }
                    if (ParkingSlots[slot - 1].VehicleType == VehicleType.HeavyVehicle)
                    {
                        OccupiedHeavyVehicleSlots--;
                        ParkingSlots[slot - 1].IsOccupied = false;
                    }
                    Methods.UpdateTicket(ticket);
                    ParkingFee(ticket);
                    return;
                }
            }
            Console.WriteLine("Invalid Ticket ID");
            Console.WriteLine("\n******************");
        }
        public void DisplayOccupancy()
        {
            Console.WriteLine("Checking Occupancy");
            Console.WriteLine("\n");
            Console.WriteLine("Number of Two Wheeler Slot :" + TwoWheelerSlots);
            Console.WriteLine("Number of Four Wheeler Slot :" + FourWheelerSlots);
            Console.WriteLine("Number of Heavy Vehicle Slot :" + HeavyVehicleSlots);

            Console.WriteLine("\nOccupied Slots:");

            Console.WriteLine("Number of Available TwoWheeler Slot :" + (TwoWheelerSlots - OccupiedTwoWheelerSlots));
            Console.WriteLine("Number of Available FourWheeler Slot :" + (FourWheelerSlots - OccupiedFourWheelerSlots));
            Console.WriteLine("Number of Available HeavyVehicle Slot :" +(HeavyVehicleSlots - OccupiedHeavyVehicleSlots));
            Console.WriteLine("\n******************");
        }
        public void ParkingFee(ParkingTicket Ticket)
        {
            double parkingRate = 0.05;

            DateTime InTime = Ticket.InTime;
            DateTime OutTime = Ticket.OutTime;

            TimeSpan elapsed = OutTime - InTime;

            double TotalPrice = elapsed.TotalMinutes * parkingRate;

            Console.WriteLine("\nTotal parking price: $" + TotalPrice.ToString("0.00"));
            Console.WriteLine("\n******************");
        }
    }
}
