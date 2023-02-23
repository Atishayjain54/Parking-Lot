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
                string input = Console.ReadLine();
                int vehicleType;
                if (int.TryParse(input, out vehicleType) && vehicleType >= 1 && vehicleType <= 3)
                {
                    int SlotNumber;
                    ParkingTicket ticket;
                    VehicleType VehicleType;

                    if (vehicleType == 1)
                    {
                        VehicleType = VehicleType.TwoWheeler;
                    }
                    else if (vehicleType == 2)
                    {
                        VehicleType = VehicleType.FourWheeler;
                    }
                    else 
                    {
                        VehicleType = VehicleType.HeavyVehicle;
                    }

                    SlotNumber = ParkingLot.IsSlotAvailable(VehicleType);
                    if (SlotNumber != -1)
                    {
                        Console.WriteLine($"\nEnter The Vehicle Number for {VehicleType}");
                        string VehicleNumber = Console.ReadLine();
                        if (ParkingLot.IsValidVehicleNumber(VehicleNumber))
                        {
                            ticket = ParkingLot.ParkingVehicle(SlotNumber, VehicleNumber, VehicleType);

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
                else
                {
                    Console.WriteLine("Invalid Input,Please enter any of these.");
                }
            }
        }
        public static void UnParkingVehicle(ParkingLotService ParkingLot)
        {
            Console.WriteLine("Please Enter the Ticket ID");
            string TicketId = Console.ReadLine();
            ParkingTicket Ticket =ParkingLot.UnParkVehicle(TicketId);
            if(Ticket != null)
            {
                Console.WriteLine("Unparking ticket issued.\n Slot number :" + Ticket.SlotNumber + " \n Vehicle number: " + Ticket.VehicleNumber + "\nIn Time :" + Ticket.InTime + "\nOut Time :" + Ticket.OutTime);
                Console.WriteLine("\n Unparked Vehicle");
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
            Console.WriteLine("Display Occupancy");
            Console.WriteLine("\n");

            Console.WriteLine("Number of Two Wheeler Slot :" + Parking.VehicleSlots[VehicleType.TwoWheeler]);
            Console.WriteLine("Number of Four Wheeler Slot :" + Parking.VehicleSlots[VehicleType.FourWheeler]);
            Console.WriteLine("Number of Heavy Vehicle Slot :" + Parking.VehicleSlots[VehicleType.HeavyVehicle]);

            Console.WriteLine("\nOccupied Slots:");

            Console.WriteLine("Number of Available TwoWheeler Slot :" + (Parking.VehicleSlots[VehicleType.TwoWheeler] - Parking.OccupiedSlots[VehicleType.TwoWheeler]));
            Console.WriteLine("Number of Available FourWheeler Slot :" + (Parking.VehicleSlots[VehicleType.FourWheeler] - Parking.OccupiedSlots[VehicleType.FourWheeler]));
            Console.WriteLine("Number of Available HeavyVehicle Slot :" + (Parking.VehicleSlots[VehicleType.HeavyVehicle] - Parking.OccupiedSlots[VehicleType.HeavyVehicle]));
            Console.WriteLine("\n******************");

        }
    }
}
