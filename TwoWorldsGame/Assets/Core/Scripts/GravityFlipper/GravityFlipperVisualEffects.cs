using System;
using Core.Scripts.Player;
using UnityEngine;
using Zenject;

namespace Core.Scripts
{
    public class GravityFlipperVisualEffects : MonoBehaviour
    {
        #region Fields

        [Header("Gravity Flipper Visual")]
        [Space(10)]
        [SerializeField] private ParticleSystem _gravityFlipperEffectUp; 
        [SerializeField] private ParticleSystem _gravityFlipperEffectDown;
        
        private IFlipGravity _gravityFlipper;

        #endregion

        [Inject]
        public void Construct(IFlipGravity gravityFlipper)
        {
            _gravityFlipper = gravityFlipper;
        }

        private void PlayEffect(bool oldTypeGravity, bool IsNormalGravity)
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
            _gravityFlipper.IsNormalGravity.Changed -= PlayEffect;
        }
        
        private void Start()
        {
            _gravityFlipper.IsNormalGravity.Changed += PlayEffect;
        }

        #endregion
    }
}
