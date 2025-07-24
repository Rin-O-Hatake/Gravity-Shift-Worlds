using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Scripts.UI.LevelPoint__Door_Interaction_
{
    public class TooltipsVisibilityView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<ViewPanelTooltip> _tooltips = new List<ViewPanelTooltip>();

        #endregion

        [Inject]
        private void Construct(ITooltipDataRepository tooltipDataRepository)
        {
            foreach (var tooltip in _tooltips)
            {
                tooltipDataRepository.InteractiveObjects.FirstOrDefault(interactiveObject => 
                    interactiveObject.Type == tooltip.Type).IsInteractObject.Subscribe(isShowButton =>
                {
                    ToggleTooltips(tooltip.TooltipView, isShowButton);
                }).AddTo(this);
            }
        }

        private void ToggleTooltips(GameObject tooltipView, bool state)
        {
            tooltipView.SetActive(state);
        }
    }
}
