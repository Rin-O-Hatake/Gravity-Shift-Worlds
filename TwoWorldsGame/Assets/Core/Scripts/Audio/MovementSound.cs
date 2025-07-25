using System;
using System.Threading;
using Core.Scripts.Player;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core.Scripts.Audio
{
    public class MovementSound : MonoBehaviour, IJumpSound, IMoveSound
    {
        #region Fields

        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _footstepClips;

        [SerializeField] private float _stepInterval;

        private IGroundCheck _groundCheck;
        private IPlayerMovement _playerMovement;
        
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CompositeDisposable _disposable = new CompositeDisposable();

        #endregion

        #region Inject

        [Inject]
        public void Construct(IGroundCheck groundCheck, IPlayerMovement playerMovement)
        {
            playerMovement.HorizontalInput.Where(horizontalInput => horizontalInput != 0).Subscribe(StopFootsteps).AddTo(_disposable);
            _playerMovement = playerMovement;
            _groundCheck = groundCheck;
        }

        #endregion
        
        void IJumpSound.PlayJumpSound()
        {
            if (!_jumpSound || !_audioSource)
            {
                return;
            }
            _audioSource.PlayOneShot(_jumpSound);
        }

        void IMoveSound.PlayMoveSound()
        {
            if (_footstepClips.Length == 0 || !_audioSource)
            {
                return;
            }
            
            if (!_groundCheck.IsGround.Value)
            {
                return;
            }
            
            StartFootsteps().Forget();
        }
        
        public void StopFootsteps(float value)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }
        
        public async UniTaskVoid StartFootsteps()
        {
            try
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    
                    PlayFootstepSound();
                    await UniTask.Delay(TimeSpan.FromSeconds(_stepInterval), cancellationToken: _cancellationTokenSource.Token);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Footsteps task cancelled.");
            }
        }

        public void PlayFootstepSound()
        {
            int randomIndex = Random.Range(0, _footstepClips.Length);
            _audioSource.PlayOneShot(_footstepClips[randomIndex]);
        }
    }
}
