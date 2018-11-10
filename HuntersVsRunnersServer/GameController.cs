using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace HuntersVsRunnersServer
{
    public class GameController : BaseScript
    {

        public static int GameState { get; private set; } = 0;

        public GameController()
        {
            EventHandlers.Add("hvsr:playerJoined", new Action<Player>(PlayerJoined));
        }


        private void PlayerJoined([FromSource] Player source)
        {
            if (source != null)
            {
                source.TriggerEvent("hvsr:setGameState", GameState);
            }
        }
    }
}
