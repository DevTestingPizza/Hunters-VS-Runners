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
    public static class SoundController
    {

        private const string bomb_camera_audio_scene = "DLC_SM_Bomb_Bay_View_Scene";

        private readonly static Dictionary<GameSounds, KeyValuePair<string, string>> _sounds = new Dictionary<GameSounds, KeyValuePair<string, string>>()
        {
            [GameSounds.flare_deployed] = new KeyValuePair<string, string>("flares_released", "DLC_SM_Countermeasures_Sounds"),
            [GameSounds.flare_empty] = new KeyValuePair<string, string>("flares_empty", "DLC_SM_Countermeasures_Sounds"),

            [GameSounds.bomb_deployed] = new KeyValuePair<string, string>("bomb_deployed", "DLC_SM_Bomb_Bay_Bombs_Sounds"),
            [GameSounds.bomb_empty] = new KeyValuePair<string, string>("bombs_empty", "DLC_SM_Bomb_Bay_Bombs_Sounds"),

            [GameSounds.race_won] = new KeyValuePair<string, string>("Checkpoint_Finish", "DLC_Stunt_Race_Frontend_Sounds"),
            [GameSounds.checkpoint_reached] = new KeyValuePair<string, string>("CHECKPOINT_NORMAL", "HUD_MINI_GAME_SOUNDSET"),
            [GameSounds.race_go] = new KeyValuePair<string, string>("Round_Start", "DLC_LOW2_Sumo_Soundset"),
            [GameSounds.race_countdown] = new KeyValuePair<string, string>("3_2_1", "HUD_MINI_GAME_SOUNDSET"),

        };

        public static void ResetMusicEvents()
        {
            //TriggerMusicEvent("GLOBAL_KILL_MUSIC_FADEIN_RADIO");
            //TriggerMusicEvent("HALLOWEEN_FAST_STOP_MUSIC");
            TriggerMusicEvent("GLOBAL_KILL_MUSIC");
        }

        public static async void SlowRadioFadeout()
        {
            while (!PrepareMusicEvent("MP_MC_DZ_FADE_OUT_RADIO"))
            {
                await BaseScript.Delay(0);
            }
            TriggerMusicEvent("MP_MC_DZ_FADE_OUT_RADIO");
        }

        public static void TriggerSuspenseMusicEvent()
        {
            //while (!PrepareMusicEvent("MP_MC_VEHICLE_CHASE_HFIN"))
            //{
            //    await BaseScript.Delay(0);
            //}
            TriggerMusicEvent("KILL_LIST_START_MUSIC");
            //TriggerMusicEvent("HALLOWEEN_START_MUSIC");
            //TriggerMusicEvent("MP_MC_VEHICLE_CHASE_HFIN");
        }

        public enum GameSounds
        {
            bomb_deployed,
            bomb_empty,

            flare_deployed,
            flare_empty,

            race_won,
            checkpoint_reached,
            race_go,
            race_countdown
        }

        /// <summary>
        /// Play a gamemode sound from the player's vehicle so everyone can hear it.
        /// </summary>
        /// <param name="sound"></param>
        /// <param name="entity"></param>
        public static void PlayGameSoundFromEntity(GameSounds sound, Vehicle entity)
        {
            PlaySoundFromEntity(-1, _sounds[sound].Key, entity.Handle, _sounds[sound].Value, true, 0);
        }

        /// <summary>
        /// Play a gamemode sound for this player only.
        /// </summary>
        /// <param name="sound"></param>
        public static void PlayGameSound(GameSounds sound)
        {
            PlaySoundFrontend(-1, _sounds[sound].Key, _sounds[sound].Value, true);
        }

        /// <summary>
        /// Enables or disables the bomb camera audio scene.
        /// </summary>
        /// <param name="toggle"></param>
        public static void ToggleBombCameraAudioScene(bool toggle)
        {
            if (toggle)
            {
                StartAudioScene(bomb_camera_audio_scene);
            }
            else
            {
                StopAudioScene(bomb_camera_audio_scene);
            }
        }
    }
}
