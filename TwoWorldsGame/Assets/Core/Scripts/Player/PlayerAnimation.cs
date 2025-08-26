using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Scripts.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Animator _playerAnimator;

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
            FlipScalePlayer(value);
            _playerAnimator.SetBool(IsRun, value != 0);
        }

        private void FlipScalePlayer(float value)
        {
            Transform transform = _playerAnimator.gameObject.transform;
            float scaleXPlayer = transform.localScale.x;

            if ((scaleXPlayer > 0 && value < 0) || (scaleXPlayer < 0 && value > 0))
            {
                scaleXPlayer *= -1;
            }
            
            Vector3 newScalePlayer = new Vector3(scaleXPlayer, transform.localScale.y, transform.localScale.z);
            _playerAnimator.gameObject.transform.localScale = newScalePlayer;
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
