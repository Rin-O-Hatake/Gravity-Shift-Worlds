using UnityEngine;

namespace Core.Scripts.Camera
{
    public interface ISetupCamera
    {
        public void SetupLimitationsMove(PolygonCollider2D collider);
    }
}
