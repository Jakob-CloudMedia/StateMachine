using System.Linq;
using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.States
{
    // THIS BE THE "SEARCHING FOR LEADER" STATE!
    // In this state, the goblin (or "entity") feels lost and lonely.
    // Its mission? To find a worthy leader among its friends who isn’t currently... well... dead.

    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/States/SearchingForLeader")]
    public class StateSearchingForLeader : AbsStateSo<EntityBlackboard>
    {
        public override void OnEnter(EntityBlackboard blackboard)
        {
            Utilities.Utilities.LogMessage(blackboard.Data.DebugLog, "Starting to search for a leader >8|");
        }
        
        public override void OnExit(EntityBlackboard blackboard)
        {
            // 📣 Let the goblin world know it succeeded!
            Utilities.Utilities.LogMessage(blackboard.Data.DebugLog, "Leader found ;D");
        }

        // Every frame in this state, the goblin inspects its friends to see if any can become its leader.
        public override void Tick(EntityBlackboard blackboard)
        {
            Utilities.Utilities.LogMessage(blackboard.Data.DebugLog, "Searching for leader >8|");

            // Check the goblin's living friends (not the dead ones).
            var leader = blackboard.Data.Friends.FirstOrDefault(f => f.IsDead == false);

            // If no leader is found, give up for this tick.
            if (!leader) return;

            // Measure the distance between the goblin and the potential leader.
            var distance = Vector3.Distance(blackboard.Data.Me.transform.position, leader.transform.position);

            // Found a good leader within range? Assign them as the new shiny leader!
            if (distance <= blackboard.Settings.DetectionRange)
                blackboard.Data.Leader = leader;
        }
    }
}

