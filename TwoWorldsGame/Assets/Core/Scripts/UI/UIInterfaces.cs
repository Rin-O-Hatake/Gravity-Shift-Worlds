using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Core.Scripts.UI
{
    public interface IDiamondCounter
    {
        public ReactiveProperty<int> DiamondCount { get; }
        
        public void AddDiamonds(int amount = 1);
        public void RemoveDiamonds(int amount = 1);
        public void LoadDiamonds(int amount = 0);
    }

    public interface ITooltipDataRepository
    {
        public List<LevelTransitionContactData> InteractiveObjects { get; }
    }
    
}
