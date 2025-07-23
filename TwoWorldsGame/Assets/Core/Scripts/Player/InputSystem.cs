using System;
using UnityEngine;
using Zenject;

namespace Core.Scripts.Player
{
    public class InputSystem : ITickable, IDisposable
    {
        #region Fields

        private PlayerInputAction _playerInputAction;
        
        private IPlayerMovement _playerMovement;
        private IPlayerJump _playerJump;
        private IFlipGravity _playerGravity;

        #endregion

        #region MovementInject
        
        [Inject]
        private void Construct(IPlayerMovement playerMovement, IPlayerJump playerJump, IFlipGravity playerGravity)
        {
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.PlayerMovement.Enable();
            
            _playerMovement = playerMovement;
            _playerGravity = playerGravity;
            _playerJump = playerJump;
            
            _playerInputAction.PlayerMovement.Jump.performed += _playerJump.Jump;
            _playerInputAction.PlayerMovement.FLipGravity.performed += _playerGravity.FlipGravity;
        }
        
        #endregion

        public void Tick()
        {
            _playerMovement.Move(_playerInputAction.PlayerMovement.Move.ReadValue<Vector2>());
        }

        public void Dispose()
        {
            _playerInputAction?.Dispose();
        }
    }
}
