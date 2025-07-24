using UniRx;
using UnityEngine;

namespace Core.Scripts.LevelController
{
    public interface ILevelTransitionContact
    {
        public ReactiveProperty<bool> IsTransitionContact { get; }
    }
}
