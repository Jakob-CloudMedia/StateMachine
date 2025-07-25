using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CloudMedia.StateMachine
{
    // THIS THING BE A GOBLIN BRAIN MACHINE!
    // It use generic brain type (TBlackboard), like giving goblin a scroll it knows how to read.
    // Needs to sit on same GameObject as brain (TBlackboard is MonoBehaviour!)
    public abstract class AbsStateMachine<TBlackboard> : MonoBehaviour where TBlackboard : MonoBehaviour
    {
        // How often goblin checks what it’s doing. Too slow = stupid. Too fast = dead CPU.
        [SerializeField] private float _interval = 0.05f;
        
        // This be goblin’s starting point! Where goblin wakes up and starts running.
        [SerializeField] private AbsStateSo<TBlackboard> _entryState;
        
        // List of goblin jump-rules! Tells it when to switch from one thing to another.
        [SerializeField] private List<Transition<TBlackboard>> _transitions;

        // 🧟‍♂️ Has the goblin actually initialized itself or is it a rotten husk? Don’t poke it too early.
        private bool _isInitialized;
        
        // Goblin be doing THIS thing right now (current state).
        private IState<TBlackboard> _currentState;
        // Goblin’s brain data! Holds things like target, health, what smells good, etc.
        private TBlackboard _blackboard;
        // Protected access to blackboard for your subclassed goblin contraptions.
        protected TBlackboard Blackboard => _blackboard;
        
        private void Awake()
        {
            // Grab the blackboard component off the GameObject.
            _blackboard = GetComponent<TBlackboard>();

            // 💀 If it’s not there, panic and disable self.
            if (_blackboard) return;
            
            Debug.LogError($"[StateMachine] No Blackboard of type {typeof(TBlackboard).Name} found on {gameObject.name}");
            enabled = false;
        }

        private void OnEnable()
        {
            // When enabled, start ticking like a mad rat heart
            StartCoroutine(Tick());
        }


        private void Start()
        {
            _isInitialized = true;
            
            _currentState = _entryState; // Set starting state
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        // TICK TICK TICK FOREVER (unless goblin dead)
        private IEnumerator Tick()
        {
            // If no transitions, goblin doesn’t know how to brain. Warn and stop ticking.
            if (_transitions.Count == 0)
            {
                Debug.LogWarning($"StateMachine: {gameObject.name} has no states");
                yield break;
            }
            
            while (true)
            {
                // Not ready? Just wait and stare at ceiling.
                if(!_isInitialized)
                {
                    yield return Utilities.Utilities.GetWaitForSeconds(_interval);
                    continue;
                }   
                // Let current state do its dirty work!
                _currentState?.Tick(_blackboard);
                
                // Check if we need to jump to new state!
                CheckState();
                
                // Wait for next tick (interval seconds)
                yield return Utilities.Utilities.GetWaitForSeconds(_interval);
            }
        }
        
        // 🧠🪓 THE BIG BRAIN DECISION MAKER!
        // Decides if goblin should switch to new state. Uses rules with priorities.
        private void CheckState()
        {
            Transition<TBlackboard> bestEntry = null;
            var highestPriority = int.MinValue;

            // Pass ONE: try all any state transitions (no "from" state)
            foreach (var stateEntry in _transitions)
            {
                if (stateEntry.GetFromState())
                    continue; // Not an any state transition, skip

                var predicate = stateEntry.GetPredicate();
                if (!predicate || !predicate.Evaluate(_blackboard))
                    continue; // Rule say NO, continue loop

                if (stateEntry.Priority <= highestPriority) continue; // Test for the highest priority state.
                
                // 📌 Found better jump rule! Save it!
                highestPriority = stateEntry.Priority;
                bestEntry = stateEntry;
            }
            
            // Pass 2: If no entry transition, try "from current state" transitions.
            if (bestEntry == null)
            {
                highestPriority = int.MinValue;

                foreach (var stateEntry in _transitions)
                {
                    var from = stateEntry.GetFromState();
                    if (from == null)
                        continue; // The goblin mind passes all any states.

                    // ❌ Must match current state to even consider this!
                    if(from != (AbsStateSo<TBlackboard>)_currentState)
                        continue;
                    
                    var predicate = stateEntry.GetPredicate();
                    if (predicate == null || !predicate.Evaluate(_blackboard))
                        continue; // Rule say NO again
                        
                    if (stateEntry.Priority <= highestPriority) continue;

                    // 🎯 This one looks promising!
                    highestPriority = stateEntry.Priority;
                    bestEntry = stateEntry;
                }
            }

            // Still no valid transition? Goblin still doing the same thing.
            if (bestEntry == null)
            {
                return;
            }

            var newState = bestEntry.GetToState();

            // Already in that state? No need to change.
            if(newState == (AbsStateSo<TBlackboard>)_currentState)
                return;
            
            // GOBLIN SWITCH STATES!
            // First say goodbye to old state...
            _currentState?.OnExit(_blackboard);
            
            // ...then update to new state...
            _currentState = newState;
            
            // ...and say hello to new state!
            _currentState?.OnEnter(_blackboard);
        }
    }
}