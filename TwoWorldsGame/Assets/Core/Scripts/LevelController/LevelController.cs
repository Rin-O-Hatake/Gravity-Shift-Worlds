using System.Collections.Generic;
using System.Linq;
using Core.Scripts.Camera;
using Core.Scripts.Player;
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

        #endregion

        public void LoadNextLevel()
        {
            HideLevel(CurrentLevelIndex.Value);
            CurrentLevelIndex.Value++;
            LevelDataView levelDataView = _levelDataViews.FirstOrDefault(level => CurrentLevelIndex.Value == level.LevelNumber);

            if (!levelDataView)
            {
                return;
            }

            SetupLevelData(levelDataView);
        }

        private void SetupLevelData(LevelDataView levelDataView)
        {
            ShowLevel(levelDataView);
            _levelTransitionContact.SetNewPosition(levelDataView.ExitLevelPortal.position);
            _setupCamera.SetupLimitationsMove(levelDataView.LevelPolygonCollider2D);
            _playerSetup.SetupPosition(levelDataView.StartPositionPlayer.position);
        }
        private void HideLevel(int levelNumber)
        {
            _levelDataViews.FirstOrDefault(level => level.LevelNumber == levelNumber).gameObject.SetActive(false);
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
