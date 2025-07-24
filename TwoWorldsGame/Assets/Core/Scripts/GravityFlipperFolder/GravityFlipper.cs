using Core.Scripts.Player;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Scripts.GravityFlipperFolder
{
    public class GravityFlipper : MonoBehaviour, IFlipGravity
    {
        #region Feilds

        [SerializeField] private Rigidbody2D _rigidbodyPlayer;
        [SerializeField] private float _gravityFlipForce = 10f;
        
        private IGroundCheck _groundCheck;
        
        private const float _defaultGravityScale = 1.0f; 
        private const float _noGravityScale = -1.0f;

        private const float _defaultRotationPlayer = 180.0f;
        private const float _noGravityRotationPlayer = -180.0f;

        private bool _isFlipping;
        
        private CompositeDisposable _disposables = new CompositeDisposable();

        #region Properties

        public ReactiveProperty<bool> IsNormalGravity { get; } = new ReactiveProperty<bool>();

        #endregion

        #endregion

        #region MonoBehaviour

        private void OnDestroy()
        {
            _disposables.Clear();
        }

        #endregion
        
        [Inject]
        public void Construct(IGroundCheck groundCheck)
        {
            _groundCheck = groundCheck;
            
            _groundCheck.IsGround.Subscribe(HandlerGrounded).AddTo(_disposables);
            
            IsNormalGravity.Value = true;
        }

        public void FlipGravity(InputAction.CallbackContext context)
        {
            if (_groundCheck.IsGround.Value && !_isFlipping)
            {
                _isFlipping = true;

                _rigidbodyPlayer.gravityScale *= -1;
                
                _rigidbodyPlayer.AddForce(Vector2.down * _rigidbodyPlayer.gravityScale * _gravityFlipForce, ForceMode2D.Force);

                ChangeRotationPlayerAfterFlipGravity(_rigidbodyPlayer.gravityScale > 0);
            }
        }

        private void ChangeRotationPlayerAfterFlipGravity(bool gravityIsNormal)
        {
            Quaternion rotation = Quaternion.Euler(gravityIsNormal ? _noGravityRotationPlayer : _defaultRotationPlayer,
                transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = rotation;
        }

        private void HandlerGrounded(bool isGrounded)
        {
            if (_isFlipping && isGrounded)
            {
                _isFlipping = false;
                IsNormalGravity.Value = _rigidbodyPlayer.gravityScale > 0;
            }
        }
    }
}
