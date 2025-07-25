using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.States
{
    // Ancient Horror Appeared State – a special goblin behavior state.
    //
    // This state represents a critical moment when an ancient horror appears,
    // causing significant changes to the goblin's behavior. Upon entering this 
    // state, the goblin identifies the AncientEntity, sets it as the action target, 
    // and switches to an aggressive or alert mode (e.g., enabling shooting capabilities).
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/States/AncientHorrorAppeared")]
    public class StateAncientHorrorAppeared : AbsStateSo<EntityBlackboard>
    {
        public override void OnEnter(EntityBlackboard blackboard)
        {
            blackboard.Data.AncientEntity = FindAnyObjectByType<AncientEntity>();
            blackboard.Data.ActionTarget = blackboard.Data.AncientEntity;
            
            blackboard.Data.IsAbleToShoot = true;

        }

        public override void OnExit(EntityBlackboard blackboard)
        {
            blackboard.Data.IsAbleToShoot = false;
        }

        public override void Tick(EntityBlackboard blackboard)
        {
            if (!blackboard.Data.ActionTarget || blackboard.Data.ActionTarget.GetType() != typeof(AncientEntity))
                blackboard.Data.ActionTarget = blackboard.Data.AncientEntity;
            
            Utilities.Utilities.LogMessage(blackboard.Data.DebugLog, "Ohh noo, we are sooo doooomed. ;@");
        }
    }
}