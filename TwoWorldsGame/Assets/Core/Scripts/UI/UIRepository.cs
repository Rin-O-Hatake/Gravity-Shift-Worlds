using System;
using UniRx;
using UnityEngine;

namespace Core.Scripts.UI
{
    public enum TooltipType
    {
        Door
    }

    [Serializable]
    public class ShowTooltipInfoData
    {
        #region Fields

        [SerializeField] private TooltipType _type;
        [SerializeField] private ReactiveProperty<bool> _isShowTooltip;

        #region Properties

        public TooltipType Type => _type;
        public ReactiveProperty<bool> IsShowTooltip => _isShowTooltip;

        #endregion
        
        #endregion
    }
    
    [Serializable]
    public class ViewPanelTooltip
    {
        #region Fields

        [SerializeField] private TooltipType _type;
        [SerializeField] private GameObject _tooltipView;

        #region Properties

        public TooltipType Type => _type;
        public GameObject TooltipView => _tooltipView;

        #endregion
        
        #endregion
    }
}
