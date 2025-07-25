using UniRx;
using UnityEngine;

namespace Core.Scripts.StatesGame
{
    public interface IPauseState
    {
        public ReactiveProperty<bool> IsPaused { get; }
    }
    
    public interface ILoadingState
    {
        public ReactiveProperty<bool> IsLoading { get; }
    }
}
