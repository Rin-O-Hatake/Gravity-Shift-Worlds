using UnityEngine;

namespace Core.Scripts.Items
{
    public abstract class BaseItem : MonoBehaviour
    {
        #region Field

        [SerializeField] private ItemType _itemType;
        [SerializeField] private GameObject _visualGameObject;
        [SerializeField] private Animator _animatorPickupEffect;
        
        private const string PICKUP_EFFECT_NAME = "PickupEffect"; 

        #region Properties

        public ItemType ItemType => _itemType;

        #endregion

        #endregion

        public void DestroyItem()
        {
            Destroy(gameObject);
        }

        public void HideVisual()
        {
            _visualGameObject.SetActive(false);
        }

        public void ShowPickupEffect()
        {
            HideVisual();
            
            _animatorPickupEffect.CrossFade(PICKUP_EFFECT_NAME, 0.1f);
        }
    }
}
