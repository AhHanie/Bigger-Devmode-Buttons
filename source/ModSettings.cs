using Verse;

namespace SK.BiggerDevmodeButtons
{
    public class ModSettings : Verse.ModSettings
    {
        public static int debugWindowColumnWidth = 300;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref debugWindowColumnWidth, "debugWindowColumnWidth", 300);
        }
    }
}
