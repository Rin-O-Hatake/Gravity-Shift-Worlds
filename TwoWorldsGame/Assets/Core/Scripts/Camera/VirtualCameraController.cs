using Cinemachine;
using UnityEngine;

namespace Core.Scripts.Camera
{
    public class VirtualCameraController : MonoBehaviour, ISetupCamera
    {
        #region Fields

        [SerializeField] private CinemachineConfiner _cinemachineConfiner;

        #endregion
        
        public void SetupLimitationsMove(PolygonCollider2D collider)
        {
            _cinemachineConfiner.m_BoundingShape2D = collider;
        }
    }
}
