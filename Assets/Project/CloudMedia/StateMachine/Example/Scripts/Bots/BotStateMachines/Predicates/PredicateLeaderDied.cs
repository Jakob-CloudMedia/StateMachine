using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.Predicates
{
    // Leader Died Predicate – goblin grief detector.
    //
    // This predicate checks if the goblin's leader has perished,
    // enabling state transitions when leadership is lost.
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/Predicates/LeaderDied")]
    public class PredicateLeaderDied : AbsPredicateSo<EntityBlackboard>
    {
        public override bool Evaluate(EntityBlackboard blackboard)
        {
            
            Utilities.Utilities.LogMessage(blackboard.Data.DebugLog, "Checking if leader is alive.");

            return blackboard.Data.Leader && blackboard.Data.Leader.IsDead;
        }
    }
}