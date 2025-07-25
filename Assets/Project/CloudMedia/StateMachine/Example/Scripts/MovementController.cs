using UnityEngine;

namespace CloudMedia.StateMachine.Example.Scripts
{
    // 🏃‍♂️ GOBLIN MOVEMENT CONTROLLER!
    // This script makes goblins run! 🐗
    // It listens to input (like arrow keys or WASD), calculates where the goblin wants to go, 
    // and uses physics to push the goblin in the right direction!
    public class MovementController : MonoBehaviour
    {
        // GOBLIN SPEED! How fast can this little critter move around the battlefield?
        [SerializeField] private float _speed = 5.0f; 

        // The goblin's rigid body! Helps it interact with the world using physics.
        [SerializeField] private Rigidbody _rb; 

        // The goblin's "eyes in the sky" (the main camera). Useful for figuring out directions.
        private Camera _camera; 

        // Player input for moving the goblin. This stores movement directions.
        private Vector2 _input; 

        private void Awake()
        {
            // When the goblin wakes up, it finds the magical "camera in the sky."
            _camera = Camera.main;
        }

        private void Update()
        {
            // LISTEN TO PLAYER INPUT!
            // Goblin checks which buttons the player is smashing: horizontal (A/D or left/right) and vertical (W/S or up/down).
            var h = Input.GetAxisRaw("Horizontal"); // 👈 Horizontal movement input.
            var v = Input.GetAxisRaw("Vertical");   // 👆 Vertical movement input.
        
            // Normalize the input so goblin runs evenly in all directions.
            _input = new Vector2(h, v).normalized; 
        }

        private void FixedUpdate()
        {
            // Apply the goblin's movement during the physics update!
            HandleMovement(_input);
        }

        private void HandleMovement(Vector2 inputVector)
        {
            // MOVE THE GOBLIN USING PHYSICS!
            
            // Treat the ground as flat (upward direction is the surface's normal).
            var surfaceNormal = Vector3.up;

            // Calculate directions based on the camera's perspective.
            var forward = Vector3.ProjectOnPlane(_camera.transform.forward, surfaceNormal).normalized; // Goblin's "forward".
            var right = Vector3.ProjectOnPlane(_camera.transform.right, surfaceNormal).normalized;     // Goblin's "right".

            // Combine input directions to figure out where the goblin should move.
            var inputForce = right * inputVector.x + forward * inputVector.y;

            // Make sure the movement sticks to the ground (no flying goblins here!).
            var forceDirection = Vector3.ProjectOnPlane(inputForce, surfaceNormal).normalized;

            // APPLY THE MAGIC FORCE!
            // Give the goblin a little shove in the calculated direction at the set speed.
            _rb.AddForce(forceDirection * _speed, ForceMode.Acceleration);
        }
    }
}