using System;
using Core.Scripts.LevelController;
using UniRx;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Scripts.Player
{
    public class InputSystem : ITickable, IDisposable
    {
        #region Fields

        private PlayerInputAction _playerInputAction = new PlayerInputAction();
        
        private IPlayerMovement _playerMovement;
        
        #endregion

        #region Inject

        [Inject]
        private void Construct(ILevelTransitionContact levelTransitionContact)
        {
            _playerInputAction.Interaction.Enable();
            
            _playerInputAction.Interaction.InteractionDoor.performed += levelTransitionContact.InteractionPortal;
        }

        #region MovementInject
        
        [Inject]
        private void Construct(IPlayerMovement playerMovement, IPlayerJump playerJump, IFlipGravity playerGravity)
        {
            _playerInputAction.PlayerMovement.Enable();
            
            _playerMovement = playerMovement;
            
            _playerInputAction.PlayerMovement.Jump.performed += playerJump.Jump;
            _playerInputAction.PlayerMovement.FLipGravity.performed += playerGravity.FlipGravity;
        }
        
        #endregion

        #endregion

        #region Zenject Interfaces

        public void Tick()
        {
            _playerMovement.Move(_playerInputAction.PlayerMovement.Move.ReadValue<Vector2>());
        }

        public void Dispose()
        {
            _playerInputAction?.Dispose();
        }

        #endregion
    }
}
