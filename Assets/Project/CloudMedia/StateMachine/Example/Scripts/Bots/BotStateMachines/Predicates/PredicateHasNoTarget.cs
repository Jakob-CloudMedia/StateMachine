// Has No Target Predicate – aimless goblin detector.
//
// This predicate checks if the goblin has no target to focus on,
// identifying moments of distraction or indecision in the goblin's actions.
using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.Predicates
{
    
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/Predicates/HasNoTarget")]
    public class PredicateHasNoTarget : AbsPredicateSo<EntityBlackboard>
    {
        public override bool Evaluate(EntityBlackboard blackboard) => !blackboard.Data.ActionTarget;
    }
}