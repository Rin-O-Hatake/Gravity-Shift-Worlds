using UnityEngine;

namespace Core.Scripts.LevelController
{
    public class LevelDataView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private PolygonCollider2D _levelPolygonCollider2D;
        [SerializeField] private Transform _StartPositionPlayer;
        [SerializeField] private Transform _exitLevelPortal;
        [SerializeField] private int _levelNumber;

        #region Properties

        public int LevelNumber => _levelNumber;
        public Transform StartPositionPlayer => _StartPositionPlayer;
        public Transform ExitLevelPortal => _exitLevelPortal;
        public PolygonCollider2D LevelPolygonCollider2D => _levelPolygonCollider2D;

        #endregion

        #endregion
    }
}
