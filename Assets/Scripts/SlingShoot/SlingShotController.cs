using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BounceHeros
{
    public class SlingShotController : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float maxLength;
        
        [Header("Slingshot Components")]
        [SerializeField] private DragInputHandler inputHandler;
        [SerializeField] private SlingShotVisualizer visualizer;
        [SerializeField] private HeroCatcher heroCatcher;


        private bool isAiming;
        private float finalLaunchPower;
        private Vector2 finalLaunchDirection;

        private void Awake()
        {
            if (inputHandler == null || visualizer == null)
            {
                Debug.LogError("InputHandler 또는 Visualizer가 연결되지 않았습니다.");
                return;
            }
            if (mainCamera == null) mainCamera = Camera.main;

            inputHandler.OnDragStarted += HandleDragStart;
            inputHandler.OnDragging += HandleDragging;
            inputHandler.OnDragEnded += HandleDragEnd;
            isAiming = false;
        }

        private void OnDestroy()
        {
            if (inputHandler != null)
            {
                inputHandler.OnDragging -= HandleDragging;
                inputHandler.OnDragEnded -= HandleDragEnd;
            }
        }

        private void HandleDragStart()
        {
            if (heroCatcher.Hero == null) return;

            isAiming = true;
        }

        private void HandleDragging(Vector2 startScreenPos, Vector2 currentScreenPos)
        {
            if (heroCatcher.Hero == null || !isAiming) return;

            heroCatcher.Catch();

            Vector3 heroWorldPos = heroCatcher.Hero.transform.position;
            Vector2 heroScreenPos = mainCamera.WorldToScreenPoint(heroWorldPos);
            Vector2 dragVector = currentScreenPos - startScreenPos;
            Vector2 aimEndPointScreenPos = heroScreenPos - dragVector;

            float cameraZ = mainCamera.transform.position.z;
            Vector3 lineStartWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(heroScreenPos.x, heroScreenPos.y, -cameraZ));
            Vector3 lineEndWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(aimEndPointScreenPos.x, aimEndPointScreenPos.y, -cameraZ));


            Vector2 worldDirection = (lineEndWorldPos - lineStartWorldPos).normalized;
            float worldLength = Mathf.Min(Vector2.Distance(lineStartWorldPos, lineEndWorldPos), maxLength);

            visualizer.UpdateAimLine(lineStartWorldPos, worldDirection, worldLength);
            visualizer.ShowAimLine();

            finalLaunchDirection = worldDirection;
            finalLaunchPower = worldLength;
        }

        private void HandleDragEnd()
        {
            if (heroCatcher.Hero == null || !isAiming) return;

            isAiming = false;
            visualizer.HideAimLine();
            heroCatcher.Launch(finalLaunchDirection, finalLaunchPower);
           
        }

    }
}