using Microsoft.Win32;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Task1;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter Number of Two Wheeler Slot");

            int NumberOfTwoWheelerSlot = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Number of Four Wheeler Slot");

            int NumberOfFourWheelerSlot = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Number of Heavy Vehicle Slot");

            int NumberOfHeavyVehicle = Convert.ToInt32(Console.ReadLine());

            ParkingLotService ParkingLot = new ParkingLotService(NumberOfTwoWheelerSlot, NumberOfFourWheelerSlot, NumberOfHeavyVehicle);

            int Option = 0;
            while (Option != 4)
            {
                Console.WriteLine("Enter an option:");
                Console.WriteLine("1: Park a vehicle");
                Console.WriteLine("2: Unpark a vehicle");
                Console.WriteLine("3: Display parking lot occupancy");
                Console.WriteLine("4: Exit");

                Option = Convert.ToInt32(Console.ReadLine());
                switch (Option)
                {
                    case 1:

                        Simulation.ParkVehicle(ParkingLot);
                        break;

                    case 2:

                        Simulation.UnParking(ParkingLot);
                        break;

                    case 3:

                        Simulation.CheckOccupancy(ParkingLot);
                        break;

                    case 4:

                        Console.WriteLine("Thank you");
                        Console.WriteLine("\n******************");
                        break;

                    default:

                        Console.WriteLine("Invalid input");
                        Console.WriteLine("\n******************");
                        break;

                }
            }
        }
    }
}