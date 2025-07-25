using UnityEngine;

namespace CloudMedia.Utilities
{
    // 📸 BILLBOARDING – GOBLIN'S LOOKAT MAGIC
    // This component makes goblins (or other objects) always face the camera!
    // Great for UI effects, health bars, or a goblin giving you side-eyes! 🧙

    public class Billboarding : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            // Update rotation to face the camera's direction.
            // Now the goblin will stare straight into your soul.
            transform.rotation = _camera.transform.rotation;

            // Lock the goblin's Z-rotation to keep it upright and not do somersaults.
            // Nobody likes a dizzy goblin.
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
        }
    }
}
