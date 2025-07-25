using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Scripts.Camera;
using Core.Scripts.Player;
using Core.Scripts.StatesGame;
using Core.Scripts.UI.Loading;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Scripts.LevelController
{
    public class LevelController : MonoBehaviour, ILoadingNextLevel, ILoadingLevel, ILevelTracker
    {
        #region Fields

        [SerializeField] private List<LevelDataView> _levelDataViews = new List<LevelDataView>();

        private ILevelTransitionContact _levelTransitionContact;
        private ISetupCamera _setupCamera;
        private IPlayerSetup _playerSetup;

        private ILoadingState _loadingState;

        private BaseFadePanel _loadingFadePanel;

        private const int START_LEVEL = 1;

        #region Properties

        public ReactiveProperty<int> CurrentLevelIndex { get; } = new ReactiveProperty<int>();

        #endregion
        
        #endregion

        #region Inject

        [Inject]
        public void Construct(ILevelTransitionContact levelTransitionContact, ISetupCamera setupCamera, IPlayerSetup playerSetup)
        {
            _setupCamera = setupCamera;
            _levelTransitionContact = levelTransitionContact;
            _playerSetup = playerSetup;
        }

        [Inject]
        public void Construct(BaseFadePanel loadingView)
        {
            _loadingFadePanel = loadingView;
        }
        
        [Inject]
        public void Construct(ILoadingState loadingState)
        {
            _loadingState = loadingState;
        }

        #endregion

        public void LoadNextLevel()
        {
            if (!CurrentLevelIndex.HasValue)
            {
                Debug.LogError("CurrentLevelIndex is Null");
                return;
            }

            if (_loadingState.IsLoading.Value)
            {
                return;
            }
            
            _loadingState.SetLoadingState(true);
            
            CurrentLevelIndex.Value++;
            LevelDataView levelDataView = _levelDataViews.FirstOrDefault(level => CurrentLevelIndex.Value == level.LevelNumber);

            if (!levelDataView)
            {
                return;
            }

            _loadingFadePanel.FadeInLoadingPanel(() => SetupLevelData(levelDataView)).Forget();
        }

        private void SetupLevelData(LevelDataView levelDataView)
        {
            ShowLevel(levelDataView);
            _levelTransitionContact.SetNewPosition(levelDataView.ExitLevelPortal.position);
            _setupCamera.SetupLimitationsMove(levelDataView.LevelPolygonCollider2D);
            _playerSetup.SetupPosition(levelDataView.StartPositionPlayer.position);
            
            HideLevel(CurrentLevelIndex.LastValue);
        }
        private void HideLevel(int levelNumber)
        {
            _levelDataViews.FirstOrDefault(level => level.LevelNumber == levelNumber).gameObject.SetActive(false);
            _loadingFadePanel.FadeOutLoadingPanel().Forget();
            _loadingState.SetLoadingState(false);
        }

        private void ShowLevel(LevelDataView levelDataView)
        {
            levelDataView.gameObject.SetActive(true);
        }

        public void LoadLevel(int levelIndex = START_LEVEL)
        {
            if (levelIndex == default)
            {
                levelIndex = START_LEVEL;
            }
            
            CurrentLevelIndex.Value = levelIndex;
        }
    }
}
