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

        [SerializeField] private float launchPowerMultiplier = 0.1f;
        [SerializeField] private Camera mainCamera;
        private void Awake()
        {
            if (inputHandler == null || visualizer == null)
            {
                Debug.LogError("InputHandler 또는 Visualizer가 연결되지 않았습니다.");
                return;
            }
            if (mainCamera == null) mainCamera = Camera.main;

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


        private void HandleDragging(Vector2 startScreenPos, Vector2 currentScreenPos)
        {
            if (heroCatcher.Hero == null) return;

            heroCatcher.Catch();

            Vector3 heroWorldPos = heroCatcher.Hero.transform.position;
            Vector2 heroScreenPos = mainCamera.WorldToScreenPoint(heroWorldPos);
            Vector2 dragVector = currentScreenPos - startScreenPos;
            Vector2 aimEndPointScreenPos = heroScreenPos - dragVector;

            float cameraZ = mainCamera.transform.position.z;
            Vector3 lineStartWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(heroScreenPos.x, heroScreenPos.y, -cameraZ));
            Vector3 lineEndWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(aimEndPointScreenPos.x, aimEndPointScreenPos.y, -cameraZ));

            visualizer.ShowAimLine();

            Vector2 worldDirection = (lineEndWorldPos - lineStartWorldPos).normalized;
            float worldLength = Vector2.Distance(lineStartWorldPos, lineEndWorldPos);
            visualizer.UpdateAimLine(lineStartWorldPos, worldDirection, worldLength);
        }

        //private void HandleDragEnd(Vector2 startPos, Vector2 endPos)
        private void HandleDragEnd(Vector2 startScreenPos, Vector2 endScreenPos)
        {
            if (heroCatcher.Hero == null) return;

            visualizer.HideAimLine();

            Vector3 heroWorldPos = heroCatcher.Hero.transform.position;
            Vector2 heroScreenPos = mainCamera.WorldToScreenPoint(heroWorldPos);
            Vector2 aimVectorScreen = heroScreenPos - endScreenPos; // 조준 방향 (스크린)

            Vector3 launchStartWorldPos = heroWorldPos;
            Vector3 launchEndWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(endScreenPos.x, endScreenPos.y, -mainCamera.transform.position.z));

            Vector2 launchVectorWorld = launchStartWorldPos - launchEndWorldPos;
            
            Vector2 launchDirection = launchVectorWorld.normalized;
            float launchDistance = launchVectorWorld.magnitude;

            heroCatcher.Shoot(launchDirection, launchDistance * launchPowerMultiplier);
        }

    }
}