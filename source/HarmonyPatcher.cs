using System.Reflection;
using HarmonyLib;
using LudeonTK;
using Verse;

namespace SK.BiggerDevmodeButtons
{
    public class HarmonyPatcher
    {
        public static Harmony instance;
        public static void PatchVanillaMethods()
        {
            if (instance == null)
            {
                Logger.WriteToHarmonyFile("Missing harmony instance");
                return;
            }

            // Patch DebugTabMenu ListOptions
            MethodInfo listOptionsMethod = AccessTools.Method(typeof(DebugTabMenu), "ListOptions");
            HarmonyMethod listOptionsPrefixPatchMethod = new HarmonyMethod(typeof(Patches).GetMethod("ListOptionsPrefix"));
            instance.Patch(listOptionsMethod, listOptionsPrefixPatchMethod);

            // Patch Dialog_Debug DoWindowContents
            MethodInfo doWindowContentsMethod = AccessTools.Method(typeof(Dialog_Debug), "DoWindowContents");
            HarmonyMethod doWindowContentsPrefixMethod = new HarmonyMethod(typeof(Patches).GetMethod("DoWindowContentsPrefix"));
            HarmonyMethod doWindowContentsPostfixMethod = new HarmonyMethod(typeof(Patches).GetMethod("DoWindowContentsPostfix"));
            instance.Patch(doWindowContentsMethod, doWindowContentsPrefixMethod, doWindowContentsPostfixMethod);

            // Patch Dialog_Debug NewColumnIfNeeded
            MethodInfo newColumnIfNeededMethod = AccessTools.Method(typeof(Dialog_Debug), "NewColumnIfNeeded");
            HarmonyMethod NewColumnIfNeededPrefixPatchMethod = new HarmonyMethod(typeof(Patches).GetMethod("NewColumnIfNeededPrefix"));
            instance.Patch(newColumnIfNeededMethod, NewColumnIfNeededPrefixPatchMethod);

            // Patch Dialog_Debug SwitchTab
            MethodInfo switchTabMethod = AccessTools.Method(typeof(Dialog_Debug), "SwitchTab");
            HarmonyMethod switchTabPrefixPatchMethod = new HarmonyMethod(typeof(Patches).GetMethod("SwitchTabPrefix"));
            instance.Patch(switchTabMethod, switchTabPrefixPatchMethod);

            // Patch Dialog_Debug SwitchTab
            MethodInfo toggleDebugActionsMenuMethod = AccessTools.Method(typeof(DebugWindowsOpener), "ToggleDebugActionsMenu");
            MethodInfo toggleDebugLogMenuMethod = AccessTools.Method(typeof(DebugWindowsOpener), "ToggleDebugLogMenu");
            HarmonyMethod toggleMenuPrefixPatchMethod = new HarmonyMethod(typeof(Patches).GetMethod("ToggleMenuPrefix"));
            instance.Patch(toggleDebugActionsMenuMethod, toggleMenuPrefixPatchMethod);
            instance.Patch(toggleDebugLogMenuMethod, toggleMenuPrefixPatchMethod);
        }
    }
}
