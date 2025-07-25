using UniRx;

namespace Core.Scripts.StatesGame
{
    public class GameState : IPauseState, ILoadingState
    {
        #region Fields

        #region Properties

        public ReactiveProperty<bool> IsPaused { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> IsLoading { get; } = new ReactiveProperty<bool>(false);

        #endregion

        #endregion
    }
}
