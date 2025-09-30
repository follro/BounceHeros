using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BounceHeros
{
    public class SlingShotController : IStartable ,IDisposable //,ITickable
    {
        private Camera mainCamera;
        
        private readonly DragInputHandler inputHandler;
        private readonly SlingShotVisualizer visualizer;
        private readonly HeroCatcher heroCatcher;


        private bool isAiming;
        private float finalLaunchPower;
        private Vector2 finalLaunchDirection;

        [Inject]
        public SlingShotController(DragInputHandler dragInputHandler, SlingShotVisualizer slingShotVisualizer, HeroCatcher heroCatcher, Camera camera)
        {
            this.inputHandler = dragInputHandler;
            this.visualizer = slingShotVisualizer;
            this.heroCatcher = heroCatcher;
            this.mainCamera = camera;
            isAiming = false;
        }

        public void Start()
        {
            inputHandler.OnDragStarted += HandleDragStart;
            inputHandler.OnDragging += HandleDragging;
            inputHandler.OnDragEnded += HandleDragEnd;
        }

        public void Dispose()
        {
            inputHandler.OnDragStarted -= HandleDragStart;
            inputHandler.OnDragging -= HandleDragging;
            inputHandler.OnDragEnded -= HandleDragEnd;
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
            float worldLength = Mathf.Min(Vector2.Distance(lineStartWorldPos, lineEndWorldPos));

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