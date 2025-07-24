using Core.Scripts.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Scripts.GravityFlipperFolder
{
    public class GravityFlipperVisualEffects : MonoBehaviour
    {
        #region Fields

        [Header("Gravity Flipper Visual")]
        [Space(10)]
        [SerializeField] private ParticleSystem _gravityFlipperEffectUp; 
        [SerializeField] private ParticleSystem _gravityFlipperEffectDown;
        
        private IFlipGravity _gravityFlipper;
        private CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        [Inject]
        public void Construct(IFlipGravity gravityFlipper)
        {
            _gravityFlipper = gravityFlipper;
            
            _gravityFlipper.IsNormalGravity.Skip(1).Subscribe(PlayEffect).AddTo(_disposables);
        }

        private void PlayEffect(bool IsNormalGravity)
        {
            EnableEffect(IsNormalGravity ? _gravityFlipperEffectDown : _gravityFlipperEffectUp);
        }

        private void EnableEffect(ParticleSystem gravityFlipperEffect)
        {
            if (gravityFlipperEffect.isPlaying)
            {
                gravityFlipperEffect.Stop();
                gravityFlipperEffect.Play();
                return;
            }
            
            gravityFlipperEffect.gameObject.SetActive(true);
        }

        #region MonoBehaviour

        private void OnDestroy()
        {
            _disposables.Clear();
        }

        #endregion
    }
}
