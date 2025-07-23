using Core.Scripts.Player;
using Core.Scripts.Player.Movement;
using Core.Scripts.Player.Movement.Jumper;
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

        #endregion
        
        public override void InstallBindings()
        {
            InjectIPlayerMovement();
            InjectPlayerJumper();
            InjectGroundCheck();
            InjectGravityFlipper();

            BindingInputSystem();
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

        private void BindingInputSystem()
        {
            Container.Bind<InputSystem>().FromNew().AsCached().NonLazy();
            Container.Bind<ITickable>().To<InputSystem>().AsCached();
        }
    }
}
