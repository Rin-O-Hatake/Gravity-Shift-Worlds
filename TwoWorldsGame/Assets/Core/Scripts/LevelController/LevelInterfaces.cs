using Core.Scripts.UI;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Scripts.LevelController
{
    public interface ILevelTransitionContact
    {
        public LevelTransitionContactData IsTransitionContact { get; }
        public void SetNewPosition(Vector3 newPosition);
        public void InteractionPortal(InputAction.CallbackContext context);
    }

    public interface ILoadingNextLevel
    {
        public void LoadNextLevel();
    }

    public interface ILoadingLevel
    {
        public void LoadLevel(int levelIndex = 1);
    }

    public interface ILevelTracker
    {
        public ReactiveProperty<int> CurrentLevelIndex { get; }
    }
}
