using System;
using UniRx;
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
        
        [SerializeField] private PlayerJumperVisualEffects playerJumperVisualEffects = new PlayerJumperVisualEffects();
        
        private IGroundCheck _groundCheck;
        private IFlipGravity _flipGravity;
        
        private CompositeDisposable _disposables = new CompositeDisposable();

        #region Properties

        public ReactiveProperty<bool> IsJump { get; } = new ReactiveProperty<bool>();
        
        #endregion

        #endregion

        [Inject]
        public void Construct(IGroundCheck groundCheck, IFlipGravity flipGravity)
        {
            _groundCheck = groundCheck;
            _flipGravity = flipGravity;

            _groundCheck.IsGround.Subscribe(HandlerGrounded).AddTo(_disposables);
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

        public void HandlerGrounded(bool isGrounded)
        {
            if (IsJump.Value && isGrounded)
            {
                IsJump.Value = false;
            }
        }

        #region MonoBehaviour

        private void OnDestroy()
        {
            _disposables.Clear();
        }

        #endregion
    }
}
