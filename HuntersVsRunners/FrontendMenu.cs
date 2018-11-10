using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace HuntersVsRunners
{
    public enum FrontendType
    {
        FE_MENU_VERSION_SP_PAUSE,
        FE_MENU_VERSION_MP_PAUSE,
        FE_MENU_VERSION_CREATOR_PAUSE,
        FE_MENU_VERSION_CUTSCENE_PAUSE,
        FE_MENU_VERSION_SAVEGAME,
        FE_MENU_VERSION_PRE_LOBBY,
        FE_MENU_VERSION_LOBBY,
        FE_MENU_VERSION_MP_CHARACTER_SELECT,
        FE_MENU_VERSION_MP_CHARACTER_CREATION,
        FE_MENU_VERSION_EMPTY,
        FE_MENU_VERSION_EMPTY_NO_BACKGROUND,
        FE_MENU_VERSION_TEXT_SELECTION,
        FE_MENU_VERSION_CORONA,
        FE_MENU_VERSION_CORONA_LOBBY,
        FE_MENU_VERSION_CORONA_JOINED_PLAYERS,
        FE_MENU_VERSION_CORONA_INVITE_PLAYERS,
        FE_MENU_VERSION_CORONA_INVITE_FRIENDS,
        FE_MENU_VERSION_CORONA_INVITE_CREWS,
        FE_MENU_VERSION_CORONA_INVITE_MATCHED_PLAYERS,
        FE_MENU_VERSION_CORONA_INVITE_LAST_JOB_PLAYERS,
        FE_MENU_VERSION_CORONA_RACE,
        FE_MENU_VERSION_CORONA_BETTING,
        FE_MENU_VERSION_JOINING_SCREEN,
        FE_MENU_VERSION_LANDING_MENU,
        FE_MENU_VERSION_LANDING_KEYMAPPING_MENU,
    };

    public enum PlayerIcon
    {
        NONE = 0,
        VOICE_ACTIVE = 47,
        VOICE_IDLE = 48,
        VOICE_MUTED = 49,
        GTA_V_LOGO = 54,
        GLOBE = 63,
        KICK_BOOT = 64,
        FREEMODE_RANK = 65,
        SPECTATOR_EYE = 66,
        GAME_PAD = 119,
        MOUSE = 120
    };

    public enum RowColor
    {
        RED
    }

    public enum StatusColor
    {
        RED
    }

    public enum HudColor
    {
        HUD_COLOUR_PURE_WHITE,
        HUD_COLOUR_WHITE,
        HUD_COLOUR_BLACK,
        HUD_COLOUR_GREY,
        HUD_COLOUR_GREYLIGHT,
        HUD_COLOUR_GREYDARK,
        HUD_COLOUR_RED,
        HUD_COLOUR_REDLIGHT,
        HUD_COLOUR_REDDARK,
        HUD_COLOUR_BLUE,
        HUD_COLOUR_BLUELIGHT,
        HUD_COLOUR_BLUEDARK,
        HUD_COLOUR_YELLOW,
        HUD_COLOUR_YELLOWLIGHT,
        HUD_COLOUR_YELLOWDARK,
        HUD_COLOUR_ORANGE,
        HUD_COLOUR_ORANGELIGHT,
        HUD_COLOUR_ORANGEDARK,
        HUD_COLOUR_GREEN,
        HUD_COLOUR_GREENLIGHT,
        HUD_COLOUR_GREENDARK,
        HUD_COLOUR_PURPLE,
        HUD_COLOUR_PURPLELIGHT,
        HUD_COLOUR_PURPLEDARK,
        HUD_COLOUR_PINK,
        HUD_COLOUR_RADAR_HEALTH,
        HUD_COLOUR_RADAR_ARMOUR,
        HUD_COLOUR_RADAR_DAMAGE,
        HUD_COLOUR_NET_PLAYER1,
        HUD_COLOUR_NET_PLAYER2,
        HUD_COLOUR_NET_PLAYER3,
        HUD_COLOUR_NET_PLAYER4,
        HUD_COLOUR_NET_PLAYER5,
        HUD_COLOUR_NET_PLAYER6,
        HUD_COLOUR_NET_PLAYER7,
        HUD_COLOUR_NET_PLAYER8,
        HUD_COLOUR_NET_PLAYER9,
        HUD_COLOUR_NET_PLAYER10,
        HUD_COLOUR_NET_PLAYER11,
        HUD_COLOUR_NET_PLAYER12,
        HUD_COLOUR_NET_PLAYER13,
        HUD_COLOUR_NET_PLAYER14,
        HUD_COLOUR_NET_PLAYER15,
        HUD_COLOUR_NET_PLAYER16,
        HUD_COLOUR_NET_PLAYER17,
        HUD_COLOUR_NET_PLAYER18,
        HUD_COLOUR_NET_PLAYER19,
        HUD_COLOUR_NET_PLAYER20,
        HUD_COLOUR_NET_PLAYER21,
        HUD_COLOUR_NET_PLAYER22,
        HUD_COLOUR_NET_PLAYER23,
        HUD_COLOUR_NET_PLAYER24,
        HUD_COLOUR_NET_PLAYER25,
        HUD_COLOUR_NET_PLAYER26,
        HUD_COLOUR_NET_PLAYER27,
        HUD_COLOUR_NET_PLAYER28,
        HUD_COLOUR_NET_PLAYER29,
        HUD_COLOUR_NET_PLAYER30,
        HUD_COLOUR_NET_PLAYER31,
        HUD_COLOUR_NET_PLAYER32,
        HUD_COLOUR_SIMPLEBLIP_DEFAULT,
        HUD_COLOUR_MENU_BLUE,
        HUD_COLOUR_MENU_GREY_LIGHT,
        HUD_COLOUR_MENU_BLUE_EXTRA_DARK,
        HUD_COLOUR_MENU_YELLOW,
        HUD_COLOUR_MENU_YELLOW_DARK,
        HUD_COLOUR_MENU_GREEN,
        HUD_COLOUR_MENU_GREY,
        HUD_COLOUR_MENU_GREY_DARK,
        HUD_COLOUR_MENU_HIGHLIGHT,
        HUD_COLOUR_MENU_STANDARD,
        HUD_COLOUR_MENU_DIMMED,
        HUD_COLOUR_MENU_EXTRA_DIMMED,
        HUD_COLOUR_BRIEF_TITLE,
        HUD_COLOUR_MID_GREY_MP,
        HUD_COLOUR_NET_PLAYER1_DARK,
        HUD_COLOUR_NET_PLAYER2_DARK,
        HUD_COLOUR_NET_PLAYER3_DARK,
        HUD_COLOUR_NET_PLAYER4_DARK,
        HUD_COLOUR_NET_PLAYER5_DARK,
        HUD_COLOUR_NET_PLAYER6_DARK,
        HUD_COLOUR_NET_PLAYER7_DARK,
        HUD_COLOUR_NET_PLAYER8_DARK,
        HUD_COLOUR_NET_PLAYER9_DARK,
        HUD_COLOUR_NET_PLAYER10_DARK,
        HUD_COLOUR_NET_PLAYER11_DARK,
        HUD_COLOUR_NET_PLAYER12_DARK,
        HUD_COLOUR_NET_PLAYER13_DARK,
        HUD_COLOUR_NET_PLAYER14_DARK,
        HUD_COLOUR_NET_PLAYER15_DARK,
        HUD_COLOUR_NET_PLAYER16_DARK,
        HUD_COLOUR_NET_PLAYER17_DARK,
        HUD_COLOUR_NET_PLAYER18_DARK,
        HUD_COLOUR_NET_PLAYER19_DARK,
        HUD_COLOUR_NET_PLAYER20_DARK,
        HUD_COLOUR_NET_PLAYER21_DARK,
        HUD_COLOUR_NET_PLAYER22_DARK,
        HUD_COLOUR_NET_PLAYER23_DARK,
        HUD_COLOUR_NET_PLAYER24_DARK,
        HUD_COLOUR_NET_PLAYER25_DARK,
        HUD_COLOUR_NET_PLAYER26_DARK,
        HUD_COLOUR_NET_PLAYER27_DARK,
        HUD_COLOUR_NET_PLAYER28_DARK,
        HUD_COLOUR_NET_PLAYER29_DARK,
        HUD_COLOUR_NET_PLAYER30_DARK,
        HUD_COLOUR_NET_PLAYER31_DARK,
        HUD_COLOUR_NET_PLAYER32_DARK,
        HUD_COLOUR_BRONZE,
        HUD_COLOUR_SILVER,
        HUD_COLOUR_GOLD,
        HUD_COLOUR_PLATINUM,
        HUD_COLOUR_GANG1,
        HUD_COLOUR_GANG2,
        HUD_COLOUR_GANG3,
        HUD_COLOUR_GANG4,
        HUD_COLOUR_SAME_CREW,
        HUD_COLOUR_FREEMODE,
        HUD_COLOUR_PAUSE_BG,
        HUD_COLOUR_FRIENDLY,
        HUD_COLOUR_ENEMY,
        HUD_COLOUR_LOCATION,
        HUD_COLOUR_PICKUP,
        HUD_COLOUR_PAUSE_SINGLEPLAYER,
        HUD_COLOUR_FREEMODE_DARK,
        HUD_COLOUR_INACTIVE_MISSION,
        HUD_COLOUR_DAMAGE,
        HUD_COLOUR_PINKLIGHT,
        HUD_COLOUR_PM_MITEM_HIGHLIGHT,
        HUD_COLOUR_SCRIPT_VARIABLE,
        HUD_COLOUR_YOGA,
        HUD_COLOUR_TENNIS,
        HUD_COLOUR_GOLF,
        HUD_COLOUR_SHOOTING_RANGE,
        HUD_COLOUR_FLIGHT_SCHOOL,
        HUD_COLOUR_NORTH_BLUE,
        HUD_COLOUR_SOCIAL_CLUB,
        HUD_COLOUR_PLATFORM_BLUE,
        HUD_COLOUR_PLATFORM_GREEN,
        HUD_COLOUR_PLATFORM_GREY,
        HUD_COLOUR_FACEBOOK_BLUE,
        HUD_COLOUR_INGAME_BG,
        HUD_COLOUR_DARTS,
        HUD_COLOUR_WAYPOINT,
        HUD_COLOUR_MICHAEL,
        HUD_COLOUR_FRANKLIN,
        HUD_COLOUR_TREVOR,
        HUD_COLOUR_GOLF_P1,
        HUD_COLOUR_GOLF_P2,
        HUD_COLOUR_GOLF_P3,
        HUD_COLOUR_GOLF_P4,
        HUD_COLOUR_WAYPOINTLIGHT,
        HUD_COLOUR_WAYPOINTDARK,
        HUD_COLOUR_PANEL_LIGHT,
        HUD_COLOUR_MICHAEL_DARK,
        HUD_COLOUR_FRANKLIN_DARK,
        HUD_COLOUR_TREVOR_DARK,
        HUD_COLOUR_OBJECTIVE_ROUTE,
        HUD_COLOUR_PAUSEMAP_TINT,
        HUD_COLOUR_PAUSE_DESELECT,
        HUD_COLOUR_PM_WEAPONS_PURCHASABLE,
        HUD_COLOUR_PM_WEAPONS_LOCKED,
        HUD_COLOUR_END_SCREEN_BG,
        HUD_COLOUR_CHOP,
        HUD_COLOUR_PAUSEMAP_TINT_HALF,
        HUD_COLOUR_NORTH_BLUE_OFFICIAL,
        HUD_COLOUR_SCRIPT_VARIABLE_2,
        HUD_COLOUR_H,
        HUD_COLOUR_HDARK,
        HUD_COLOUR_T,
        HUD_COLOUR_TDARK,
        HUD_COLOUR_HSHARD,
        HUD_COLOUR_CONTROLLER_MICHAEL,
        HUD_COLOUR_CONTROLLER_FRANKLIN,
        HUD_COLOUR_CONTROLLER_TREVOR,
        HUD_COLOUR_CONTROLLER_CHOP,
        HUD_COLOUR_VIDEO_EDITOR_VIDEO,
        HUD_COLOUR_VIDEO_EDITOR_AUDIO,
        HUD_COLOUR_VIDEO_EDITOR_TEXT,
        HUD_COLOUR_HB_BLUE,
        HUD_COLOUR_HB_YELLOW,
        HUD_COLOUR_VIDEO_EDITOR_SCORE,
        HUD_COLOUR_VIDEO_EDITOR_AUDIO_FADEOUT,
        HUD_COLOUR_VIDEO_EDITOR_TEXT_FADEOUT,
        HUD_COLOUR_VIDEO_EDITOR_SCORE_FADEOUT,
        HUD_COLOUR_HEIST_BACKGROUND,
        HUD_COLOUR_VIDEO_EDITOR_AMBIENT,
        HUD_COLOUR_VIDEO_EDITOR_AMBIENT_FADEOUT,
        HUD_COLOUR_GB,
        HUD_COLOUR_G,
        HUD_COLOUR_B,
        HUD_COLOUR_LOW_FLOW,
        HUD_COLOUR_LOW_FLOW_DARK,
        HUD_COLOUR_G1,
        HUD_COLOUR_G2,
        HUD_COLOUR_G3,
        HUD_COLOUR_G4,
        HUD_COLOUR_G5,
        HUD_COLOUR_G6,
        HUD_COLOUR_G7,
        HUD_COLOUR_G8,
        HUD_COLOUR_G9,
        HUD_COLOUR_G10,
        HUD_COLOUR_G11,
        HUD_COLOUR_G12,
        HUD_COLOUR_G13,
        HUD_COLOUR_G14,
        HUD_COLOUR_G15,
        HUD_COLOUR_ADVERSARY,
        HUD_COLOUR_DEGEN_RED,
        HUD_COLOUR_DEGEN_YELLOW,
        HUD_COLOUR_DEGEN_GREEN,
        HUD_COLOUR_DEGEN_CYAN,
        HUD_COLOUR_DEGEN_BLUE,
        HUD_COLOUR_DEGEN_MAGENTA,
        HUD_COLOUR_STUNT_1,
        HUD_COLOUR_STUNT_2
    }

    public struct PlayerRow
    {
        public int RowIndex { get; set; }
        public int PlayerRank { get; set; }
        public Player Player { get; set; }
        public string Status { get; set; }
        public string CrewTag { get; set; }
        public PlayerIcon Icon { get; set; }
        public HudColor RowColor { get; set; }
        public HudColor StatusColor { get; set; }
        public PlayerRow(int index, int rank, Player player, string status, string crewTag, PlayerIcon icon, HudColor rowColor, HudColor statusColor)
        {
            this.RowIndex = index;
            this.PlayerRank = rank;
            this.Player = player;
            this.Status = status;
            this.CrewTag = crewTag;
            this.Icon = icon;
            this.RowColor = rowColor;
            this.StatusColor = statusColor;
        }
    }

    public struct SettingsItem
    {
        public int RowIndex;
        public List<string> SelectionItems;
        public string Title;
        public string Description;
        public int SelectedIndex;
        public bool Selectable;
        public int Type;
        public int RowColor;

        public SettingsItem(int row, List<string> items, string title, string description, int selectedIndex, bool selectable, int type, int rowColor)
        {
            this.RowIndex = row;
            this.SelectionItems = items;
            this.Title = title;
            this.Description = description;
            this.SelectedIndex = selectedIndex;
            this.Selectable = selectable;
            this.Type = type;
            this.RowColor = rowColor;
        }
    }


    public class FrontendMenu
    {
        #region constructors
        /// <summary>
        /// FrontendMenu class constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="menuType"></param>
        public FrontendMenu(string name, FrontendType menuType) : this(name, "", menuType) { }

        /// <summary>
        /// FrontendMenu class constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subtitle"></param>
        /// <param name="menuType"></param>
        public FrontendMenu(string name, string subtitle, FrontendType menuType)
        {
            this.name = name;
            this.subtitle = subtitle;
            this.menuType = menuType;
        }
        #endregion


        #region private variables
        private string name;
        private string subtitle;
        private readonly int id = 0;
        private readonly FrontendType menuType;
        private List<PlayerRow> playerRows = new List<PlayerRow>();
        public List<SettingsItem> settingsList = new List<SettingsItem>();
        #endregion

        #region public variables
        public bool IsVisible
        {
            get
            {
                return (IsPauseMenuActive() || IsPauseMenuRestarting()) && GetCurrentFrontendMenu() == GetHashKey(Enum.GetName(typeof(FrontendType), menuType));
            }
        }
        #endregion


        #region public getter functions
        /// <summary>
        /// Get the name/title of this frontend menu.
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Get the subtitle of this frontend menu.
        /// </summary>
        /// <returns></returns>
        public string GetSubtitle()
        {
            return subtitle;
        }

        /// <summary>
        /// Get the ID of this frontend menu.
        /// </summary>
        /// <returns></returns>
        public int GetID()
        {
            return id;
        }

        public int GetNumPlayerRows()
        {
            return playerRows.Count;
        }
        #endregion

        #region public setter functions
        /// <summary>
        /// Set the title/name of this frontend menu.
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Set the subtitle of this frontend menu.
        /// </summary>
        /// <param name="subtitle"></param>
        public void SetSubtitle(string subtitle)
        {
            this.subtitle = subtitle;
        }
        #endregion


        #region main public functions
        public async void UpdatePlayers()
        {
            SetColumnTitle(1, $"PLAYERS ~1~ OF 2-12", NetworkGetNumConnectedPlayers());

            PlayerList list = new PlayerList();
            foreach (Player p in list)
            {
                if (playerRows.Any(Row => Row.Player == p))
                {
                    UpdatePlayer(p, p.ServerId, "JOINED", "SNAIL", PlayerIcon.FREEMODE_RANK, HudColor.HUD_COLOUR_FREEMODE, HudColor.HUD_COLOUR_GREEN);
                }
                else
                {
                    AddPlayer(p, p.ServerId, "JOINED", "SNAIL", PlayerIcon.FREEMODE_RANK, HudColor.HUD_COLOUR_FREEMODE, HudColor.HUD_COLOUR_GREEN);
                }
            }
            await BaseScript.Delay(0);
            List<PlayerRow> tmp_delete_rows_list = new List<PlayerRow>();
            foreach (PlayerRow pr in playerRows)
            {
                if (!(list.Any(P => P == pr.Player)))
                {
                    tmp_delete_rows_list.Add(pr);
                }
            }
            await BaseScript.Delay(0);
            if (tmp_delete_rows_list.Count > 0)
            {
                foreach (PlayerRow pr in tmp_delete_rows_list)
                {
                    DeletePlayer(pr.RowIndex);
                }
            }
        }

        public async void AddPlayer(Player player, int rank, string status, string crewTag, PlayerIcon icon, HudColor rowColor, HudColor statusColor)
        {
            if (!IsVisible)
            {
                await ToggleMenu();
            }
            playerRows.Add(new PlayerRow(index: playerRows.Count, rank: rank, player: player, status: status, crewTag: crewTag, icon: icon, rowColor: rowColor, statusColor: statusColor));
            //await BaseScript.Delay(100);
            await UpdateList();
        }

        public async void UpdatePlayer(int index, int rank, string status, string crewTag, PlayerIcon icon, HudColor rowColor, HudColor statusColor)
        {
            PlayerRow p = playerRows[index];
            p.Status = status;
            p.PlayerRank = rank;
            p.CrewTag = crewTag;
            p.Icon = icon;
            p.RowColor = rowColor;
            p.StatusColor = statusColor;
            await UpdateList();
        }

        public async void UpdatePlayer(Player player, int rank, string status, string crewTag, PlayerIcon icon, HudColor rowColor, HudColor statusColor)
        {
            PlayerRow p = playerRows.Find(pr => { return pr.Player.ServerId == player.ServerId; });
            p.Status = status;
            p.PlayerRank = rank;
            p.CrewTag = crewTag;
            p.Icon = icon;
            p.RowColor = rowColor;
            p.StatusColor = statusColor;
            await UpdateList();
        }

        public async void DeletePlayer(int index)
        {
            playerRows.RemoveAt(index);
            PushScaleformMovieFunctionN("SET_DATA_SLOT_EMPTY");
            PushScaleformMovieFunctionParameterInt(3);
            PushScaleformMovieFunctionParameterInt(GetNumPlayerRows() - 1);
            PopScaleformMovieFunctionVoid();
            await UpdateList();
        }

        private async Task UpdateList()
        {
            //for (var i = 0; i < 32; i++)
            //{
            //    PushScaleformMovieFunctionN("SET_DATA_SLOT_EMPTY");
            //    PushScaleformMovieFunctionParameterInt(3);
            //    PushScaleformMovieFunctionParameterInt(i);
            //    PopScaleformMovieFunctionVoid();
            //}
            //await BaseScript.Delay(0);
            if (playerRows.Count > 0)
            {
                for (var i = 0; i < playerRows.Count; i++)
                {
                    PlayerRow pr = playerRows[i];
                    if (pr.RowIndex != i)
                    {
                        pr.RowIndex = i;
                        playerRows[i] = pr;
                    }

                    PushScaleformMovieFunctionN("SET_DATA_SLOT");                   // call scaleform function

                    PushScaleformMovieFunctionParameterInt(3);                      // frontend menu column
                    PushScaleformMovieFunctionParameterInt(i);                      // row index

                    PushScaleformMovieFunctionParameterInt(0);                      // menu ID
                    PushScaleformMovieFunctionParameterInt(0);                      // unique ID
                    PushScaleformMovieFunctionParameterInt(2);                      // type (2 = AS_ONLINE_IN_SESSION)

                    PushScaleformMovieFunctionParameterInt(pr.PlayerRank);          // rank value / (initialIndex 1337)
                    PushScaleformMovieFunctionParameterBool(false);                 // isSelectable

                    PushScaleformMovieFunctionParameterString(pr.Player.Name);      // playerName

                    PushScaleformMovieFunctionParameterInt((int)pr.RowColor);       // rowColor

                    PushScaleformMovieFunctionParameterBool(false);                 // reduceColors (if true: removes color from left bar & reduces color opacity on row itself.)

                    PushScaleformMovieFunctionParameterInt(0);                      // unused
                    PushScaleformMovieFunctionParameterInt((int)pr.Icon);           // right player icon.
                    PushScaleformMovieFunctionParameterInt(0);                      // unused

                    PushScaleformMovieFunctionParameterString($"..+{pr.CrewTag}");  // crew label text.

                    PushScaleformMovieFunctionParameterBool(false);                 // should be a thing to toggle blinking of (kick) icon, but doesn't seem to work.

                    PushScaleformMovieFunctionParameterString(pr.Status);           // badge/status tag text
                    PushScaleformMovieFunctionParameterInt((int)pr.StatusColor);    // badge/status tag background color

                    PopScaleformMovieFunctionVoid();                                // done

                }
            }
            else
            {
                await BaseScript.Delay(0);
            }
            //await BaseScript.Delay(500);

            //UpdateDetails();
            //PushScaleformMovieFunctionN("DISPLAY_DATA_SLOT");
            //PushScaleformMovieFunctionParameterInt(0);
            //PopScaleformMovieFunctionVoid();

            //UpdateSettings();
            //PushScaleformMovieFunctionN("DISPLAY_DATA_SLOT");
            //PushScaleformMovieFunctionParameterInt(1);
            //PopScaleformMovieFunctionVoid();

            //await UpdateList();
            PushScaleformMovieFunctionN("DISPLAY_DATA_SLOT");
            PushScaleformMovieFunctionParameterInt(3);
            PopScaleformMovieFunctionVoid();

            /// ACTIVATE THE FIRST COLUMN (FOCUS).
            PushScaleformMovieFunctionN("SET_COLUMN_FOCUS");
            PushScaleformMovieFunctionParameterInt(0); // column index // _loc7_
            PushScaleformMovieFunctionParameterBool(true); // highlightIndex // _loc6_
            PushScaleformMovieFunctionParameterBool(false); // scriptSetUniqID // _loc4_
            PushScaleformMovieFunctionParameterBool(false); // scriptSetMenuState // _loc5_
            PopScaleformMovieFunctionVoid();


        }

        public async Task ToggleMenu()
        {
            //IsVisible = !IsVisible;
            if (!IsVisible)
            {
                if (IsPauseMenuActive() || IsPauseMenuRestarting() || IsFrontendFading())
                {
                    SetFrontendActive(false);
                }
                while (IsPauseMenuActive() || IsPauseMenuRestarting() || IsFrontendFading())
                {
                    SetFrontendActive(false);
                    await BaseScript.Delay(0);
                }

                //RestartFrontendMenu(menuType == FrontendType.FE_MENU_VERSION_CORONA ? (uint)GetHashKey("FE_MENU_VERSION_CORONA") : (uint)GetHashKey("FE_MENU_VERSION_CORONA_RACE"), -1);
                RestartFrontendMenu((uint)GetHashKey(menuType.ToString()), -1);

                //AddFrontendMenuContext((uint)GetHashKey("FM_TUTORIAL"));
                //AddFrontendMenuContext((uint)GetHashKey("AUTOFILL_CORONA"));
                //AddFrontendMenuContext((uint)GetHashKey("CORONA_TOURNAMENT"));
                //AddFrontendMenuContext((uint)GetHashKey("AUTOFILL_CONTINUE"));
                //AddFrontendMenuContext((uint)GetHashKey("HUD_CASH_HEAD"));
                //AddFrontendMenuContext(2010410515);
                //ObjectDecalToggle((uint)Int64.Parse("-228602367"));

                //ActivateFrontendMenu(menuType == FrontendType.FE_MENU_VERSION_CORONA ? (uint)GetHashKey("FE_MENU_VERSION_CORONA") : (uint)GetHashKey("FE_MENU_VERSION_CORONA_RACE"), false, -1);
                ActivateFrontendMenu((uint)GetHashKey(menuType.ToString()), false, -1);

                // start a call
                while (!IsPauseMenuActive() || IsPauseMenuRestarting())
                {
                    await BaseScript.Delay(0);
                }

                //AddFrontendMenuContext((uint)GetHashKey("FM_TUTORIAL"));
                //AddFrontendMenuContext((uint)GetHashKey("AUTOFILL_CORONA"));
                //AddFrontendMenuContext((uint)GetHashKey("CORONA_TOURNAMENT"));
                //AddFrontendMenuContext((uint)GetHashKey("AUTOFILL_CONTINUE"));


                BeginScaleformMovieMethodV("SHIFT_CORONA_DESC");       // start call function - BeginScaleformMovieMethodV
                PushScaleformMovieFunctionParameterBool(true);          // push frontend title menu up.
                PushScaleformMovieFunctionParameterBool(false);         // show extra top border line
                PopScaleformMovieFunction();                        // end call function

                BeginScaleformMovieMethodV("SET_HEADER_TITLE");        // Call set header function

                //BeginTextCommandScaleformString("STRING");
                //AddTextComponentSubstringPlayerName(name);        // Set the title
                //EndTextCommandScaleformString();
                PushScaleformMovieFunctionParameterString(name);        // Set the title

                PushScaleformMovieFunctionParameterBool(false);         // purpose unknown, is always 0 in decompiled scripts.


                PushScaleformMovieFunctionParameterString(subtitle);    // set the subtitle.

                //BeginTextCommandScaleformString("STRING");
                //AddTextComponentSubstringPlayerName(subtitle);        // Set the subtitle
                //EndTextCommandScaleformString();

                PushScaleformMovieFunctionParameterBool(true);          // purpose unknown, is always 1 in decompiled scripts.
                PopScaleformMovieFunctionVoid();                        // finish the set header function






                //await BaseScript.Delay(500);
                await BaseScript.Delay(100);
                UpdateSettings();
                await BaseScript.Delay(100);
                PushScaleformMovieFunctionN("DISPLAY_DATA_SLOT");
                PushScaleformMovieFunctionParameterInt(0);
                PopScaleformMovieFunctionVoid();



                await SetDetailsMissionName("Hunters VS Runners", null, null);
                await BaseScript.Delay(100);
                UpdateDetails();
                await BaseScript.Delay(100);
                PushScaleformMovieFunctionN("DISPLAY_DATA_SLOT");
                PushScaleformMovieFunctionParameterInt(1);
                PopScaleformMovieFunctionVoid();

                //SetColumnTitle(1, "test", "test2");

                await BaseScript.Delay(100);
                await UpdateList();
                await BaseScript.Delay(100);
                PushScaleformMovieFunctionN("DISPLAY_DATA_SLOT");
                PushScaleformMovieFunctionParameterInt(3);
                PopScaleformMovieFunctionVoid();

                /// ACTIVATE THE FIRST COLUMN (FOCUS).
                await BaseScript.Delay(100);
                PushScaleformMovieFunctionN("SET_COLUMN_FOCUS");
                PushScaleformMovieFunctionParameterInt(0); // column index // _loc7_
                PushScaleformMovieFunctionParameterBool(true); // highlightIndex // _loc6_
                PushScaleformMovieFunctionParameterBool(true); // scriptSetUniqID // _loc4_
                PushScaleformMovieFunctionParameterBool(true); // scriptSetMenuState // _loc5_
                PopScaleformMovieFunctionVoid();
                //SetFrontendRadioActive(true);
                SoundController.TriggerSuspenseMusicEvent();
            }
            else
            {
                SetFrontendActive(false);
            }
        }

        public async Task<int> GetSelection()
        {
            BeginScaleformMovieMethodN("GET_COLUMN_SELECTION");
            PushScaleformMovieMethodParameterInt(0);
            var ret = EndScaleformMovieMethodReturn();

            int maxTimer = GetGameTimer();

            while (!GetScaleformMovieFunctionReturnBool(ret))
            {
                if (GetGameTimer() - maxTimer > 100)
                {
                    break;
                }
                await BaseScript.Delay(0);
            }

            var retInt = GetScaleformMovieFunctionReturnInt(ret);
            return retInt;
        }

        public void SetColumnTitle(int column, string title, int thing)
        {
            BeginScaleformMovieMethodN("SET_MENU_HEADER_TEXT_BY_INDEX");
            PushScaleformMovieFunctionParameterInt(column);
            AddTextEntry("fe_col_title", title);
            BeginTextCommandScaleformString("fe_col_title");
            AddTextComponentInteger(thing);
            EndTextCommandScaleformString();
            PushScaleformMovieFunctionParameterInt(0);
            PushScaleformMovieFunctionParameterBool(false);
            PopScaleformMovieFunctionVoid();
        }

        public async Task SetDetailsMissionName(string name, string rp, string cash, string textureDict = "prop_screen_nhp_base3", string textureName = "3_2_prep_01")
        {
            if (!HasStreamedTextureDictLoaded(textureDict))
            {
                RequestStreamedTextureDict(textureDict, true);
                while (!HasStreamedTextureDictLoaded(textureDict))
                {
                    await BaseScript.Delay(0);
                }
            }
            PushScaleformMovieFunctionN("SET_COLUMN_TITLE");                // scale function name
            PushScaleformMovieMethodParameterInt(1);                        // column id;
            PushScaleformMovieFunctionParameterString(name);                // mission name
            PushScaleformMovieFunctionParameterString(name);                // mission name
            PushScaleformMovieMethodParameterInt(0);                        // 1 = R* verified, 2 = R* created
            PushScaleformMovieMethodParameterString(textureDict);           // texture dict
            PushScaleformMovieMethodParameterString(textureName);           // texture name
            PushScaleformMovieMethodParameterInt(0);                        // idk
            PushScaleformMovieMethodParameterInt(0);                        // idk
            if (string.IsNullOrEmpty(rp))
            {
                PushScaleformMovieMethodParameterBool(false);               // RP
            }
            else
            {
                PushScaleformMovieMethodParameterString(rp);                // RP
            }
            if (string.IsNullOrEmpty(cash))
            {
                PushScaleformMovieMethodParameterBool(false);               // CASH
            }
            else
            {
                PushScaleformMovieMethodParameterString(cash);              // CASH
            }

            PopScaleformMovieFunctionVoid();                                // done

        }

        public void UpdateSettings(bool update = false, int row = -1)
        {
            //SetSettingsSlot(0, "Selectable Option #0", "Right Text", true, 0);
            //SetSettingsSlot(1, "Selectable Option #1", "Right Text", true, 1);
            //SetSettingsSlot(2, "Selectable Option #2", "Right Text", true, 5);
            //SetSettingsSlot(3, "Selectable Option #3", "Right Text", true, 6);
            //SetSettingsSlot(4, "Selectable Option #4", "Right Text", true, 7);
            //SetSettingsSlot(5, "info #1", "", false, 5);
            //SetSettingsSlot(6, "info #2", "", false, 6);
            //SetSettingsSlot(7, "info #3", "", false, 7);
            //SetSettingsSlot(8, "info #4", "", false, 8);
            //SetSettingsSlot(9, "info #5", "", false, 9);
            //SetSettingsSlot(10, "info #6", "", false, 10);

            //SetSettingsCurrentDescription("This is a description for an option. It does not live update so you have to do this yourself somehow.", false);
            //if (update && row == 1)
            //{
            //    if (settingsList[1].SelectedIndex)
            //}
            if (update && row != -1)
            {
                var s = settingsList[row];
                SetSettingsSlot(s.RowIndex, s.Title, s.SelectionItems[s.SelectedIndex], s.Selectable, s.Type, s.RowColor);
                if (row == 1)
                {
                    if (s.SelectedIndex == 0)
                    {
                        SoundController.TriggerSuspenseMusicEvent();
                    }
                    else
                    {
                        SoundController.ResetMusicEvents();
                    }

                }
                if (row == 0)
                {
                    SetSettingsSlotVehicleInfo(settingsList.Count - 4, "Speed", GetStatFromSelectedVehicle(0, (uint)GetHashKey(GetSelectedVehicle())));
                    SetSettingsSlotVehicleInfo(settingsList.Count - 3, "Acceleration", GetStatFromSelectedVehicle(1, (uint)GetHashKey(GetSelectedVehicle())));
                    SetSettingsSlotVehicleInfo(settingsList.Count - 2, "Traction", GetStatFromSelectedVehicle(2, (uint)GetHashKey(GetSelectedVehicle())));
                    SetSettingsSlotVehicleInfo(settingsList.Count - 1, "Breaking", GetStatFromSelectedVehicle(3, (uint)GetHashKey(GetSelectedVehicle())));
                }
            }
            else
            {
                foreach (var s in settingsList)
                {
                    SetSettingsSlot(s.RowIndex, s.Title, s.SelectionItems[s.SelectedIndex], s.Selectable, s.Type, s.RowColor);
                }
                SetSettingsSlotVehicleInfo(settingsList.Count - 4, "Speed", GetStatFromSelectedVehicle(0, (uint)GetHashKey(GetSelectedVehicle())));
                SetSettingsSlotVehicleInfo(settingsList.Count - 3, "Acceleration", GetStatFromSelectedVehicle(1, (uint)GetHashKey(GetSelectedVehicle())));
                SetSettingsSlotVehicleInfo(settingsList.Count - 2, "Traction", GetStatFromSelectedVehicle(2, (uint)GetHashKey(GetSelectedVehicle())));
                SetSettingsSlotVehicleInfo(settingsList.Count - 1, "Breaking", GetStatFromSelectedVehicle(3, (uint)GetHashKey(GetSelectedVehicle())));
            }


            PushScaleformMovieFunctionN("DISPLAY_DATA_SLOT");
            PushScaleformMovieFunctionParameterInt(0);
            PopScaleformMovieFunctionVoid();
            SetSettingsCurrentDescription(settingsList[GameController.FeCurrentSelection].Description, false);
        }

        private string GetSelectedVehicle()
        {
            int index = settingsList[0].SelectedIndex;
            if (index == 0)
            {
                return "wastelander";
            }
            else if (index == 1)
            {
                return "ratloader2";
            }
            else if (index == 2)
            {
                return "bison";
            }
            else if (index == 3)
            {
                return "blazer";
            }
            return "invalid";
        }

        private float GetStatFromSelectedVehicle(int statType, uint vehicleModel)
        {
            if (statType == 0)
            {
                return GetVehicleModelMaxSpeed(vehicleModel) / 70f;
            }
            else if (statType == 1)
            {
                return GetVehicleModelAcceleration(vehicleModel);
            }
            else if (statType == 2)
            {
                return GetVehicleModelMaxTraction(vehicleModel) / 2.5f;
            }
            else if (statType == 3)
            {
                return GetVehicleModelMaxBraking(vehicleModel);
            }
            return 0f;
        }

        private void SetSettingsSlot(int row, string leftText, string rightSomething, bool selectable, int type, int rowColor)
        {
            ///// COLUMN 0 (LEFT) - ROW 1
            PushScaleformMovieFunctionN("SET_DATA_SLOT");
            PushScaleformMovieFunctionParameterInt(0); // column
            PushScaleformMovieFunctionParameterInt(row); // index

            // com.rockstargames.gtav.pauseMenu.pauseMenuItems.PauseMenuBaseItem::__set__data
            PushScaleformMovieFunctionParameterInt(0); // menu ID 0
            PushScaleformMovieFunctionParameterInt(99); // unique ID 0
            PushScaleformMovieFunctionParameterInt(type); // type 0

            //if (type != 0)
            //{
            //PushScaleformMovieFunctionParameterInt(28); // initialIndex 0 (right thing color)
            //}
            //else
            //{
            PushScaleformMovieFunctionParameterInt(0); // initialIndex 0 (right thing color)
                                                       //}

            PushScaleformMovieFunctionParameterBool(selectable); // isSelectable true

            PushScaleformMovieFunctionParameterString(leftText);


            PushScaleformMovieFunctionParameterInt(0);

            ///// UNSURE HOW THIS WORKS, BUT IF YOU UNCOMMENT THIS, IT'LL ADD AN ICON TO THE ROW.
            ///// MAKING THE STRING "20" AND THE BOOL TRUE SEEMS TO DO SOMETHING WITH A ROCKSTAR LOGO INSTEAD.
            PushScaleformMovieFunctionParameterInt(0);
            PushScaleformMovieFunctionParameterString(rightSomething);


            if (type == 2)
            {
                PushScaleformMovieFunctionParameterInt(rowColor);
            }
            else
            {
                PushScaleformMovieFunctionParameterInt(0);
            }


            //PushScaleformMovieFunctionParameterBool(false); // reduce colors

            PushScaleformMovieFunctionParameterBool(false); // SOMETHING WITH ROCKSTAR/STAR LOGO SWITCHING.
                                                            ///// FINISH.
            PopScaleformMovieFunctionVoid();
        }

        public void SetSettingsSlotVehicleInfo(int row, string leftText, float value)
        {
            ///// COLUMN 0 (LEFT) - ROW 1
            PushScaleformMovieFunctionN("SET_DATA_SLOT");
            PushScaleformMovieFunctionParameterInt(0); // column
            PushScaleformMovieFunctionParameterInt(row); // index

            // com.rockstargames.gtav.pauseMenu.pauseMenuItems.PauseMenuBaseItem::__set__data
            PushScaleformMovieFunctionParameterInt(0); // menu ID 0
            PushScaleformMovieFunctionParameterInt(0); // unique ID 0
            PushScaleformMovieFunctionParameterInt(3); // type 0

            //if (type != 0)
            //{
            //PushScaleformMovieFunctionParameterInt(28); // initialIndex 0 (right thing color)
            //}
            //else
            //{
            PushScaleformMovieFunctionParameterInt(0); // initialIndex 0 (right thing color)
                                                       //}

            PushScaleformMovieFunctionParameterBool(false); // isSelectable true

            PushScaleformMovieFunctionParameterString(leftText);


            PushScaleformMovieFunctionParameterInt(0);

            ///// UNSURE HOW THIS WORKS, BUT IF YOU UNCOMMENT THIS, IT'LL ADD AN ICON TO THE ROW.
            ///// MAKING THE STRING "20" AND THE BOOL TRUE SEEMS TO DO SOMETHING WITH A ROCKSTAR LOGO INSTEAD.
            PushScaleformMovieFunctionParameterInt(0);
            PushScaleformMovieFunctionParameterString("");


            PushScaleformMovieFunctionParameterInt(0);
            PushScaleformMovieMethodParameterFloat(value);


            //PushScaleformMovieFunctionParameterBool(false); // reduce colors

            PushScaleformMovieFunctionParameterBool(false); // SOMETHING WITH ROCKSTAR/STAR LOGO SWITCHING.
                                                            ///// FINISH.
            PopScaleformMovieFunctionVoid();
        }

        public void SetSettingsCurrentDescription(string text, bool blinkInfoIcon)
        {
            PushScaleformMovieFunctionN("SET_DESCRIPTION");
            PushScaleformMovieMethodParameterInt(0);
            PushScaleformMovieMethodParameterString(text);
            PushScaleformMovieMethodParameterBool(blinkInfoIcon);
            PopScaleformMovieFunctionVoid();
            //Debug.WriteLine(text);
        }

        //public void UpdateSettings(int row, string left, string right, string right2)
        //{
        //    ///// COLUMN 0 (LEFT) - ROW 0
        //    PushScaleformMovieFunctionN("SET_DATA_SLOT");
        //    PushScaleformMovieFunctionParameterInt(0); // column
        //    PushScaleformMovieFunctionParameterInt(row); // index

        //    // com.rockstargames.gtav.pauseMenu.pauseMenuItems.PauseMenuBaseItem::__set__data
        //    PushScaleformMovieFunctionParameterInt(0); // menu ID 0
        //    PushScaleformMovieFunctionParameterInt(0); // unique ID 0
        //    PushScaleformMovieFunctionParameterInt(-1); // type 0
        //    int rightThingColor = 0;
        //    PushScaleformMovieFunctionParameterInt(rightThingColor); // initialIndex 0
        //    PushScaleformMovieFunctionParameterBool(true); // isSelectable true

        //    PushScaleformMovieFunctionParameterString(left);
        //    PushScaleformMovieFunctionParameterString(right);

        //    ///// UNSURE HOW THIS WORKS, BUT IF YOU UNCOMMENT THIS, IT'LL ADD AN ICON TO THE ROW.
        //    ///// MAKING THE STRING "20" AND THE BOOL TRUE SEEMS TO DO SOMETHING WITH A ROCKSTAR LOGO INSTEAD.
        //    PushScaleformMovieFunctionParameterInt(1);
        //    PushScaleformMovieFunctionParameterString(right2);
        //    PushScaleformMovieFunctionParameterInt(1);

        //    PushScaleformMovieFunctionParameterBool(false); // SOMETHING WITH ROCKSTAR/STAR LOGO SWITCHING.


        //    ///// FINISH.
        //    PopScaleformMovieFunctionVoid();

        //}

        private void UpdateDetails()
        {
            //SetDetailsSlot(0, GetLabelText("PM_TYPE"), "Hunters vs Runners");       // Type
            SetDetailsSlot(0, GetLabelText("PM_RATING"), "77.3%");                  // Rating
            SetDetailsSlot(1, GetLabelText("PM_CREATED"), "<C>Vespura</C>");        // Created by
                                                                                    //SetDetailsSlot(2, GetLabelText("PM_RANK"), "1");                      // Opens at Rank
            SetDetailsSlot(2, GetLabelText("PM_PLAYERS"), "2-12");                  // Players
            SetDetailsSlot(3, GetLabelText("PM_TEAMS"), "2");                       // Teams
            SetDetailsSlot(4, GetLabelText("PM_AREA"), GetLabelText(GetNameOfZone(2321.30f, 3843.73f, 34.27f))); // Area
            SetDetailsSlot(5, GetLabelText("FM_ISC_DIST"), "6.25 km");              // Distance
        }

        public void SetDetailsSlot(int row, string leftText, string rightText)
        {
            PushScaleformMovieFunctionN("SET_DATA_SLOT");
            PushScaleformMovieFunctionParameterInt(1); // column
            PushScaleformMovieFunctionParameterInt(row); // index

            // com.rockstargames.gtav.pauseMenu.pauseMenuItems.PauseMenuBaseItem::__set__data
            PushScaleformMovieFunctionParameterInt(1); // menu ID 0
            PushScaleformMovieFunctionParameterInt(1); // unique ID 0
            PushScaleformMovieFunctionParameterInt(1); // type 0
            dynamic rightThingColor = 28;
            PushScaleformMovieFunctionParameterInt((int)rightThingColor); // initialIndex 0
            PushScaleformMovieFunctionParameterBool(false); // isSelectable true

            PushScaleformMovieFunctionParameterString(leftText);
            PushScaleformMovieFunctionParameterString(rightText);

            ///// UNSURE HOW THIS WORKS, BUT IF YOU UNCOMMENT THIS, IT'LL ADD AN ICON TO THE ROW.
            ///// MAKING THE STRING "20" AND THE BOOL TRUE SEEMS TO DO SOMETHING WITH A ROCKSTAR LOGO INSTEAD.
            PushScaleformMovieFunctionParameterInt(0);
            PushScaleformMovieFunctionParameterString("2");
            PushScaleformMovieFunctionParameterInt(0);

            PushScaleformMovieFunctionParameterBool(true); // SOMETHING WITH ROCKSTAR/STAR LOGO SWITCHING.

            ///// FINISH.
            PopScaleformMovieFunctionVoid();
        }

        #endregion


    }
}
