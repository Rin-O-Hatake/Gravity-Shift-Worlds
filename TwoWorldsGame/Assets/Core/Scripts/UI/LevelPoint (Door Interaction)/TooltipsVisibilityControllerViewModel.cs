using System.Collections.Generic;
using Core.Scripts.LevelController;
using Zenject;

namespace Core.Scripts.UI.LevelPoint__Door_Interaction_
{
    public class TooltipsVisibilityControllerViewModel : ITooltipDataRepository
    {
        #region Fields

        private ILevelTransitionContact _levelTransitionContact;

        #region Properties

        public List<ShowTooltipInfoData> InteractiveObjects { get; } = new List<ShowTooltipInfoData>();

        #endregion

        #endregion
        
        [Inject]
        private void Construct(ILevelTransitionContact levelTransitionContact)
        {
            InteractiveObjects.Add(levelTransitionContact.IsTransitionContact);
        }
    }
}
