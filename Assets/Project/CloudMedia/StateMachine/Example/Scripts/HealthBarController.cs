using UnityEngine;
using UnityEngine.UI;

namespace CloudMedia.StateMachine.Example.Scripts
{
    // 🩸 GOBLIN HEALTH BAR CONTROLLER!
    // This magical script makes sure the goblin health bar changes when a goblin gets bonked or healed!
    public class HealthBarController : MonoBehaviour
    {
        // THE HEALTH BAR! This is the graphical red (or whatever) bar showing how close to giblets the goblin is.
        [SerializeField] private Image _bar; 

        // Controller for the goblin this health bar belongs to. It tells the bar when to change.
        private AbsEntityController _entityController; 

        private void Awake()
        {
            // Find the goblin's controller on the same game object.
            _entityController = GetComponent<AbsEntityController>();
        }

        private void OnEnable()
        {
            // Listen to the goblin's screams whenever its health changes.
            _entityController.OnHealthChanged += HealthChanged;
        }

        private void OnDisable()
        {
            // Stop listening when the goblin goes to sleep (or dies a terrible death).
            _entityController.OnHealthChanged -= HealthChanged;
        }

        private void HealthChanged(float health)
        {
            // 🩸 UPDATE THE HEALTH BAR!
            // Take the goblin's current health, divide it by its total max health to get the fill percentage.
            _bar.fillAmount = health / _entityController.Eb.Settings.BaseHealth;
        }
    }
}