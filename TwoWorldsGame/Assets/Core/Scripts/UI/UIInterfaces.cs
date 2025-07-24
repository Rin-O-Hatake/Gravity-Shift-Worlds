using System.Collections.Generic;
using UniRx;

namespace Core.Scripts.UI
{
    public interface IDiamondCounter
    {
        public ReactiveProperty<int> DiamondCount { get; }
        
        public void AddDiamonds(int amount = 1);
        public void RemoveDiamonds(int amount = 1);
    }

    public interface ITooltipDataRepository
    {
        public List<LevelTransitionContactData> InteractiveObjects { get; }
    }
}
