namespace CloudMedia.StateMachine
{
    // 🤔 GOBLIN PREDICATES! 
    // This tricksy interface lets goblins decide "should I or shouldn't I?"
    // Goblins use this for STATE MAGIC in the state machine!
    public interface IPredicate<in TBlackboard>
    {
        // Goblin checks if condition is TRUE or FALSE.
        // For example: "is enemy close?", "is health low?", "is poop cooldown over?"
        // Blackboard is injected so the condition can sniff current goblin's brain.
        bool Evaluate(TBlackboard blackboard);
    }
}