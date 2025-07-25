using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.States
{
    // Following Leader State – goblin behavior for group coordination.
    //
    // This state enables a goblin to follow its designated leader by dynamically updating
    // its destination to match the leader's current position. This ensures the goblin remains
    // close to its leader during movement and navigation within the environment.
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/States/FollowingLeader")]
    public class StateFollowingLeader : AbsStateSo<EntityBlackboard>
    {
        public override void OnEnter(EntityBlackboard blackboard)
        {
        }

        public override void OnExit(EntityBlackboard blackboard)
        {
            blackboard.Data.NavMeshAgent.SetDestination(blackboard.Data.Me.transform.position);
        }

        public override void Tick(EntityBlackboard blackboard)
        {
            if (!blackboard.Data.Leader) return;

            var leaderPosition = blackboard.Data.Leader.transform.position;
            
            if (blackboard.Data.NavMeshAgent.steeringTarget != leaderPosition)
                blackboard.Data.NavMeshAgent.SetDestination(leaderPosition);
        }
    }
}