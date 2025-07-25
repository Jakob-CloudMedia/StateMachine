namespace CloudMedia.StateMachine
{
    // 🛑 GOBLIN STATES! 🛑
    // This sneaky interface is what goblins use to have DIFFERENT MOODS (or behaviors)!
    // Each mood (state) tells goblins what to do when they enter, leave, or hang around.
    // Blackboard needs to be injected to separate goblin brains or all will work as one brain is shared.
    public interface IState<in TBlackboard>
    {
        // ENTERING THE MOOD! 
        // Goblin gets ready! This happens when the goblin changes to this state.
        // For example: "Start chasing the shiny thing!"
        public void OnEnter(TBlackboard blackboard);

        // LEAVING THE MOOD! 
        // Goblin tidies up! This happens when the goblin leaves this state.
        // For example: "Stop dancing, shiny thing is gone!" or "Reset traps."
        public void OnExit(TBlackboard blackboard);

        // TICK TOCK!
        // Time flies, but goblins keep doing stuff here depending on their current mood!
        // This runs every frame while the goblin is in this state.
        // For example: "Keep running towards shiny object!" or "Keep casting spells!"
        public void Tick(TBlackboard blackboard);
    }
}