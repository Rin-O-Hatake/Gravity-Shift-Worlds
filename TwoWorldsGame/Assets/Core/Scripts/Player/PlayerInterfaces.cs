using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Scripts.Player
{
    public interface IPlayerMovement
    {
        public void Move(Vector2 movement);
        public ReactiveProperty<float> HorizontalInput { get; }
    }
    
    public interface IPlayerJump
    {
        public void Jump(InputAction.CallbackContext context);
        public ReactiveProperty<bool> IsJump { get; }
    }

    public interface IGroundCheck
    {
        public ReactiveProperty<bool> IsGround { get; }
        public Transform GetFootTransform();
    }

    public interface IFlipGravity
    {
        public void FlipGravity(InputAction.CallbackContext context);
        public ReactiveProperty<bool> IsNormalGravity { get; }
    }

    public interface IPlayerSetup
    {
        public void SetupPosition(Vector3 position);
    }
}
