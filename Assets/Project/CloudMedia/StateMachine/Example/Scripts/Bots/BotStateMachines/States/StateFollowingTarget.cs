using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.States
{
    // Following Target State – goblin behavior for pursuing a specific target.
    //
    // This state allows a goblin to actively follow a designated target, such as an enemy or an objective.
    // While in this state, the goblin updates its movement to maintain alignment with the target's
    // position, ensuring it stays in pursuit as the target moves. When exiting this state, the goblin
    // halts movement and clears the target reference.
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/States/FollowingTarget")]
    public class StateFollowingTarget : AbsStateSo<EntityBlackboard>
    {
        public override void OnEnter(EntityBlackboard blackboard)
        {
        }

        public override void OnExit(EntityBlackboard blackboard)
        {
            if (!blackboard.Data.ActionTarget) return;
            blackboard.Data.ActionTarget = null;
            blackboard.Data.NavMeshAgent.SetDestination(blackboard.Data.Me.transform.position);
        }

        public override void Tick(EntityBlackboard blackboard)
        {
            if (!blackboard.Data.ActionTarget) return;
            
            if (blackboard.Data.NavMeshAgent.steeringTarget != blackboard.Data.ActionTarget.transform.position)
                blackboard.Data.NavMeshAgent.SetDestination(blackboard.Data.ActionTarget.transform.position);
        }
    }
}