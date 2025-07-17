using HarmonyLib;
using LudeonTK;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace SK.BiggerDevmodeButtons
{
    public static class Patches
    {
        public static DebugTabMenuDef actionsTabDef;
        public static DebugTabMenuDef outputTabDef;
        public static float cachedTotalHeight = 0f;
        public static bool isTotalHeightCached = false;
        private static bool isRenderingButtonsTab = false;
        private static Rect windowRect;
        public static void ListOptionsPrefix(DebugTabMenu __instance, int highlightedIndex, ref float columnWidth)
        {
            if (__instance.def == actionsTabDef || __instance.def == outputTabDef)
            {
                columnWidth = ModSettings.debugWindowColumnWidth;
            }
        }

        public static void DoWindowContentsPrefix(Rect inRect, DebugTabMenu ___currentTabMenu, ref float ___totalOptionsHeight, out float __state)
        {
            // We cannot do the caching calculation here because for some reason ___totalOptionsHeight is alawys 0
            __state = ___totalOptionsHeight;
            if (___currentTabMenu.def == actionsTabDef || ___currentTabMenu.def == outputTabDef && isTotalHeightCached)
            {
                isRenderingButtonsTab = true;
                windowRect = inRect;
                ___totalOptionsHeight = cachedTotalHeight;
            }
        }

        public static void DoWindowContentsPostfix(Rect inRect, DebugTabMenu ___currentTabMenu, ref float ___totalOptionsHeight, float __state)
        {
            if (___currentTabMenu.def == actionsTabDef || ___currentTabMenu.def == outputTabDef)
            {
                isRenderingButtonsTab = false;
                if (___totalOptionsHeight != 0) {
                    if (!isTotalHeightCached)
                    {
                        const float ORIGINAL_COLUMN_WIDTH = 200f;
                        const float COLUMN_GAP = 17f;

                        float availableWidth = inRect.width - 16f;

                        int originalColumns = (int)(availableWidth / ORIGINAL_COLUMN_WIDTH);

                        int newColumns = Mathf.Max(1, (int)((availableWidth + COLUMN_GAP) / (ModSettings.debugWindowColumnWidth + COLUMN_GAP)));

                        // The totalOptionsHeight is distributed across columns, so we need to scale it
                        // If we have fewer columns, we need more height per column
                        float columnRatio = (float)originalColumns / (float)newColumns;
                        cachedTotalHeight = ___totalOptionsHeight * columnRatio;
                        isTotalHeightCached = true;
                    }
                }

                ___totalOptionsHeight = __state;
            }
        }

        public static bool NewColumnIfNeededPrefix(Dialog_Debug __instance, float columnWidth)
        {
            if (isRenderingButtonsTab && isTotalHeightCached)
            {
                float curX = Traverse.Create(__instance).Field("curX").GetValue<float>();
                if (curX + columnWidth > (windowRect.width - 16f))
                {
                    return false;
                }
            }
            return true;
        }

        public static void SwitchTabPrefix(ref float ___totalOptionsHeight)
        {
            ___totalOptionsHeight = 0;
            ResetCache();
        }

        public static void Init()
        {
            actionsTabDef = DefDatabase<DebugTabMenuDef>.AllDefsListForReading.Find((DebugTabMenuDef def) => def.defName == "Actions");
            outputTabDef = DefDatabase<DebugTabMenuDef>.AllDefsListForReading.Find((DebugTabMenuDef def) => def.defName == "Output");
        }

        public static void ToggleMenuPrefix()
        {
            ResetCache();
        }

        private static void ResetCache()
        {
            isTotalHeightCached = false;
            cachedTotalHeight = 0;
            isRenderingButtonsTab = false;;
        }
    }
}
