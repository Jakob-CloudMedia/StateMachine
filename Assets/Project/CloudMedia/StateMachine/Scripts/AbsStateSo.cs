using UnityEngine;

namespace CloudMedia.StateMachine
{
    // ABSTRACT GOBLIN STATE!
    // This ScriptableObject is a reusable goblin behavior chunk — no MonoBehaviour mess!
    // It be used by the state machine to represent "what goblin is doing right now".
    public abstract class AbsStateSo<TBlackboard> : ScriptableObject, IState<TBlackboard>
    {
        // Called when goblin ENTERS this state.
        public abstract void OnEnter(TBlackboard blackboard);
        
        // Called when goblin EXITS this state.
        public abstract void OnExit(TBlackboard blackboard);
        
        // Called every TICK while goblin is in this state.
        public abstract void Tick(TBlackboard blackboard);
    }
}