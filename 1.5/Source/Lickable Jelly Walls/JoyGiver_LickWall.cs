using RimWorld;
using Verse.AI;
using Verse;

namespace Lickable_Walls
{
    public class JoyGiver_LickWall : JoyGiver
    {
        public override Job TryGiveJob(Pawn pawn)
        {
             if (!(GenClosest.ClosestThingReachable(
                pawn.Position, 
                pawn.Map, 
                ThingRequest.ForGroup(ThingRequestGroup.BuildingArtificial), 
                PathEndMode.Touch, 
                TraverseParms.For(pawn), 
                30, 
                (Thing t) => BaseWallValidator(t) && t.Position.GetDangerFor(pawn, t.Map) == Danger.None
                ) is Thing thing)) return null;
            return JobMaker.MakeJob(InternalDefOf.BBLK_Job_LickWall, thing);
            
            bool BaseWallValidator(Thing t)
            {
                if (!base.def.thingDefs.Contains(t.def) || 
                    t.def.building == null || 
                    t.Position.Fogged(t.Map) || 
                    !pawn.CanReserve(t) || 
                    t.IsBurning() || 
                    t.HostileTo(pawn)
                    ) return false;
                return true;
            }
        }

        public override bool CanBeGivenTo(Pawn pawn)
        {
            if (!(pawn?.health?.hediffSet?.GetBodyPartRecord(InternalDefOf.Tongue) is BodyPartRecord tongue) || 
                (!pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(tongue) && pawn.health.hediffSet.PartIsMissing(tongue))
                ) return false;
            return base.CanBeGivenTo(pawn);
        }
    }
}
