using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Scripts.UI.DiamondCounter
{
    public class DiamondCountView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TMP_Text _diamondCountText;
        
        private CompositeDisposable _disposable = new CompositeDisposable();

        #endregion
        
        [Inject]
        public void Construct(IDiamondCounter diamondCounter)
        {
            diamondCounter.DiamondCount.Subscribe(UpdateTextCountDiamond).AddTo(_disposable);
        }

        private void UpdateTextCountDiamond(int diamondCount)
        {
            _diamondCountText.text = $"{diamondCount}x";
        }

        #region MonoBehaviour

        private void OnDestroy()
        {
            _disposable.Clear();
        }

        #endregion
    }
}
