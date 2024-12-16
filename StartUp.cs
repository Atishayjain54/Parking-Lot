using Microsoft.Extensions.DependencyInjection;
using ParkingLot.Repositories;
using ParkingLot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot
{
    public class StartUp
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection()
                .AddSingleton<IParkingSlotRepository, ParkingSlotFileRepository>()
                .AddSingleton<IParkingTicketRepository, ParkingTicketFileRepository>()
                .AddSingleton<IParkingLotService, ParkingLotService>()
                .AddSingleton<Simulation>()
                .BuildServiceProvider();
            return services;
        }
    }
}
