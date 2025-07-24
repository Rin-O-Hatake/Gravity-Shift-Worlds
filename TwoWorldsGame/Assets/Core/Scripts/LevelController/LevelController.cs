using System.Collections.Generic;
using System.Linq;
using Core.Scripts.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Scripts.LevelController
{
    public class LevelController : MonoBehaviour, ILoadingNextLevel
    {
        #region Fields

        [SerializeField] private List<LevelDataView> _levelDataViews = new List<LevelDataView>();

        private ILevelTransitionContact _levelTransitionContact;

        #region Properties

        public ReactiveProperty<int> CurrentLevelIndex { get; } = new ReactiveProperty<int>();

        #endregion
        
        #endregion

        #region Inject

        [Inject]
        public void Construct(ILevelTransitionContact levelTransitionContact)
        {
            _levelTransitionContact = levelTransitionContact;
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
        }
        private void HideLevel(int levelNumber)
        {
            _levelDataViews.FirstOrDefault(level => level.LevelNumber == levelNumber).gameObject.SetActive(false);
        }

        private void ShowLevel(LevelDataView levelDataView)
        {
            levelDataView.gameObject.SetActive(true);
        }
    }
}
