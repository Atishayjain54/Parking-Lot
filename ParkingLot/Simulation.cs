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
            while (true)
            {
                Console.WriteLine("Type of Vehicle: ");
                Console.WriteLine("\n1: Two Wheeler");
                Console.WriteLine("2: Four Wheeler");
                Console.WriteLine("3: Heavy Vehicle");
                int vehicleType = 0;
                try
                {
                    int.TryParse(Console.ReadLine(), out vehicleType);
                    if (vehicleType < 1 || vehicleType > 3)
                    {
                        throw new Exception("Invalid vehicle type entered.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }

                VehicleType VehicleType = vehicleType switch
                {
                    1 => VehicleType.TwoWheeler,
                    2 => VehicleType.FourWheeler,
                    3 => VehicleType.HeavyVehicle,
                };
                int SlotNumber = ParkingLot.IsSlotAvailable(VehicleType);
                    if (SlotNumber != -1)
                    {
                        Console.WriteLine($"\nEnter The Vehicle Number for {VehicleType}");
                        string VehicleNumber = Console.ReadLine();
                        if (ParkingLot.IsValidVehicleNumber(VehicleNumber))
                        {
                            ParkingTicket ticket = ParkingLot.ParkingVehicle(SlotNumber, VehicleNumber, VehicleType);
                            Console.WriteLine("\nParking ticket issued");
                            Console.WriteLine($"Here is your Ticket Details.\n Ticket ID : {ticket.TicketId}\n Slot number: {ticket.SlotNumber}\n Vehicle number: {ticket.VehicleNumber}\n In Time: {ticket.InTime}\n");
                            Console.WriteLine("Please save the Ticket ID for UnParking\n");
                        }
                        else
                        {
                            Console.WriteLine("\nPlease Enter Valid Vehicle Number");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Slot is Not Available for {VehicleType}");
                        Console.WriteLine("\n");
                    }
                    break;
            }
        }
        public static void UnParkVehicle(ParkingLotService ParkingLot)
        {
            Console.WriteLine("Please Enter the Ticket ID");
            string ticketId = Console.ReadLine();
            ParkingTicket ticket =ParkingLot.UnParkVehicle(ticketId);
            if(ticket != null)
            {
                Console.WriteLine("Unparking ticket issued.\n Slot number :" + ticket.SlotNumber + " \n Vehicle number: " + ticket.VehicleNumber + "\nIn Time :" + ticket.InTime + "\nOut Time :" + ticket.OutTime);
                Console.WriteLine("\n Unparked Vehicle");
                double TotalPrice = ticket.ParkingFee;
                Console.WriteLine("\nTotal parking price: $" + TotalPrice.ToString("0.00"));
                Console.WriteLine("\n******************");
            }
            else
            {
                Console.WriteLine("Invalid Ticket ID");
                Console.WriteLine("\n******************");
            }
        }
        public static void DisplayOccupancy(ParkingLotService Parking)
        {
            Dictionary<VehicleType,int[]> OccupancyDetail = Parking.GetOccupancyDetails();
            Console.WriteLine("Display Occupancy:");
            Console.WriteLine("\n");
            Console.WriteLine("Number of Two Wheeler Slot :" + OccupancyDetail[VehicleType.TwoWheeler][0]);
            Console.WriteLine("Number of Four Wheeler Slot :" + OccupancyDetail[VehicleType.FourWheeler][0]);
            Console.WriteLine("Number of Heavy Vehicle Slot :" + OccupancyDetail[VehicleType.HeavyVehicle][0]);

            Console.WriteLine("\nAvailable Slots:");
            Console.WriteLine("\n");

            Console.WriteLine("Number of Available TwoWheeler Slot :" + (OccupancyDetail[VehicleType.TwoWheeler][0] - OccupancyDetail[VehicleType.TwoWheeler][1]));
            Console.WriteLine("Number of Available FourWheeler Slot :" + (OccupancyDetail[VehicleType.FourWheeler][0] - OccupancyDetail[VehicleType.FourWheeler][1]));
            Console.WriteLine("Number of Available HeavyVehicle Slot :" + (OccupancyDetail[VehicleType.HeavyVehicle][0] - OccupancyDetail[VehicleType.HeavyVehicle][1]));
            Console.WriteLine("\n******************");

        }
    }
}
