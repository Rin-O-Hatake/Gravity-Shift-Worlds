using System;
using Core.Scripts.Camera;
using Core.Scripts.DataManager;
using Core.Scripts.LevelController;
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
        }

        private void InjectStorageManager()
        {
            Container.Bind<StorageManager>().FromNew().AsCached().NonLazy();
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
