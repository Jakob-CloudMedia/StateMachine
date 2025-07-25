using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.Predicates
{
    // Target In Range Predicate – goblin distance evaluator.
    //
    // This predicate determines if the goblin's current target is within a specified range.
    // It ensures the goblin only acts on targets that are close enough and valid (not dead).
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/Predicates/TargetInRange")]
    public class PredicateHasTargetInRange : AbsPredicateSo<EntityBlackboard>
    {
        public override bool Evaluate(EntityBlackboard blackboard)
        {
            Utilities.Utilities.LogMessage(blackboard.Data.DebugLog, "Checking if target in distance.");
            
            // If there is no target or the target is dead, return false (out of range or invalid).
            if (!blackboard.Data.ActionTarget || blackboard.Data.ActionTarget.IsDead) return false;
            
            // Calculate the distance between the target and the goblin.
            var distance = Vector3.Distance(blackboard.Data.ActionTarget.transform.position, blackboard.Data.Me.transform.position);

            // Check if the calculated distance is within the shooting range.
            var isInDistance = distance <= blackboard.Settings.ShootingRange;
            
            if (isInDistance)
                Utilities.Utilities.LogMessage(blackboard.Data.DebugLog, "Target in distance.");

            // Return whether the target is in range.
            return isInDistance;
        }
    }
}