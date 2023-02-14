using Microsoft.Win32;
using System;
using System.Security.Cryptography.X509Certificates;
using Task1;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter Number of Two Wheeler Slot");

                int NumberOfTwoWheelerSlot = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Number of Four Wheeler Slot");

                int NumberOfFourWheelerSlot = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Number of Heavy Vehicle Slot");

                int NumberOfHeavyVehicle = Convert.ToInt32(Console.ReadLine());

                ParkingLotService parkingLot = new ParkingLotService(NumberOfTwoWheelerSlot, NumberOfFourWheelerSlot, NumberOfHeavyVehicle);

                while (true)
                {
                    int Option = parkingLot.ChooseOption();
                    String VehicleNumber = "";
                    switch (Option)
                    {
                        case 1:

                            Console.WriteLine("Type of Vehicle :  ");
                            Console.WriteLine("\n1: Two Wheeler");

                            Console.WriteLine("2: Four Wheeler");
                            Console.WriteLine("3: HeavyVehicle");
                            int vehicleType = Convert.ToInt32(Console.ReadLine());
                            if (vehicleType == 1)
                            {
                                parkingLot.ParkVehicle(VehicleType.TwoWheeler, VehicleNumber);
                            }
                            else if (vehicleType == 2)
                            {
                                parkingLot.ParkVehicle(VehicleType.FourWheeler, VehicleNumber);
                            }
                            else if (vehicleType == 3)
                            {
                                parkingLot.ParkVehicle(VehicleType.HeavyVehicle, VehicleNumber);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input");
                                Console.WriteLine("\n ******************");
                            }
                            break;

                        case 2:

                            parkingLot.UnParking();
                            break;

                        case 3:

                            parkingLot.DisplayOccupancy();
                            break;

                        case 4:

                            Console.WriteLine("Thank you");
                            Console.WriteLine("\n******************");
                            return;

                        default:
                            Console.WriteLine("Invalid input");
                            Console.WriteLine("\n******************");
                            break;
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid input format. Please enter a valid integer value.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }}
