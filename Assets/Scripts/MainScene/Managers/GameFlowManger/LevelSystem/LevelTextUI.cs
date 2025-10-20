using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace BounceHeros
{
    public class LevelTextUI : MonoBehaviour, ILevelObserver
    {

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


        public void OnLevelChanged(int newLevel)
        {
            waveText.text = initWaveText + newLevel.ToString();
        }


    }
}