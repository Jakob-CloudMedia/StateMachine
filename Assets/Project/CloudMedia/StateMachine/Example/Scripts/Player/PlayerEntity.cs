using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts.Player
{
    // PLAYER ENTITY CONTROLLER – THE LEADER OF THE GOBLINS!
    // This class controls the player, who can shoot projectiles whenever you press the left mouse button.
    public class PlayerEntity : AbsEntityController
    {
        private void Update()
        {
            // If goblin clicks, the goblin shoots :P
            if (Input.GetMouseButton(0))
            {
                Shoot(_shootingController);
            }
        }
        
        protected override void Shoot(ShootingController shootingController)
        {
            // Check for the main camera.
            if (!Camera.main) return;
            
            // Magical ray from the mouse pointer to the scene.
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // If we hit something, shoot a projectile.
            if (Physics.Raycast(ray, out var hit))
            {
                shootingController.Fire(hit.point);
            }
        }
    }
}