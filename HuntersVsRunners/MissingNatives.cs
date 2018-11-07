using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace HuntersVsRunners
{
    public static class MissingNatives
    {
        public static bool AreBombBayDoorsOpen(Vehicle vehicle)
        {
            return CitizenFX.Core.Native.Function.Call<bool>((CitizenFX.Core.Native.Hash)0xD0917A423314BBA8, vehicle.Handle);
        }

        public static float GetHoverModePercentage(Vehicle vehicle)
        {
            if (vehicle.Model == (VehicleHash)(uint)GetHashKey("tula") || vehicle.Model == (VehicleHash)(uint)GetHashKey("avenger") || vehicle.Model == (VehicleHash)(uint)GetHashKey("hydra"))
                return CitizenFX.Core.Native.Function.Call<float>((CitizenFX.Core.Native.Hash)0xBBE00FBD9BB33AF0, vehicle.Handle);
            return 0f;
        }

        public static void SetPlaneTurbulenceMultiplier(int vehicleHandle, float multiplier)
        {
            N_0xad2d28a1afdff131(vehicleHandle, multiplier);
        }

    }
}
