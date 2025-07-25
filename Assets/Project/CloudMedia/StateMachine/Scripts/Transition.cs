using UnityEngine;

// 🧪📜 TRANSITION – THE GOBLIN THOUGHT PROCESS!
// This class is the magical bridge that helps goblins hop between states!
// It decides:
//   - Where the goblin starts (from state) - optional; without works as any state.
//   - When or why the goblin should switch (predicate – a big IF).
//   - Where the goblin ends up (to state).
//   - How important it is (priority)
// State machine reads these and picks the best one based on brain + rule.

namespace CloudMedia.StateMachine
{
    [System.Serializable]
    public class Transition<TBlackboard> where TBlackboard : MonoBehaviour
    {
        
        // FROM this state (optional):
        // - If NULL → it's an entry transition (can fire from anywhere, even before entry state runs)
        // - If NOT NULL → only works if goblin is currently in this state
        [SerializeField] private AbsStateSo<TBlackboard> _fromStateSo; 
        
        // CONDITION to check before jumping!
        // This be a reusable goblin rule (predicate) like “is health low?”, “is enemy near?”, “is fire delicious?”
        // Predicate is evaluated using the current goblin’s brain!
        [SerializeField] private AbsPredicateSo<TBlackboard> _predicateSo; 

        // The state we go TO if this transition is chosen.
        // Must never be NULL, or goblin will jump into the abyss.
        [SerializeField] private AbsStateSo<TBlackboard> _toStateSo; 

        // PRIORITY for this transition:
        // Higher = better = more likely to be picked.
        // If multiple transitions are valid, the one with the highest priority WINS.
        [SerializeField] private int _priority; 

        // Read-only access to priority, so state machine can sort through transitions like a goblin warlord.
        public int Priority => _priority; 

        // Get the "FROM" state. Can be NULL (for entry transitions)!
        public AbsStateSo<TBlackboard> GetFromState() => _fromStateSo; 

        // Get the "TO" state. Goblin will jump to this if transition is chosen.
        public AbsStateSo<TBlackboard> GetToState() => _toStateSo; 

        // Get the logic rule that must return TRUE for transition to be allowed.
        public AbsPredicateSo<TBlackboard> GetPredicate() => _predicateSo; 
    }
}