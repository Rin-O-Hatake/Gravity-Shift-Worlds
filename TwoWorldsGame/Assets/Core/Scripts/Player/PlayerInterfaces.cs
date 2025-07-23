
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Scripts.Player
{
    public interface IPlayerMovement
    {
        public void Move(Vector2 movement);
        public ReactiveVariable<float> HorizontalInput { get; }
    }
    
    public interface IPlayerJump
    {
        public void Jump(InputAction.CallbackContext context);
        public ReactiveVariable<bool> IsJump { get; }
    }

    public interface IGroundCheck
    {
        public ReactiveVariable<bool> IsGround { get; }
        public Transform GetFootTransform();
    }

    public interface IFlipGravity
    {
        public void FlipGravity(InputAction.CallbackContext context);
        public ReactiveVariable<bool> IsNormalGravity { get; }
    }
}
