using UniRx;

namespace Core.Scripts.UI.DiamondCounter
{
    public class DiamondCountViewModel : IDiamondCounter
    {
        #region Fields

        #region Properties

        public ReactiveProperty<int> DiamondCount { get; } = new ReactiveProperty<int>();

        #endregion

        #endregion

        public void AddDiamonds(int amount = 1)
        {
            DiamondCount.Value += amount;
        }

        public void RemoveDiamonds(int amount = 1)
        {
            DiamondCount.Value -= amount;
        }

        public void LoadDiamonds(int amount = 0)
        {
            DiamondCount.Value = amount;
        }
    }
}
