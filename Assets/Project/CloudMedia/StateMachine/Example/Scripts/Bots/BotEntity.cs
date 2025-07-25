// Bot Entity – Used by goblin's braining.
//
// This class represents bots that roam the world,
// navigating, identifying friends, enemies, and deciding when to
// unleash their goblin wrath. It's a foundation for all goblin-like
// automated behaviors.

using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace CloudMedia.StateMachine.Example.Scripts.Bots
{
    public class BotEntity : AbsEntityController
    {
        protected override void Awake()
        {
            // The goblin equips itself with navigation magic, using NavMeshAgent
            // to plot sneaky paths around obstacles.
            base.Awake();
            var navMeshAgent = GetComponent<NavMeshAgent>();
            if (navMeshAgent)
                _eb.Data.NavMeshAgent = navMeshAgent;
        }

        protected override void Start()
        {
            // This is where the goblin remembers its spawn point and learns about
            // other goblins (both friendly and enemy ones) in the world.
            base.Start();

            _eb.Data.Me = this; // The goblin identifies itself.
            _eb.Data.StartPosition = transform.position; // Goblin recalls its starting location.

            // Goblin scans the area for enemies it doesn't like.
            var enemies = 
                FindObjectsByType<AbsEntityController>(FindObjectsSortMode.None)
                    .Where(otherEntity => Eb.Settings.MyEnemies.Contains(otherEntity.Eb.Settings.MySide))
                    .ToList();

            _eb.Data.Enemies = enemies; // Goblin notes down its foes.
            
            // Goblin also scans the area for allies to mingle with.
            var friends = 
                FindObjectsByType<AbsEntityController>(FindObjectsSortMode.None)
                    .Where(otherEntity => Eb.Settings.MyFriends.Contains(otherEntity.Eb.Settings.MySide))
                    .ToList();

            _eb.Data.Friends = friends; // Goblin buddies to hang out with.
        }

        private void Update()
        {
            // Every goblin tick, this bot decides if it wants to shoot its foes.
            if (_eb.Data.IsAbleToShoot)
            {
                Shoot(_shootingController);
            }
        }
        
        protected override void Shoot(ShootingController shootingController)
        {
            // The goblin aims at its current target and launches a surprise attack!
            var target = _eb.Data.ActionTarget;
            if (target)
                shootingController.Fire(target.transform.position); // Goblin strikes!
        }
    }
}