using Core.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Core.Scripts.Items
{
    public class ItemPickup : MonoBehaviour
    {
        #region Fields

        private IDiamondCounter _diamondCounter;

        #endregion
        
        [Inject]
        private void Construct(IDiamondCounter diamondCounter)
        {
            _diamondCounter = diamondCounter;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out BaseItem baseItem))
            {
                return;
            }

            if (baseItem.ItemType == ItemType.Diamond)
            {
                _diamondCounter.AddDiamonds();
                
                baseItem.ShowPickupEffect();
            }
        }
    }
}
