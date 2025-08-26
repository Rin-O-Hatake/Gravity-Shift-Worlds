using UniRx;
using UnityEngine.InputSystem;

namespace Core.Scripts.Player.Attacker
{
    public interface IAttackerBase
    {
        public void Attack();
        public ReactiveProperty<bool> IsAttacking { get; }
        public TypePlayerAttack Type { get; }
    }

    public interface IAttackerSetup
    {
        public void Setup(IAttackerBase attacker);
    }

    public interface ISenderTypeAttack
    {
        public ReactiveProperty<TypePlayerAttack> Type { get; }
    }

    public interface IAttackerInput
    {
        public void AttackWeapon(InputAction.CallbackContext context);
    }

    public interface ITypeWeaponSetup
    {
        public void Setup(InputAction.CallbackContext context);
    }

    public interface ISetupMeleeAttack : ITypeWeaponSetup
    {
    }
    
    public interface ISetupRangeAttack : ITypeWeaponSetup
    {
    }
}
