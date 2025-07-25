using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.States
{
    // Entry State – a placeholder or starting state for goblin state machines.
    //
    // This state is effectively empty and does not perform any behavior.
    // It can be used as an initial starting point or a "neutral" state in the state machine.
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/States/EmptyState")]
    public class EntryState : AbsStateSo<EntityBlackboard>
    {
        public override void OnEnter(EntityBlackboard blackboard)
        {
        }

        public override void OnExit(EntityBlackboard blackboard)
        {
        }

        public override void Tick(EntityBlackboard blackboard)
        {
        }
    }
}