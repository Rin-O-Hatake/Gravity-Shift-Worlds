using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Scripts.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;

        private CompositeDisposable _disposables = new CompositeDisposable();

        #region Animation Names

        private const string JUMP_ANIMATION = "IsJump";
        private const string RUN_ANIMATION = "IsRun";
        
        private static readonly int IsJump = Animator.StringToHash(JUMP_ANIMATION);
        private static readonly int IsRun = Animator.StringToHash(RUN_ANIMATION);

        #endregion

        #endregion

        [Inject]
        public void Construct(IPlayerMovement playerMovement, IPlayerJump playerJump)
        {
            playerMovement.HorizontalInput.Subscribe(FlipSprite).AddTo(_disposables);
            playerJump.IsJump.Subscribe(StartJumpAnimation).AddTo(_disposables);
        }

        private void FlipSprite(float value)
        {
            _playerSpriteRenderer.flipX = value < 0;
            
            _playerAnimator.SetBool(IsRun, value != 0);
        }

        private void StartJumpAnimation(bool value)
        {
            _playerAnimator.SetBool(IsJump, value);
        }

        #region MonoBehaviour

        private void OnDestroy()
        {
            _disposables.Clear();
        }

        #endregion
    }
}
