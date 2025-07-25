using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.UI.Loading
{
    public abstract class BaseFadePanel : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _currentImage;
        [SerializeField, Range(0,5)] private float _durationFade;
        [SerializeField] private GameObject _rootPanel;

        #endregion

        public async UniTaskVoid FadeInLoadingPanel(Action onFinished = default, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Debug.Log("FadeInAsync cancelled before start.");
                return;
            }
            
            Color color = _currentImage.color;
            color.a = 0f;
            _currentImage.color = color;
            
            _rootPanel.SetActive(true);

            Tween fadeTween = _currentImage.DOFade(1f, _durationFade).SetEase(Ease.Linear);

            var taskCompletionSource = TaskCompletionSource(fadeTween, cancellationToken, onFinished);
            
            try
            {
                await taskCompletionSource.Task;
            }
            catch(TaskCanceledException)
            {
                Debug.Log("FadeOutAsync cancelled.");
            }
            
        }
        
        public async UniTaskVoid FadeOutLoadingPanel(Action onFinished = default, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Debug.Log("FadeOutAsync cancelled before start.");
                return;
            }
            
            Color color = _currentImage.color;
            color.a = 1f;
            _currentImage.color = color;

            Tween fadeTween = _currentImage.DOFade(0f, _durationFade).SetEase(Ease.Linear);
            
            var taskCompletionSource = TaskCompletionSource(fadeTween, cancellationToken, onFinished);
            
            try
            {
                _rootPanel.SetActive(false);
                await taskCompletionSource.Task;
            }
            catch(TaskCanceledException)
            {
                Debug.Log("FadeInAsync cancelled.");
            }
        }

        private TaskCompletionSource<bool> TaskCompletionSource(Tween fadeTween, CancellationToken cancellationToken, Action onFinished)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            fadeTween.OnComplete(() =>
            {
                onFinished?.Invoke();
                taskCompletionSource.SetResult(true);
            });

            cancellationToken.Register(() =>
            {
                if (!taskCompletionSource.Task.IsCompleted)
                {
                    fadeTween.Kill();
                    taskCompletionSource.SetCanceled();
                }
            });

            return taskCompletionSource;
        }
    }
}
