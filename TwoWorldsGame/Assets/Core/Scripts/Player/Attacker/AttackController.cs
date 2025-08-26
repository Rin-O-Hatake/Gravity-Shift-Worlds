using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Scripts.Player.Attacker
{
    public class AttackController : MonoBehaviour, IAttackerSetup, IAttackerInput, ISenderTypeAttack
    {
        #region Fields

        private IAttackerBase _currentAttacker;

        #region Properties

        public ReactiveProperty<TypePlayerAttack> Type { get; } = new ReactiveProperty<TypePlayerAttack>();

        #endregion

        #endregion
        
        public void Setup(IAttackerBase attacker)
        {
            Type.Value = attacker.Type;
            _currentAttacker = attacker;
        }

        public void AttackWeapon(InputAction.CallbackContext context)
        {
            if (_currentAttacker.IsAttacking.Value)
            {
                return;
            }
            
            _currentAttacker.Attack();
        }
    }
}
