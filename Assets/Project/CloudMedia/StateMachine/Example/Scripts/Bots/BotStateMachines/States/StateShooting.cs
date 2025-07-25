using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.States
{
    
    // PEW PEW STATE! GOBLIN FOUND ENEMY AND NOW IT SHOOTS.
    // This state allows the goblin to attack its target (if it has one).
    // It enables shooting on enter, disables it on exit, and logs goblin aggression.
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/States/Shooting")]
    public class StateShooting : AbsStateSo<EntityBlackboard>
    {
        // This allows other systems (like weapon logic or animation) to know: time to fire!
        public override void OnEnter(EntityBlackboard blackboard)
        {
            blackboard.Data.IsAbleToShoot = true;
        }

        // This disables any fire-happy systems. Goblin must chill now.
        public override void OnExit(EntityBlackboard blackboard)
        {
            blackboard.Data.IsAbleToShoot = false;
        }

        public override void Tick(EntityBlackboard blackboard)
        {
            Utilities.Utilities.LogMessage(blackboard.Data.DebugLog, "Pew pew pew >:F");

            // SAFETY CHECK: If somehow shooting got turned off mid-tick, turn it back on!
            if (!blackboard.Data.IsAbleToShoot) blackboard.Data.IsAbleToShoot = true;
            
            // ☠️ If target died mid-pew — stop attacking it!
            if (blackboard.Data.ActionTarget != null && blackboard.Data.ActionTarget.IsDead)
                blackboard.Data.ActionTarget = null;
        }
    }
}
