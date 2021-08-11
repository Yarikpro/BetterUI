using HarmonyLib;
using Synapse;
using Synapse.Api.Plugin;
using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterUI
{
    [PluginInformation(
        Author = "TheVoidNebula",
        Description = "Add some more options to the screen!",
        LoadPriority = 0,
        Name = "BetterUI",
        SynapseMajor = 2,
        SynapseMinor = 6,
        SynapsePatch = 0,
        Version = "1.0"
        )]
    public class Plugin : AbstractPlugin
    {
        [Synapse.Api.Plugin.Config(section = "BetterUI")]
        public static Config Config;

        [SynapseTranslation]
        public static SynapseTranslation<PluginTranslation> PluginTranslation;

        public override void Load()
        {
            PluginTranslation.AddTranslation(new PluginTranslation());
            PluginTranslation.AddTranslation(new PluginTranslation
            {
                Respawn = "Du wirst respawnen in:",
                Respawning = "Du respawnst als:",
                Kill = "Abschuss:",
                Killed = "Du wurdest getötet von:",
                PressEscToExit = "\n<color=blue>Drücke Esc um dieses Fenster zu schließen</color>",
                MostKillsNone = "<size=30><i>Niemand hat Kills!</i></size>",
                ScpsRecontained = "<size=30><i>[<color=#00FFE4>%position%</color>] <color=yellow><b>%player%</b></color> hat <color=#26FF02><b>%SCPS%</b></color> SCP(s) recontained!</i></size>",
                ScpsRecontainedTitel = "<size=30><i>SCPs Recontained:</i></size>",
                ScpsRecontainedNone = "<size=30><i>Keine SCPs wurden diese Runde recontained!</i></size>",
                FastestEscapeTitel = "<size=30><i>Schnellste Fluchten:</i></size>",
                FastestEscape = "<size=30><i>[<color=#00FFE4>%position%</color>] <color=yellow><b>%player%</b></color> ist in <color=#FF9303><b>%time%</b></color> entkommen!</i></size>",
                NoEscape = "<size=30><i>Niemand ist entkommen!</i></size>",
                FirstKillAndDeath = "<size=30><i>Erster Kill/Erster Tod:</i></size>",
                FirstKill = "<size=30><i><color=yellow><b>%player%</b></color> hat das <color=#9503E6>erste Blut</color> vergossen!</i></size>",
                FirstDeath = "<size=30><i><color=yellow><b>%player%</b></color> war das <color=#0E1EED>erste Todesopfer</color>!</i></size>",
                DealtDamage = "Du hast <color=red>%damage%</color> Schaden verursacht!",
                Scp096TriggerSelf = "%amount% haben dein Gesicht gesehen!",
                Scp096TriggerHuman = "Du hast das Gesicht von SCP-096 gesehen!",
                ChaosInsurgency = "Chaos Insurgency",
                MobileTaskForce = "Mobile Task Force",
                CI = "CI",
                MTF = "MTF",
                Scp035 = "SCP-035",
                Scp049 = "SCP-049",
                Scp0492 = "SCP-049-2",
                Scp056 = "SCP-056",
                Scp079 = "SCP-079",
                Scp096 = "SCP-096",
                Scp106 = "SCP-106",
                Scp173 = "SCP-173",
                 Scp682 = "SCP-682",
                Scp939 = "SCP-939",
                SerpentsHand = "Serpents Hand",
                ClassD = "Klasse-D",
                Scientists = "Wissenschaftler",
                Commanders = "Kommandant",
                Lieutenants = "Leutnant",
                MtfScientist = "NTF-Wissenschaftler",
                Cadets = "Kadett",
                Guards = "Wachpersonal",
                Tutorial = "Tutorial",
                RoundTime = "Rundenzeit",
                Spectators = "Zuschauer",
                Team = "Team",
                Role = "Rolle",
                Targets = "Ziele",
                SCP = "SCP",
                RIP = "Zuschauer",
                RSC = "Forschungspersonal",
                CDP = "Klasse-D Personal",
                TUT = "Tutorial Gang",
                Ammo = "Munition",
                Stats = "Statistiken",
                KillStats = "Kills",
                DeathStats = "Tode",
                KdRStats = "KD/r",
                Grenades = "Granaten",
                Frag = "Splitter",
                Flash = "Blend",
                Scp018 = "SCP-018",
                Minutes = "Min",
                Seconds = "Sekunden",
    }           ,"GERMAN");
            //Feel free to ask me or create a PR in order to add more languages
            new EventHandlers();
        }
        private void PatchMethods()
        {
            try
            {
                var instance = new Harmony("ui.patches");
                instance.PatchAll();
                Server.Get.Logger.Info("[BetterUI] Text Hint patching was successful!");
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"Harmony Patching threw an error:\n\n {e}");
            }
        }
    }
}
