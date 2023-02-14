using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1;

 class Vehicle
 {
    public string VehicleNumber { get; set; }

    public VehicleType Type { get; set; }

 }

 public enum VehicleType
 {
    TwoWheeler,
    FourWheeler,
    HeavyVehicle
 }


