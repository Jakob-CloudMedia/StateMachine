using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.Predicates
{
    // Has No Leader Predicate – goblin loneliness tracer.
    //
    // This predicate checks if the goblin lacks a leader,
    // detecting chaos or independence within the goblin ranks.
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/Predicates/HasNoLeader")]
    public class PredicateHasNoLeader : AbsPredicateSo<EntityBlackboard>
    {
        public override bool Evaluate(EntityBlackboard blackboard)
        {
            return !blackboard.Data.Leader;
        }
    }
}