using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterUI
{
    public class PluginTranslation : IPluginTranslation
    {
        public string Respawn = "You will respawn in:";

        public string Respawning = "You are respawning as:";

        public string Kill = "You have killed:";

        public string Killed = "You have been killed by:";

        public string PressEscToExit = "\n<color=blue>Press Esc to close this window</color>";

        public string MostKills = "<size=30><i>[<color=#00FFE4>%position%</color>] <color=yellow><b>%player%</b></color> has <color=red><b>%kills%</b></color> Kills!</i></size>";

        public string MostKillsNone = "<size=30><i>Nobody has any kills!</i></size>";

        public string ScpsRecontained = "<size=30><i>[<color=#00FFE4>%position%</color>] <color=yellow><b>%player%</b></color> has recontained <color=#26FF02><b>%SCPS%</b></color> SCP(s)!</i></size>";

        public string ScpsRecontainedTitel = "<size=30><i>SCPs Recontained:</i></size>";

        public string ScpsRecontainedNone = "<size=30><i>No SCPs were recontained this round!</i></size>";

        public string FastestEscapeTitel = "<size=30><i>Fastest Escapes:</i></size>";

        public string FastestEscape = "<size=30><i>[<color=#00FFE4>%position%</color>] <color=yellow><b>%player%</b></color> escaped at <color=#FF9303><b>%time%</b></color>!</i></size>";

        public string NoEscape = "<size=30><i>Nobody did escape!</i></size>";

        public string FirstKillAndDeath = "<size=30><i>Firstkill/Firstdeath:</i></size>";

        public string FirstKill = "<size=30><i><color=yellow><b>%player%</b></color> has sheed the <color=#9503E6>first blood</color>!</i></size>";

        public string FirstDeath = "<size=30><i><color=yellow><b>%player%</b></color> was the <color=#0E1EED>first to die</color>!</i></size>";

        public string DealtDamage = "You dealt <color=red>%damage%</color> Damage!";

        public string Scp096TriggerSelf = "%amount% have seen your face!";

        public string Scp096TriggerHuman = "You have seen SCP-096 face!";

        public string ChaosInsurgency = "Chaos Insurgency";

        public string MobileTaskForce = "Mobile Task Force";

        public string CI = "CI";

        public string MTF = "MTF";

        public string Scp049 = "SCP-049";

        public string Scp0492 = "SCP-049-2";

        public string Scp079 = "SCP-079";

        public string Scp096 = "SCP-096";

        public string Scp106 = "SCP-106";

        public string Scp173 = "SCP-173";

        public string Scp939 = "SCP-939";

        public string ClassD = "Class-D";

        public string Scientists = "Scientist";

        public string Commanders = "Commander";

        public string Lieutenants = "Lieutenant";

        public string MtfScientist = "NTF-Scientist";

        public string Cadets = "Cadet";

        public string Guards = "Guard";

        public string Tutorial = "Tutorial";

        public string RoundTime = "Round Time";

        public string Spectators = "Spectator";

        public string Team = "Team";

        public string Role = "Role";

        public string Targets = "Targets";

        public string SCP = "SCP";

        public string RIP = "Spectators";

        public string RSC = "Research Personnel";

        public string CDP = "Class-D Personnel";

        public string TUT = "Tutorial Gang";

        public string Ammo = "Ammo";

        public string Stats = "Stats";

        public string KillStats = "Kills";

        public string DeathStats = "Deaths";

        public string KdRStats = "KD/r";

        public string Grenades = "Grenades";

        public string Frag = "Frag";

        public string Flash = "Flash";

        public string Scp018 = "SCP-018";

        public string Minutes = "Min";

        public string Seconds = "Seconds";
    }
}
