using Core.Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Scripts.LevelController
{
    public class NextLevelPortal : MonoBehaviour, ILevelTransitionContact
    {
        #region Fields
        
        [SerializeField] private LevelTransitionContactData levelTransitionContact;

        private ILoadingNextLevel _loadingNextLevel;
        
        #region Properties

        public LevelTransitionContactData IsTransitionContact => levelTransitionContact;

        #endregion

        #endregion

        #region Inject

        [Inject]
        private void Construct(ILoadingNextLevel loadingNextLevel)
        {
            _loadingNextLevel = loadingNextLevel;
        }

        #endregion
        
        public void SetNewPosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        public void InteractionPortal(InputAction.CallbackContext context)
        {
            if (!IsTransitionContact.IsInteractObject.Value)
            {
                return;
            }
            
            _loadingNextLevel.LoadNextLevel();
        }

        #region MonoBehaviour

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                levelTransitionContact.IsInteractObject.Value = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                levelTransitionContact.IsInteractObject.Value = false;
            }
        }

        #endregion
    }
}
