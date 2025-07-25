using System.Linq;
using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.States
{
    // THIS STATE MAKES THE GOBLIN *LOOK* FOR SOMETHING TO KILL!
    // It loops through all enemies in sniffing range.
    // If it finds a still-alive one, it picks it as a target and ends the hunt.
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/States/SearchingForTarget")]
    public class StateSearchingForTarget : AbsStateSo<EntityBlackboard>
    {
        public override void OnEnter(EntityBlackboard blackboard)
        {
            // If goblin already has target, wipe it clean! Start fresh hunt.
            if (blackboard.Data.ActionTarget)
                blackboard.Data.ActionTarget = null;

            // Goblin not allowed to shoot until it finds something. Disable fire button!
            if (blackboard.Data.IsAbleToShoot)
                blackboard.Data.IsAbleToShoot = false;
        }

        public override void OnExit(EntityBlackboard blackboard)
        {
            Utilities.Utilities.LogMessage(blackboard.Data.DebugLog, "Target acquired :D.");
        }

        // Every tick, goblin scans the world for fresh meat.
        public override void Tick(EntityBlackboard blackboard)
        {
            Utilities.Utilities.LogMessage(blackboard.Data.DebugLog, "Searching for target >8|.");

            // Get all enemies that aren't dead. Dead things don’t scream.
            var aliveEnemies = blackboard.Data.Enemies.Where(e => e.IsDead == false);
            
            // Check each enemy's distance — first one in range becomes the new chew toy.
            foreach (var enemy in aliveEnemies)
            {
                var me = blackboard.Data.Me;
                var distanceToEnemy = Vector3.Distance(me.transform.position, enemy.transform.position);
                
                // If enemy is close enough — target acquired!
                if (distanceToEnemy < blackboard.Settings.DetectionRange)
                {
                    blackboard.Data.ActionTarget = enemy;
                    return;
                }
                
            }
            
        }
    }
}
