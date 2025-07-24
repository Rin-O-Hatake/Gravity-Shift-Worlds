using UniRx;
using UnityEngine;

namespace Core.Scripts.Player.Movement
{
    public class PlayerMover : MonoBehaviour, IPlayerMovement
    {
        #region Fields

        [SerializeField] private Rigidbody2D _rigidbodyPlayer;
        [SerializeField] private float moveSpeed = 5f;
        
        #region Peroperties

        public ReactiveProperty<float> HorizontalInput { get; } = new ReactiveProperty<float>();

        #endregion
        
        #endregion
        
        public void Move(Vector2 movement)
        {
            HorizontalInput.Value = movement.x;
            
            _rigidbodyPlayer.velocity = new Vector2(movement.x * moveSpeed, _rigidbodyPlayer.velocity.y);
        }
        
    }
}
