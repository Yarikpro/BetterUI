﻿using Hints;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterUI
{
    public class EventHandlers
    {

        private List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();
        private CoroutineHandle LobbyHandle;
        private Dictionary<string, float> Kills = new Dictionary<string, float>();
        private Dictionary<string, float> Deaths = new Dictionary<string, float>();
        private Dictionary<string, float> ScpsRecontained = new Dictionary<string, float>();
        private Dictionary<string, TimeSpan> Escapes = new Dictionary<string, TimeSpan>();
        private string FirstKill = "empty";
        private string FirstDeath = "empty";

        private readonly Dictionary<int, string> RoleToText = new Dictionary<int, string>()
        {
            { (int) RoleType.Scp173, "<color=red>" + Plugin.PluginTranslation.ActiveTranslation.Scp173 + "</color>" },
            { (int) RoleType.ClassD, "<color=#FF9302>" + Plugin.PluginTranslation.ActiveTranslation.ClassD + "</color>" },
            { (int) RoleType.Scp106, "<color=red>" + Plugin.PluginTranslation.ActiveTranslation.Scp106 + "</color>" },
            { (int) RoleType.Spectator, "<color=#666666>" + Plugin.PluginTranslation.ActiveTranslation.Spectators + "</color>" },
            { (int) RoleType.NtfSpecialist, "<color=#1782FF>" + Plugin.PluginTranslation.ActiveTranslation.Specialists + "</color>" },
            { (int) RoleType.Scp049, "<color=red>" + Plugin.PluginTranslation.ActiveTranslation.Scp049 + "</color>" },
            { (int) RoleType.Scientist, "<color=yellow>" + Plugin.PluginTranslation.ActiveTranslation.Scientists + "</color>" },
            { (int) RoleType.Scp079, "<color=red>" + Plugin.PluginTranslation.ActiveTranslation.Scp079 + "</color>" },
            { (int) RoleType.ChaosConscript, "<color=green>" + Plugin.PluginTranslation.ActiveTranslation.ChaosConscript + "</color>" },
            { (int) RoleType.ChaosMarauder, "<color=green>" + Plugin.PluginTranslation.ActiveTranslation.ChaosMarauder + "</color>" },
            { (int) RoleType.ChaosRepressor, "<color=green>" + Plugin.PluginTranslation.ActiveTranslation.ChaosRepressor + "</color>" },
            { (int) RoleType.ChaosRifleman, "<color=green>" + Plugin.PluginTranslation.ActiveTranslation.ChaosRifleman + "</color>" },
            { (int) RoleType.Scp096, "<color=red>" + Plugin.PluginTranslation.ActiveTranslation.Scp096 + "</color>" },
            { (int) RoleType.Scp0492, "<color=red>" + Plugin.PluginTranslation.ActiveTranslation.Scp0492 + "</color>" },
            { (int) RoleType.NtfSergeant, "<color=#1782FF>" + Plugin.PluginTranslation.ActiveTranslation.Sergeants + "</color>" },
            { (int) RoleType.NtfCaptain, "<color=#0009FF>" + Plugin.PluginTranslation.ActiveTranslation.Captains + "</color>" },
            { (int) RoleType.NtfPrivate, "<color=#1782FF>" + Plugin.PluginTranslation.ActiveTranslation.Privats + "</color>" },
            { (int) RoleType.Tutorial, "<color=#00FF05>" + Plugin.PluginTranslation.ActiveTranslation.Tutorial + "</color>" },
            { (int) RoleType.FacilityGuard, "<color=#57555>" + Plugin.PluginTranslation.ActiveTranslation.Guards + "</color>" },
            { (int) RoleType.Scp93953, "<color=red>" + Plugin.PluginTranslation.ActiveTranslation.Scp939 + "</color>" },
            { (int) RoleType.Scp93989, "<color=red>" + Plugin.PluginTranslation.ActiveTranslation.Scp939 + "</color>" },
        };

        private readonly Dictionary<int, string> TeamToText = new Dictionary<int, string>()
        {
            { (int) Team.SCP, "<color=red>" + Plugin.PluginTranslation.ActiveTranslation.SCP + "</color>" },
            { (int) Team.MTF, "<color=blue>" + Plugin.PluginTranslation.ActiveTranslation.MobileTaskForce + "</color>" },
            { (int) Team.CHI, "<color=green>" + Plugin.PluginTranslation.ActiveTranslation.ChaosInsurgency + "</color>" },
            { (int) Team.RSC, "<color=yellow>" + Plugin.PluginTranslation.ActiveTranslation.RSC + "</color>" },
            { (int) Team.CDP, "<color=#FF9302>" + Plugin.PluginTranslation.ActiveTranslation.CDP + "</color>" },
            { (int) Team.RIP, "<color=#666666>" + Plugin.PluginTranslation.ActiveTranslation.RIP + "</color>" },
            { (int) Team.TUT, "<color=#00FF05>" + Plugin.PluginTranslation.ActiveTranslation.TUT + "</color>" },
        };
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundStartEvent += OnRoundStart;
            Server.Get.Events.Round.RoundEndEvent += OnRoundEnd;
            Server.Get.Events.Round.RoundRestartEvent += OnRoundRestart;
            Server.Get.Events.Round.WaitingForPlayersEvent += OnWaitingForPlayers;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent += OnScp096Rage;
            Server.Get.Events.Player.PlayerDeathEvent += OnKill;
            Server.Get.Events.Player.PlayerEscapesEvent += OnEscape;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
            Server.Get.Events.Player.PlayerJoinEvent += OnJoin;
            Server.Get.Events.Player.PlayerLeaveEvent += OnLeave;
        }

        public void OnRoundEnd()
        {
            if (Plugin.Config.EnableLeaderboard)
            {
                    List<float> KillList = Kills.Values.ToList();
                    KillList.Sort();
                    KillList.Reverse();
                    if (Kills.Values.Max() == 0)
                        Map.Get.SendBroadcast(10, $"{Plugin.PluginTranslation.ActiveTranslation.MostKillsNone}", true);
                    else
                        if (Kills.Keys.Count == 1)
                        Map.Get.SendBroadcast(10, $"{Plugin.PluginTranslation.ActiveTranslation.MostKills.Replace("%player%", Server.Get.GetPlayer(Kills.FirstOrDefault(x => x.Value == KillList[0]).Key).DisplayName).Replace("%kills%", KillList[0].ToString()).Replace("%position%", "1")}", true);
                    else if (Kills.Keys.Count == 2)
                        Map.Get.SendBroadcast(10, $"{Plugin.PluginTranslation.ActiveTranslation.MostKills.Replace("%player%", Server.Get.GetPlayer(Kills.FirstOrDefault(x => x.Value == KillList[0]).Key).DisplayName).Replace("%kills%", KillList[0].ToString()).Replace("%position%", "1")}\n{Plugin.PluginTranslation.ActiveTranslation.MostKills.Replace("%player%", Server.Get.GetPlayer(Kills.FirstOrDefault(x => x.Value == KillList[1]).Key).DisplayName).Replace("%kills%", KillList[1].ToString()).Replace("%position%", "2")}", true);
                    else if (Kills.Keys.Count >= 3)
                        Map.Get.SendBroadcast(10, $"{Plugin.PluginTranslation.ActiveTranslation.MostKills.Replace("%player%", Server.Get.GetPlayer(Kills.FirstOrDefault(x => x.Value == KillList[0]).Key).DisplayName).Replace("%kills%", KillList[0].ToString()).Replace("%position%", "1")}\n{Plugin.PluginTranslation.ActiveTranslation.MostKills.Replace("%player%", Server.Get.GetPlayer(Kills.FirstOrDefault(x => x.Value == KillList[1]).Key).DisplayName).Replace("%kills%", KillList[1].ToString()).Replace("%position%", "2")}\n{Plugin.PluginTranslation.ActiveTranslation.MostKills.Replace("%player%", Server.Get.GetPlayer(Kills.FirstOrDefault(x => x.Value == KillList[3]).Key).DisplayName).Replace("%kills%", KillList[3].ToString()).Replace("%position%", "3")}", true);
            }
        }

        public void OnRoundRestart()
        {
            foreach (CoroutineHandle x in Coroutines)
                Timing.KillCoroutines(x);
            Coroutines.Clear();
            Kills.Clear();
            Deaths.Clear();
            ScpsRecontained.Clear();
            Escapes.Clear();
            FirstDeath = "empty";
            FirstKill = "empty";
        }

        public void OnRoundStart()
        {
            if (LobbyHandle.IsRunning)
                Timing.KillCoroutines(LobbyHandle);

            if (Plugin.Config.IsEnabled)
                Coroutines.Add(Timing.RunCoroutine(SendUI()));

            if(Plugin.Config.IsEnabled && Plugin.Config.EnableRespawnTimerElement)
                Coroutines.Add(Timing.RunCoroutine(RespawnTimer()));
        }

        public void OnWaitingForPlayers()
        {
            if (Plugin.Config.IsEnabled && Plugin.Config.EnableWaitingForUsersText)
                LobbyHandle = Timing.RunCoroutine(LobbyText());
        }


        public void OnJoin(Synapse.Api.Events.SynapseEventArguments.PlayerJoinEventArgs ev)
        {
            if(Kills.ContainsKey(ev.Player.UserId))
            Timing.CallDelayed(1f, () => Kills.Add(ev.Player.UserId, 0)); Deaths.Add(ev.Player.UserId, 0);
        }

        public void OnLeave(Synapse.Api.Events.SynapseEventArguments.PlayerLeaveEventArgs ev)
        {
            if(Kills.ContainsKey(ev.Player.UserId))
                Kills.Remove(ev.Player.UserId);

            if (Deaths.ContainsKey(ev.Player.UserId))
                Deaths.Remove(ev.Player.UserId);
        }


        public void GiveTextHint(Player player, string message, float duration = 5f)
        {
            player.Hub.hints.Show(new Hints.TextHint(message, new HintParameter[]
                {
                    new StringHintParameter("")
                }, HintEffectPresets.FadeInAndOut(duration), duration));
        }

        public void OnDamage(Synapse.Api.Events.SynapseEventArguments.PlayerDamageEventArgs ev)
        {
            if (Plugin.Config.EnableDamageElement)
                ev.Killer.GiveTextHint($"{Plugin.PluginTranslation.ActiveTranslation.DealtDamage.Replace("%damage%", ev.DamageAmount.ToString())}", 1);
        }


        public void OnKill(Synapse.Api.Events.SynapseEventArguments.PlayerDeathEventArgs ev)
        {
            if (ev.Killer != null && ev.Killer != ev.Victim && !Round.Get.RoundEnded)
            {
                if (!Kills.ContainsKey(ev.Killer.UserId))
                    Kills.Add(ev.Killer.UserId, 1);
                else
                    Kills[ev.Killer.UserId]++;

                if (!Deaths.ContainsKey(ev.Victim.UserId))
                    Deaths.Add(ev.Victim.UserId, 1);
                else
                    Deaths[ev.Victim.UserId]++;

                if(ev.Victim.TeamID == (int)Team.SCP)
                {
                    if (!ScpsRecontained.ContainsKey(ev.Killer.UserId))
                        ScpsRecontained.Add(ev.Killer.UserId, 1);
                    else
                        ScpsRecontained[ev.Killer.UserId]++;
                }

                if(FirstKill == "empty")
                    FirstKill = ev.Killer.DisplayName;
            }

            if (FirstDeath == "empty")
                FirstDeath = ev.Killer.DisplayName;

            if (Plugin.Config.EnableKillElement && ev.Killer != ev.Victim)
            {
                if(RoleToText[ev.Victim.RoleID] != null && TeamToText[ev.Victim.TeamID] != null)
                    ev.Killer.SendBroadcast(5, Plugin.PluginTranslation.ActiveTranslation.Kill + $" <color=yellow>{ev.Victim.DisplayName}</color>\n{Plugin.PluginTranslation.ActiveTranslation.Role}: {RoleToText[ev.Victim.RoleID]}\n{Plugin.PluginTranslation.ActiveTranslation.Team}: {TeamToText[ev.Victim.TeamID]}", true);
                else if(RoleToText[ev.Victim.RoleID] == null && TeamToText[ev.Victim.TeamID] != null)
                    ev.Killer.SendBroadcast(5, Plugin.PluginTranslation.ActiveTranslation.Kill + $" <color=yellow>{ev.Victim.DisplayName}</color>\n{Plugin.PluginTranslation.ActiveTranslation.Role}: {ev.Victim.RoleName}\n{Plugin.PluginTranslation.ActiveTranslation.Team}: {TeamToText[ev.Victim.TeamID]}", true);
                else if (RoleToText[ev.Victim.RoleID] == null && TeamToText[ev.Victim.TeamID] == null)
                    ev.Killer.SendBroadcast(5, Plugin.PluginTranslation.ActiveTranslation.Kill + $" <color=yellow>{ev.Victim.DisplayName}</color>\n{Plugin.PluginTranslation.ActiveTranslation.Role}: {ev.Victim.RoleName}\n{Plugin.PluginTranslation.ActiveTranslation.Team}: {ev.Victim.Team}", true);

                if (RoleToText[ev.Killer.RoleID] != null && TeamToText[ev.Killer.TeamID] != null)
                    ev.Victim.OpenReportWindow(Plugin.PluginTranslation.ActiveTranslation.Killed + $" <color=yellow>{ev.Killer.DisplayName}</color>\n{Plugin.PluginTranslation.ActiveTranslation.Role}: {RoleToText[ev.Killer.RoleID]}\n{Plugin.PluginTranslation.ActiveTranslation.Team}: {TeamToText[ev.Killer.TeamID]}");
                else if (RoleToText[ev.Killer.RoleID] == null && TeamToText[ev.Killer.TeamID] != null)
                    ev.Victim.OpenReportWindow(Plugin.PluginTranslation.ActiveTranslation.Killed + $" <color=yellow>{ev.Killer.DisplayName}</color>\n{Plugin.PluginTranslation.ActiveTranslation.Role}: {ev.Killer.RoleName}\n{Plugin.PluginTranslation.ActiveTranslation.Team}: {TeamToText[ev.Killer.TeamID]}");
                else if (RoleToText[ev.Killer.RoleID] == null && TeamToText[ev.Killer.TeamID] == null)
                    ev.Victim.OpenReportWindow(Plugin.PluginTranslation.ActiveTranslation.Killed + $" <color=yellow>{ev.Killer.DisplayName}</color>\n{Plugin.PluginTranslation.ActiveTranslation.Role}: {ev.Killer.RoleName}\n{Plugin.PluginTranslation.ActiveTranslation.Team}: {ev.Killer.Team}");
            }
        }

        public void OnEscape(Synapse.Api.Events.SynapseEventArguments.PlayerEscapeEventArgs ev)
        {
            if (!Escapes.ContainsKey(ev.Player.UserId) && ev.Allow)
                Escapes.Add(ev.Player.UserId, Round.Get.RoundLength);

        }
       
        public void OnScp096Rage(Synapse.Api.Events.SynapseEventArguments.Scp096AddTargetEventArgument ev)
        {
            if (ev.RageState != PlayableScps.Scp096PlayerState.Calming && Plugin.Config.Enable096IndicatorElement)
            {
                VoidAPI.GiveTextHint(ev.Player, $"{Plugin.PluginTranslation.ActiveTranslation.Scp096TriggerHuman}", 5);
                VoidAPI.GiveTextHint(ev.Scp096, $"{Plugin.PluginTranslation.ActiveTranslation.Scp096TriggerSelf.Replace("%amount%", ev.Scp096.Scp096Controller.Targets.ToString())}", 5);
            }
        }

        public StringBuilder BuildUI(StringBuilder UI, Player players)
        {
            if (!Round.Get.RoundEnded)
            {
                    if (Plugin.Config.EnableTeamCountElement)
                    {
                        switch (players.TeamID)
                        {
                            case 0:
                                Dictionary<int, string> tempRoles = new Dictionary<int, string>();
                                if (Plugin.Config.EnableStatsElements)
                                {
                                    if (HasInventory(players))
                                    {
                                        UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Team}:</pos><pos=78%>{Plugin.PluginTranslation.ActiveTranslation.Stats}:</pos>\n");
                                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp049}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp049)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KillStats}:</color> <color=yellow>{Kills[players.UserId]}</color></pos></align>\n");
                                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp0492}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp0492)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.DeathStats}:</color></pos> <color=yellow>{Deaths[players.UserId]}</color></pos></align>\n");
                                        if (Kills[players.UserId] != 0 && Deaths[players.UserId] != 0)
                                            UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp079}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp079)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>{Kills[players.UserId] / Deaths[players.UserId]}</color></pos></align>\n");
                                        else
                                            UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp079}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp079)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>0</color></pos></align>\n");
                                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp096}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp096)}x</color></pos></align>\n");
                                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp106}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp106)}x</color></pos></align>\n");
                                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp173}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp173)}x</color></pos></align>\n");
                                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp939}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp93953) + Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp93989)}x</color></pos></align>\n");
                                        foreach (Player x in Server.Get.Players.Where(p => p.CustomRole != null && p.RealTeam == Team.SCP))
                                            tempRoles.Add(x.RoleID, x.RoleName);
                                        foreach (var element in tempRoles)
                                            UI.Append($"<align=left><pos=-20%><color=red>{element.Value}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == element.Key)}x</color></pos></align>\n");
                                        tempRoles.Clear();
                                        UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Targets}: <color=yellow>{Server.Get.GetPlayers(x => x.RealTeam == Team.MTF || x.RealTeam == Team.CDP || x.RealTeam == Team.RSC).Count}</color></pos></align>\n");
                                        break;
                                    }
                                    else if(!Plugin.Config.EnableTargetCounter || !HasInventory(players))
                                    {
                                        UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Team}:</pos><pos=78%>{Plugin.PluginTranslation.ActiveTranslation.Stats}:</pos>\n");
                                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp049}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp049)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KillStats}:</color> <color=yellow>{Kills[players.UserId]}</color></pos></align>\n");
                                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp0492}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp0492)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.DeathStats}:</color></pos> <color=yellow>{Deaths[players.UserId]}</color></pos></align>\n");
                                        if (Kills[players.UserId] != 0 && Deaths[players.UserId] != 0)
                                            UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp079}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp079)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>{Kills[players.UserId] / Deaths[players.UserId]}</color></pos></align>\n");
                                        else
                                            UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp079}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp079)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>0</color></pos></align>\n");
                                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp096}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp096)}x</color></pos></align>\n");
                                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp106}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp106)}x</color></pos></align>\n");
                                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp173}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp173)}x</color></pos></align>\n");
                                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp939}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp93953) + Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp93989)}x</color></pos></align>\n");
                                        foreach (Player x in Server.Get.Players.Where(p => p.CustomRole != null && p.RealTeam == Team.SCP))
                                            tempRoles.Add(x.RoleID, x.RoleName);
                                        foreach (var element in tempRoles)
                                            UI.Append($"<align=left><pos=-20%><color=red>{element.Value}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == element.Key)}x</color></pos></align>\n");
                                        tempRoles.Clear();
                                        break;
                                    }
                                } else
                                {
                                    UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Team}:</pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp049}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp049)}x</color></pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp0492}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp0492)}x</color></pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp079}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp079)}x</color></pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp096}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp096)}x</color></pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp106}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp106)}x</color></pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp173}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp173)}x</color></pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.Scp939}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp93953) + Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scp93989)}x</color></pos></align>\n");
                                    foreach (Player x in Server.Get.Players.Where(p => p.CustomRole != null && p.RealTeam == Team.SCP))
                                        tempRoles.Add(x.RoleID, x.RoleName);
                                    foreach(var element in tempRoles)
                                            UI.Append($"<align=left><pos=-20%><color=red>{element.Value}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == element.Key)}x</color></pos></align>\n");
                                    tempRoles.Clear();
                                    break;
                                }
                            break;
                            case 1:
                                if (Plugin.Config.EnableStatsElements)
                                {
                                    UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Team}:</pos><pos=78%>{Plugin.PluginTranslation.ActiveTranslation.Stats}:</pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=#0009FF>{Plugin.PluginTranslation.ActiveTranslation.Captains}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.NtfCaptain)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KillStats}:</color> <color=yellow>{Kills[players.UserId]}</color></pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=#1782FF>{Plugin.PluginTranslation.ActiveTranslation.Sergeants}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.NtfSergeant) + Server.Get.Players.Count(p => p.RoleID == (int)RoleType.NtfSpecialist)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.DeathStats}:</color></pos> <color=yellow>{Deaths[players.UserId]}</color></pos></align>\n");
                                    if (Kills[players.UserId] != 0 && Deaths[players.UserId] != 0)
                                        UI.Append($"<align=left><pos=-20%><color=#1782FF>{Plugin.PluginTranslation.ActiveTranslation.Privats}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.NtfPrivate)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>{Kills[players.UserId] / Deaths[players.UserId]}</color></pos></align>\n");
                                    else
                                        UI.Append($"<align=left><pos=-20%><color=#1782FF>{Plugin.PluginTranslation.ActiveTranslation.Privats}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.NtfPrivate)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>0</color></pos></align>\n");
                                UI.Append($"<align=left><pos=-20%><color=#575550>{Plugin.PluginTranslation.ActiveTranslation.Guards}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.FacilityGuard)}x</color></pos></align>\n");
                                    break;
                                }
                                else
                                {
                                    UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Team}:</align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=#0009FF>{Plugin.PluginTranslation.ActiveTranslation.Captains}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.NtfCaptain)}x</color></pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=#1782FF>{Plugin.PluginTranslation.ActiveTranslation.Sergeants}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.NtfSergeant) + Server.Get.Players.Count(p => p.RoleID == (int)RoleType.NtfSpecialist)}x</color></pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=#1782FF>{Plugin.PluginTranslation.ActiveTranslation.Privats}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.NtfPrivate)}x</color></pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=#575550>{Plugin.PluginTranslation.ActiveTranslation.Guards}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.FacilityGuard)}x</color></pos></align>\n");
                                    break;
                                }
                            case 2:
                                if (Plugin.Config.EnableStatsElements)
                                {
                                    UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Team}:</pos><pos=78%>{Plugin.PluginTranslation.ActiveTranslation.Stats}:</pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=#1B7805>{Plugin.PluginTranslation.ActiveTranslation.ChaosRepressor}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.ChaosRepressor)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KillStats}:</color> <color=yellow>{Kills[players.UserId]}</color></pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=#1B7805>{Plugin.PluginTranslation.ActiveTranslation.ChaosMarauder}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.ChaosMarauder)}x</pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.DeathStats}:</color></pos> <color=yellow>{Deaths[players.UserId]}</color></pos></align>\n");
                                if (Kills[players.UserId] != 0 && Deaths[players.UserId] != 0)
                                        UI.Append($"<align=left><pos=-20%><color=#1B7805>{Plugin.PluginTranslation.ActiveTranslation.ChaosRifleman}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.ChaosRifleman)}x</pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>{Kills[players.UserId] / Deaths[players.UserId]}</color></pos></align>\n");
                                    else
                                        UI.Append($"<align=left><pos=-20%><color=#1B7805>{Plugin.PluginTranslation.ActiveTranslation.ChaosRifleman}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.ChaosRifleman)}x</pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>0</color></pos></align>\n");
                                }
                                else
                                {
                                    UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Team}:</pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=#1B7805>{Plugin.PluginTranslation.ActiveTranslation.ChaosRepressor}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.ChaosRepressor)}x</color></pos></align>");
                                UI.Append($"<align=left><pos=-20%><color=#1B7805>{Plugin.PluginTranslation.ActiveTranslation.ChaosMarauder}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.ChaosMarauder)}x</color></pos></align>");
                                UI.Append($"<align=left><pos=-20%><color=#1B7805>{Plugin.PluginTranslation.ActiveTranslation.ChaosRifleman}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.ChaosRifleman)}x</color></pos></align>");
                            }
                                break;
                            case 3:
                                if (Plugin.Config.EnableStatsElements)
                                {
                                    UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Team}:</pos><pos=78%>{Plugin.PluginTranslation.ActiveTranslation.Stats}:</pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=#FFED01>{Plugin.PluginTranslation.ActiveTranslation.Scientists}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scientist)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KillStats}:</color> <color=yellow>{Kills[players.UserId]}</color></pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.DeathStats}:</color></pos> <color=yellow>{Deaths[players.UserId]}</color></pos></align>\n");
                                    if (Kills[players.UserId] != 0 && Deaths[players.UserId] != 0)
                                        UI.Append($"<align=left><pos=-20%></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>{Kills[players.UserId] / Deaths[players.UserId]}</color></pos></align>\n");
                                    else
                                        UI.Append($"<align=left><pos=-20%></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>0</color></pos></align>\n");
                                    break;
                                }
                                else
                                {
                                    UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Team}:</pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=#FFED01>{Plugin.PluginTranslation.ActiveTranslation.Scientists}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.Scientist)}x</color></pos></align>");
                                    break;
                                }
                            case 4:
                                if (Plugin.Config.EnableStatsElements)
                                {
                                    UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Team}:</pos><pos=78%>{Plugin.PluginTranslation.ActiveTranslation.Stats}:</pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=#FF9302>{Plugin.PluginTranslation.ActiveTranslation.ClassD}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.ClassD)}x</color></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KillStats}:</color> <color=yellow>{Kills[players.UserId]}</color></pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.DeathStats}:</color></pos> <color=yellow>{Deaths[players.UserId]}</color></pos></align>\n");
                                    if (Kills[players.UserId] != 0 && Deaths[players.UserId] != 0)
                                        UI.Append($"<align=left><pos=-20%></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>{Kills[players.UserId] / Deaths[players.UserId]}</color></pos></align>\n");
                                    else
                                        UI.Append($"<align=left><pos=-20%></pos><pos=78%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>0</color></pos></align>\n");
                                    break;
                                }
                                else
                                {
                                    UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Team}:</pos></align>\n");
                                    UI.Append($"<align=left><pos=-20%><color=#FF9302>{Plugin.PluginTranslation.ActiveTranslation.ClassD}: </color><color=yellow>{Server.Get.Players.Count(p => p.RoleID == (int)RoleType.ClassD)}x</color></pos></align>\n");
                                    break;
                                }

                        }
                    }
                    else
                    {
                        if (Plugin.Config.EnableStatsElements && Plugin.Config.EnableTargetCounter && players.RealTeam == Team.SCP)
                        {
                            UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Stats}:</pos><pos=78%>{Plugin.PluginTranslation.ActiveTranslation.Targets}: <color=yellow>{Server.Get.GetPlayers(x => x.RealTeam == Team.MTF || x.RealTeam == Team.CDP || x.RealTeam == Team.RSC).Count}</color></pos></align>\n");
                            UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KillStats}:</color> <color=yellow>{Kills[players.UserId]}</color></pos></align>\n");
                            UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.DeathStats}:</color></pos> <color=yellow>{Deaths[players.UserId]}</color></pos></align>\n");
                            if (Kills[players.UserId] != 0 && Deaths[players.UserId] != 0)
                                UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>{Kills[players.UserId] / Deaths[players.UserId]}</color></pos></align>\n");
                            else
                                UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>0</color></pos></align>\n");
                        }
                        else if(!Plugin.Config.EnableStatsElements && Plugin.Config.EnableTargetCounter && players.RealTeam == Team.SCP)
                            UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Targets}: <color=yellow>{Server.Get.GetPlayers(x => x.RealTeam == Team.MTF || x.RealTeam == Team.CDP || x.RealTeam == Team.RSC).Count}</color></pos></align>\n");
                    }

                if (!Plugin.Config.EnableTeamCountElement && Plugin.Config.EnableStatsElements && !Plugin.Config.EnableTargetCounter)
                {
                    UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Stats}:</pos></align>\n");
                    UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KillStats}:</color> <color=yellow>{Kills[players.UserId]}</color></pos></align>\n");
                    UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.DeathStats}:</color></pos> <color=yellow>{Deaths[players.UserId]}</color></pos></align>\n");
                    if (Kills[players.UserId] != 0 && Deaths[players.UserId] != 0)
                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>{Math.Round(Kills[players.UserId] / Deaths[players.UserId])}</color></pos></align>\n");
                    else
                        UI.Append($"<align=left><pos=-20%><color=red>{Plugin.PluginTranslation.ActiveTranslation.KdRStats}:</color></pos> <color=yellow>0</color></pos></align>\n");
                }
                
                if (Plugin.Config.EnableTotalAmmoElement && Plugin.Config.EnableGrenadesElement)
                {
                    if (HasInventory(players))
                    {
                        int frags = 0;
                        int flashes = 0;
                        int scp018 = 0;
                        foreach (var items in players.Inventory.Items)
                        {
                            if (items.ID == (int)ItemType.GrenadeHE)
                                frags++;
                            if (items.ID == (int)ItemType.GrenadeFlash)
                                flashes++;
                            if (items.ID == (int)ItemType.SCP018)
                                scp018++;
                        }
                        UI.Append($"\n<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Ammo}:</pos><pos=78%>{Plugin.PluginTranslation.ActiveTranslation.Grenades}:</align></pos>\n");
                        UI.Append($"<align=left><pos=-20%><color=#00FFD2>{Plugin.PluginTranslation.ActiveTranslation.AmmoGauge}: </color><color=yellow>{players.AmmoBox[AmmoType.Ammo9x19]}</color></pos><pos=78%> <color=#FF6E01>{Plugin.PluginTranslation.ActiveTranslation.Frag}: </color><color=yellow>{frags}</color></align></pos>\n");
                        UI.Append($"<align=left><pos=-20%><color=#00FFD2>{Plugin.PluginTranslation.ActiveTranslation.AmmoCal44}: </color><color=yellow>{players.AmmoBox[AmmoType.Ammo44cal]}</color></pos><pos=78%> <color=#FF6E01>{Plugin.PluginTranslation.ActiveTranslation.Flash}: </color><color=yellow>{flashes}</color></align></pos>\n");
                        UI.Append($"<align=left><pos=-20%><color=#00FFD2>{Plugin.PluginTranslation.ActiveTranslation.Ammo556}: </color><color=yellow>{players.AmmoBox[AmmoType.Ammo556x45]}</color></pos><pos=78%> <color=#FF6E01>{Plugin.PluginTranslation.ActiveTranslation.Scp018}: </color><color=yellow>{scp018}</color></align></pos>\n");
                        UI.Append($"<align=left><pos=-20%><color=#00FFD2>{Plugin.PluginTranslation.ActiveTranslation.Ammo762}: </color><color=yellow>{players.AmmoBox[AmmoType.Ammo762x39]}</color></align></pos>\n");
                        UI.Append($"<align=left><pos=-20%><color=#00FFD2>{Plugin.PluginTranslation.ActiveTranslation.Ammo919}: </color><color=yellow>{players.AmmoBox[AmmoType.Ammo9x19]}</color></align></pos>\n");
                    }
                }
                else if (Plugin.Config.EnableTotalAmmoElement && !Plugin.Config.EnableGrenadesElement)
                {
                    if (HasInventory(players))
                    {
                        UI.Append($"\n<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Ammo}:</align></pos>\n");
                        UI.Append($"<align=left><pos=-20%><color=#00FFD2>{Plugin.PluginTranslation.ActiveTranslation.AmmoGauge}: </color><color=yellow>{players.AmmoBox[AmmoType.Ammo12gauge]}</color></pos></align>\n");
                        UI.Append($"<align=left><pos=-20%><color=#00FFD2>{Plugin.PluginTranslation.ActiveTranslation.AmmoCal44}: </color><color=yellow>{players.AmmoBox[AmmoType.Ammo44cal]}</color></pos></align>\n");
                        UI.Append($"<align=left><pos=-20%><color=#00FFD2>{Plugin.PluginTranslation.ActiveTranslation.Ammo556}: </color><color=yellow>{players.AmmoBox[AmmoType.Ammo556x45]}</color></pos></align>\n");
                        UI.Append($"<align=left><pos=-20%><color=#00FFD2>{Plugin.PluginTranslation.ActiveTranslation.Ammo762}: </color><color=yellow>{players.AmmoBox[AmmoType.Ammo762x39]}</color></align></pos>\n");
                        UI.Append($"<align=left><pos=-20%><color=#00FFD2>{Plugin.PluginTranslation.ActiveTranslation.Ammo919}: </color><color=yellow>{players.AmmoBox[AmmoType.Ammo9x19]}</color></align></pos>\n");
                    }
                }
                else if (!Plugin.Config.EnableTotalAmmoElement && Plugin.Config.EnableGrenadesElement)
                {
                    if (HasInventory(players))
                    {
                        int frags = 0;
                        int flashes = 0;
                        int scp018 = 0;
                        foreach (var items in players.Inventory.Items)
                        {
                            if (items.ID == (int)ItemType.GrenadeHE)
                                frags++;
                            if (items.ID == (int)ItemType.GrenadeFlash)
                                flashes++;
                            if (items.ID == (int)ItemType.SCP018)
                                scp018++;
                        }
                        UI.Append($"\n<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.Grenades}:</align></pos>\n");
                        UI.Append($"<align=left><pos=-20%><color=#FF6E01>{Plugin.PluginTranslation.ActiveTranslation.Frag}: </color><color=yellow>{frags}</color></align></pos>\n");
                        UI.Append($"<align=left><pos=-20%><color=#FF6E01>{Plugin.PluginTranslation.ActiveTranslation.Flash}: </color><color=yellow>{flashes}</color></align></pos>\n");
                        UI.Append($"<align=left><pos=-20%><color=#FF6E01>{Plugin.PluginTranslation.ActiveTranslation.Scp018}: </color><color=yellow>{scp018}</color></align></pos>\n");
                    }
                }
            }
            else
            {
                if (Plugin.Config.EnableLeaderboard)
                {
                    if (!FirstKill.Contains("empty"))
                    {
                        UI.Append($"\n\n<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.FirstKillAndDeath}</align></pos>\n");
                        UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.FirstKill.Replace("%player%", FirstKill)}</align></pos>\n");
                        UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.FirstDeath.Replace("%player%", FirstDeath)}</align></pos>\n\n");
                    }

                    UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.FastestEscapeTitel}:</align></pos>\n");
                    List<TimeSpan> EscapeList = Escapes.Values.ToList();
                    EscapeList.Sort();
                    if (EscapeList.IsEmpty())
                        UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.NoEscape}</align></pos>\n");
                    else
                    {
                        if (Escapes.Keys.Count <= 5)
                        {
                            for (int i = 0; i < Escapes.Keys.Count; i++)
                            {
                                UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.FastestEscape.Replace("%player%", Server.Get.GetPlayer(Escapes.FirstOrDefault(x => x.Value == EscapeList[i]).Key).DisplayName).Replace("%time%", EscapeList[i].Minutes + ":" + EscapeList[i].Seconds + Plugin.PluginTranslation.ActiveTranslation.Minutes).Replace("%position%", (i + 1).ToString())}</align></pos>\n\n");
                            }
                        }
                    }

                    List<float> ScpsRecontainedList = ScpsRecontained.Values.ToList();
                    ScpsRecontainedList.Sort();
                    ScpsRecontainedList.Reverse();

                    UI.Append($"\n<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.ScpsRecontainedTitel}</align></pos>\n");
                    if (ScpsRecontainedList.IsEmpty())
                        UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.ScpsRecontainedNone}</align></pos>\n");
                    else
                    {
                        if (Escapes.Keys.Count <= 3)
                        {
                            for (int i = 0; i < ScpsRecontained.Keys.Count; i++)
                            {
                                UI.Append($"<align=left><pos=-20%>{Plugin.PluginTranslation.ActiveTranslation.ScpsRecontained.Replace("%player%", Server.Get.GetPlayer(ScpsRecontained.FirstOrDefault(x => x.Value == ScpsRecontainedList[i]).Key).DisplayName).Replace("%SCPS%", ScpsRecontainedList[0].ToString()).Replace("%position%", (i+1).ToString())}</align></pos>\n");
                            }
                        }
                    }
                }
            }
            
   
            return UI;
        }

        public bool HasInventory(Player player)
        {
            switch(player.RoleType)
            {
                case RoleType.None:
                case RoleType.Spectator:
                case RoleType.Scp93989:
                case RoleType.Scp93953:
                case RoleType.Scp173:
                case RoleType.Scp106:
                case RoleType.Scp096:
                case RoleType.Scp079:
                case RoleType.Scp0492:
                case RoleType.Scp049:
                    return false;
                default:
                    return true;
            }
        }

        public IEnumerator<float> LobbyText()
        {
            int r = 255, g = 0, b = 0;
            while (true)
            {
                var hexColor = $"#{r:X2}{g:X2}{b:X2}";

                if (r > 0 && b == 0)
                {
                    r -= 3;
                    g += 3;
                }

                if (g > 0 && r == 0)
                {
                    g -= 3;
                    b += 3;
                }

                if (b > 0 && g == 0)
                {
                    b -= 3;
                    r += 3;
                }
                foreach (Player players in Server.Get.Players)
                    GiveTextHint(players, Plugin.Config.WaitingForUsersText.Replace("%rainbow%", hexColor), 1f);

                yield return Timing.WaitForSeconds(0.5f);
            }
  
        }

            public IEnumerator<float> RespawnTimer()
            {
            while (!Round.Get.RoundEnded)
            {
                float time = Round.Get.NextRespawn;
                var respawnTime = TimeSpan.FromSeconds(time);
                var roundTime = Round.Get.RoundLength;
                foreach (Player player in Server.Get.Players)
                {
                    if(player.RoleID == (int)RoleType.Spectator)
                    {
                        if(respawnTime.TotalSeconds > 17 && roundTime.TotalSeconds > 60)
                        {
                            player.SendBroadcast(2, $"<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.Respawn} <b><color=#FFF203>{respawnTime.Minutes} {Plugin.PluginTranslation.ActiveTranslation.Minutes} & {respawnTime.Seconds} {Plugin.PluginTranslation.ActiveTranslation.Seconds}</color></b>.</i></size>\n<size=30><i><b><color=blue>{Plugin.PluginTranslation.ActiveTranslation.MTF}:</color> {Round.Get.MtfTickets}</b></i><color=#666666> | </color><i><b><color=green>{Plugin.PluginTranslation.ActiveTranslation.CI}:</color> {Round.Get.ChaosTickets}</b></i><color=#666666> | </color><i><b><color=#666666>{Plugin.PluginTranslation.ActiveTranslation.Spectators}:</color> {RoleType.Spectator.GetPlayers().Count()}</b></i></size>\n<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.RoundTime}: <b><color=#FFF203>{roundTime.Minutes} {Plugin.PluginTranslation.ActiveTranslation.Minutes} & {roundTime.Seconds} {Plugin.PluginTranslation.ActiveTranslation.Seconds}</color></b></i></size>", true);
                        }
                        else if (respawnTime.TotalSeconds < 17 && roundTime.TotalSeconds > 60 && Respawning.RespawnManager.Singleton.NextKnownTeam == Respawning.SpawnableTeamType.ChaosInsurgency)
                        {
                            player.SendBroadcast(2, $"<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.Respawning} <b><color=green>{Plugin.PluginTranslation.ActiveTranslation.ChaosInsurgency}</color></b>.</i></size>\n<size=30><i><b><color=blue>NTF:</color> {Round.Get.MtfTickets}</b></i><color=#666666> | </color><i><b><color=green>CI:</color> {Round.Get.ChaosTickets}</b></i><color=#666666> | </color><i><b><color=#666666>{Plugin.PluginTranslation.ActiveTranslation.Spectators}:</color> {RoleType.Spectator.GetPlayers().Count()}</b></i></size>\n<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.RoundTime}: <b><color=#FFF203>{roundTime.Minutes} {Plugin.PluginTranslation.ActiveTranslation.Minutes} & {roundTime.Seconds} {Plugin.PluginTranslation.ActiveTranslation.Seconds}</color></b></i></size>", true);
                        }
                        else if (respawnTime.TotalSeconds < 17 && roundTime.TotalSeconds > 60 && Respawning.RespawnManager.Singleton.NextKnownTeam == Respawning.SpawnableTeamType.NineTailedFox)
                        {
                            player.SendBroadcast(2, $"<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.Respawning} <b><color=blue>{Plugin.PluginTranslation.ActiveTranslation.MobileTaskForce}</color></b>.</i></size>\n<size=30><i><b><color=blue>NTF:</color> {Round.Get.MtfTickets}</b></i><color=#666666> | </color><i><b><color=green>CI:</color> {Round.Get.ChaosTickets}</b></i><color=#666666> | </color><i><b><color=#666666>{Plugin.PluginTranslation.ActiveTranslation.Spectators}:</color> {RoleType.Spectator.GetPlayers().Count()}</b></i></size>\n<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.RoundTime}: <b><color=#FFF203>{roundTime.Minutes} {Plugin.PluginTranslation.ActiveTranslation.Minutes} & {roundTime.Seconds} {Plugin.PluginTranslation.ActiveTranslation.Seconds}</color></b></i></size>", true);
                        }
                        else if (respawnTime.TotalSeconds > 17 && roundTime.TotalSeconds < 60)
                        {
                            player.SendBroadcast(2, $"<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.Respawn} <b><color=#FFF203>{respawnTime.Minutes} {Plugin.PluginTranslation.ActiveTranslation.Minutes} & {respawnTime.Seconds} {Plugin.PluginTranslation.ActiveTranslation.Seconds}</color></b>.</i></size>\n<size=30><i><b><color=blue>{Plugin.PluginTranslation.ActiveTranslation.MTF}:</color> {Round.Get.MtfTickets}</b></i><color=#666666> | </color><i><b><color=green>{Plugin.PluginTranslation.ActiveTranslation.CI}:</color> {Round.Get.ChaosTickets}</b></i><color=#666666> | </color><i><b><color=#666666>{Plugin.PluginTranslation.ActiveTranslation.Spectators}:</color> {RoleType.Spectator.GetPlayers().Count()}</b></i></size>\n<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.RoundTime}: <b><color=#FFF203>{roundTime.Seconds} {Plugin.PluginTranslation.ActiveTranslation.Seconds}</color></b></i></size>", true);
                        }
                        else if (respawnTime.TotalSeconds < 17 && roundTime.TotalSeconds < 60 && Respawning.RespawnManager.Singleton.NextKnownTeam == Respawning.SpawnableTeamType.ChaosInsurgency)
                        {
                            player.SendBroadcast(2, $"<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.Respawning} <b><color=blue>{Plugin.PluginTranslation.ActiveTranslation.ChaosInsurgency}</color></b>.</i></size>\n<size=30><i><b><color=blue>{Plugin.PluginTranslation.ActiveTranslation.MTF}:</color> {Round.Get.MtfTickets}</b></i><color=#666666> | </color><i><b><color=green>{Plugin.PluginTranslation.ActiveTranslation.CI}:</color> {Round.Get.ChaosTickets}</b></i><color=#666666> | </color><i><b><color=#666666>{Plugin.PluginTranslation.ActiveTranslation.Spectators}:</color> {RoleType.Spectator.GetPlayers().Count()}</b></i></size>\n<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.RoundTime}: <b><color=#FFF203>{roundTime.Seconds} {Plugin.PluginTranslation.ActiveTranslation.Seconds}</color></b></i></size>", true);
                        }
                        else if (respawnTime.TotalSeconds < 17 && roundTime.TotalSeconds < 60 && Respawning.RespawnManager.Singleton.NextKnownTeam == Respawning.SpawnableTeamType.NineTailedFox)
                        {
                            player.SendBroadcast(2, $"<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.Respawning} <b><color=blue>{Plugin.PluginTranslation.ActiveTranslation.MobileTaskForce}</color></b>.</i></size>\n<size=30><i><b><color=blue>{Plugin.PluginTranslation.ActiveTranslation.MTF}:</color> {Round.Get.MtfTickets}</b></i><color=#666666> | </color><i><b><color=green>{Plugin.PluginTranslation.ActiveTranslation.CI}:</color> {Round.Get.ChaosTickets}</b></i><color=#666666> | </color><i><b><color=#666666>{Plugin.PluginTranslation.ActiveTranslation.Spectators}:</color> {RoleType.Spectator.GetPlayers().Count()}</b></i></size>\n<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.RoundTime}: <b><color=#FFF203>{roundTime.Seconds} {Plugin.PluginTranslation.ActiveTranslation.Seconds}</color></b></i></size>", true);
                        }
                        else if (respawnTime.TotalSeconds > 17 && respawnTime.TotalSeconds < 60 &&  roundTime.TotalSeconds < 60)
                        {
                            player.SendBroadcast(2, $"<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.Respawn} <b><color=#FFF203>{respawnTime.Seconds} {Plugin.PluginTranslation.ActiveTranslation.Seconds}</color></b>.</i></size>\n<size=30><i><b><color=blue>{Plugin.PluginTranslation.ActiveTranslation.MTF}:</color> {Round.Get.MtfTickets}</b></i><color=#666666> | </color><i><b><color=green>{Plugin.PluginTranslation.ActiveTranslation.CI}:</color> {Round.Get.ChaosTickets}</b></i><color=#666666> | </color><i><b><color=#666666>{Plugin.PluginTranslation.ActiveTranslation.Spectators}:</color> {RoleType.Spectator.GetPlayers().Count()}</b></i></size>\n<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.RoundTime}: <b><color=#FFF203>{roundTime.Seconds} {Plugin.PluginTranslation.ActiveTranslation.Seconds}</color></b></i></size>", true);
                        }
                        else if (respawnTime.TotalSeconds > 17 && respawnTime.TotalSeconds < 60 && roundTime.TotalSeconds > 60)
                        {
                            player.SendBroadcast(2, $"<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.Respawn} <b><color=#FFF203>{respawnTime.Seconds} {Plugin.PluginTranslation.ActiveTranslation.Seconds}</color></b>.</i></size>\n<size=30><i><b><color=blue>{Plugin.PluginTranslation.ActiveTranslation.MTF}:</color> {Round.Get.MtfTickets}</b></i><color=#666666> | </color><i><b><color=green>{Plugin.PluginTranslation.ActiveTranslation.CI}:</color> {Round.Get.ChaosTickets}</b></i><color=#666666> | </color><i><b><color=#666666>{Plugin.PluginTranslation.ActiveTranslation.Spectators}:</color> {RoleType.Spectator.GetPlayers().Count()}</b></i></size>\n<size=30><i>{Plugin.PluginTranslation.ActiveTranslation.RoundTime}: <b><color=#FFF203>{roundTime.Minutes} {Plugin.PluginTranslation.ActiveTranslation.Minutes} & {roundTime.Seconds} {Plugin.PluginTranslation.ActiveTranslation.Seconds}</color></b></i></size>", true);
                        }
                    }
                }
                yield return Timing.WaitForSeconds(1f);
            }
        }

        public IEnumerator<float> SendUI()
        {
            while (true)
            {
                foreach (Player players in Server.Get.Players)
                {
                    if (VoidAPI.NextTextHint[players] == null || VoidAPI.NextTextHint[players].Duration == 0)
                    {
                        StringBuilder UIElements = new StringBuilder();
                        BuildUI(UIElements, players);
                        GiveTextHint(players, UIElements.ToString(), 2);
                    }
                    else
                    {
                        StringBuilder UIElements = new StringBuilder();
                        BuildUI(UIElements, players);
                        UIElements.Append(VoidAPI.NextTextHint[players].Message);
                        GiveTextHint(players, UIElements.ToString(), 2);
                        if (VoidAPI.NextTextHint[players].Duration == 0)
                            VoidAPI.NextTextHint.Remove(players);
                        VoidAPI.NextTextHint[players].Duration--;

                    }
                }
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
