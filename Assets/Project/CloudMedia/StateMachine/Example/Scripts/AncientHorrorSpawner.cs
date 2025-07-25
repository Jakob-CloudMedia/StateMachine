using CloudMedia.StateMachine.Example.Scripts.Bots;
using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts
{
    // 🧟‍♂️ ANCIENT HORROR SPAWNER – AWAKEN THE BEAST!
    // This script controls the spawning of an ancient horror entity. 
    // The horror stays hidden at first, then emerges after a timer runs out!
    // Useful for spooky surprises or unleashing chaos in the game! 👻
    public class AncientHorrorSpawner : MonoBehaviour
    {
        // Time until the ancient horror awakens (in seconds).
        [SerializeField] private float _spawnTime = 20f; 

        // The ancient horror entity to be spawned (hidden at first).
        [SerializeField] private AncientEntity _ancient; 

        // Tracks how much time is left before the ancient horror appears.
        [SerializeField] private float _timeLeft; 

        private void Start()
        {
            // On game start, set the timer to the defined spawn time.
            _timeLeft = _spawnTime;
        }

        private void Update()
        {
            // If the timer is up, stop (we already spawned the ancient horror).
            if (_timeLeft <= 0) return;

            // Countdown the timer with the passage of time.
            _timeLeft -= Time.deltaTime;

            // If the timer runs out, activate (spawn) the ancient horror!
            if (_timeLeft <= 0)
                _ancient.gameObject.SetActive(true); // Let the chaos begin!
        }
    }
}