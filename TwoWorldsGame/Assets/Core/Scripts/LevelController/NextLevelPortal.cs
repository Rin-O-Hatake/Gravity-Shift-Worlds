using Core.Scripts.UI;
using UnityEngine;

namespace Core.Scripts.LevelController
{
    public class NextLevelPortal : MonoBehaviour, ILevelTransitionContact
    {
        #region Fields
        
        [SerializeField] private ShowTooltipInfoData _showTooltipInfo;
        
        #region Properties

        public ShowTooltipInfoData IsTransitionContact => _showTooltipInfo;

        #endregion

        #endregion
        
        public void SetNewPosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _showTooltipInfo.IsShowTooltip.Value = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _showTooltipInfo.IsShowTooltip.Value = false;
            }
        }
    }
}
