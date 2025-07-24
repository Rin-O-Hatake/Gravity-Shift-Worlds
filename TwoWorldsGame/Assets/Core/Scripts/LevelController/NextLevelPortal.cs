using UniRx;
using UnityEngine;

namespace Core.Scripts.LevelController
{
    public class NextLevelPortal : MonoBehaviour, ILevelTransitionContact
    {
        public ReactiveProperty<bool> IsTransitionContact { get; } = new ReactiveProperty<bool>(false);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                IsTransitionContact.Value = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                IsTransitionContact.Value = false;
            }
        }
    }
}
