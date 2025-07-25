using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.Predicates
{
    // Target Too Far Predicate – goblin range escape detector.
    //
    // This predicate evaluates whether the goblin's followed target has moved
    // beyond the detection range, allowing a state transition when the target
    // is no longer within a manageable distance.
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/Predicates/TargetTooFar")]
    public class PredicateTargetTooFar : AbsPredicateSo<EntityBlackboard>
    {
        public override bool Evaluate(EntityBlackboard blackboard)
        {
            Utilities.Utilities.LogMessage(blackboard.Data.DebugLog, "Checking if target is too far.");
            
            // Check if there's a valid action target.
            if (blackboard.Data.ActionTarget)
            {
                // If the target is an AncientEntity, it never counts as "too far."
                if (blackboard.Data.ActionTarget is AncientEntity) return false;
                
                // Calculate the distance between the goblin and the target.
                var distance = Vector3.Distance(blackboard.Data.ActionTarget.transform.position, blackboard.Data.Me.transform.position);
                
                // If the distance exceeds the detection range, the target is too far.
                if (distance > blackboard.Settings.DetectionRange)
                {
                    Utilities.Utilities.LogMessage(blackboard.Data.DebugLog, "Target too far.");
                    return true;
                }
            }

            Utilities.Utilities.LogMessage(blackboard.Data.DebugLog, "Target in range.");
            
            // If no valid target or within range,return false.
            return false;
        }
    }
}