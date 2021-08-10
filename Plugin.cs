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
            }, "GERMAN");
            PatchMethods();
            SynapseController.Server.Logger.Info("BetterUI loaded!");
            new EventHandlers();
        }

        public override void ReloadConfigs()
        {

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
