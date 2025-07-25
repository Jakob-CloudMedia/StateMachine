using System;
using Example;
using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts
{
    // 🛡️ ABSOLUTE GOBLIN ENTITY CONTROLLER
    // This script controls goblin entities. It's an abstract overlord for goblin behaviors,
    // handling health, shooting, and death stuff. Every goblin entity uses or extends this.
    // It's basically the goblin's personal assistant! 

    public abstract class AbsEntityController : MonoBehaviour
    {
        // 🧠 The goblin's blackboard—a magical brain storing all goblin data and settings!
        [SerializeField] protected EntityBlackboard _eb; 

        // 🎯 Manages the goblin's shooting (if it has the means to blow stuff up).
        [SerializeField] protected ShootingController _shootingController;

        // ☠️ Tracks if the goblin is dead and out of the game.
        [SerializeField] private bool _isDead;

        // 💔 Health notifier! Tells whoever's listening when the goblin's health changes.
        public event Action<float> OnHealthChanged; 

        public bool IsDead
        {
            get => _isDead; // Is the goblin dead? True or false.
            private set => _isDead = value; // Set the goblin's dead/alive status.
        }

        // 🧠 Direct access to the goblin's big brain (blackboard).
        public EntityBlackboard Eb => _eb; 

        protected virtual void Awake() {}
        
        protected virtual void OnEnable()
        {
            // 💪 When goblin wakes up (enabled), give it fresh health and mark it alive.
            _eb.Data.CurrentHealth = _eb.Settings.BaseHealth;
            IsDead = false;
        }

        protected virtual void Start()
        {
            // 🎬 Preparations before the goblin's adventure begins.
            _eb.Data.CurrentHealth = _eb.Settings.BaseHealth; // Reset its health!
            GameOverController.Instance.RegisterEntity(this); // Register goblin with the game overseer.
        }

        // 💥 Abstract shooting behavior. Derived goblins define how they fire weapons.
        protected abstract void Shoot(ShootingController shootingController); 

        public void DoDamage(float amount)
        {
            // 🔪 Inflict damage on the goblin!
            _eb.Data.CurrentHealth -= amount; // Reduce goblin's health.

            if (_eb.Data.CurrentHealth <= 0)
                Death(); // If health hits zero, goblin bites the dust.

            // Notify listeners of the goblin's new health.
            OnHealthChanged?.Invoke(_eb.Data.CurrentHealth); 

            // Log all these exciting events!
            Utilities.Utilities.LogMessage(_eb.Data.DebugLog, $"{amount} damage done to: {gameObject.name}");
        }
        
        private void Death()
        {
            // ☠️ The end of the line—goblin goes to the big cave in the sky.
            Utilities.Utilities.LogMessage( _eb.Data.DebugLog,$"I am dead: {gameObject.name}");
            IsDead = true;
            GameOverController.Instance.UpdateEntityStatus();
            gameObject.SetActive(false);
        }

        protected virtual void OnOnHealthChanged(float obj)
        {
            OnHealthChanged?.Invoke(obj);
        }
    }
}