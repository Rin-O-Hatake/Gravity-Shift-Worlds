using UnityEngine;

namespace Core.Scripts.Player.Movement
{
    public class GroundCheck : MonoBehaviour, IGroundCheck
    {
        #region Fields

        [SerializeField] private float _groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask _groundLayer;

        #region Properties

        public ReactiveVariable<bool> IsGround { get; } = new ReactiveVariable<bool>();

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
