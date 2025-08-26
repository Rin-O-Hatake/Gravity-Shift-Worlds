using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Scripts.Player.Attacker
{
    public class RangeAttacker : MonoBehaviour, IAttackerBase, ISetupRangeAttack
    {
        #region Fields
        
        [SerializeField] private ProjectileRangeAttack _projectilePrefab;
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField] private float _attackInterval = 1f;
        
        private float _timeSinceLastAttack = 0f;
        private IAttackerSetup _setup;
        private List<ProjectileRangeAttack> _currentProjectiles = new List<ProjectileRangeAttack>();
        private CompositeDisposable _disposables = new CompositeDisposable();
        private Vector3 _projectileDirection;

        #region Properties

        public ReactiveProperty<bool> IsAttacking { get; } = new ReactiveProperty<bool>(false);
        public TypePlayerAttack Type { get; } = TypePlayerAttack.Ranged;

        #endregion

        #endregion
        
        #region Inject

        [Inject]
        private void Construct(IAttackerSetup attackerSetup, IPlayerMovement playerMovement)
        {
            _setup = attackerSetup;
            playerMovement.HorizontalInput.Subscribe(ChangeDirectionMoveProjectile).AddTo(_disposables);
        }

        #endregion
        
        public void Attack()
        {
            if (IsAttacking.Value)
            {
                return;
            }
            
            _timeSinceLastAttack = 0f;
            IsAttacking.Value = true;
            InitializeProjectile();
        }

        private void InitializeProjectile()
        {
            ProjectileRangeAttack newProjectile = _currentProjectiles.FirstOrDefault(projectile => projectile.IsEnable == false);

            if (newProjectile == null)
            {
                newProjectile = CreateProjectile();
            }
            
            newProjectile.transform.position = _projectileSpawnPoint.position;
            newProjectile.Shoot(_projectileDirection);
        }

        private ProjectileRangeAttack CreateProjectile()
        {
            ProjectileRangeAttack projectileRangeAttack = Instantiate(_projectilePrefab, _projectileSpawnPoint.position, Quaternion.identity);
            _currentProjectiles.Add(projectileRangeAttack);
            return projectileRangeAttack;
        }

        public void Setup(InputAction.CallbackContext context)
        {
            _setup.Setup(this);
        }

        private void ChangeDirectionMoveProjectile(float horizontalInput)
        {
            if (horizontalInput == 0)
            {
                return;
            }
            
            _projectileDirection = horizontalInput > 0 ? Vector3.right : Vector3.left;
        }
        
        #region MonoBehaviour

        private void OnDestroy()
        {
            _disposables.Clear();
        }
        
        void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;

            if (_timeSinceLastAttack >= _attackInterval)
            {
                IsAttacking.Value = false;
            }
        }

        #endregion
    }
}
