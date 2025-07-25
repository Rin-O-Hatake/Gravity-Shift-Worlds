using UniRx;
using UnityEngine;

namespace Core.Scripts.StatesGame
{
    public interface IPauseState
    {
        public ReactiveProperty<bool> IsPaused { get; }
        public void SetPauseState(bool isPaused);
    }
    
    public interface ILoadingState
    {
        public ReactiveProperty<bool> IsLoading { get; }
        public void SetLoadingState(bool isLoading);
    }

    public interface IStoppable
    {
        public ReactiveProperty<bool> IsStop { get; }
    }
}
