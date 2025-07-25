using UniRx;
using UnityEngine;

namespace Core.Scripts.StatesGame
{
    public class GameState : IPauseState, ILoadingState, IStoppable
    {
        #region Fields

        #region Properties

        public ReactiveProperty<bool> IsPaused { get; } = new ReactiveProperty<bool>(false);

        public ReactiveProperty<bool> IsLoading { get; } = new ReactiveProperty<bool>(false);
        

        public ReactiveProperty<bool> IsStop { get; } = new ReactiveProperty<bool>(false);

        #endregion

        #endregion
        
        
        public void SetPauseState(bool isPaused)
        {
            IsPaused.Value = isPaused;
            CheckStoppable();
        }
        
        public void SetLoadingState(bool isLoading)
        {
            IsLoading.Value = isLoading;
            CheckStoppable();
        }

        private void CheckStoppable()
        {
            IsStop.Value = IsPaused.Value || IsLoading.Value;
        }
    }
}
