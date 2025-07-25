using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CloudMedia.Utilities
{
    // 🛠️ GOBLIN UTILITY BOX – MAGIC TOOLS FOR THE BUSY GOBLIN!
    // This class is like the goblin's treasure chest, full of reusable goodies:
    public static class Utilities
    {
        // WAIT CACHE – LAZY GOBLIN'S TIME HACK (from git-amend youtube channel)
        // Maps durations to reusable WaitForSeconds objects.
        // Why create new WFS objects all the time when you can reuse them?
        private static readonly Dictionary<float, WaitForSeconds> WaitForSeconds = new();
        public static WaitForSeconds GetWaitForSeconds(float seconds)
        {
            if (WaitForSeconds.TryGetValue(seconds, out var forSeconds)) return forSeconds;

            var waitForSeconds = new WaitForSeconds(seconds);
            WaitForSeconds.Add(seconds, waitForSeconds);
            return waitForSeconds;
        }
        
        // LOGGING – GOBLINS LOVE TO CHAT!
        // If "doIt" is true, let's tell the log something fun.
        public static void LogMessage(bool doIt, string message)
        {
            if(doIt)
                Debug.Log(message);
        }
        
        
        // FIND RANDOM NAVMESH POINT
        // Goblin picks a random legal spot on the navmesh near the given center.
        // Perfect for roaming or sneaking around obstacles!
        public static Vector3 GetRandomTargetOnNavmesh(Vector3 center, float radius, Vector3 selfPosition, LayerMask obstructionLayers)
        {
            for (var i = 0; i < 10; i++) // Try up to 10 times to find a nice spot.
            {
                var point = RandomPointInCircleRange(center, radius); // Roll dice for random point.
                if (!NavMesh.SamplePosition(point, out var hit, 2.0f, NavMesh.AllAreas)) continue; // Is this legal land?

                // Check if the path to the point is free from sneaky obstructions.
                if(IsPathClear(hit.position, selfPosition, obstructionLayers))
                    return hit.position; // Found the perfect spot!
            }
            
            // If no great spot was found, goblin will just settle for whatever is closest.
            var xPoint = RandomPointInCircleRange(center, radius);
            return NavMesh.SamplePosition(xPoint, out var xhit, radius, NavMesh.AllAreas)
                ? xhit.position : selfPosition;
        }
        
        // IS PATH TO TARGET CLEAR?
        // Checks if there are no obstacles between goblin and the target point.
        // If there's a scary rock or a mean troll in the way, the goblin won't sneak there.
        private static bool IsPathClear(Vector3 target, Vector3 self, LayerMask obstructionLayers)
        {
            if(target.y <= 0)
                target = new Vector3(target.x, 1f, target.z); // Avoid diving into the void.
            
            var dir = target - self;
            return !Physics.Raycast(self, dir, out _, dir.magnitude, obstructionLayers); // No obstacles? Good to go!

        }
        // ROLL RANDOM POINT IN RANGE
        // Generates a random point within a circle around the center.
        // A bit of chaos for goblin mischief!
        private static Vector3 RandomPointInCircleRange(Vector3 center, float radius)
        {
            var randomCircle = Random.insideUnitCircle; // Roll for random circle position.
            var randomCircleInRange = new Vector3(randomCircle.x, 0, randomCircle.y) * Random.Range(2, radius); // Scale it.
            var pointInCircle = center - randomCircleInRange; // Offset it from the center.
            return pointInCircle; // Voilà! A goblin-approved spot.
        }


    }
}