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
        SynapseMinor = 7,
        SynapsePatch = 0,
        Version = "2.1.0"
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

            //German Translation by TheVoidNebula#5090
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
                Scp049 = "SCP-049",
                Scp0492 = "SCP-049-2",
                Scp079 = "SCP-079",
                Scp096 = "SCP-096",
                Scp106 = "SCP-106",
                Scp173 = "SCP-173",
                Scp939 = "SCP-939",
                ClassD = "Klasse-D",
                ChaosConscript = "Rekrut",
                ChaosRifleman = "Schütze",
                ChaosMarauder = "Plünderer",
                ChaosRepressor = "Repressor",
                Scientists = "Wissenschaftler",
                Captains = "Kapitän",
                Sergeants = "Sergeant",
                Specialists = "Spezialist",
                Privats = "Privat",
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
                AmmoGauge = "12-Gauge",
                AmmoCal44 = "Cal-44",
                Ammo556 = "556x45",
                Ammo762 = "762x39",
                Ammo919 = "9x19",
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

            //Polish Translation made by Doktor#7356 
            PluginTranslation.AddTranslation(new PluginTranslation
            {
                Respawn = "Odrodzisz się w ciągu:",
                Respawning = "Odrodzisz się jako:",
                Kill = "Zabiłeś:",
                Killed = "Zostałeś zabity przez:",
                PressEscToExit = "\n<color=blue>Wciśnij klawisz Esc, aby zamknąć okno</color>",
                MostKills = "<size=30><i>[<color=#00FFE4>%position%</color>] <color=yellow><b>%player%</b></color> posiada <color=red><b>%kills%</b></color> zabójstw!</i></size>",
                MostKillsNone = "<size=30><i>Nikt nikogo nie zdołał zabić!</i></size>",
                ScpsRecontained = "<size=30><i>[<color=#00FFE4>%position%</color>] <color=yellow><b>%player%</b></color> zabezpieczył <color=#26FF02><b>%SCPS%</b></color> podmiotów SCP!</i></size>",
                ScpsRecontainedTitel = "<size=30><i>Zabezpieczone podmioty SCP:</i></size>",
                ScpsRecontainedNone = "<size=30><i>Nie udało się zabezpieczyć żadnych podmiotów SCP w tej rundzie!</i></size>",
                FastestEscapeTitel = "<size=30><i>Najszybsze ucieczki:</i></size>",
                FastestEscape = "<size=30><i>[<color=#00FFE4>%position%</color>] <color=yellow><b>%player%</b></color> uciekł w czasie <color=#FF9303><b>%time%</b></color>!</i></size>",
                NoEscape = "<size=30><i>Nikt nie zdołał uciec!</i></size>",
                FirstKillAndDeath = "<size=30><i>Pierwsze zabójstwo/Pierwsza śmierć:</i></size>",
                FirstKill = "<size=30><i><color=yellow><b>%player%</b></color> przelał <color=#9503E6>pierwszą krew</color>!</i></size>",
                FirstDeath = "<size=30><i><color=yellow><b>%player%</b></color><color=#0E1EED>umarł jako pierwszy</color>!</i></size>",
                DealtDamage = "Zadałeś <color=red>%damage%</color> obrażeń!",
                Scp096TriggerSelf = "%amount% zobaczyło twoją twarz",
                Scp096TriggerHuman = "Zobaczyłeś twarz SCP-096!",
                ChaosInsurgency = "Rebelia Chaosu",
                MobileTaskForce = "Mobilna Formacja Operacyjna",
                CI = "Rebelia Chaosu",
                MTF = "MFO",
                Scp049 = "SCP-049",
                Scp0492 = "SCP-049-2",
                Scp079 = "SCP-079",
                Scp096 = "SCP-096",
                Scp106 = "SCP-106",
                Scp173 = "SCP-173",
                Scp939 = "SCP-939",
                ClassD = "Personel Klasy D",
                Scientists = "Naukowiec",
                ChaosConscript = "Poborowi Rebelii Chaosu",
                ChaosMarauder = "Maruderzy Rebelii Chaosu",
                ChaosRepressor = "Represorzy Rebelii Chaosu",
                ChaosRifleman = "Strzelcy Rebelii Chaosu",
                Captains = "Kapitanowie NTF",
                Sergeants = "Sierżanci NTF",
                Specialists = "Specjaliści NTF",
                Privats = "Szeregowi NTF",
                Guards = "Ochroniarz",
                Tutorial = "Poradnik",
                RoundTime = "Czas trwania rundy",
                Spectators = "Obserwator",
                Team = "Drużyna",
                Role = "Klasa",
                Targets = "Cele",
                SCP = "SCP",
                RIP = "Obserwatorzy",
                RSC = "Personel badawczy",
                CDP = "Personel klasy D",
                TUT = "Mafia poradnikowa",
                Ammo = "Amunicja",
                AmmoGauge = "12-Gauge",
                AmmoCal44 = "Cal-44",
                Ammo556 = "556x45",
                Ammo762 = "762x39",
                Ammo919 = "9x19",
                Stats = "Statystyki",
                KillStats = "Zabójstwa",
                DeathStats = "Śmierci",
                KdRStats = "Współczynnik zabójstw do śmierci",
                Grenades = "Granaty",
                Frag = "Granat zaczepny",
                Flash = "Granat błyskowy",
                Scp018 = "SCP-018",
                Minutes = "Minut",
                Seconds = "Sekund",
            }, "POLISH");
            //Feel free to ask me or create a PR in order to add more languages
            new EventHandlers();
            PatchMethods();
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
