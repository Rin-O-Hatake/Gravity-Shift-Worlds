using UnityEngine;

namespace Core.Scripts.Player.Attacker
{
    public class ProjectileRangeAttack : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _projectileTransform;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _maxDistance = 10f; 
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private Vector3 _startPosition;
        private bool _isEnable;

        #region Properties

        public bool IsEnable => _isEnable;

        #endregion

        #endregion

        public void Shoot(Vector3 direction)
        {
            gameObject.SetActive(true);
            _isEnable = true;
            SetupMove(direction);
            SetDirectionScale(direction);
        }

        private void SetupMove(Vector3 direction)
        {
            _startPosition = transform.position;
            _rigidbody.velocity = direction.normalized * _speed;
        }

        private void SetDirectionScale(Vector3 direction)
        {
            float scaleXProjectile = _projectileTransform.localScale.x;

            if ((scaleXProjectile > 0 && direction == Vector3.right) || (scaleXProjectile < 0 && direction == Vector3.left))
            {
                scaleXProjectile *= -1;
            }
            
            _projectileTransform.localScale = 
                new Vector3(scaleXProjectile, _projectileTransform.localScale.y, _projectileTransform.localScale.z);
        }

        private void HideProjectile()
        {
            _isEnable = false;
            gameObject.SetActive(false);
        }

        #region MonoBehavior

        void Update()
        {
            if (!_isEnable)
            {
                return;
            }
            
            if (Vector3.Distance(_startPosition, transform.position) > _maxDistance)
            {
                HideProjectile();
            }
        }

        #endregion
    }
}
