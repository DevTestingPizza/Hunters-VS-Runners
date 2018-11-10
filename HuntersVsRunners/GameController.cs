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
    public class GameController : BaseScript
    {
        //private static int progress = 0; // # of checkpoints reached.
        public static int team = 0; // 0 = nothing yet, 1 = runner, 2 = hunter, 3 = spectator/dead.
        //private static int hunters = 1;
        //private static int runners = 1;
        //private static int minutes = 0;
        //private static int seconds = 0;
        //private static int gameTime = 0;
        //private static string gameTimeString = "00:00";
        //private static bool gameTimeRunning = false;
        //private static Vehicle playerVehicle;
        //private static bool firstTick = true;
        public static bool gameRestarting = false;
        //public static bool frontendActive = false;
        public FrontendMenu fe = new FrontendMenu("Hunters VS Runners", FrontendType.FE_MENU_VERSION_CORONA);
        //private static Scaleform instr_btn_scale = new Scaleform("instructional_buttons");
        public static int FeCurrentSelection = 0;
        public static int GameState { get; private set; } = 0;

        private static bool _ready = false;


        public struct CheckPoint
        {
            public Vector3 Location;
            public int Red;
            public int Green;
            public int Blue;
            public int Alpha;
            public int Type;
            public int Id;
            public int Blip;
            public int CpHandle;
        }

        private static List<CheckPoint> checkPoints = new List<CheckPoint>()
        {
            new CheckPoint(){ Location = new Vector3(  2533.77f, 4888.76f,  36.74f ), Red = 255, Green = 255, Blue = 0, Alpha = 150, Type = 0, Id = 0 },
            new CheckPoint(){ Location = new Vector3(  2551.54f, 4607.26f,  32.50f ), Red = 255, Green = 255, Blue = 0, Alpha = 150, Type = 0, Id = 1 },
            new CheckPoint(){ Location = new Vector3(  2321.30f, 3843.73f,  34.27f ), Red = 255, Green = 255, Blue = 0, Alpha = 150, Type = 0, Id = 2 },
            new CheckPoint(){ Location = new Vector3(  2228.99f, 3248.18f,  47.34f ), Red = 255, Green = 255, Blue = 0, Alpha = 150, Type = 0, Id = 3 },
            new CheckPoint(){ Location = new Vector3(  1347.14f, 2962.53f,  40.12f ), Red = 255, Green = 255, Blue = 0, Alpha = 150, Type = 0, Id = 4 },
            new CheckPoint(){ Location = new Vector3(   506.50f, 3067.66f,  39.99f ), Red = 255, Green = 255, Blue = 0, Alpha = 150, Type = 0, Id = 5 },
            new CheckPoint(){ Location = new Vector3(   199.67f, 3112.23f,  41.33f ), Red = 255, Green = 255, Blue = 0, Alpha = 150, Type = 0, Id = 6 },
            new CheckPoint(){ Location = new Vector3(  -162.39f, 2964.99f,  31.87f ), Red = 255, Green = 255, Blue = 0, Alpha = 150, Type = 0, Id = 7 },
            new CheckPoint(){ Location = new Vector3( -1438.65f, 2148.70f,  53.14f ), Red = 255, Green = 255, Blue = 0, Alpha = 150, Type = 0, Id = 8 },
            new CheckPoint(){ Location = new Vector3( -1542.50f, 1372.70f, 124.75f ), Red = 255, Green = 255, Blue = 0, Alpha = 150, Type = 4, Id = 9 },
        };

        //public readonly uint runner_veh_hash = (uint)GetHashKey("ratloader2");
        //public readonly uint runner_veh_hash = (uint)GetHashKey("deluxo");
        public static readonly uint runner_veh_hash = (uint)GetHashKey("wastelander");
        public static readonly uint hunter_veh_hash = (uint)GetHashKey("rogue");

        /// <summary>
        /// constructor
        /// </summary>
        public GameController()
        {
            SetManualShutdownLoadingScreenNui(true);

            EventHandlers.Add("hvr:ready", new Action(SetReady));
            EventHandlers.Add("onClientMapStart", new Action(OnResourceStart));
            EventHandlers.Add("hvsr:setGameState", new Action<int>(SetGamePhase));

            Tick += FrontendSelectionManager;
            Tick += FrontendSelectionManager2;
        }

        private void SetGamePhase(int phase)
        {
            GameState = phase;
        }

        private void OnResourceStart()
        {
            SetManualShutdownLoadingScreenNui(true);
            StartAudioScene("MP_LEADERBOARD_SCENE");
            Exports["spawnmanager"].setAutoSpawn(false);
            Exports["spawnmanager"].spawnPlayer(PlayerId() + 1, new Action(FirstPlayerSpawn));
        }

        private void SetReady()
        {
            _ready = true;
        }

        private async void FirstPlayerSpawn()
        {
            if (!IsScreenFadedOut())
            {
                DoScreenFadeOut(0);
            }
            SwitchOutPlayer(PlayerPedId(), 0, 1);
            while (GetPlayerSwitchState() < 5)
            {
                await Delay(0);
                HideHudAndRadarThisFrame();
            }
            while (!IsScreenFadedOut())
            {
                HideHudAndRadarThisFrame();
                await Delay(0);
            }
            ShutdownLoadingScreen();
            ShutdownLoadingScreenNui();
            DoScreenFadeIn(100);
            while (!IsScreenFadedIn())
            {
                HideHudAndRadarThisFrame();
                await Delay(0);
            }

            if (GameState == 0)
            {
                AddTextEntry("hvr_loading_game", "Waiting for players.");
                SetLoadingPromptTextEntry("hvr_loading_game");
            }
            else if (GameState == 1)
            {
                AddTextEntry("hvr_loading_game", "Loading new round.");
                SetLoadingPromptTextEntry("hvr_loading_game");
            }
            else
            {
                AddTextEntry("hvr_loading_game", "Loading.");
                SetLoadingPromptTextEntry("hvr_loading_game");
            }


            StopAudioScene("MP_LEADERBOARD_SCENE");

            SoundController.TriggerSuspenseMusicEvent();

            fe.SetSubtitle("There are 2 teams. The ~r~HUNTERS~s~ and the ~b~RUNNERS~s~. The ~b~RUNNERS~s~ will have to race to the finish line, while the ~r~HUNTERS~s~ try to take them out.");

            fe.settingsList.Add(new SettingsItem(0, new List<string>() { "Wastelander", "Rat-Truck", "Bison", "Blazer" }, "Vehicle", "Choose a vehicle. This vehicle will be used for when you are on the runners team.", 0, true, 0, 0));
            fe.settingsList.Add(new SettingsItem(1, new List<string>() { "On", "Off" }, "Menu Music", "Tired of the music yet?", 0, true, 0, 0));
            fe.settingsList.Add(new SettingsItem(2, new List<string>() { "" }, "Ready", "Mark yourself ready to play.", 0, true, 2, 116));
            fe.settingsList.Add(new SettingsItem(3, new List<string>() { "" }, "Speed", "", 0, false, 3, 60));
            fe.settingsList.Add(new SettingsItem(4, new List<string>() { "" }, "Acceleration", "", 0, false, 3, 40));
            fe.settingsList.Add(new SettingsItem(5, new List<string>() { "" }, "Breaking", "", 0, false, 3, 20));
            fe.settingsList.Add(new SettingsItem(5, new List<string>() { "" }, "Traction", "", 0, false, 3, 20));
            fe.UpdateSettings();


            if (GameState == 0 || GameState == 1)
            {
                SetFrontendActive(false);
                while (IsFrontendFading() || IsPauseMenuActive() || IsPauseMenuRestarting())
                {
                    await Delay(0);
                }

                await fe.ToggleMenu();

                //frontendActive = true;

                int updateTimer = GetGameTimer();

                while (!AreWeReadyToStart())
                {
                    if (GetGameTimer() - updateTimer > 500)
                    {
                        fe.UpdatePlayers();
                        updateTimer = GetGameTimer();
                    }
                    SetCloudHatOpacity(0.002f);
                    ShowLoadingPrompt(1);

                    await Delay(0);
                }

                await fe.ToggleMenu();
                //frontendActive = false;
            }



        }


        private async Task FrontendSelectionManager2()
        {
            //if (fe != null)
            //{
            //    //Debug.WriteLine(fe.IsVisible.ToString());
            //}

            if (Game.IsControlJustPressed(0, Control.FrontendRight))
            {
                if (fe.settingsList[FeCurrentSelection].Type == 0)
                {
                    try
                    {
                        int sel = FeCurrentSelection;
                        //Debug.WriteLine(FeCurrentSelection.ToString());
                        int newIndex = fe.settingsList[sel].SelectedIndex + 1;
                        if (newIndex >= fe.settingsList[sel].SelectionItems.Count)
                        {
                            newIndex = 0;
                        }
                        //Debug.WriteLine(newIndex.ToString() + fe.settingsList[sel].SelectionItems[newIndex]);
                        fe.settingsList[sel] = new SettingsItem(fe.settingsList[sel].RowIndex, fe.settingsList[sel].SelectionItems, fe.settingsList[sel].Title, fe.settingsList[sel].Description, newIndex, fe.settingsList[sel].Selectable, fe.settingsList[sel].Type, fe.settingsList[sel].RowColor);
                        //Debug.WriteLine(fe.settingsList[sel].SelectedIndex.ToString() + fe.settingsList[sel].SelectionItems[newIndex]);
                        //fe.settingsList[FeCurrentSelection].RowIndex
                        await Delay(100);
                        fe.UpdateSettings(true, sel);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                    Game.PlaySound("NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET");
                }
            }
            if (Game.IsControlJustPressed(0, Control.FrontendLeft))
            {
                if (fe.settingsList[FeCurrentSelection].Type == 0)
                {
                    try
                    {
                        int sel = FeCurrentSelection;
                        //Debug.WriteLine(FeCurrentSelection.ToString());
                        int newIndex = fe.settingsList[sel].SelectedIndex - 1;
                        if (newIndex < 0)
                        {
                            newIndex = fe.settingsList[sel].SelectionItems.Count - 1;
                        }
                        //Debug.WriteLine(newIndex.ToString() + fe.settingsList[sel].SelectionItems[newIndex]);
                        fe.settingsList[sel] = new SettingsItem(fe.settingsList[sel].RowIndex, fe.settingsList[sel].SelectionItems, fe.settingsList[sel].Title, fe.settingsList[sel].Description, newIndex, fe.settingsList[sel].Selectable, fe.settingsList[sel].Type, fe.settingsList[sel].RowColor);
                        //Debug.WriteLine(fe.settingsList[sel].SelectedIndex.ToString() + fe.settingsList[sel].SelectionItems[newIndex]);
                        //fe.settingsList[FeCurrentSelection].RowIndex
                        await Delay(100);
                        fe.UpdateSettings(true, sel);

                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                    Game.PlaySound("NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET");
                }

            }

            if (Game.IsControlJustPressed(0, Control.FrontendSelect))
            {
                //if (FeCurrentSelection)
                Game.PlaySound("NAV_SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET");
            }

            PushScaleformMovieFunctionN("SET_COLUMN_FOCUS");
            PushScaleformMovieFunctionParameterInt(0); // column index // _loc7_
            PushScaleformMovieFunctionParameterBool(true); // highlightIndex // _loc6_
            PushScaleformMovieFunctionParameterBool(false); // scriptSetUniqID // _loc4_
            PushScaleformMovieFunctionParameterBool(false); // scriptSetMenuState // _loc5_
            PopScaleformMovieFunctionVoid();
        }

        private async Task FrontendSelectionManager()
        {
            if (fe.IsVisible)
            {

                int tmpSelection = await fe.GetSelection();
                if (tmpSelection == -1)
                {
                    return;
                }
                if (tmpSelection != FeCurrentSelection)
                {
                    fe.UpdateSettings();
                    try
                    {
                        if (fe.settingsList != null && fe.settingsList.Count > tmpSelection)
                        {
                            await Delay(0);
                            fe.SetSettingsCurrentDescription(fe.settingsList[tmpSelection].Description, false);
                        }
                        else
                        {
                            await Delay(0);
                            fe.SetSettingsCurrentDescription("", false);
                        }
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Debug.WriteLine(e.Message);
                    }

                    FeCurrentSelection = tmpSelection;
                }

            }
        }

        public static bool AreWeReadyToStart()
        {
            if (_ready || GameState >= 2)
            {
                return true;
            }
            return false;
        }

        /*
        public GameController()
        {
            RequestScriptAudioBank("DLC_STUNT/STUNT_RACE_01", false);
            RequestScriptAudioBank("DLC_STUNT/STUNT_RACE_02", false);
            RequestScriptAudioBank("DLC_STUNT/STUNT_RACE_03", false);
            RequestScriptAudioBank("DLC_LOW2/SUMO_01", false);

            //CreateCheckpoints();

            Tick += MangeGamePlay;
            Tick += ManageAiDensity;
            Tick += ManageCheckpointProgress;
            Tick += DrawHudOverlays;
            Tick += ManageTeamsAndBlips;
            Tick += RespawnManager;
            Tick += CheckForEndGame;

            AddTextEntry("hvsr_runner_info", "Race to the finish line! Be careful where you drive however, because the ~r~Hunters~s~ will try to take you out from the sky!");
            AddTextEntry("hvsr_hunter_info", "Hold ~INPUT_VEH_FLY_BOMB_BAY~ to open or close the bomb bay doors.While the doors are open, press ~INPUT_VEH_FLY_ATTACK~ to drop a bomb.~n~Take out all the ~b~Runners~s~ before they reach the finish line.");

            Tick += ManageHudInfo;
            Tick += ManageMiniMap;
            Tick += ManageGameTimer;

            EventHandlers.Add("hvsr:go", new Action(Go));
            EventHandlers.Add("hvsr:hunter", new Action(SetHunter));
            EventHandlers.Add("hvsr:runner", new Action(SetRunner));

            RegisterCommand("hunter", new Action(SetHunter), false);
            RegisterCommand("runner", new Action(SetRunner), false);
            RegisterCommand("go", new Action(Go), false);
            RegisterCommand("reset", new Action(Go), false);
            RegisterCommand("finish", new Action(ShowFinishScale), false);
            ClearBrief();


        }

        private static async void ShowFinishScale()
        {
            await GfxController.ShowFinishScaleform();
        }

        private static void SetHunter()
        {
            ResetCheckpoints();
            team = 2;
        }

        private static void SetRunner()
        {
            ResetCheckpoints();
            team = 1;
            CreateCheckpoints();
        }

        private static void ResetCheckpoints()
        {
            progress = -1;
            for (int i = 0; i < checkPoints.Count; i++) // CheckPoint cp in checkPoints)
            {
                CheckPoint cp = checkPoints[i];

                if (DoesBlipExist(cp.Blip))
                {
                    RemoveBlip(ref cp.Blip);
                }

                DeleteCheckpoint(cp.CpHandle);
            }
        }

        public static async void Go()
        {

            StopScreenEffect("MenuMGTournamentTint");
            StopAudioScene("MP_LEADERBOARD_SCENE");
            SetFrozenRenderingDisabled(true);
            if (!gameRestarting)
            {
                if (DoesEntityExist(GetVehiclePedIsIn(PlayerPedId(), true)))
                {
                    SetEntityAsMissionEntity(Game.PlayerPed.LastVehicle.Handle, true, true);
                    Game.PlayerPed.LastVehicle.Delete();
                }
                gameRestarting = true;
                ResetCheckpoints();

                CreateCheckpoints();
                if (team == 2)
                {
                    ResetCheckpoints();
                }

                SetCamActive(GetBombCamera(), false);
                RenderScriptCams(false, false, 0, false, false);

                DestroyCam(GetBombCamera(), true);
                DestroyAllCams(true); // just in case some random case where the above native doesn't remove the cam.

                SoundController.TriggerSuspenseMusicEvent();
                gameTimeRunning = false;
                //SetAudioFlag("DisableFlightMusic", false);
                //TriggerMusicEvent("MP_MC_VEHICLE_CHASE_HFIN");

                if (team != 1 && team != 2)
                {
                    if (runners > hunters)
                    {
                        team = 2;
                    }
                    else
                    {
                        team = 1;
                    }
                }
                int id = PlayerId();

                // if there are somehow no hunters, or all players are on the hunters team, check our player ID, if it's the lowest ID currently on the server, then we will switch to hunters.
                // if not, then someone else will do it automatically.
                if (runners == NetworkGetNumConnectedPlayers() || hunters == NetworkGetNumConnectedPlayers())
                {
                    for (var tmpid = 0; tmpid < 255; tmpid++)
                    {
                        if (NetworkIsPlayerActive(tmpid))
                        {
                            if (tmpid < PlayerId())
                            {
                                id = -2;
                                break;
                            }
                        }
                    }
                    if (id == PlayerId())
                    {
                        team = 2;
                        gameRestarting = false;
                        Go();
                        return;
                    }
                }
                
                if (team == 1) // runner
                {
                    if (await Runner.SpawnPlayer(new Vector3(2533.77f, 4888.76f, 36.74f), 225.1f, runner_veh_hash))
                    {
                        Game.PlayerPed.CurrentVehicle.IsPositionFrozen = false;
                        Game.PlayerPed.CurrentVehicle.IsEngineRunning = true;

                        if (Game.PlayerPed.CurrentVehicle.Model.IsPlane)
                        {
                            Game.PlayerPed.CurrentVehicle.Speed = GetVehicleModelMaxSpeed((uint)Game.PlayerPed.CurrentVehicle.Model.Hash);
                        }
                        else
                        {
                            Game.PlayerPed.CurrentVehicle.PlaceOnGround();
                        }
                        while (!IsScreenFadedIn())
                        {
                            await Delay(0);
                        }
                        Game.PlayerPed.CurrentVehicle.IsPositionFrozen = true;
                        await Delay(1000);
                        SoundController.PlayGameSound(SoundController.GameSounds.race_countdown);
                        await Delay(1000);
                        SoundController.PlayGameSound(SoundController.GameSounds.race_countdown);
                        await Delay(1000);
                        SoundController.PlayGameSound(SoundController.GameSounds.race_countdown);
                        await Delay(1000);
                        SoundController.PlayGameSound(SoundController.GameSounds.race_go);
                        Game.PlayerPed.CurrentVehicle.IsPositionFrozen = false;
                    }
                    else
                    {
                        Debug.WriteLine("Vehicle creation failed.");
                    }
                }
                else if (team == 2) // hunter
                {
                    if (Game.PlayerPed.IsInVehicle())
                    {
                        Game.PlayerPed.CurrentVehicle.Delete();
                    }
                    if (await Hunter.SpawnPlayer(new Vector3(2533.77f, 4888.76f, 250.0f), 225.1f, hunter_veh_hash))
                    {
                        Game.PlayerPed.CurrentVehicle.IsPositionFrozen = false;
                        //Game.PlayerPed.CurrentVehicle.IsEngineRunning = true;

                        SetVehicleModKit(Game.PlayerPed.CurrentVehicle.Handle, 0);
                        SetVehicleMod(Game.PlayerPed.CurrentVehicle.Handle, 9, 0, false);
                        //Game.PlayerPed.CurrentVehicle.Speed = 
                        SetVehicleForwardSpeed(Game.PlayerPed.CurrentVehicle.Handle, GetVehicleModelMaxSpeed((uint)Game.PlayerPed.CurrentVehicle.Model.Hash));

                        Vector3 offset = GetOffsetFromEntityInWorldCoords(Game.PlayerPed.Handle, 0f, 10000f, 0f);
                        SetVehicleLandingGear(Game.PlayerPed.CurrentVehicle.Handle, 3);

                        ClearPedTasks(PlayerPedId());
                        SetPlaneTurbulenceMultiplier(Game.PlayerPed.CurrentVehicle.Handle, 0f);
                        TaskPlaneMission(PlayerPedId(), Game.PlayerPed.CurrentVehicle.Handle, 0, 0, offset.X, offset.Y, offset.Z, 4, 30f, 0.1f, Game.PlayerPed.CurrentVehicle.Heading, 30f, 20f);
                        //SetVehicleForwardSpeed(Game.PlayerPed.CurrentVehicle.Handle, GetVehicleModelMaxSpeed((uint)Game.PlayerPed.CurrentVehicle.Model.Hash));
                        //else
                        //{
                        //    Game.PlayerPed.CurrentVehicle.PlaceOnGround();
                        //}
                        while (!IsScreenFadedIn())
                        {
                            await Delay(0);
                        }
                        //Game.PlayerPed.CurrentVehicle.IsPositionFrozen = true;
                        await Delay(1000);
                        SoundController.PlayGameSound(SoundController.GameSounds.race_countdown);
                        await Delay(1000);
                        SoundController.PlayGameSound(SoundController.GameSounds.race_countdown);
                        await Delay(1000);
                        SoundController.PlayGameSound(SoundController.GameSounds.race_countdown);
                        await Delay(1000);
                        SoundController.PlayGameSound(SoundController.GameSounds.race_go);
                        //Game.PlayerPed.CurrentVehicle.IsPositionFrozen = false;
                        ClearPedTasks(PlayerPedId());
                        SetVehicleForwardSpeed(Game.PlayerPed.CurrentVehicle.Handle, GetVehicleModelMaxSpeed((uint)Game.PlayerPed.CurrentVehicle.Model.Hash));
                    }
                    else
                    {
                        Debug.WriteLine("Vehicle creation failed.");
                    }
                }
                progress = 0;

                //SetAudioFlag("DisableFlightMusic", false);
                //TriggerMusicEvent("MP_MC_VEHICLE_CHASE_HFIN");

                StartGameTimer();
                gameRestarting = false;
            }


        }

        private static void StartGameTimer()
        {
            gameTimeString = "00:00";
            gameTime = 0;
            gameTimeRunning = true;
        }

        private static async Task CheckForEndGame()
        {
            bool end = false;
            foreach (Player p in new PlayerList())
            {
                if (GetPlayerTeam(p.Handle) == 4) // runners win
                {
                    end = true;
                    break;
                }
            }
            if (end)
            {
                if (team == 4 || team == 1)
                {
                    await GfxController.ShowFinishScaleform(false);
                    //team = 2;
                }
                else
                {
                    await GfxController.ShowFinishScaleform(true);
                    //team = 1;
                }
                await Delay(5000);
                //if (team == 4 || team == 1)
                //{
                //    team = 2;
                //}
                //else
                //{
                //    team = 1;
                //}
                Go();
                Debug.WriteLine("Called Go() because the game ended.");
            }
        }
        private async Task RespawnManager()
        {
            if (!gameRestarting)
            {
                if (Game.PlayerPed.IsDead)
                {
                    int veh = GetVehiclePedIsIn(PlayerPedId(), true);
                    while (Game.PlayerPed.IsDead)
                    {
                        await Delay(0);
                    }
                    SetEntityAsMissionEntity(veh, true, true);
                    DeleteVehicle(ref veh);
                    if (team == 2) // hunter
                    {
                        Go();
                        Debug.WriteLine("Called Go() because the player died.");
                    }
                    else
                    {
                        //if (team == 1)
                        //{

                        //}
                        team = 3; // dead
                                  //Go(); // todo replace with spectating.
                    }
                }
            }
        }

        private static async Task ManageTeamsAndBlips()
        {
            if (!gameRestarting)
            {
                SetPlayerTeam(PlayerId(), team);
                int tmp_runners = 0;
                int tmp_hunters = 0;

                PlayerList list = new PlayerList();
                //int active_players = list.Count();
                foreach (Player p in list)
                {
                    int tmp_team = 0;
                    if (GetPlayerTeam(p.Handle) == 2) // hunters
                    {
                        tmp_hunters++;
                        tmp_team = 2;
                    }
                    else if (GetPlayerTeam(p.Handle) == 1) // runners
                    {
                        tmp_runners++;
                        tmp_team = 1;
                    }
                    //else //(GetPlayerTeam(p.Handle) == 3)
                    //{
                    //    active_players--;
                    //}

                    if (p.Handle != PlayerId())
                    {
                        if (p.Character.AttachedBlip == null)
                        {
                            p.Character.AttachBlip();
                        }
                        var b = p.Character.AttachedBlip;
                        if (tmp_team == 1 || tmp_team == 4)
                        {
                            b.Sprite = (BlipSprite)532;
                            b.Color = BlipColor.Blue;
                        }
                        else if (tmp_team == 2)
                        {
                            b.Sprite = (BlipSprite)580;
                            b.Color = BlipColor.Red;
                        }
                        else if (tmp_team == 3)
                        {
                            b.Sprite = BlipSprite.Dead;
                            b.Color = BlipColor.White;
                        }
                        b.Rotation = (int)p.Character.Heading;
                        b.Name = p.Name;
                        SetBlipCategory(b.Handle, 7);
                    }
                }
                await Delay(0);
                if (tmp_runners == 0 && NetworkGetNumConnectedPlayers() > 1) // hunters won
                {
                    if (team == 2)
                    {
                        await GfxController.ShowFinishScaleform(false);
                        team = 1; // switch teams to become a runner.
                    }
                    else
                    {
                        await GfxController.ShowFinishScaleform(true);
                        team = 2; // switch teams to become a hunter.
                    }

                    //await Delay(10000);
                    //Go();
                    //Debug.WriteLine("Calling Go() because the hunters won. (all runners died)");
                }
                hunters = tmp_hunters;
                runners = tmp_runners;
                await Delay(10);
            }

        }

        private static async Task ManageGameTimer()
        {
            if (gameTimeRunning)
            {
                int tmpTimer = GetGameTimer();
                while (GetGameTimer() - tmpTimer < 1000)
                {
                    await Delay(0);
                }
                //await Delay(1000);
                gameTime++;
            }
            minutes = gameTime / 60;
            seconds = gameTime % 60;
            gameTimeString = $"{((minutes < 10) ? ("0" + minutes.ToString()) : minutes.ToString())}:{((seconds < 10) ? ("0" + seconds.ToString()) : seconds.ToString())}";

        }

        private static void CreateCheckpoints()
        {
            if (team == 1) // runners
            {
                float size = 5f;
                for (var i = 0; i < checkPoints.Count; i++)// cp in checkPoints)
                {
                    var cp = checkPoints[i];

                    if (cp.Id == 0) // start
                    {

                    }
                    else // non-start location
                    {
                        int blip = AddBlipForCoord(cp.Location.X, cp.Location.Y, cp.Location.Z);
                        if (cp.Id + 1 == checkPoints.Count) // finish line
                        {
                            int cphandle = CreateCheckpoint(cp.Type, cp.Location.X, cp.Location.Y, cp.Location.Z - 1f, cp.Location.X, cp.Location.Y, cp.Location.Z + 1f, size, cp.Red, cp.Green, cp.Blue, cp.Alpha, 0);

                            SetBlipSprite(blip, 38);
                            checkPoints[i] = new CheckPoint() { Blip = blip, CpHandle = cphandle, Alpha = cp.Alpha, Blue = cp.Blue, Green = cp.Green, Id = cp.Id, Location = cp.Location, Red = cp.Red, Type = cp.Type };
                        }
                        else // regular checkpoint
                        {
                            var nextCp = checkPoints[i + 1];
                            int cphandle = CreateCheckpoint(cp.Type, cp.Location.X, cp.Location.Y, cp.Location.Z - 1f, nextCp.Location.X, nextCp.Location.Y, nextCp.Location.Z + 1f, size, cp.Red, cp.Green, cp.Blue, cp.Alpha, 0);
                            SetBlipSprite(blip, 614);
                            checkPoints[i] = new CheckPoint() { Blip = blip, CpHandle = cphandle, Alpha = cp.Alpha, Blue = cp.Blue, Green = cp.Green, Id = cp.Id, Location = cp.Location, Red = cp.Red, Type = cp.Type };
                        }
                    }
                }
            }
            else if (team == 2) // hunters
            {

            }


        }

        private static async Task ManageAiDensity()
        {
            SetParkedVehicleDensityMultiplierThisFrame(0f);
            SetPedDensityMultiplierThisFrame(0f);
            SetRandomVehicleDensityMultiplierThisFrame(0f);
            SetScenarioPedDensityMultiplierThisFrame(0f, 0f);
            SetSomeVehicleDensityMultiplierThisFrame(0f);
            SetVehicleDensityMultiplierThisFrame(0f);

            NetworkSetFriendlyFireOption(true);

            if (ReturnFalse()) // used to stop the annoying async warnings
            {
                await Delay(0);
            }
        }

        private static async Task DrawHudOverlays()
        {
            GfxController.DrawStatBar("TIME", gameTimeString, 0);
            GfxController.DrawStatBar("~b~RUNNERS", "{car}" + runners, 1);
            GfxController.DrawStatBar("~r~HUNTERS", "{plane}" + hunters, 2);

            //SetStreamedTextureDictAsNoLongerNeeded("timerbars");
            //SetStreamedTextureDictAsNoLongerNeeded("mpleaderboard");

            if (ReturnFalse()) // used to stop the annoying async warnings
            {
                await Delay(0);
            }
        }

        private static bool ReturnFalse()
        {
            return false;
        }


        private static async Task ManageCheckpointProgress()
        {
            if (team == 1)
            {
                if (progress != -1)
                {
                    var cp = checkPoints[progress];

                    float dist = Vdist2(Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y, Game.PlayerPed.Position.Z, cp.Location.X, cp.Location.Y, cp.Location.Z);
                    if (dist < 40)
                    {
                        if (progress == 0 && IsScreenFadedIn())
                        {
                            //Game.PlayerPed.CurrentVehicle.IsPositionFrozen = true;
                            //await Delay(1000);
                            //SoundController.PlayGameSound(SoundController.GameSounds.race_countdown);
                            //await Delay(1000);
                            //SoundController.PlayGameSound(SoundController.GameSounds.race_countdown);
                            //await Delay(1000);
                            //SoundController.PlayGameSound(SoundController.GameSounds.race_countdown);
                            //await Delay(1000);
                            //SoundController.PlayGameSound(SoundController.GameSounds.race_go);
                            //Game.PlayerPed.CurrentVehicle.IsPositionFrozen = false;
                        }
                        else
                        {
                            if (DoesBlipExist(cp.Blip))
                            {
                                RemoveBlip(ref cp.Blip);
                            }
                            DeleteCheckpoint(cp.CpHandle);
                            SoundController.PlayGameSound(SoundController.GameSounds.checkpoint_reached);
                        }


                        if (progress + 1 != checkPoints.Count)
                        {
                            progress++;
                            if (DoesBlipExist(checkPoints[progress].Blip))
                            {
                                SetBlipRoute(checkPoints[progress].Blip, true);
                            }
                        }
                        else
                        {
                            progress = -1;
                            await Delay(0);
                            // player won.
                            StartScreenEffect("MenuMGTournamentTint", -1, false);
                            SoundController.PlayGameSound(SoundController.GameSounds.race_won);
                            StartAudioScene("MP_LEADERBOARD_SCENE");

                            int timer = GetGameTimer();
                            while (GetGameTimer() - timer < 1000)
                            {
                                await Delay(0);
                                HideHudAndRadarThisFrame();
                            }

                            SetFrozenRenderingDisabled(false);

                            StopScreenEffect("MenuMGTournamentTint");
                            Game.PlayerPed.CurrentVehicle.IsPositionFrozen = true;
                            team = 4;

                            timer = GetGameTimer();
                            while (GetGameTimer() - timer < 3000)
                            {
                                await Delay(0);
                                HideHudAndRadarThisFrame();
                            }
                            DoScreenFadeOut(1000);
                            while (!IsScreenFadedOut())
                            {
                                await Delay(0);
                                HideHudAndRadarThisFrame();
                            }
                            ResetCheckpoints();
                            CreateCheckpoints();
                            team = 2;
                            Go();
                            SetFrozenRenderingDisabled(true);
                            Debug.WriteLine("Calling Go() because the runners won.");
                            timer = GetGameTimer();
                            while (GetGameTimer() - timer < 500)
                            {
                                await Delay(0);
                                HideHudAndRadarThisFrame();
                            }
                            DoScreenFadeIn(1000);
                            progress = 0;
                        }
                    }
                }
            }
            else if (team == 2)
            {

            }
        }



        private static async Task ManageMiniMap()
        {
            if (Game.IsControlJustPressed(0, Control.MultiplayerInfo) && !IsPauseMenuActive())
            {
                int timer = GetGameTimer();
                bool bigMap = false;
                while (GetGameTimer() - timer < 10000)
                {
                    await Delay(0);
                    if (Game.IsControlJustPressed(0, Control.MultiplayerInfo) && !IsPauseMenuActive())
                    {
                        bigMap = !bigMap;
                    }
                    SetRadarBigmapEnabled(bigMap, false);
                    SetRadarZoomLevelThisFrame(180f);
                }
                SetRadarBigmapEnabled(false, false);
            }
        }



        private static async Task ManageHudInfo()
        {

            if (team == 2) // hunter
            {
                BeginTextCommandDisplayHelp("hvsr_hunter_info");
                EndTextCommandDisplayHelp(0, true, false, 950);
            }
            else if (team == 1) // runner
            {
                BeginTextCommandDisplayHelp("hvsr_runner_info");
                EndTextCommandDisplayHelp(0, true, false, 950);
            }
            await Delay(1000);
        }

        private static async Task MangeGamePlay()
        {
            if (firstTick)
            {
                firstTick = false;
                while (IsPlayerSwitchInProgress() || !IsScreenFadedIn())
                {
                    await Delay(0);
                }
                //int tmp_runners_count = 0;
                //int tmp_hunters_count = 0;
                if (GetPlayerTeam(PlayerId()) != 1 && GetPlayerTeam(PlayerId()) != 2)
                {
                    await Delay(1000);
                    //foreach (Player p in new PlayerList())
                    //{
                    //    if (p.Handle != PlayerId())
                    //    {
                    //        if (GetPlayerTeam(p.Handle) == 1)
                    //        {
                    //            tmp_runners_count++;
                    //        }
                    //        else if (GetPlayerTeam(p.Handle) == 2)
                    //        {
                    //            tmp_hunters_count++;
                    //        }
                    //    }
                    //}
                    if (runners > hunters) // if there are more runners than hunters, become a hunter.
                    {
                        team = 2;
                    }
                    else
                    {
                        team = 1; // if there are an equal amount of runners and hunters or if there are more hunters, become a runner.
                    }
                }

                Go();
                Debug.WriteLine("Calling Go() because it's the first tick.");
            }

            if (Game.PlayerPed.IsInVehicle())
            {
                //Debug.WriteLine("a");
                playerVehicle = Game.PlayerPed.CurrentVehicle;
                if (playerVehicle != null && playerVehicle.Exists())
                {
                    //Debug.WriteLine("b");
                    //playerVehicle.IsRadioEnabled = false;
                    //veh.RadioStation = RadioStation.RadioOff;
                    SetVehicleRadioEnabled(playerVehicle.Handle, false);

                    if (team == 2) // hunter
                    {
                        SoundController.TriggerSuspenseMusicEvent();
                        //Debug.WriteLine("c");

                        if (Hunter.bombPlanes.ContainsKey((uint)playerVehicle.Model.Hash))
                        {
                            //Debug.WriteLine("d");
                            Game.DisableControlThisFrame(0, Control.VehicleFlyAttack); // disable vehicle weapons from being fired. (114)
                            if (GetVehicleMod(playerVehicle.Handle, 9) > -1)
                            {
                                int timer = GetGameTimer();
                                bool toggle = false;
                                //Debug.WriteLine("e");
                                while (Game.IsControlPressed(0, (Control)355)) // INPUT_VEH_FLY_BOMB_BAY (not in the api set so have to cast it to Control manually)
                                {
                                    if (GetGameTimer() - timer > 500)
                                    {
                                        toggle = true;
                                        break;
                                    }

                                    await Delay(0);
                                }

                                if (toggle)
                                {
                                    if (AreBombBayDoorsOpen(playerVehicle))
                                    {
                                        CloseBombBayDoors(playerVehicle.Handle);
                                        ClearPedTasks(PlayerPedId());
                                        SoundController.ToggleBombCameraAudioScene(false);
                                        SetCamActive(GetBombCamera(), false);
                                        RenderScriptCams(false, false, 0, false, false);

                                        DestroyCam(GetBombCamera(), true);
                                        DestroyAllCams(true); // just in case some random case where the above native doesn't remove the cam.
                                        SetPlaneTurbulenceMultiplier(playerVehicle.Handle, 0.1f);
                                    }
                                    else
                                    {
                                        OpenBombBayDoors(playerVehicle.Handle);


                                        SetCamActive(GetBombCamera(), true);

                                        Vector3 bombCamPos = new Vector3(0f, 0f, 0f);
                                        Hunter.GetBombCameraAttachPosition(ref bombCamPos);
                                        AttachCamToEntity(GetBombCamera(), playerVehicle.Handle, bombCamPos.X, bombCamPos.Y, bombCamPos.Z, true);

                                        RenderScriptCams(true, false, 0, false, false);


                                        Vector3 taskFlyDestinationCoords = GetOffsetFromEntityInWorldCoords(playerVehicle.Handle, 0f, 5000f, 0f);
                                        if (IsThisModelAPlane((uint)playerVehicle.Model.Hash))
                                        {
                                            TaskPlaneMission(PlayerPedId(), playerVehicle.Handle, 0, 0, taskFlyDestinationCoords.X, taskFlyDestinationCoords.Y, taskFlyDestinationCoords.Z, 4, 30f, 0.1f, playerVehicle.Heading, 30f, 20f);
                                            //SetPlaneMinHeightAboveGround(veh.Handle, 30);
                                            // BRAIN::TASK_PLANE_MISSION(PLAYER::PLAYER_PED_ID(), iParam0, 0, 0, vLocal_12678.x, vLocal_12678.y, vVar1.z, 4, 50f, 0.1f, -1f, 30, 20, 1);
                                        }
                                        SoundController.ToggleBombCameraAudioScene(true);
                                        SetPlaneTurbulenceMultiplier(playerVehicle.Handle, 0f);
                                    }
                                }

                                while (Game.IsControlPressed(0, (Control)355)) // INPUT_VEH_FLY_BOMB_BAY (not in the api set so have to cast it to Control manually)
                                {
                                    await Delay(0);
                                    Game.DisableControlThisFrame(0, Control.VehicleFlyAttack); // disable vehicle weapons from being fired. (114)
                                }

                                if (AreBombBayDoorsOpen(playerVehicle))
                                {
                                    Game.DisableControlThisFrame(0, Control.VehicleFlyAttack); // disable vehicle weapons from being fired. (114)

                                    if (Game.IsDisabledControlJustReleased(0, Control.VehicleFlyAttack))
                                    {
                                        Hunter.DropBomb(playerVehicle, Hunter.GetBombTypeFromVehicle(playerVehicle));
                                        SoundController.PlayGameSoundFromEntity(SoundController.GameSounds.bomb_deployed, playerVehicle);

                                        int timer2 = GetGameTimer();
                                        while (GetGameTimer() - timer2 < 1000)
                                        {
                                            await Delay(0);

                                            Game.DisableControlThisFrame(0, Control.VehicleFlyAttack); // disable vehicle weapons from being fired. (114)
                                            if (Game.IsDisabledControlJustReleased(0, Control.VehicleFlyAttack))
                                            {
                                                if (GetGameTimer() - Timera() > 800)
                                                {
                                                    SoundController.PlayGameSound(SoundController.GameSounds.bomb_empty);
                                                    Settimera(GetGameTimer());
                                                }

                                            }
                                        }
                                    }
                                }
                                else // bomb bay doors are closed
                                {
                                }
                            }
                        }
                    }
                    else if (team == 1) // runner
                    {
                    }
                }

            }
            else
            {
                SetCamActive(GetBombCamera(), false);
                RenderScriptCams(false, false, 0, false, false);

                DestroyCam(GetBombCamera(), true);
                DestroyAllCams(true); // just in case some random case where the above native doesn't remove the cam.
            }
        }

        private static int _camera = 0;

        private static int GetBombCamera()
        {
            if (!DoesCamExist(_camera))
            {
                _camera = CreateCameraWithParams(26379945, 0f, 0f, 0f, -90f, 0f, GetEntityHeading(PlayerPedId()), 65f, true, 2);
            }
            return _camera;
        }
        */
    }
}
