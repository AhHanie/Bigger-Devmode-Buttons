using UnityEngine;
using Verse;

namespace SK.BiggerDevmodeButtons
{
    public static class ModSettingsWindow
    {
        private static Vector2 scrollPosition = Vector2.zero;
        public static void Draw(Rect parent)
        {
            Listing_Standard list = new Listing_Standard(GameFont.Medium);
            Rect outerRect = new Rect(parent.x, parent.y + 20, parent.width, parent.height - 20);
            Rect innerRect = new Rect(outerRect);
            innerRect.x += 5;
            innerRect.width -= 35;
            innerRect.y += 10;
            innerRect.height -= 20;
            Rect scrollRect = new Rect(0f, innerRect.y, innerRect.width - 20f, parent.height * 1.1f);
            Widgets.DrawMenuSection(outerRect);
            Widgets.BeginScrollView(innerRect, ref scrollPosition, scrollRect, true);
            list.Begin(scrollRect);

            // Side Flanking
            Text.Font = GameFont.Medium;
            list.Label("SK_BiggerDevmodeButtons_DebugWindowColumnWidthLabel".Translate());
            ModSettings.debugWindowColumnWidth = (int)Widgets.HorizontalSlider(list.GetRect(22f), ModSettings.debugWindowColumnWidth, 0, 1000, false, ModSettings.debugWindowColumnWidth.ToString(), null, null, 5);
            
            list.End();
            Widgets.EndScrollView();
        }
    }
}
