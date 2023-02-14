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
        public int ChooseOption()
        {

            Console.WriteLine("What do you want to do, Please select one option");
            Console.WriteLine("1 : Park the Vehicle");
            Console.WriteLine("2 : Unpark the Vehicle");
            Console.WriteLine("3 : Check Occupancy of the ParkingLot");
            Console.WriteLine("4 : Exit");

            int Option = Convert.ToInt32(Console.ReadLine());
            return Option;
        }
        public void ParkVehicle(VehicleType VehicleType, string VehicleNumber)
        {
            int SlotNumber = 0;
            if (VehicleType == VehicleType.TwoWheeler)
            {
                SlotNumber = ChechAvailability(VehicleType.TwoWheeler);
                if (ChechAvailability(VehicleType.TwoWheeler) != -1)
                {
                    Console.WriteLine("\nEnter The Vehicle Number for two Wheeler");
                    VehicleNumber = Console.ReadLine();
                    OccupiedTwoWheelerSlots++;
                    Console.WriteLine("\n Here is your Ticket Details");
                    DateTime CurrentTime = DateTime.Now;
                    string TicketId = CurrentTime.ToString("yyyyMMddHHmmss");
                    ParkingTicket ticket = new ParkingTicket(SlotNumber, VehicleNumber, DateTime.Now, "TW" + TicketId);
                    Console.WriteLine("Parking ticket issued.\n Ticket ID : " + ticket.TicketId + " \nSlot number: " + ticket.SlotNumber + "\n Vehicle number: " + ticket.VehicleNumber + "\nIn Time: " + ticket.InTime);
                    Tickets.Add(ticket);
                    ParkingSlots[SlotNumber - 1].VehicleNumber = VehicleNumber;
                    ParkingSlots[SlotNumber - 1].IsOccupied = true;
                }
                else
                {
                    Console.WriteLine("Slot is Not Available for two Wheeler");
                }
                Console.WriteLine("\n");
            }
            else if (VehicleType == VehicleType.FourWheeler)
            {
                SlotNumber = ChechAvailability(VehicleType.FourWheeler);
                if (ChechAvailability(VehicleType.FourWheeler) != -1)
                {
                    OccupiedFourWheelerSlots++;
                    Console.WriteLine("Enter The Vehicle Number for Four Wheeler");
                    VehicleNumber = Console.ReadLine();
                    Console.WriteLine("\n Here is your Ticket Details");
                    DateTime CurrentTime = DateTime.Now;
                    string TicketId = CurrentTime.ToString("yyyyMMddHHmmss");
                    ParkingTicket ticket = new ParkingTicket(SlotNumber, VehicleNumber, DateTime.Now, "FW" + TicketId);
                    Console.WriteLine("Parking ticket issued.\n Ticket ID : " + ticket.TicketId + " \nSlot number: " + ticket.SlotNumber + "\n Vehicle number: " + ticket.VehicleNumber + "\nIn Time: " + ticket.InTime);
                    Tickets.Add(ticket);
                    ParkingSlots[SlotNumber - 1].VehicleNumber = VehicleNumber;
                    ParkingSlots[SlotNumber - 1].IsOccupied = true;
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
                if (ChechAvailability(VehicleType.HeavyVehicle) != -1)
                {
                    Console.WriteLine("Enter The Vehicle Number for Heavy Vehicle");
                    VehicleNumber = Console.ReadLine();
                    OccupiedHeavyVehicleSlots++;
                    Console.WriteLine("\n Here is your Ticket Details");
                    DateTime CurrentTime = DateTime.Now;
                    string TicketId = CurrentTime.ToString("yyyyMMddHHmmss");
                    ParkingTicket ticket = new ParkingTicket(SlotNumber, VehicleNumber, DateTime.Now, "HV" + TicketId);
                    Console.WriteLine("Parking ticket issued.\n Ticket ID : " + ticket.TicketId + " \nSlot number: " + ticket.SlotNumber + "\n Vehicle number: " + ticket.VehicleNumber + "\nIn Time: " + ticket.InTime);
                    Tickets.Add(ticket);
                    ParkingSlots[SlotNumber - 1].VehicleNumber = VehicleNumber;
                    ParkingSlots[SlotNumber - 1].IsOccupied = true;
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

                    ticket.OutTime = DateTime.Now;
                    Console.WriteLine("Unparking ticket issued.\n Slot number :" + ticket.SlotNumber + " \n Vehicle number: " + ticket.VehicleNumber + "\nIn Time :" + ticket.InTime + "\nOut Time :" + ticket.OutTime);
                    Console.WriteLine("\n Unparked");
                    Console.WriteLine("\n******************");
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
            Console.WriteLine("\n");
            Console.WriteLine("Number of Two Wheeler Slot :" + TwoWheelerSlots);
            Console.WriteLine("Number of Four Wheeler Slot :" + FourWheelerSlots);
            Console.WriteLine("Number of Heavy Vehicle Slot :" + HeavyVehicleSlots);

            Console.WriteLine("\nOccupied Slots:");

            Console.WriteLine("Number of Occupied TwoWheeler Slot :" + OccupiedTwoWheelerSlots);
            Console.WriteLine("Number of Occupied FourWheeler Slot :" + OccupiedFourWheelerSlots);
            Console.WriteLine("Number of Occupied HeavyVehicle Slot :" + OccupiedHeavyVehicleSlots);
            Console.WriteLine("\n******************");
        }
    }
}
