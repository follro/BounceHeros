using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace BounceHeros
{
    public class LevelTextUI : MonoBehaviour, ILevelObserver
    {
        ILevelNotifier ILevelObserver.Notifier { get ; set ; }

        [SerializeField] private TextMeshProUGUI waveText;
      

        private RectTransform parentRect;
        private RectTransform childRect;

        private Vector2 initialPosition;
        private Vector2 initialSize;
        private float initialFontSize; // 초기 폰트 크기 저장
        private string initWaveText;

        private void Start()
        {
            parentRect = GetComponent<RectTransform>();
            childRect = waveText.GetComponent<RectTransform>();

            initialPosition = childRect.anchoredPosition; // RectTransform의 로컬 위치를 사용 (position 대신)
            initialSize = childRect.sizeDelta;
            initialFontSize = waveText.fontSize; // 초기 폰트 크기 저장
            initWaveText = waveText.text;
        }

        private void OnDestroy()
        {
            ((IDisposable)this).Dispose();
        }

        void ILevelObserver.OnLevelChanged(int newLevel)
        {
            waveText.text = initWaveText + newLevel.ToString();
        }

        void IDisposable.Dispose()
        {
            ILevelNotifier notifier = ((ILevelObserver)this).Notifier;

            if (notifier != null)
                notifier.Unsubscribe(this);

            ((ILevelObserver)this).Notifier = null;
        }
    }
}