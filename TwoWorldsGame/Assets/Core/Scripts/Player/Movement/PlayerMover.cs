using Core.Scripts.Audio;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Scripts.Player.Movement
{
    public class PlayerMover : MonoBehaviour, IPlayerMovement
    {
        #region Fields

        [SerializeField] private Rigidbody2D _rigidbodyPlayer;
        [SerializeField] private float moveSpeed = 5f;

        private IMoveSound _moveSound;
        
        #region Peroperties

        public ReactiveProperty<float> HorizontalInput { get; } = new ReactiveProperty<float>();

        #endregion
        
        #endregion

        #region Inject

        [Inject]
        public void Construct(IMoveSound moveSound)
        {
            _moveSound = moveSound;
        }

        #endregion
        
        public void Move(Vector2 movement)
        {
            HorizontalInput.Value = movement.x;
            
            _rigidbodyPlayer.velocity = new Vector2(movement.x * moveSpeed, _rigidbodyPlayer.velocity.y);

            if (Mathf.Approximately(movement.x, default))
            {
                return;
            }
            
            PlaySoundMove();
        }

        private void PlaySoundMove()
        {
            // _moveSound.PlayMoveSound();
        }
        
    }
}
