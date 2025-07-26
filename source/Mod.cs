using Verse;
using HarmonyLib;
using UnityEngine;

namespace SK.BiggerDevmodeButtons
{
    public class Mod : Verse.Mod
    {
        public Mod(ModContentPack content) : base(content)
        {
            Harmony instance = new Harmony("rimworld.sk.biggerdevmodebuttons");
            HarmonyPatcher.instance = instance;

            LongEventHandler.QueueLongEvent(Init, "SK.BiggerDevmodeButtons", true, null);
        }

        public override string SettingsCategory()
        {
            return "Bigger Devmode Buttons";
        }

        public override void DoSettingsWindowContents(Rect rect)
        {
            ModSettingsWindow.Draw(rect);
            base.DoSettingsWindowContents(rect);
        }

        public void Init()
        {
            GetSettings<ModSettings>();
            HarmonyPatcher.PatchVanillaMethods();
            Patches.Init();
        }
    }
}
