using System;
using Core.Scripts.LevelController;
using Core.Scripts.StatesGame;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Scripts.Player
{
    public class InputSystem : ITickable, IDisposable
    {
        #region Fields

        private PlayerInputAction _playerInputAction = new PlayerInputAction();
        
        private IPlayerMovement _playerMovement;
        
        private CompositeDisposable _disposables = new CompositeDisposable();
        
        #endregion

        #region Inject

        [Inject]
        private void Construct(ILevelTransitionContact levelTransitionContact)
        {
            _playerInputAction.Interaction.Enable();
            
            _playerInputAction.Interaction.InteractionDoor.performed += levelTransitionContact.InteractionPortal;
        }

        //TODO not working, does not subscribe
        [Inject]
        private void Construct(IStoppable stoppable)
        {
            stoppable.IsStop.Subscribe(CheckState).AddTo(_disposables);
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

        #region Check State

        private void CheckState(bool isStopped)
        {
            if (isStopped)
            {
                _playerInputAction.Disable();
                return;
            }
            
            _playerInputAction.Enable();
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
            Debug.Log("1");
            _disposables.Dispose();
            _playerInputAction?.Dispose();
        }

        #endregion
    }
}
