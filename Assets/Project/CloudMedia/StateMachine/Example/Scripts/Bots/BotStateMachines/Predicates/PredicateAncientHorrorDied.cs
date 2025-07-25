// Ancient Horror Died Predicate.
//
// This predicate checks if the ancient horror has passed away,
// letting the goblins react to the loss of a formidable entity.
using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.Predicates
{
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/Predicates/AncientHorrorDied")]
    public class PredicateAncientHorrorDied : AbsPredicateSo<EntityBlackboard>
    {
        public override bool Evaluate(EntityBlackboard blackboard)
        {
            return blackboard.Data.AncientEntity && blackboard.Data.ActionTarget.IsDead;
        }
    }
}