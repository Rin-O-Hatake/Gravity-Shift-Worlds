using System;
using Core.Scripts.LevelController;
using Core.Scripts.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Scripts.DataManager
{
    public class StorageManager : IDisposable
    {
        #region Feilds

        private ILoaderDataStorage _loaderDataStorage;
        private ISaveDataStorage _saveDataStorage;
        
        private CompositeDisposable _disposables = new CompositeDisposable();

        private const int LOAD_SKIP_ITERATION = 2;

        #endregion

        #region Inject

        [Inject]
        private void Construct(ILoaderDataStorage loaderDataStorage, ISaveDataStorage saveDataStorage)
        {
            _loaderDataStorage = loaderDataStorage;
            _saveDataStorage = saveDataStorage;
        }

        [Inject]
        private void Construct(ILevelTracker levelTracker, ILoadingLevel  loadingLevel, IDiamondCounter diamondCounter)
        {
            levelTracker.CurrentLevelIndex.Skip(LOAD_SKIP_ITERATION).Subscribe(level => BeginLevelSequence(level, diamondCounter))
                .AddTo(_disposables);
            
            LoadLevel(loadingLevel);
            LoadDiamonds(diamondCounter);
        }

        #endregion

        #region Save
        
        private void BeginLevelSequence(int currentLevel, IDiamondCounter diamondCounter)
        {
            SaveLevel(currentLevel);
            SaveCountDiamonds(diamondCounter);
        }

        private void SaveCountDiamonds(IDiamondCounter diamondCounter)
        {
            _saveDataStorage.SaveData(SaveDataType.Diamonds, diamondCounter.DiamondCount.Value);
        }

        private void SaveLevel(int currentLevel)
        {
            _saveDataStorage.SaveData(SaveDataType.Level, currentLevel);
        }

        #endregion

        #region Loader

        private void LoadLevel(ILoadingLevel loadingLevel)
        {
            loadingLevel.LoadLevel(_loaderDataStorage.GetDataInt(SaveDataType.Level));
        }
        
        private void LoadDiamonds(IDiamondCounter diamondCounter)
        {
            diamondCounter.LoadDiamonds(_loaderDataStorage.GetDataInt(SaveDataType.Diamonds));
        }

        #endregion

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}
