using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Scripts.Player.Movement.Jumper
{
    [Serializable]
    public class PlayerJumperVisualEffects 
    {
        #region Field

        [Header("Player Jumper Effect")]
        [SerializeField] private ParticleSystem _playerJumperEffectPrefab;
        
        private ParticleSystem _currentJumperEffect;

        #endregion

        public void Initialize()
        {
            SpawnEffect();
        }

        public void PlayEffect(Vector3 position)
        {
            SetPosition(position);
            EnableJumperEffect();
        }

        private void SetPosition(Vector3 position)
        {
            _currentJumperEffect.transform.position = position;
        }

        private void EnableJumperEffect()
        {
            if (_currentJumperEffect.isPlaying)
            {
                _currentJumperEffect.Stop();
                _currentJumperEffect.Play();
                return;
            }
            
            _currentJumperEffect.gameObject.SetActive(true);
        }

        private void SpawnEffect()
        {
            _currentJumperEffect = Object.Instantiate(_playerJumperEffectPrefab, new Vector3(), _playerJumperEffectPrefab.transform.rotation);
        }
    }
}
