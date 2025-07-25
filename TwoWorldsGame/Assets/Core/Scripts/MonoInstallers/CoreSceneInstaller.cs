using System;
using Core.Scripts.Audio;
using Core.Scripts.Camera;
using Core.Scripts.DataManager;
using Core.Scripts.LevelController;
using Core.Scripts.StatesGame;
using UnityEngine;
using Zenject;

namespace Core.Scripts.MonoInstallers
{
    public class CoreSceneInstaller : MonoInstaller
    {
        #region Fields

        [SerializeField] private VirtualCameraController _cameraController;
        [SerializeField] private LevelController.LevelController _levelController;
        [SerializeField] private MovementSound _movementSound;

        #endregion
        
        public override void InstallBindings()
        {
            InjectSetupCamera();
            InjectLevelController();
            InjectDataStorage();
            InjectStorageManager();
            InjectGameState();
            InjectMovementSound();
        }

        #region Audio

        private void InjectMovementSound()
        {
            Container.Bind<IJumpSound>().To<MovementSound>().FromInstance(_movementSound);
            Container.Bind<IMoveSound>().To<MovementSound>().FromInstance(_movementSound);
        }

        #endregion

        #region Game State

        private void InjectGameState()
        {
            Container.Bind<GameState>().FromNew().AsCached().NonLazy();
            Container.Bind<IPauseState>().To<GameState>().AsCached();
            Container.Bind<ILoadingState>().To<GameState>().AsCached();
            Container.Bind<IStoppable>().To<GameState>().AsCached();
        }

        #endregion

        #region Data Storage

        private void InjectStorageManager()
        {
            Container.Bind<StorageManager>().FromNew().AsSingle().NonLazy();
        }
        
        private void InjectDataStorage()
        {
            Container.Bind<DataStorage>().FromNew().AsCached().NonLazy();
            
            Container.Bind<ISaveDataStorage>().To<DataStorage>().AsCached();
            Container.Bind<ILoaderDataStorage>().To<DataStorage>().AsCached();
        }

        #endregion

        private void InjectSetupCamera()
        {
            Container.Bind<ISetupCamera>().To<VirtualCameraController>().FromInstance(_cameraController);
        }
        private void InjectLevelController()
        {
            Container.Bind<ILoadingNextLevel>().To<LevelController.LevelController>().FromInstance(_levelController);
            Container.Bind<ILoadingLevel>().To<LevelController.LevelController>().FromInstance(_levelController);
            Container.Bind<ILevelTracker>().To<LevelController.LevelController>().FromInstance(_levelController);
        }
    }
}
