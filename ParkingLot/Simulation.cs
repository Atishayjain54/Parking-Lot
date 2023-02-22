using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Task1;

namespace Task1
{
     class Simulation
    {
        public static void ParkVehicle(ParkingLotService ParkingLot)
        {
            Console.WriteLine("Type of Vehicle :  ");
            Console.WriteLine("\n1: Two Wheeler");
            Console.WriteLine("2: Four Wheeler");
            Console.WriteLine("3: HeavyVehicle");
            int vehicleType = Convert.ToInt32(Console.ReadLine());
            int SlotNumber;
            ParkingTicket ticket;
            if (vehicleType == 1)
            {
                SlotNumber = ParkingLot.IsSlotAvailable(VehicleType.TwoWheeler);
                if (SlotNumber != -1)
                {
                    Console.WriteLine("\nEnter The Vehicle Number for two Wheeler");
                    string VehicleNumber = Console.ReadLine();
                    if (ParkingLot.IsValidVehicleNumber(VehicleNumber))
                    {
                         ticket = ParkingLot.Parking(SlotNumber, VehicleNumber, VehicleType.TwoWheeler);

                        Console.WriteLine("\n Parking ticket issued");
                        Console.WriteLine(" Here is your Ticket Details.\n Ticket ID : " + ticket.TicketId + "\n Slot number: " + ticket.SlotNumber + "\n Vehicle number: " + ticket.VehicleNumber + "\n In Time: " + ticket.InTime + "\n");
                        Console.WriteLine("Please save the Ticket ID for UnParking\n");
                    }
                    else
                    {
                        Console.WriteLine("\nPlease Enter Valid Vehicle Number");
                    }
                }
                else
                {
                    Console.WriteLine("Slot is Not Available for two Wheeler");
                    Console.WriteLine("\n");
                }
            }
            else if (vehicleType == 2)
            {
                SlotNumber = ParkingLot.IsSlotAvailable(VehicleType.FourWheeler);
                if (SlotNumber != -1)
                {
                    Console.WriteLine("\nEnter The Vehicle Number for Four Wheeler");
                    string VehicleNumber = Console.ReadLine();
                    if (ParkingLot.IsValidVehicleNumber(VehicleNumber))
                    {
                         ticket = ParkingLot.Parking(SlotNumber, VehicleNumber, VehicleType.FourWheeler);

                        Console.WriteLine("\n Parking ticket issued");
                        Console.WriteLine(" Here is your Ticket Details.\n Ticket ID : " + ticket.TicketId + "\n Slot number: " + ticket.SlotNumber + "\n Vehicle number: " + ticket.VehicleNumber + "\n In Time: " + ticket.InTime + "\n");
                        Console.WriteLine("Please save the Ticket ID for UnParking\n");
                    }
                    else
                    {
                        Console.WriteLine("\nPlease Enter Valid Vehicle Number");
                    }
                }
                else
                {
                    Console.WriteLine("Slot is Not Available for Four Wheeler");
                    Console.WriteLine("\n");
                }
            }
            else if (vehicleType == 3)
            {
                SlotNumber = ParkingLot.IsSlotAvailable(VehicleType.HeavyVehicle);
                if (SlotNumber != -1)
                {
                    Console.WriteLine("\nEnter The Vehicle Number for Heavy Vehicle");
                    string VehicleNumber = Console.ReadLine();
                    if (ParkingLot.IsValidVehicleNumber(VehicleNumber))
                    {
                         ticket = ParkingLot.Parking(SlotNumber, VehicleNumber, VehicleType.HeavyVehicle);

                        Console.WriteLine("\n Parking ticket issued");
                        Console.WriteLine(" Here is your Ticket Details.\n Ticket ID : " + ticket.TicketId + "\n Slot number: " + ticket.SlotNumber + "\n Vehicle number: " + ticket.VehicleNumber + "\n In Time: " + ticket.InTime + "\n");
                        Console.WriteLine("Please save the Ticket ID for UnParking\n");
                    }
                    else
                    {
                        Console.WriteLine("\nPlease Enter Valid Vehicle Number");
                    }
                }
                else
                {
                    Console.WriteLine("Slot is Not Available for Heavy Vehicle");
                    Console.WriteLine("\n");
                }
            }
            else
            {

                Console.WriteLine("Invalid Input");
                Console.WriteLine("\n ******************");
            }
        }
        public static void UnParking(ParkingLotService ParkingLot)
        {
            Console.WriteLine("Please Enter the Ticket ID");
            string TicketId = Console.ReadLine();
            ParkingTicket Ticket =ParkingLot.UnPark(TicketId);
            if(Ticket != null)
            {
                Console.WriteLine("Unparking ticket issued.\n Slot number :" + Ticket.SlotNumber + " \n Vehicle number: " + Ticket.VehicleNumber + "\nIn Time :" + Ticket.InTime + "\nOut Time :" + Ticket.OutTime);
                Console.WriteLine("\n Unparked");
                double TotalPrice = ParkingLot.ParkingFee(Ticket);
                Console.WriteLine("\nTotal parking price: $" + TotalPrice.ToString("0.00"));
                Console.WriteLine("\n******************");
            }
            else
            {
                Console.WriteLine("Invalid Ticket ID");
                Console.WriteLine("\n******************");
            }
        }
        public static void CheckOccupancy(ParkingLotService Parking)
        {   
            Console.WriteLine("Checking Occupancy");
            Console.WriteLine("\n");

            Console.WriteLine("Number of Two Wheeler Slot :" + Parking.TwoWheelerSlots);
            Console.WriteLine("Number of Four Wheeler Slot :" + Parking.FourWheelerSlots);
            Console.WriteLine("Number of Heavy Vehicle Slot :" + Parking.HeavyVehicleSlots);

            Console.WriteLine("\nOccupied Slots:");

            Console.WriteLine("Number of Available TwoWheeler Slot :" + (Parking.TwoWheelerSlots - Parking.OccupiedTwoWheelerSlots));
            Console.WriteLine("Number of Available FourWheeler Slot :" + (Parking.FourWheelerSlots - Parking.OccupiedFourWheelerSlots));
            Console.WriteLine("Number of Available HeavyVehicle Slot :" + (Parking.HeavyVehicleSlots - Parking.OccupiedHeavyVehicleSlots));
            Console.WriteLine("\n******************");

        }
    }
}
