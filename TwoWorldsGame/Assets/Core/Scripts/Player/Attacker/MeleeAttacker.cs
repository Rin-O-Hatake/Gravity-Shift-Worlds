using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Scripts.Player.Attacker
{
    public class MeleeAttacker : MonoBehaviour, IAttackerBase, ISetupMeleeAttack
    {
        #region Fields

        [SerializeField] private Animator _animation;
        
        private IAttackerSetup _setup;

        #region Cons

        private const string MELEE_ATTACK = "Attack";

        #endregion

        #region Properties

        public ReactiveProperty<bool> IsAttacking { get; } = new ReactiveProperty<bool>(false);
        public TypePlayerAttack Type { get; } = TypePlayerAttack.Melee;

        #endregion

        #endregion

        #region Inject

        [Inject]
        private void Construct(IAttackerSetup attackerSetup)
        {
            _setup = attackerSetup;
            _setup.Setup(this);
        }

        #endregion
        
        public void Attack()
        {
            IsAttacking.Value = true;
            _animation.gameObject.SetActive(true);
            _animation.CrossFade(MELEE_ATTACK, 0.1f);
        }

        public void StopAttacking()
        {
            IsAttacking.Value = false;
            _animation.gameObject.SetActive(false);
        }

        public void Setup(InputAction.CallbackContext context)
        {
            _setup.Setup(this);
        }
    }
}
