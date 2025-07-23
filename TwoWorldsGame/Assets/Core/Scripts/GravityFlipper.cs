using Core.Scripts.Player;
using ModestTree;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Scripts
{
    public class GravityFlipper : MonoBehaviour, IFlipGravity
    {
        #region Feilds

        [SerializeField] private Rigidbody2D _rigidbodyPlayer;
        
        private IGroundCheck _groundCheck;
        
        private const float _defaultGravityScale = 1.0f; 
        private const float _noGravityScale = -1.0f;

        private const float _defaultRotationPlayer = 180.0f;
        private const float _noGravityRotationPlayer = -180.0f;

        private bool _isFlipping;

        #region Properties

        public ReactiveVariable<bool> IsNormalGravity { get; } = new ReactiveVariable<bool>();

        #endregion

        #endregion

        private void Awake()
        {
            IsNormalGravity.Value = true;
        }
        
        [Inject]
        public void Construct(IGroundCheck groundCheck)
        {
            _groundCheck = groundCheck;
            
            _groundCheck.IsGround.Changed += HandlerGrounded;
        }

        public void FlipGravity(InputAction.CallbackContext context)
        {
            if (_groundCheck.IsGround.Value && !_isFlipping)
            {
                _isFlipping = true;
                bool gravityIsNormal = _rigidbodyPlayer.gravityScale > 0;
                
                IsNormalGravity.Value = !gravityIsNormal;
                
                _rigidbodyPlayer.gravityScale = gravityIsNormal ? _noGravityScale : _defaultGravityScale;
                
                Quaternion rotation = Quaternion.Euler(gravityIsNormal ? _noGravityRotationPlayer : _defaultRotationPlayer,
                    transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                transform.rotation = rotation;
            }
        }

        public void HandlerGrounded(bool oldValue, bool isGrounded)
        {
            if (_isFlipping && isGrounded)
            {
                _isFlipping = false;
            }
        }
    }
}
