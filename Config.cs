using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterUI
{
    public class Config : AbstractConfigSection
    {

        [Description("Should this Plugin be enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should the amount of alive players in your team be displayed?")]
        public bool EnableTeamCountElement { get; set; } = true;

        [Description("Should the amount of all of your ammo should be displayed?")]
        public bool EnableTotalAmmoElement { get; set; } = true;

        [Description("Should the amount of your grenades be displayed?")]
        public bool EnableGrenadesElement { get; set; } = true;

        [Description("Should a Targetcounter be displayed for SCP-035/056 if you have them installed?")]
        public bool EnableTargetCounter { get; set; } = true;

        [Description("Should the time in which spectators respawn be displayed to dead players?")]
        public bool EnableRespawnTimerElement { get; set; } = true;

        [Description("Should a player be notificated if he sees SCP-096?")]
        public bool Enable096IndicatorElement { get; set; } = true;

        [Description("Should a player be shown how much damage he dealt?")]
        public bool EnableDamageElement { get; set; } = true;

        [Description("Should a player be shown the role name and hp of the player he killed/he got killed from?")]
        public bool EnableKillElement { get; set; } = true;

        [Description("Should a player be shown the current kills, deaths and a KD/r on the screen?")]
        public bool EnableStatsElements { get; set; } = true;

        [Description("Should at the end of the round the best players be shown?")]
        public bool EnableLeaderboard { get; set; } = true;

        [Description("Should a Text appear when the server is starting and waiting for users? Only use this if you do NOT have the WaitAndChill Plugin installed!")]
        public bool EnableWaitingForUsersText { get; set; } = false;

        [Description("What Text should show up on a roundstart?")]
        public string WaitingForUsersText { get; set; } = "\n\n\n\nWelcome to MyServer!\n<b><color=%rainbow%>Join our Discord Server!\ndiscord.gg/yourdiscord</color></b>";

    }
}
