using System;
using System;
using ParkingLot.Repositories;
using ParkingLot.Services;
using ParkingLot.Models;


namespace ParkingLot
{
    class Simulation
    {
        private IParkingLotService _parkingLotService;
        public Simulation(IParkingLotService parkingLotService) 
        { 
           _parkingLotService= parkingLotService;
        }
        public void ParkVehicle()
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
                VehicleType vehicleTypeEnum = vehicleType switch
                {
                    1 => VehicleType.TwoWheeler,
                    2 => VehicleType.FourWheeler,
                    3 => VehicleType.HeavyVehicle,
                };
                //VehicleType vehicleTypeEnum = (VehicleType)vehicleType;
                int slotNumber = _parkingLotService.IsSlotAvailable(vehicleTypeEnum);
                if (slotNumber != -1)
                {
                    Console.WriteLine($"\nEnter The Vehicle Number for {vehicleTypeEnum}");
                    string vehicleNumber = Console.ReadLine();
                    if (_parkingLotService.IsValidVehicleNumber(vehicleNumber))
                    {
                        ParkingTicket ticket = _parkingLotService.ParkVehicle(slotNumber, vehicleNumber, vehicleTypeEnum);
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
                    Console.WriteLine($"Slot is Not Available for {vehicleTypeEnum}");
                    Console.WriteLine("\n");
                }
                break;
            }
        }
        public void UnParkVehicle()
        {
            Console.WriteLine("Please Enter the Ticket ID");
            string ticketId = Console.ReadLine();
            ParkingTicket ticket = _parkingLotService.UnParkVehicle(ticketId);
            if (ticket != null)
            {
                Console.WriteLine("Unparking ticket issued.\n Slot number :" + ticket.SlotNumber + " \n Vehicle number: " + ticket.VehicleNumber + "\nIn Time :" + ticket.InTime + "\nOut Time :" + ticket.OutTime);
                Console.WriteLine("\n Unparked Vehicle");
                double totalPrice = ticket.ParkingFee;
                Console.WriteLine("\nTotal parking price: $" + totalPrice.ToString("0.00"));
                Console.WriteLine("\n******************");
            }
            else
            {
                Console.WriteLine("Invalid Ticket ID");
                Console.WriteLine("\n******************");
            }
        }
        public void DisplayOccupancy()
        {
            Dictionary<VehicleType, int[]> occupancyDetail = _parkingLotService.GetOccupancyDetails();
            Console.WriteLine("Display Occupancy:");
            Console.WriteLine("\n");
            Console.WriteLine("Number of Two Wheeler Slot :" + occupancyDetail[VehicleType.TwoWheeler][0]);
            Console.WriteLine("Number of Four Wheeler Slot :" + occupancyDetail[VehicleType.FourWheeler][0]);
            Console.WriteLine("Number of Heavy Vehicle Slot :" + occupancyDetail[VehicleType.HeavyVehicle][0]);

            Console.WriteLine("\nAvailable Slots:");
            Console.WriteLine("\n");

            Console.WriteLine("Number of Available TwoWheeler Slot :" + (occupancyDetail[VehicleType.TwoWheeler][0] - occupancyDetail[VehicleType.TwoWheeler][1]));
            Console.WriteLine("Number of Available FourWheeler Slot :" + (occupancyDetail[VehicleType.FourWheeler][0] - occupancyDetail[VehicleType.FourWheeler][1]));
            Console.WriteLine("Number of Available HeavyVehicle Slot :" + (occupancyDetail[VehicleType.HeavyVehicle][0] - occupancyDetail[VehicleType.HeavyVehicle][1]));
            Console.WriteLine("\n******************");
        }
    }
}

