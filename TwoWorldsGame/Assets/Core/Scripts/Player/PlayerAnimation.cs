using System;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Core.Scripts.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;

        private IPlayerJump _playerJump;
        
        private CompositeDisposable _disposables = new CompositeDisposable();

        #region Animation Names

        private const string JumpAnimation = "IsJump";
        private const string RunAnimation = "IsRun";
        
        private static readonly int IsJump = Animator.StringToHash(JumpAnimation);
        private static readonly int IsRun = Animator.StringToHash(RunAnimation);

        #endregion

        #endregion

        [Inject]
        public void Construct(IPlayerMovement playerMovement, IPlayerJump playerJump)
        {
            _playerJump = playerJump;

            playerMovement.HorizontalInput.Subscribe(FlipSprite).AddTo(_disposables);
            _playerJump.IsJump.Changed += StartJumpAnimation;
        }

        private void FlipSprite(float value)
        {
            _playerSpriteRenderer.flipX = value < 0;
            
            _playerAnimator.SetBool(IsRun, value != 0);
        }

        private void StartJumpAnimation(bool oldValue, bool value)
        {
            _playerAnimator.SetBool(IsJump, value);
        }

        #region MonoBehaviour

        private void OnDestroy()
        {
            _disposables.Clear();
            _playerJump.IsJump.Changed -= StartJumpAnimation;
        }

        #endregion
    }
}
