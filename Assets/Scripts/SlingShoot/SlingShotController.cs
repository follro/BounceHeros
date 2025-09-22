using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class SlingShotController : MonoBehaviour
    {
        [SerializeField] private DragInputHandler inputHandler;
        [SerializeField] private SlingShotVisualizer visualizer;
        [SerializeField] private HeroCatcher heroCatcher;

        private void Awake()
        {
            if (inputHandler == null || visualizer == null)
            {
                Debug.LogError("InputHandler 또는 Visualizer가 연결되지 않았습니다.");
                return;
            }

            inputHandler.OnDragging += HandleDragging;
            inputHandler.OnDragEnded += HandleDragEnd;
        }

        private void OnDestroy()
        {
            if (inputHandler != null)
            {
                inputHandler.OnDragging -= HandleDragging;
                inputHandler.OnDragEnded -= HandleDragEnd;
            }
        }

        private void HandleDragging(Vector2 startPos, Vector2 currentPos)
        {
            if (heroCatcher == null) return;


            Vector3 heroWorldPos = heroCatcher.Hero.transform.position;
            Vector2 heroScreenPos = Camera.main.WorldToScreenPoint(heroWorldPos);

            Vector2 dragVector = currentPos - startPos;
            Vector2 aimEndPoint = heroScreenPos - dragVector;

            visualizer.ShowAimLine();
            visualizer.UpdateAimLine(heroScreenPos, aimEndPoint);

        }

        private void HandleDragEnd(Vector2 startPos, Vector2 endPos)
        {
            if (heroCatcher == null) return;
            
            visualizer.HideAimLine();
        }

    }
}