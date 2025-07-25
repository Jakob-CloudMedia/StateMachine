// Has Target Predicate – goblin focus verifier.
//
// This predicate checks if the goblin has a designated target,
// ensuring the goblin knows where to direct its efforts or attention.
using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.Predicates
{
    
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/Predicates/HasTarget")]
    public class PredicateHasTarget : AbsPredicateSo<EntityBlackboard>
    {
        public override bool Evaluate(EntityBlackboard blackboard) => blackboard.Data.ActionTarget;
    }
}