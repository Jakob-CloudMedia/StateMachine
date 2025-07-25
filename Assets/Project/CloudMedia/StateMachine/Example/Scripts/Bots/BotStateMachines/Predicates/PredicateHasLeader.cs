using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.Predicates
{
    // Has Leader Predicate – goblin loyalty checker.
    //
    // This predicate verifies if the goblin has a leader to follow,
    // ensuring proper hierarchy and allegiance within the goblin ranks.
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/Predicates/HasLeader")]
    public class PredicateHasLeader : AbsPredicateSo<EntityBlackboard>
    {
        public override bool Evaluate(EntityBlackboard blackboard)
        {
            return blackboard.Data.Leader;
        }
    }
}