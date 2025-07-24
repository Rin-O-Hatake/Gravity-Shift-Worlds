using System;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Scripts.UI
{
    public enum InteractionType
    {
        Door
    }

    [Serializable]
    public class LevelTransitionContactData
    {
        #region Fields

        [SerializeField] private InteractionType _type;
        [SerializeField] private ReactiveProperty<bool> _isInteractObject;

        #region Properties

        public InteractionType Type => _type;
        public ReactiveProperty<bool> IsInteractObject => _isInteractObject;

        #endregion
        
        #endregion
    }
    
    [Serializable]
    public class ViewPanelTooltip
    {
        #region Fields

        [SerializeField] private InteractionType _type;
        [SerializeField] private GameObject _tooltipView;

        #region Properties

        public InteractionType Type => _type;
        public GameObject TooltipView => _tooltipView;

        #endregion
        
        #endregion
    }
}
