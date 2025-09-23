using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BounceHeros
{
    public class SlingShotController : MonoBehaviour
    {
        [SerializeField] private DragInputHandler inputHandler;
        [SerializeField] private SlingShotVisualizer visualizer;
        [SerializeField] private HeroCatcher heroCatcher;

        [SerializeField] private float launchPowerMultiplier;
        [SerializeField] private Camera mainCamera;

        private bool isAiming;
        private Vector2 finalLaunchDirection;
        private float finalLaunchPower;

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

            visualizer.ShowAimLine();

            Vector2 worldDirection = (lineEndWorldPos - lineStartWorldPos).normalized;
            float worldLength = Vector2.Distance(lineStartWorldPos, lineEndWorldPos);
            visualizer.UpdateAimLine(lineStartWorldPos, worldDirection, worldLength);

            finalLaunchDirection = worldDirection;
            finalLaunchPower = worldLength * launchPowerMultiplier;
            //Debug.DrawRay(lineStartWorldPos, worldDirection * 5f, Color.green);
        }

        private void HandleDragEnd()
        {
            if (heroCatcher.Hero == null || !isAiming) return;

            isAiming = false;
            visualizer.HideAimLine();
            Debug.DrawRay(heroCatcher.Hero.transform.position, finalLaunchDirection * 5f, Color.red, 5f);
            heroCatcher.Launch(finalLaunchDirection, finalLaunchPower);
           
        }

    }
}