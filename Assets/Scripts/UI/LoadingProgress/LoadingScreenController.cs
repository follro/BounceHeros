using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BounceHeros
{
    public class LoadingScreenController : MonoBehaviour
    {

        [Header("UI Elements")]
        [SerializeField] private float animationDuration = 0.5f;

        [Header("Text Wave Animation")]
        [SerializeField] private float waveHeight = -10f;       // 튀어 오르는 높이 (음수 = 위로)
        [SerializeField] private float waveCharDuration = 0.4f;  // 글자 하나가 튀었다 내려오는 시간
        [SerializeField] private float waveCharDelay = 0.08f;    // 다음 글자 애니메이션까지의 딜레이
        [SerializeField] private float waveLoopDelay = 0.5f;     // 전체 웨이브 반복 딜레이

        private VisualElement ui;
        private ProgressBar progressBar;
        private Label loadingText;
        private List<Label> loadingLabels; // 분리된 글자 Label들을 담을 리스트
        private Sequence waveSequence;       // 텍스트 웨이브 애니메이션 시퀀스

        private void Awake()
        {
            ui = GetComponent<UIDocument>().rootVisualElement;
            progressBar = ui.Q<ProgressBar>("LoadingPrgressbar");
            loadingLabels = ui.Query<Label>(className: "loading-char").ToList();
        }

        public float CurrentBarPercent
        {
            get => progressBar.value;
            set
            {
                DOTween.Kill(progressBar);

                DOTween.To(() => progressBar.value, x => progressBar.value = x, value, animationDuration)
                    .OnUpdate(() => {
                        progressBar.title = Mathf.RoundToInt(progressBar.value * 100f).ToString() + "%";
                    })
                    .SetId(progressBar); 
            }
        }

        public void Show()
        {
            ui.style.display = DisplayStyle.Flex;
            StartWaveAnimation();
        }

        public void Hide()
        {
            ui.style.display = DisplayStyle.None;
            StopWaveAnimation();
        }

        private void StartWaveAnimation()
        {
            StopWaveAnimation(); 

            waveSequence = DOTween.Sequence();

            for (int i = 0; i < loadingLabels.Count; i++)
            {
                Label characterLabel = loadingLabels[i];

                Sequence charSequence = DOTween.Sequence()
                    .Append(DOTween.To(
                        () => characterLabel.style.translate.value.y.value,
                        y => characterLabel.style.translate = new Translate(Length.Percent(0), new Length(y, LengthUnit.Pixel)),
                        waveHeight,
                        waveCharDuration / 2
                    ).SetEase(Ease.OutQuad))
                    .Append(DOTween.To(
                        () => characterLabel.style.translate.value.y.value,
                        y => characterLabel.style.translate = new Translate(Length.Percent(0), new Length(y, LengthUnit.Pixel)),
                        0, 
                        waveCharDuration / 2
                    ).SetEase(Ease.InQuad));

                
                waveSequence.Insert(i * waveCharDelay, charSequence);
            }

            
            waveSequence.AppendInterval(waveLoopDelay).SetLoops(-1).SetId("WaveSequence");
        }

        private void StopWaveAnimation()
        {
           
            DOTween.Kill("WaveSequence", true);

            
            foreach (var label in loadingLabels)
            {
                label.style.translate = new Translate(Length.Percent(0), Length.Percent(0));
            }
        }
    }
}
