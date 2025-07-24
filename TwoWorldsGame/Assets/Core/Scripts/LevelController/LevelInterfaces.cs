using Core.Scripts.UI;
using UnityEngine;

namespace Core.Scripts.LevelController
{
    public interface ILevelTransitionContact
    {
        public ShowTooltipInfoData IsTransitionContact { get; }
        public void SetNewPosition(Vector3 newPosition);
    }
}
