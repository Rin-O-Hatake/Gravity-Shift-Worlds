using UnityEngine;

namespace Core.Scripts.Items
{
    public abstract class BaseItem : MonoBehaviour
    {
        #region Field

        [SerializeField] private ItemType _itemType;

        #endregion

        public void DestroyItem()
        {
            Destroy(gameObject);
        }
    }
}
