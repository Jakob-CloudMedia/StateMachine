using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Bots.BotStateMachines.States
{
    // THIS BE A ROAMING STATE!
    // When goblin (or "entity") has no goal, it wanders around like drunk beetle.
    // This state picks a random point and makes the entity move toward it.
    // When close enough, picks another. Forever...
    [CreateAssetMenu(menuName = "CloudMedia/StateMachine/Example/States/Roaming")]
    public class StateRoaming : AbsStateSo<EntityBlackboard>
    {
        // How close is "close enough"? Goblin don't need GPS — just vibes.
        private const float DistanceTolerance = 3f;

        // Called when state starts. Goblin picks a new random place to go!
        public override void OnEnter(EntityBlackboard blackboard)
        {
            blackboard.Data.CurrentRoamPosition = GetNextRoamPosition(blackboard);
        }

        public override void OnExit(EntityBlackboard blackboard)
        {
        }

        // Called every tick. Goblin checks if it’s close to target.
        // If yes → pick new target.
        // If no → check if NavMesh agent has correct destination.
        public override void Tick(EntityBlackboard blackboard)
        {
            var entity = blackboard.Data.Me; // Goblin body (transform)
            var agent = blackboard.Data.NavMeshAgent; // Goblin pathfinding servant
            var currentTarget = blackboard.Data.CurrentRoamPosition; // Where goblin tries to go
            var distanceToTarget = Vector3.Distance(entity.transform.position, currentTarget); // How far we are

            // Already close? Pick new place to roam!
            if (distanceToTarget <= DistanceTolerance)
            {
                blackboard.Data.CurrentRoamPosition = GetNextRoamPosition(blackboard);
                return; // No need to update destination this frame
            }

            // Not close AND destination is wrong? Fix it!
            if (agent.destination != currentTarget)
            {
                agent.SetDestination(currentTarget);
            }
        }

        // Private helper spell to pick next roam position.
        // Chooses random point around start position, inside given radius, and makes sure it's on NavMesh.
        private static Vector3 GetNextRoamPosition(EntityBlackboard blackboard)
        {
            var startPoint = blackboard.Data.StartPosition; // Where goblin started its pathetic life
            var roamRadius = blackboard.Settings.RoamDistance; // How far it’s allowed to wander
            var entityPosition = blackboard.Data.Me.transform.position;  // Current position
            var obstructionLayers = blackboard.Settings.TargetObstructionLayers; // Things goblin should avoid

            // Use utility function to find random valid position nearby.
            return Utilities.Utilities.GetRandomTargetOnNavmesh(
                startPoint,
                roamRadius,
                entityPosition,
                obstructionLayers);
        }

    }
}
