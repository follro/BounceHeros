using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace BounceHeros
{
    public class LevelTextUI : MonoBehaviour, ILevelObserver
    {
        [SerializeField] private TextMeshProUGUI waveTextUI;
/*        // DOTween 애니메이션에 사용할 변수 추가
        [SerializeField] private float scaleUpDuration = 0.3f; // 폰트 커지는 시간
        [SerializeField] private float scaleDownDuration = 0.5f; // 원래대로 돌아오는 시간
        [SerializeField] private float scaleFactor = 1.5f; // 커지는 폰트 배율 (예: 1.5배)
        [SerializeField] private Ease easeType = Ease.OutBack; // 애니메이션 이징 타입*/

        private RectTransform parentRect;
        private RectTransform childRect;

        private Vector2 initialPosition;
        private Vector2 initialSize;
        private float initialFontSize; // 초기 폰트 크기 저장
        private string initWaveText;

        public void OnLevelChanged(int newLevel)
        {
            waveTextUI.text = initWaveText + newLevel.ToString();
        }

        private void Start()
        {
            parentRect = GetComponent<RectTransform>();
            childRect = waveTextUI.GetComponent<RectTransform>();

            initialPosition = childRect.anchoredPosition; // RectTransform의 로컬 위치를 사용 (position 대신)
            initialSize = childRect.sizeDelta;
            initialFontSize = waveTextUI.fontSize; // 초기 폰트 크기 저장
            initWaveText = waveTextUI.text;

        }


       /* // ILevelObserver 인터페이스 구현
        public async void OnLevelChanged(int newLevel)
        {
            // 1. 초기 설정
            waveTextUI.text = initWaveText + newLevel.ToString(); // 텍스트 업데이트
            waveTextUI.fontSize = initialFontSize; // 혹시 모를 대비로 폰트 크기 초기화

            // 중앙으로 즉시 이동 (부모의 중앙을 기준으로)
            // DOTween 애니메이션을 위해 childRect.anchoredPosition을 사용합니다.
            childRect.anchoredPosition = Vector2.zero;
            childRect.sizeDelta = parentRect.sizeDelta; // 텍스트 영역을 부모와 같게 설정하여 중앙에 위치시키기 용이하게 함

            // 2. 폰트 크기를 키우는 애니메이션 (웨이브 업데이트 시각화)
            // 현재 폰트 크기의 scaleFactor 배로 커지게 설정합니다.
            float targetFontSize = initialFontSize * scaleFactor;

            // DOTween의 DoAsync() 확장 메서드를 사용하여 await 가능하게 합니다.
            await waveTextUI.DOFontSize(targetFontSize, scaleUpDuration)
                .SetEase(easeType)
                .AsyncWaitForCompletion();


            // 3. 원래 위치와 크기로 돌아가는 애니메이션
            // 위치 애니메이션 (childRect의 위치를 원래 위치로, 부드러운 이동을 위해 RectTransform.DOAnchorPos 사용)
            var posTween = childRect.DOAnchorPos(initialPosition, scaleDownDuration)
                .SetEase(easeType);

            // 크기 애니메이션 (childRect의 크기를 원래 크기로)
            var sizeTween = childRect.DOSizeDelta(initialSize, scaleDownDuration)
                .SetEase(easeType);

            // 폰트 크기를 원래 크기로 되돌리는 애니메이션
            var fontSizeTween = waveTextUI.DOFontSize(initialFontSize, scaleDownDuration)
                .SetEase(easeType);

            // 동시에 애니메이션 실행을 기다립니다. (Sequence를 사용해도 되지만, Task.WhenAll로도 가능)
            await Task.WhenAll(posTween.AsyncWaitForCompletion(),
                               sizeTween.AsyncWaitForCompletion(),
                               fontSizeTween.AsyncWaitForCompletion());

            // 애니메이션이 완료된 후, 텍스트의 크기 영역(sizeDelta)을 다시 원래대로 돌릴 필요가 있을 수 있습니다.
            // 하지만 DOSizeDelta가 initialSize로 돌려주므로 추가적인 설정은 필요 없을 수 있습니다.
        }*/
    }
}