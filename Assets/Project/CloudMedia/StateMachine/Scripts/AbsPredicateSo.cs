using UnityEngine;

namespace CloudMedia.StateMachine
{
    // ABSTRACT GOBLIN PREDICATE!
    // This lil' ScriptableObject holds one question: "Should goblin do the thing?"
    // It be used by transitions to decide IF goblin should change states.
    public abstract class AbsPredicateSo<TBlackboard> : ScriptableObject, IPredicate<TBlackboard>
        where TBlackboard : MonoBehaviour
    {
        public abstract bool Evaluate(TBlackboard blackboard);
    }
}