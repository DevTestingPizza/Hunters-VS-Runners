using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using static HuntersVsRunners.MissingNatives;

namespace HuntersVsRunners
{
    public static class Runner
    {


        private static Vehicle _veh;


        /// <summary>
        /// Get the player's current (/last) vehicle.
        /// </summary>
        /// <returns></returns>
        public static Vehicle GetVehicle()
        {
            if (_veh.Exists() && !_veh.IsDead)
            {
                return _veh;
            }
            return null;
        }

        /// <summary>
        /// Delete the player's current vehicle.
        /// </summary>
        public static void DeleteVehicle()
        {
            if (_veh.Exists())
            {
                _veh.Delete();
            }
        }

        /// <summary>
        /// Creates a new runner vehicle. Returns true if the creation was successful, otherwise returns false.
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="position"></param>
        /// <param name="heading"></param>
        /// <param name="deleteOldVehicle"></param>
        /// <returns></returns>
        public static async Task<bool> CreateRunnerVehicle(uint hash, Vector3 position, float heading, bool deleteOldVehicle)
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                SetEntityAsMissionEntity(Game.PlayerPed.CurrentVehicle.Handle, true, true);
                Game.PlayerPed.CurrentVehicle.Delete();
            }

            if (IsModelInCdimage(hash))
            {
                if (!HasModelLoaded(hash))
                {
                    RequestModel(hash);
                    while (!HasModelLoaded(hash))
                    {
                        await BaseScript.Delay(0);
                    }
                }
                if (deleteOldVehicle)
                {
                    if (_veh != null && _veh.Exists())
                    {
                        _veh.Delete();
                    }
                }
                //Debug.WriteLine("made it this far");
                _veh = new Vehicle(CreateVehicle(hash, position.X, position.Y, position.Z, heading, true, false));
                return _veh.Exists();
            }
            //Debug.WriteLine("made it this far2");
            return false;
        }

        /// <summary>
        /// Spawns the player at the given position + heading.
        /// Returns true if the spawn was succesful and the player is not dead.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="heading"></param>
        /// <returns></returns>
        public static async Task<bool> SpawnPlayer(Vector3 position, float heading)
        {
            if (Game.PlayerPed.IsDead)
            {
                ResurrectPed(PlayerPedId());
            }

            Game.PlayerPed.Health = Game.PlayerPed.MaxHealth;
            Game.PlayerPed.Armor = GetPlayerMaxArmour(PlayerId());
            Game.PlayerPed.Position = position;
            Game.PlayerPed.Heading = heading;

            if (!IsScreenFadedIn())
            {
                DoScreenFadeIn(100);
                while (!IsScreenFadedIn())
                {
                    await BaseScript.Delay(0);
                }
            }

            return !Game.PlayerPed.IsDead;
        }

        /// <summary>
        /// Spawns the player in a new vehicle with the given hash, at the given position + heading.
        /// Returns true if spawn was successful and the player is driving the car and the car is not broken.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="heading"></param>
        /// <param name="vehicleHash"></param>
        /// <returns></returns>
        public static async Task<bool> SpawnPlayer(Vector3 position, float heading, uint vehicleHash)
        {
            if (await CreateRunnerVehicle(vehicleHash, position, heading, true))
            {
                Game.PlayerPed.SetIntoVehicle(_veh, VehicleSeat.Driver);
                SetVehicleEngineOn(_veh.Handle, true, true, true);
                _veh.IsPositionFrozen = true;
                _veh.Repair();
                _veh.Wash();
                return (_veh.Driver == Game.PlayerPed && _veh.IsDriveable);
            }
            return false;
        }

        /// <summary>
        /// Spawns the player in an existing vehicle, teleports the vehicle to the given position + heading.
        /// Returns true if spawn was successful and the player is driving the car and the car is not broken.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="heading"></param>
        /// <param name="inVehicle"></param>
        /// <returns></returns>
        public static bool SpawnPlayer(Vector3 position, float heading, Vehicle inVehicle)
        {
            if (inVehicle != null && inVehicle.Driver == null && inVehicle.IsDriveable)
            {
                Game.PlayerPed.SetIntoVehicle(inVehicle, VehicleSeat.Driver);
                inVehicle.PositionNoOffset = position;
                inVehicle.Heading = heading;
                if (!inVehicle.IsEngineRunning)
                {
                    SetVehicleEngineOn(inVehicle.Handle, true, true, true);
                }
                inVehicle.IsPositionFrozen = true;
                return (inVehicle.Driver == Game.PlayerPed && inVehicle.IsDriveable);
            }
            return false;
        }
    }
}
