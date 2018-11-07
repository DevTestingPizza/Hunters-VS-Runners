using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.UI;
using static CitizenFX.Core.BaseScript;
using static HuntersVsRunners.MissingNatives;

namespace HuntersVsRunners
{
    public static class GfxController
    {
        /// <summary>
        /// Draws text on screen.
        /// </summary>
        /// <param name="text">The text to display on screen.</param>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="size">The font size.</param>
        public static void DrawText(string text, float x, float y, float size)
        {
            DrawText(text, x, y, size, Font.ChaletComprimeCologne, Alignment.Left, false, -1, 255);
        }
        /// <summary>
        /// Draws text on screen.
        /// </summary>
        /// <param name="text">The text to display on screen.</param>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="size">The font size.</param>
        /// <param name="font">The font to use.</param>
        public static void DrawText(string text, float x, float y, float size, Font font)
        {
            DrawText(text, x, y, size, font, Alignment.Left, false, -1, 255);
        }
        /// <summary>
        /// Draws text on screen.
        /// </summary>
        /// <param name="text">The text to display on screen.</param>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="size">The font size.</param>
        /// <param name="font">The font to use.</param>
        /// <param name="align">Text alignment (center, left, right).</param>
        public static void DrawText(string text, float x, float y, float size, Font font, Alignment align)
        {
            DrawText(text, x, y, size, font, align, false, -1, 255);
        }
        /// <summary>
        /// Draws text on screen.
        /// </summary>
        /// <param name="text">The text to display on screen.</param>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="size">The font size.</param>
        /// <param name="font">The font to use.</param>
        /// <param name="align">Text alignment (center, left, right).</param>
        /// <param name="outlined">Outline the text with a black border.</param>
        public static void DrawText(string text, float x, float y, float size, Font font, Alignment align, bool outlined)
        {
            DrawText(text, x, y, size, font, align, outlined, -1, 255);
        }
        /// <summary>
        /// Draws text on screen.
        /// </summary>
        /// <param name="text">The text to display on screen.</param>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="size">The font size.</param>
        /// <param name="font">The font to use.</param>
        /// <param name="align">Text alignment (center, left, right).</param>
        /// <param name="outlined">Outline the text with a black border.</param>
        /// <param name="duration">Display time in miliseconds. Set to -1 to draw only one frame.</param>
        public static void DrawText(string text, float x, float y, float size, Font font, Alignment align, bool outlined, int duration)
        {
            DrawText(text, x, y, size, font, align, outlined, duration, 255);
        }
        /// <summary>
        /// Draws text on screen.
        /// </summary>
        /// <param name="text">The text to display on screen.</param>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="size">The font size.</param>
        /// <param name="font">The font to use.</param>
        /// <param name="align">Text alignment (center, left, right).</param>
        /// <param name="outlined">Outline the text with a black border.</param>
        /// <param name="duration">Display time in miliseconds. Set to -1 to draw only one frame.</param>
        public static async void DrawText(string text, float x, float y, float size, Font font, Alignment align, bool outlined, int duration, int opacity)
        {
            if (IsHudPreferenceSwitchedOn())
            {
                SetTextColour(255, 255, 255, opacity);
                var strings = StringToArray(text);
                if (duration == -1)
                {
                    SetTextFont((int)font);
                    SetTextScale(1f, (size * 27f) / Screen.Resolution.Height); // always pixel perfect text height in pixels.
                    SetTextJustification((int)align);
                    if (align == Alignment.Right)
                    {
                        SetTextWrap(0f, x);
                    }
                    if (outlined)
                    {
                        SetTextOutline();
                    }
                    BeginTextCommandDisplayText("THREESTRINGS");
                    foreach (string sentence in strings)
                    {
                        AddTextComponentSubstringPlayerName(sentence);
                    }

                    if (align == Alignment.Right)
                    {
                        EndTextCommandDisplayText(0f, y);
                    }
                    else
                    {
                        EndTextCommandDisplayText(x, y);
                    }
                }
                else
                {
                    var timer = GetGameTimer();
                    while (GetGameTimer() - timer <= duration)
                    {
                        await Delay(0);
                        SetTextFont((int)font);
                        SetTextScale(1f, size);
                        SetTextJustification((int)align);
                        if (align == Alignment.Right)
                        {
                            SetTextWrap(0f, x);
                        }
                        if (outlined)
                        {
                            SetTextOutline();
                        }
                        BeginTextCommandDisplayText("THREESTRINGS");
                        foreach (string sentence in strings)
                        {
                            AddTextComponentSubstringPlayerName(sentence);
                        }
                        if (align == Alignment.Right)
                        {
                            EndTextCommandDisplayText(0f, y);
                        }
                        else
                        {
                            EndTextCommandDisplayText(x, y);
                        }
                    }
                }
            }

        }

        #region StringToStringArray
        /// <summary>
        /// Converts an input string into 1, 2 or 3 strings all stacked in a string[3].
        /// Use this to convert text into multiple smaller pieces to be used in functions like 
        /// drawing text, drawing help messages or drawing notifications on screen.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string[] StringToArray(string inputString)
        {
            string[] outputString = new string[3];

            var lastSpaceIndex = 0;
            var newStartIndex = 0;
            outputString[0] = inputString;

            if (inputString.Length > 99)
            {
                for (int i = 0; i < inputString.Length; i++)
                {
                    if (inputString.Substring(i, 1) == " ")
                    {
                        lastSpaceIndex = i;
                    }

                    if (inputString.Length > 99 && i >= 98)
                    {
                        if (i == 98)
                        {
                            outputString[0] = inputString.Substring(0, lastSpaceIndex);
                            newStartIndex = lastSpaceIndex + 1;
                        }
                        if (i > 98 && i < 198)
                        {
                            if (i == 197)
                            {
                                outputString[1] = inputString.Substring(newStartIndex, (lastSpaceIndex - (outputString[0].Length - 1))
                                    - (inputString.Length - 1 > 197 ? 1 : -1));
                                newStartIndex = lastSpaceIndex + 1;
                            }
                            else if (i == inputString.Length - 1 && inputString.Length < 198)
                            {
                                outputString[1] = inputString.Substring(newStartIndex, ((inputString.Length - 1) - outputString[0].Length));
                                newStartIndex = lastSpaceIndex + 1;
                            }
                        }
                        if (i > 197)
                        {
                            if (i == inputString.Length - 1 || i == 296)
                            {
                                outputString[2] = inputString.Substring(newStartIndex, ((inputString.Length - 1) - outputString[0].Length)
                                    - outputString[1].Length);
                            }
                        }
                    }
                }
            }

            return outputString;
        }
        #endregion

        public static float GetX(float x)
        {
            return x / Screen.Resolution.Width;
        }
        public static float GetY(float y)
        {
            return y / Screen.Resolution.Height;
        }

        /// <summary>
        /// Draws a score/stats bar on the lower right of your screen.
        /// </summary>
        /// <param name="leftText">The text that should appear on the left side.</param>
        /// <param name="rightText">The text that should appear on the right side.</param>
        /// <param name="rowIndex">The row index. 0 = bottom bar, rowIndex > 0 is any row above.</param>
        public static async void DrawStatBar(string leftText, string rightText, int rowIndex)
        {
            if (IsHudPreferenceSwitchedOn())// && !DisableDrawing)
            {
                // Hide overlapping info.
                HideHudComponentThisFrame((int)HudComponent.AreaName);
                HideHudComponentThisFrame((int)HudComponent.StreetName);
                HideHudComponentThisFrame((int)HudComponent.VehicleName);

                // Loading textures
                const string timerbarsDict = "timerbars";
                //var dict = "timerbars";
                const string iconsDict = "mpleaderboard";
                const string timerbarsBgTexture = "all_black_bg";
                if (!HasStreamedTextureDictLoaded(timerbarsDict))
                {
                    RequestStreamedTextureDict(timerbarsDict, false);
                }
                if (!HasStreamedTextureDictLoaded(iconsDict))
                {
                    RequestStreamedTextureDict(iconsDict, false);
                }
                while (!HasStreamedTextureDictLoaded(timerbarsDict) || !HasStreamedTextureDictLoaded(iconsDict))
                {
                    await Delay(0);
                }

                // Base width, height and positions onscreen.
                float width = 400f / (float)Screen.Resolution.Width;
                float height = 40f / (float)Screen.Resolution.Height;
                float posx = ((float)Screen.Resolution.Width - (400f / 2f) - 20f) / (float)Screen.Resolution.Width;
                float posy = ((float)Screen.Resolution.Height - (40f / 2f) - 20f - ((45f * (float)rowIndex))) / (float)Screen.Resolution.Height;

                // Background.
                DrawSprite(timerbarsDict, timerbarsBgTexture, posx, posy, width, height, 0f, 0, 0, 0, 200);

                // Left text
                posx = posx - ((40f / (float)Screen.Resolution.Width));
                posy = posy - (height / 2f - (6f / (float)Screen.Resolution.Height));
                DrawText(leftText, posx, posy, 13f, Font.ChaletLondon, Alignment.Right, false, -1, 230);

                // Right text/dot sprite.
                if (rightText.Contains("{plane}"))
                {
                    var r = 255;
                    var g = 255;
                    var b = 255;
                    var a = 255;
                    //GetHudColour(27 + rowIndex, ref r, ref g, ref b, ref a);
                    float iconSize = 35f;
                    posx = ((float)Screen.Resolution.Width - (400f / 2f) - 20f) / (float)Screen.Resolution.Width;
                    posx = posx + (width / 2f - (iconSize / (float)Screen.Resolution.Width));
                    posy = posy = ((float)Screen.Resolution.Height - (40f / 2f) - 20f - ((45f * (float)rowIndex))) / (float)Screen.Resolution.Height;
                    //DrawSprite(dict, "circle_checkpoints", posx, posy, 25f / (float)Screen.Resolution.Width, 25f / (float)Screen.Resolution.Height, 0f, r, g, b, a);
                    DrawSprite(iconsDict, "leaderboard_transport_plane_icon", posx, posy, iconSize / (float)Screen.Resolution.Width, iconSize / (float)Screen.Resolution.Height, 0f, r, g, b, a);

                    posx = ((float)Screen.Resolution.Width - (400f / 2f) - 65f) / (float)Screen.Resolution.Width;
                    posx = posx + (width / 2f - (6f / (float)Screen.Resolution.Width));
                    posy = posy = ((float)Screen.Resolution.Height - (40f / 2f) - 20f - ((45f * (float)rowIndex))) / (float)Screen.Resolution.Height;
                    posy = posy - (height / 2f + (-3f / (float)Screen.Resolution.Height));

                    DrawText(rightText.Replace("{plane}", ""), posx, posy, 16f, Font.ChaletLondon, Alignment.Right, false, -1, 180);
                }
                else if (rightText.Contains("{car}"))
                {
                    var r = 255;
                    var g = 255;
                    var b = 255;
                    var a = 255;
                    //GetHudColour(27 + rowIndex, ref r, ref g, ref b, ref a);
                    float iconSize = 35f;
                    posx = ((float)Screen.Resolution.Width - (400f / 2f) - 20f) / (float)Screen.Resolution.Width;
                    posx = posx + (width / 2f - (iconSize / (float)Screen.Resolution.Width));
                    posy = posy = ((float)Screen.Resolution.Height - (40f / 2f) - 20f - ((45f * (float)rowIndex))) / (float)Screen.Resolution.Height;
                    //DrawSprite(dict, "circle_checkpoints", posx, posy, 25f / (float)Screen.Resolution.Width, 25f / (float)Screen.Resolution.Height, 0f, r, g, b, a);

                    DrawSprite(iconsDict, "leaderboard_transport_car_icon", posx, posy, iconSize / (float)Screen.Resolution.Width, iconSize / (float)Screen.Resolution.Height, 0f, r, g, b, a);

                    posx = ((float)Screen.Resolution.Width - (400f / 2f) - 65f) / (float)Screen.Resolution.Width;
                    posx = posx + (width / 2f - (6f / (float)Screen.Resolution.Width));
                    posy = posy = ((float)Screen.Resolution.Height - (40f / 2f) - 20f - ((45f * (float)rowIndex))) / (float)Screen.Resolution.Height;
                    posy = posy - (height / 2f + (-3f / (float)Screen.Resolution.Height));

                    DrawText(rightText.Replace("{car}", ""), posx, posy, 16f, Font.ChaletLondon, Alignment.Right, false, -1, 180);
                }
                else
                {

                    posy = posy = ((float)Screen.Resolution.Height - (40f / 2f) - 20f - ((45f * (float)rowIndex))) / (float)Screen.Resolution.Height;
                    posy = posy - (height / 2f); //+ (0.1f / (float)Screen.Resolution.Height));


                    int iterator = 0;
                    foreach (char s in rightText.ToCharArray())
                    {
                        float offset = 0f;
                        switch (iterator)
                        {
                            case 1:
                                offset = 20f;
                                break;
                            case 2:
                                offset = 28f;
                                break;
                            case 3:
                                offset = 48f;
                                break;
                            case 4:
                                offset = 68f;
                                break;
                            default:
                                offset = 0f;
                                break;
                        }
                        posx = ((float)Screen.Resolution.Width - (400f / 2f) - (92f - (offset))) / (float)Screen.Resolution.Width;
                        posx = posx + (width / 2f - (6f / (float)Screen.Resolution.Width));

                        DrawText(s.ToString(), posx, posy, 20f, Font.ChaletLondon, Alignment.Right, false, -1, 180);
                        iterator += 1;
                    }

                    //posx = ((float)Screen.Resolution.Width - (400f / 2f) - 83f) / (float)Screen.Resolution.Width;
                    //posx = posx + (width / 2f - (6f / (float)Screen.Resolution.Width));
                    //posy = posy = ((float)Screen.Resolution.Height - (40f / 2f) - 20f - ((45f * (float)rowIndex))) / (float)Screen.Resolution.Height;
                    //posy = posy - (height / 2f); //+ (0.1f / (float)Screen.Resolution.Height));
                    //DrawText(rightText.Substring(0, 2), posx, posy, 20f, Font.ChaletLondon, Alignment.Right, false, -1, 180);

                    //posx = ((float)Screen.Resolution.Width - (400f / 2f) - 73f) / (float)Screen.Resolution.Width;
                    //posx = posx + (width / 2f - (6f / (float)Screen.Resolution.Width));
                    //DrawText(":", posx, posy, 20f, Font.ChaletLondon, Alignment.Right, false, -1, 180);

                    //posx = ((float)Screen.Resolution.Width - (400f / 2f) - 33f) / (float)Screen.Resolution.Width;
                    //posx = posx + (width / 2f - (6f / (float)Screen.Resolution.Width));
                    //DrawText(rightText.Substring(3, 2), posx, posy, 20f, Font.ChaletLondon, Alignment.Right, false, -1, 180);

                }
            }
        }

        public static async Task ShowFinishScaleform(bool lost = true)
        {
            var bg = new Scaleform("MP_CELEBRATION_BG");
            var fg = new Scaleform("MP_CELEBRATION_FG");
            var cb = new Scaleform("MP_CELEBRATION");
            RequestScaleformMovie("MP_CELEBRATION_BG");
            RequestScaleformMovie("MP_CELEBRATION_FG");
            RequestScaleformMovie("MP_CELEBRATION");
            while (!bg.IsLoaded || !fg.IsLoaded || !cb.IsLoaded)
            {
                await Delay(0);
            }

            // Setting up colors.
            bg.CallFunction("CREATE_STAT_WALL", "ch", "HUD_COLOUR_BLACK", -1);
            fg.CallFunction("CREATE_STAT_WALL", "ch", "HUD_COLOUR_RED", -1);
            cb.CallFunction("CREATE_STAT_WALL", "ch", "HUD_COLOUR_BLUE", -1);

            // Setting up pause duration.
            bg.CallFunction("SET_PAUSE_DURATION", 3.0f);
            fg.CallFunction("SET_PAUSE_DURATION", 3.0f);
            cb.CallFunction("SET_PAUSE_DURATION", 3.0f);

            //bool won = new Random().Next(0, 2) == 0;
            //bool won = true;
            string win_lose = lost ? "CELEB_LOSER" : "CELEB_WINNER";

            bg.CallFunction("ADD_WINNER_TO_WALL", "ch", win_lose, GetPlayerName(PlayerId()), "", 0, false, "", false);
            fg.CallFunction("ADD_WINNER_TO_WALL", "ch", win_lose, GetPlayerName(PlayerId()), "", 0, false, "", false);
            cb.CallFunction("ADD_WINNER_TO_WALL", "ch", win_lose, GetPlayerName(PlayerId()), "", 0, false, "", false);

            // Setting up background.
            bg.CallFunction("ADD_BACKGROUND_TO_WALL", "ch");
            fg.CallFunction("ADD_BACKGROUND_TO_WALL", "ch");
            cb.CallFunction("ADD_BACKGROUND_TO_WALL", "ch");

            // Preparing to show the wall.
            bg.CallFunction("SHOW_STAT_WALL", "ch");
            fg.CallFunction("SHOW_STAT_WALL", "ch");
            cb.CallFunction("SHOW_STAT_WALL", "ch");

            // Drawing the wall on screen for 3 seconds + 1 seconds (for outro animation druation).
            var timer = GetGameTimer();
            //DisableDrawing = true;
            while (GetGameTimer() - timer <= (3000 + 1000))
            {
                await Delay(0);
                DrawScaleformMovieFullscreenMasked(bg.Handle, fg.Handle, 255, 255, 255, 255);
                DrawScaleformMovieFullscreen(cb.Handle, 255, 255, 255, 255, 0);
                HideHudAndRadarThisFrame();
            }
            //DisableDrawing = false;

            // Playing effect when it's over.
            StartScreenEffect("MinigameEndNeutral", 0, false);
            PlaySoundFrontend(-1, "SCREEN_FLASH", "CELEBRATION_SOUNDSET", false);

            // Cleaning up.
            bg.CallFunction("CLEANUP");
            fg.CallFunction("CLEANUP");
            cb.CallFunction("CLEANUP");

            bg.Dispose();
            fg.Dispose();
            cb.Dispose();
            //GameController.gameRestarting = false;
            //GameController.Go();

        }
    }
}
