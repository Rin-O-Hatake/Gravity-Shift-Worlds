using UniRx;

namespace Core.Scripts.UI.DiamondCounter
{
    public interface IDiamondCounter
    {
        public ReactiveProperty<int> DiamondCount { get; }
        
        public void AddDiamonds(int amount = 1);
        public void RemoveDiamonds(int amount = 1);
    }
}
