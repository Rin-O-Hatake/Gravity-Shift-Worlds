using UniRx;
using UnityEngine;

namespace Core.Scripts.Player.Movement
{
    public class GroundCheck : MonoBehaviour, IGroundCheck
    {
        #region Fields

        [SerializeField] private float _groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask _groundLayer;

        #region Properties

        public ReactiveProperty<bool> IsGround { get; } = new ReactiveProperty<bool>();

        #endregion
        
        #endregion

        public void FixedUpdate()
        {
            IsGround.Value = Physics2D.OverlapCircle(transform.position, _groundCheckRadius, _groundLayer);
        }
        
        public Transform GetFootTransform()
        {
            return transform;
        }
    }
}
