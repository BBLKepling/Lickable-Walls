using RimWorld;
using Verse;

namespace Lickable_Walls
{
    [DefOf]
    public static class InternalDefOf
    {
        static InternalDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
        }

        public static BodyPartDef Tongue;
        public static JobDef BBLK_Job_LickWall;
        public static ThingDef BBLK_LickableWallpaper;

        [MayRequire("Argon.ExpandedMaterials.Metals")]
        public static HediffDef EM_LeadPoisoning;

        [MayRequire("VE.VanillaChristmasExpanded")]
        public static ThingDef VCE_Gingerbread;
    }
}
