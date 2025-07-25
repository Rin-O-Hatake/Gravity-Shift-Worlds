using System;
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

        #endregion
        
        public override void InstallBindings()
        {
            InjectSetupCamera();
            InjectLevelController();
            InjectDataStorage();
            InjectStorageManager();
            InjectGameState();
        }

        private void InjectStorageManager()
        {
            Container.Bind<StorageManager>().FromNew().AsSingle().NonLazy();
        }

        private void InjectGameState()
        {
            Container.Bind<GameState>().FromNew().AsCached().NonLazy();
            Container.Bind<IPauseState>().To<GameState>().AsCached();
            Container.Bind<ILoadingState>().To<GameState>().AsCached();
            Container.Bind<IStoppable>().To<GameState>().AsCached();
        }
        
        private void InjectDataStorage()
        {
            Container.Bind<DataStorage>().FromNew().AsCached().NonLazy();
            
            Container.Bind<ISaveDataStorage>().To<DataStorage>().AsCached();
            Container.Bind<ILoaderDataStorage>().To<DataStorage>().AsCached();
        }

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
