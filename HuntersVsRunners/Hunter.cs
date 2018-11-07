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
    public static class Hunter
    {
        private static Vehicle _veh;

        public enum BombType { Invalid = -1, Explosive = 0, Incendiary = 1, Cluster = 2, Gas = 3 }

        public static readonly Dictionary<BombType, uint> bomb_hashes = new Dictionary<BombType, uint>()
        {
            [BombType.Explosive] = 1856325840,
            [BombType.Incendiary] = 1794615063,
            [BombType.Gas] = 1430300958,
            [BombType.Cluster] = 220773539,
            [BombType.Invalid] = 0
        };

        public struct BombPlane { public uint planeHash; public Vector3 bombCameraOffset; public float bombPositionOffset; }

        public static Dictionary<uint, BombPlane> bombPlanes = new Dictionary<uint, BombPlane>()
        {
            [(uint)GetHashKey("cuban800")] = new BombPlane()
            {
                planeHash = (uint)GetHashKey("cuban800"),
                bombCameraOffset = new Vector3(0f, 0.2f, 1.0f),
                bombPositionOffset = 0.5f
            },
            [(uint)GetHashKey("mogul")] = new BombPlane()
            {
                planeHash = (uint)GetHashKey("mogul"),
                bombCameraOffset = new Vector3(0f, 0.2f, 0.97f),
                bombPositionOffset = 0.45f
            },
            [(uint)GetHashKey("rogue")] = new BombPlane()
            {
                planeHash = (uint)GetHashKey("rogue"),
                bombCameraOffset = new Vector3(0f, 0.3f, 1.1f),
                bombPositionOffset = 0.46f
            },
            [(uint)GetHashKey("starling")] = new BombPlane()
            {
                planeHash = (uint)GetHashKey("starling"),
                bombCameraOffset = new Vector3(0f, 0.25f, 0.55f),
                bombPositionOffset = 0.55f
            },
            [(uint)GetHashKey("seabreeze")] = new BombPlane()
            {
                planeHash = (uint)GetHashKey("seabreeze"),
                bombCameraOffset = new Vector3(0f, 0.2f, 0.4f),
                bombPositionOffset = 0.5f
            },
            [(uint)GetHashKey("bombushka")] = new BombPlane()
            {
                planeHash = (uint)GetHashKey("bombushka"),
                bombCameraOffset = new Vector3(0f, 0.3f, 0.8f),
                bombPositionOffset = 0.43f
            },
            [(uint)GetHashKey("volatol")] = new BombPlane()
            {
                planeHash = (uint)GetHashKey("volatol"),
                bombCameraOffset = new Vector3(0f, 0f, 2.0f),
                bombPositionOffset = 0.54f
            }
        };





        #region Hunter vehicle related functions.
        /// <summary>
        /// Get the bomb model hash.
        /// </summary>
        /// <param name="bombType"></param>
        /// <returns></returns>
        public static uint GetBombModel(BombType bombType)
        {
            return bomb_hashes[bombType];
        }

        /// <summary>
        /// Gets the bomb type that is active according to the vehicle's mod type.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public static BombType GetBombTypeFromVehicle(Vehicle vehicle)
        {
            return (BombType)GetVehicleMod(vehicle.Handle, 9);
        }

        /// <summary>
        /// Drops a bomb.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="offset"></param>
        /// <param name="vehicle"></param>
        /// <param name="bombType"></param>
        public static async void DropBomb(Vehicle vehicle, BombType bombType)
        {
            // Stop if the vehicle doesn't exist or if it's dead.
            if (!vehicle.Exists() || vehicle.IsDead) return;

            // Make sure the doors are open, stop if they aren't.
            if (!AreBombBayDoorsOpen(vehicle)) return;

            // Get the bomb model.
            uint bomb = GetBombModel(bombType);

            // Stop if the model is invalid.
            if (!IsWeaponValid(bomb)) return;

            // Load the model if it's not loaded yet.
            if (!HasWeaponAssetLoaded(bomb))
            {
                RequestWeaponAsset(bomb, 31, 26);
                while (!HasWeaponAssetLoaded(bomb))
                {
                    await BaseScript.Delay(0);
                }
            }

            // Get the bomb & offset position.
            Vector3 dropPos = new Vector3(0f, 0f, 0f);
            Vector3 offsetPos = new Vector3(0f, 0f, 0f);

            CalculateBombPositionAndOffset(vehicle, ref dropPos, ref offsetPos);

            /* This is actually ShootSingleBulletBetweenCoordsWithExtraParams
             * (but the amount of parameters in the CitizenFX.Core native API are wrong, 
             * so I have to manually invoke the native so i can pass the correct amount of arguments.)
             */
            CitizenFX.Core.Native.Function.Call((CitizenFX.Core.Native.Hash)0xBFE5756E7407064A, dropPos.X, dropPos.Y, dropPos.Z, offsetPos.X, offsetPos.Y, offsetPos.Z, 0, true, bomb, PlayerPedId(), true, true, -4f, vehicle.Handle, false, false, false, true, true, false);

            SoundController.PlayGameSoundFromEntity(SoundController.GameSounds.bomb_deployed, vehicle);

        }

        /// <summary>
        /// Gets an unknown bomb offset from the plane model.
        /// </summary>
        /// <param name="vehicleModel"></param>
        /// <returns></returns>
        private static float GetUnknownBombOffsetFromModel(uint vehicleModel)
        {
            return bombPlanes[vehicleModel].bombPositionOffset;
        }

        /// <summary>
        /// See GetBombPositionAndOffset for info, this function is copied from R* Decompiled scripts
        /// </summary>
        /// <param name="vParam0"></param>
        /// <param name="vParam1"></param>
        /// <param name="fParam2"></param>
        /// <param name="fParam3"></param>
        /// <param name="fParam4"></param>
        /// <returns></returns>
        private static Vector3 UnknownOffsetCalcFunction1(Vector3 vParam0, Vector3 vParam1, float fParam2, float fParam3, float fParam4)
        {
            return new Vector3(
                UnknownOffsetCalcFunction2(vParam0.X, vParam1.X, fParam2, fParam3, fParam4),
                UnknownOffsetCalcFunction2(vParam0.Y, vParam1.Y, fParam2, fParam3, fParam4),
                UnknownOffsetCalcFunction2(vParam0.Z, vParam1.Z, fParam2, fParam3, fParam4)
                );
        }

        /// <summary>
        /// See GetBombPositionAndOffset for info, this function is copied from R* Decompiled scripts
        /// </summary>
        /// <param name="fParam0"></param>
        /// <param name="fParam1"></param>
        /// <param name="fParam2"></param>
        /// <param name="fParam3"></param>
        /// <param name="fParam4"></param>
        /// <returns></returns>
        private static float UnknownOffsetCalcFunction2(float fParam0, float fParam1, float fParam2, float fParam3, float fParam4)
        {
            return ((((fParam1 - fParam0) / (fParam3 - fParam2)) * (fParam4 - fParam2)) + fParam0);
        }

        /// <summary>
        /// Calculates bomb offset & bomb starting coords. Most of this is copied from R* scripts and I have no idea what it does exactly.
        /// All I know is that it works, and that's pretty much it.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="dropCoords"></param>
        /// <param name="offsetCoords"></param>
        public static void CalculateBombPositionAndOffset(Vehicle vehicle, ref Vector3 dropCoords, ref Vector3 offsetCoords)
        {
            // Get the dimensions
            Vector3 dimension1 = new Vector3();
            Vector3 dimension2 = new Vector3();
            GetModelDimensions((uint)vehicle.Model.Hash, ref dimension1, ref dimension2);

            /*
             *  The following part is copied/converted from R* decompiled scripts. I'm not sure how this stuff works
             *  and what it does 100%. All I know is it calcluates some offsets and starting positions of each bomb.
             *  It works perfectly, so I'm just going to use it.
             */

            Vector3 vVar0 = GetOffsetFromEntityInWorldCoords(vehicle.Handle, dimension1.X, dimension2.Y, dimension1.Z);
            Vector3 vVar1 = GetOffsetFromEntityInWorldCoords(vehicle.Handle, dimension2.X, dimension2.Y, dimension1.Z);
            Vector3 vVar2 = GetOffsetFromEntityInWorldCoords(vehicle.Handle, dimension1.X, dimension1.Y, dimension1.Z);
            Vector3 vVar3 = GetOffsetFromEntityInWorldCoords(vehicle.Handle, dimension2.X, dimension1.Y, dimension1.Z);

            Vector3 vVar4 = UnknownOffsetCalcFunction1(vVar0, vVar1, 0f, 1f, 0.5f);
            Vector3 vVar5 = UnknownOffsetCalcFunction1(vVar2, vVar3, 0f, 1f, 0.5f);

            vVar4.Z = vVar4.Z + 0.4f;
            vVar5.Z = vVar5.Z + 0.4f;

            Vector3 vVar6 = UnknownOffsetCalcFunction1(vVar4, vVar5, 0f, 1f, GetUnknownBombOffsetFromModel((uint)vehicle.Model.Hash));

            vVar4.Z = vVar4.Z - 0.2f;
            vVar5.Z = vVar5.Z - 0.2f;

            Vector3 vVar7 = UnknownOffsetCalcFunction1(vVar4, vVar5, 0f, 1f, (GetUnknownBombOffsetFromModel((uint)vehicle.Model.Hash) - 0.0001f));

            dropCoords = vVar6;
            offsetCoords = vVar7;
        }

        /// <summary>
        /// Gets the camera position for for each vehicle's bomb bay doors.
        /// Using a lot of converted code from R* scripts, not sure how all of it works, but it does the job spot on.
        /// </summary>
        /// <param name="outPos"></param>
        public static void GetBombCameraAttachPosition(ref Vector3 outPos)
        {
            // Get the dimensions
            Vector3 dimension1 = new Vector3();
            Vector3 dimension2 = new Vector3();
            GetModelDimensions((uint)Game.PlayerPed.CurrentVehicle.Model.Hash, ref dimension1, ref dimension2);

            /*
             *  The following part is copied/converted from R* decompiled scripts. I'm not sure how this stuff works
             *  and what it does 100%. All I know is it calcluates some offsets and starting positions of each bomb.
             *  It works perfectly, so I'm just going to use it.
             */

            Vector3 vVar0 = GetOffsetFromEntityInWorldCoords(Game.PlayerPed.CurrentVehicle.Handle, dimension1.X, dimension2.Y, dimension1.Z);
            Vector3 vVar1 = GetOffsetFromEntityInWorldCoords(Game.PlayerPed.CurrentVehicle.Handle, dimension2.X, dimension2.Y, dimension1.Z);
            Vector3 vVar2 = GetOffsetFromEntityInWorldCoords(Game.PlayerPed.CurrentVehicle.Handle, dimension1.X, dimension1.Y, dimension1.Z);
            Vector3 vVar3 = GetOffsetFromEntityInWorldCoords(Game.PlayerPed.CurrentVehicle.Handle, dimension2.X, dimension1.Y, dimension1.Z);

            Vector3 vVar4 = UnknownOffsetCalcFunction1(vVar0, vVar1, 0f, 1f, 0.5f);
            Vector3 vVar5 = UnknownOffsetCalcFunction1(vVar2, vVar3, 0f, 1f, 0.5f);

            vVar4.Z = vVar4.Z + 0.4f;
            vVar5.Z = vVar5.Z + 0.4f;

            Vector3 vVar6 = UnknownOffsetCalcFunction1(vVar4, vVar5, 0f, 1f, GetUnknownBombOffsetFromModel((uint)Game.PlayerPed.CurrentVehicle.Model.Hash));

            // set the referenced outPos to the correct camera offset value.
            outPos = GetOffsetFromEntityGivenWorldCoords(Game.PlayerPed.CurrentVehicle.Handle, vVar6.X, vVar6.Y, vVar6.Z) + bombPlanes[(uint)Game.PlayerPed.CurrentVehicle.Model.Hash].bombCameraOffset;
        }

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
        /// Creates a new hunter vehicle. Returns true if the creation was successful, otherwise returns false.
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="position"></param>
        /// <param name="heading"></param>
        /// <param name="deleteOldVehicle"></param>
        /// <returns></returns>
        public static async Task<bool> CreateHunterVehicle(uint hash, Vector3 position, float heading, bool deleteOldVehicle)
        {
            if (IsModelValid(hash) && IsModelAVehicle(hash))
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

                _veh = new Vehicle(CreateVehicle(hash, position.X, position.Y, position.Z, heading, true, false));
                return _veh.Exists();
            }
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
            if (await CreateHunterVehicle(vehicleHash, position, heading, true))
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
        #endregion

    }
}
