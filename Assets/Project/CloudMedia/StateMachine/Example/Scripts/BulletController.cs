using CloudMedia.StateMachine.Example.Scripts;
using UnityEngine;

namespace Example
{
    public class BulletController : MonoBehaviour
    {
        // 🏋️ The bullet's physical body! Keeps it flying straight and fast.
        [SerializeField] private Rigidbody _rb; 

        // ⏳ How long does the bullet live before it vanishes (in seconds).
        [SerializeField] private float _lifeTime = 5f; 

        // 🔪 Pain power! How much damage this bullet deals to its victim.
        [SerializeField] private float _damage = 10f; 

        // 🎯 Goblin's target filter! This mask decides who the bullet interacts with.
        [SerializeField] private LayerMask _collideWith; 

        // ☠️ Tracks whether the bullet is in its final moments before disappearing.
        private bool _isDying; 

        // ⏰ Countdown timer to track the remaining lifetime of the bullet.
        private float _lifeCounter; 

        // 🏋️ Exposes the Rigidbody so goblins can poke it, if needed.
        public Rigidbody Rb => _rb; 

        private void OnEnable()
        {
            // 🟢 Bullet is live! Reset its lifetime counter.
            _lifeCounter = _lifeTime;
        }

        private void Update()
        {
            // 💀 If the bullet is already dying, stop everything else.
            if (_isDying) return; 

            if (_lifeCounter > 0)
            {
                // 🕒 Countdown the bullet's lifetime.
                _lifeCounter -= Time.deltaTime;
            }
            else
            {
                // ⏲️ Bullet's lifespan reached the end—time to remove it.
                _isDying = true;
                Die();
            }
        }

        private void Die()
        {
            // ⚰️ The bullet meets its demise—destroy the game object.
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            // 🚫 If the collided object isn't in the target layer mask, ignore it!
            if (!IsInLayerMask(other.gameObject, _collideWith)) return;

            // 🎯 Check if the collision target is an entity with health to damage.
            var controller = other.gameObject.GetComponentInParent<AbsEntityController>();

            if (controller)
                controller.DoDamage(_damage); // Deal damage if it's a valid target!

            Die(); // 💥 Bullet's job is done—remove it.
        }
        
        private static bool IsInLayerMask(GameObject obj, LayerMask mask)
        {
            // 🕵️ Checks if the target object is part of the specified layer mask.
            return (mask.value & (1 << obj.layer)) != 0;
        }
    }
}