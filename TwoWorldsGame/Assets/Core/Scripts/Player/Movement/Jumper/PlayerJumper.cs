using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Zenject;

namespace Core.Scripts.Player.Movement.Jumper
{
    public class PlayerJumper : MonoBehaviour, IPlayerJump
    {
        #region Fields

        [SerializeField] private Rigidbody2D _rigidbodyPlayer;
        [SerializeField] private float jumpForce = 10f;
        
        [FormerlySerializedAs("_playerJumperEffect")] [SerializeField] private PlayerJumperVisualEffects playerJumperVisualEffects = new PlayerJumperVisualEffects();
        
        private IGroundCheck _groundCheck;
        private IFlipGravity _flipGravity;

        #region Properties

        public ReactiveVariable<bool> IsJump { get; } = new ReactiveVariable<bool>();
        
        #endregion

        #endregion

        [Inject]
        public void Construct(IGroundCheck groundCheck, IFlipGravity flipGravity)
        {
            _groundCheck = groundCheck;
            _flipGravity = flipGravity;

            _groundCheck.IsGround.Changed += HandlerGrounded;
            playerJumperVisualEffects.Initialize();
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (_groundCheck.IsGround.Value)
            {
                _rigidbodyPlayer.AddForce((_flipGravity.IsNormalGravity.Value ? Vector2.up : Vector2.down) * jumpForce, ForceMode2D.Impulse);
                IsJump.Value = true;

                playerJumperVisualEffects.PlayEffect(_groundCheck.GetFootTransform().position);
            }
        }

        public void HandlerGrounded(bool oldValue, bool isGrounded)
        {
            if (IsJump.Value && isGrounded)
            {
                IsJump.Value = false;
            }
        }

        #region MonoBehaviour

        private void OnDestroy()
        {
            _groundCheck.IsGround.Changed -= HandlerGrounded;
        }

        #endregion
    }
}
