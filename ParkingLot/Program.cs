using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            int NumberOfTwoWheelerSlot, NumberOfFourWheelerSlot, NumberOfHeavyVehicle;
            ParkingLotService ParkingLot;

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Number of Two Wheeler Slot");
                    NumberOfTwoWheelerSlot = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter Number of Four Wheeler Slot");
                    NumberOfFourWheelerSlot = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter Number of Heavy Vehicle Slot");
                    NumberOfHeavyVehicle = Convert.ToInt32(Console.ReadLine());

                    ParkingLot = new ParkingLotService(NumberOfTwoWheelerSlot, NumberOfFourWheelerSlot, NumberOfHeavyVehicle);
                    break;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid input! Please enter a valid Integer.");
                }
            }

            int Option = 0;
            while (Option != 4)
            {
                Console.WriteLine("Enter an option:");
                Console.WriteLine("1: Park a vehicle");
                Console.WriteLine("2: Unpark a vehicle");
                Console.WriteLine("3: Display parking lot occupancy");
                Console.WriteLine("4: Exit");

                try
                {
                    Option = Convert.ToInt32(Console.ReadLine());

                    switch (Option)
                    {
                        case 1:
                            Simulation.ParkVehicle(ParkingLot);
                            break;

                        case 2:
                            Simulation.UnParkVehicle(ParkingLot);
                            break;

                        case 3:
                            Simulation.DisplayOccupancy(ParkingLot);
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
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid input! Please enter a valid integer.");
                }
            }
        }
    }
}
