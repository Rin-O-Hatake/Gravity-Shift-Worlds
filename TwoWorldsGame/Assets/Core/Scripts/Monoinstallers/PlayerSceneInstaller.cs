using Core.Scripts.GravityFlipperFolder;
using Core.Scripts.Player;
using Core.Scripts.Player.Attacker;
using Core.Scripts.Player.Movement;
using Core.Scripts.Player.Movement.Jumper;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Core.Scripts.Monoinstallers
{
    public class PlayerSceneInstaller : MonoInstaller
    {
        #region Fields

        [Header("Player Physics")]
        [Space(15)]
        
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private PlayerJumper _playerJumper;
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private GravityFlipper _gravityFlipper;
        [SerializeField] private PlayerSetup _playerSetup;
        
        [Title("Attack")]
        [SerializeField] private AttackController _attacker;
        [SerializeField] private MeleeAttacker _meleeAttacker;
        [SerializeField] private RangeAttacker _rangeAttacker;

        #endregion
        
        public override void InstallBindings()
        {
            InjectIPlayerMovement();
            InjectPlayerJumper();
            InjectGroundCheck();
            InjectGravityFlipper();
            InjectPlayerSetup();

            BindingInputSystem();

            InjectAttacker();
        }
        
        #region Player Physics

        private void InjectIPlayerMovement()
        {
            Container.Bind<IPlayerMovement>().To<PlayerMover>().FromInstance(_playerMover);
        }
        
        private void InjectPlayerJumper()
        {
            Container.Bind<IPlayerJump>().To<PlayerJumper>().FromInstance(_playerJumper);
        }
        
        private void InjectGroundCheck()
        {
            Container.Bind<IGroundCheck>().To<GroundCheck>().FromInstance(_groundCheck);
        }
        
        private void InjectGravityFlipper()
        {
            Container.Bind<IFlipGravity>().To<GravityFlipper>().FromInstance(_gravityFlipper);
        }

        #endregion
        
        private void InjectPlayerSetup()
        {
            Container.Bind<IPlayerSetup>().To<PlayerSetup>().FromInstance(_playerSetup);
        }

        private void BindingInputSystem()
        {
            Container.Bind<InputSystem>().FromNew().AsCached().NonLazy();
            Container.Bind<ITickable>().To<InputSystem>().AsCached();
        }

        private void InjectAttacker()
        {
            Container.Bind<IAttackerSetup>().To<AttackController>().FromInstance(_attacker);
            Container.Bind<IAttackerInput>().To<AttackController>().FromInstance(_attacker);
            Container.Bind<ISenderTypeAttack>().To<AttackController>().FromInstance(_attacker);
            Container.Bind<ISetupMeleeAttack>().To<MeleeAttacker>().FromInstance(_meleeAttacker);
            Container.Bind<ISetupRangeAttack>().To<RangeAttacker>().FromInstance(_rangeAttacker);
        }
    }
}
