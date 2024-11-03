using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Lickable_Walls
{
    public class JobDriver_LickWall : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            yield return Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.Touch);
            Toil toil = ToilMaker.MakeToil("MakeNewToils");
            toil.tickAction = delegate
            {
                pawn.rotationTracker.FaceTarget(base.TargetA);
                pawn.GainComfortFromCellIfPossible();
                JoyUtility.JoyTickCheckEnd(pawn, joySource: (Building)base.TargetThingA, fullJoyAction: JoyTickFullJoyAction.EndJob);
            };
            toil.handlingFacing = true;
            toil.defaultCompleteMode = ToilCompleteMode.Delay;
            toil.defaultDuration = job.def.joyDuration;
            yield return toil;
        }
        
        public override object[] TaleParameters()
        {
            return new object[2]
            {
            pawn,
            base.TargetA.Thing.def
            };
        }
    }
}
