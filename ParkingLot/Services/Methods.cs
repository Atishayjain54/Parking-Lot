using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    static class Methods
    {

        public static int ChooseOption()
        {

            Console.WriteLine("What do you want to do, Please select one option");
            Console.WriteLine("1 : Park the Vehicle");
            Console.WriteLine("2 : Unpark the Vehicle");
            Console.WriteLine("3 : Check Occupancy of the ParkingLot");
            Console.WriteLine("4 : Exit");

            int Option = Convert.ToInt32(Console.ReadLine());
            return Option;
        }
        

        public static string ParkingVehicle(string VehicleNumber, int SlotNumber)
        {
                Console.WriteLine("\nEnter The Vehicle Number for two Wheeler");
                VehicleNumber = Console.ReadLine();
                return VehicleNumber;
        }
        public static ParkingTicket GetTicket(int SlotNumber, string VehicleNumber,VehicleType VehicleType)
        {
            DateTime CurrentTime = DateTime.Now;
            string TicketId = "";

            if (VehicleType == VehicleType.TwoWheeler)
            {
                TicketId = "TW" + VehicleNumber.Substring(0,4)+ CurrentTime.ToString("yyyyMMddHHmmss");
            }
            else if (VehicleType == VehicleType.FourWheeler)
            {
                TicketId = "FW" + VehicleNumber.Substring(0,4)+ CurrentTime.ToString("yyyyMMddHHmmss");
            }
            else
            {
                TicketId = "HV" + VehicleNumber.Substring(0,4)+ CurrentTime.ToString("yyyyMMddHHmmss");
            }
            ParkingTicket ticket = new ParkingTicket(SlotNumber, VehicleNumber, DateTime.Now,TicketId);
            Console.WriteLine("\n Parking ticket issued");
             Console.WriteLine(" Here is your Ticket Details.\n Ticket ID : " + ticket.TicketId + "\n Slot number: " + ticket.SlotNumber + "\n Vehicle number: " + ticket.VehicleNumber + "\n In Time: " + ticket.InTime +"\n");
            Console.WriteLine("Please save the Ticket ID for UnParking\n");
            return ticket;
        }
        public static void UpdateTicket(ParkingTicket Ticket)
        {
            Ticket.OutTime = DateTime.Now;
            Console.WriteLine("Unparking ticket issued.\n Slot number :" + Ticket.SlotNumber + " \n Vehicle number: " + Ticket.VehicleNumber + "\nIn Time :" + Ticket.InTime + "\nOut Time :" + Ticket.OutTime);
            Console.WriteLine("\n Unparked");
        }
    }
}
