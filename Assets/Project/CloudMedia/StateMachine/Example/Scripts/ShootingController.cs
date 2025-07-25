using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts
{
    // 🎯 GOBLIN SHOOTING CONTROLLER!
    // This script gives goblins the power of ranged destruction! 💥
    // It handles firing bullets, setting cooldowns, and making sure goblins don’t go pew-pew too fast.
    public class ShootingController : MonoBehaviour
    {
        // GOBLIN’S BLACKBOARD! Stores all the goblin's important stats and settings (like bullet type and speed).
        [SerializeField] private EntityBlackboard _eb; 

        // WHERE BULLETS COME FROM! This is the tip of the goblin's chosen weapon—a.k.a. the "boom stick."
        [SerializeField] private Transform _bulletSpawnPoint; 

        // FIRE READY? This flag tells the goblin whether it's allowed to shoot or if it's reloading.
        [SerializeField] private bool _isFireEnable; 

        // COOLDOWN TIMER! Keeps track of how much time is left before the goblin can fire again.
        [SerializeField] private float _rateCounter; 

        private void Update()
        {
            // UPDATE SHOOTING COOLDOWN!
            if (_isFireEnable) return; // Goblin can already shoot? No need to tick the timer.

            if (_rateCounter > 0)
            {
                // Countdown the reload timer.
                _rateCounter -= Time.deltaTime;
            }
            else
            {
                // READY TO FIRE!
                _isFireEnable = true;
            }
        }

        public void Fire(Vector3 targetPosition)
        {
            // FIRE A BULLET!
            if (!_isFireEnable) return; // Can't shoot if still reloading!

            // Spawn the goblin's magical bullet at the designated spawn point.
            var bullet = Instantiate(_eb.Settings.Bullet, _bulletSpawnPoint.position, Quaternion.identity);

            // Calculate the direction toward the target!
            var direction = (targetPosition - _bulletSpawnPoint.transform.position).normalized;

            // Send the bullet flying at the goblin's chosen speed!
            bullet.Rb.linearVelocity = direction * _eb.Settings.ShotSpeed;

            // Turn off firing (cooldown begins).
            _isFireEnable = false;

            // Reset the reload timer using the goblin's firing rate setting.
            _rateCounter = _eb.Settings.FireRate;
        }
    }
}