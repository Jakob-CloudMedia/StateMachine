using UnityEngine;

namespace CloudMedia.StateMachine
{
    // Base class for all GOBLIN SETTINGS!
    // These are ScriptableObjects, shared between goblins like moldy treasure maps.
    // Example: speed values, damage ranges, detection settings – stuff you don't change during runtime!
    public abstract class SettingsBase : ScriptableObject { }
    
    // Base class for DATA brains!
    // These live on goblin GameObjects and store PERSONAL, RUNTIME data!
    // Example: current target, cooldown timers, goblin mood swings.
    public abstract class DataBase : MonoBehaviour { }
    
    
    
    // 🧠🧪 GOBLIN BLACKBOARD – THE BIG COMBO BRAIN!
    // This class combines:
    //   - TSettings = SHARED config scroll (read-only, same for all goblins using same profile)
    //   - TData     = UNIQUE goblin guts (each goblin has their own data blob)
    //
    // It’s meant to be used by state machines as the MIND of the goblin.
    // All states, predicates, actions... sniff this thing.
    public abstract class StateMachineBlackboard<TSettings, TData> : MonoBehaviour
        where TSettings : SettingsBase // 👈 Must come from SettingsBase scroll
        where TData : DataBase // 👈 Must come from MonoBehaviour data blob
    {
        [Header("Profiled initial configuration")]
        // 📜 This is shared config! Like scroll from goblin general!
        // NEVER write to this during playtime – goblins will fight!
        public TSettings Settings;

        [Header("Individual setup and runtime data.")]
        // 🧠 This is the goblin's own meat! Use it to track targets, positions, cooldowns, etc.
        public TData Data;
    }
}