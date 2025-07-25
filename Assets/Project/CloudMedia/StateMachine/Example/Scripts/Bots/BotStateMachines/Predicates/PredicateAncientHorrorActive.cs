using System.Linq;
using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.Predicates
{
    // Ancient Horror Active Predicate – a goblin's way to sense if ancient horrors are awake.
    //
    // This predicate checks if any ancient, terrifying beings are active and ready to cause chaos.
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/Predicates/AncientHorrorActive")]
    public class PredicateAncientHorrorActive : AbsPredicateSo<EntityBlackboard>
    {
        public override bool Evaluate(EntityBlackboard blackboard)
        {
            var ancientHorrors = FindObjectsByType<AncientEntity>(FindObjectsSortMode.None).ToList();

            if (ancientHorrors is not { Count: > 0 }) return false;
            var ancienHorror = ancientHorrors
                .Where(ah => ah.IsDead == false)
                .FirstOrDefault(ah => ah.gameObject.activeInHierarchy);
            return ancienHorror;
        }
    }
}