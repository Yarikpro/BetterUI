using HarmonyLib;
using Synapse;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterUI
{
    [HarmonyPatch(typeof(Player), nameof(Player.GiveTextHint))]
    internal static class SynapsePatches
    {
        [HarmonyPrefix]
        private static bool GiveTextHintPatch(Player __instance, string message, float duration = 5f)
        {
            VoidAPI.GiveTextHint(__instance, message, duration);
            return false;
        }
    }

    public class VoidAPI
    {
        public static void GiveTextHint(Player player, string message, float duration)
        {
            NextTextHint[player] = new TextHint
            {
                Message = message,
                Duration = duration,
                FullDuration = duration
            };
        }

        public static readonly Dictionary<Player, TextHint> NextTextHint = new Dictionary<Player, TextHint>();
    }

    public class TextHint
    {


        public string Message { get; set; }
        public float Duration { get; set; }
        public float FullDuration { get; set; }
    }
}
