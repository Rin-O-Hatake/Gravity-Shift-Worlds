using UnityEngine;

namespace Core.Scripts.Player
{
    public class PlayerSetup : MonoBehaviour, IPlayerSetup
    {
        public void SetupPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
