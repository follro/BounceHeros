using System.Collections;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using System.Collections.Generic;

namespace BounceHeros
{
    public class MainSceneInstaller : LifetimeScope
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] DragInputHandler dragInputHandlerPrefab;
        [SerializeField] HeroCatcher heroCatcherPrefab;
        [SerializeField] SlingShotVisualizer slingShotVisualizerPrefab;
        [SerializeField] GameInitiator gameInitiatorPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(mainCamera, Lifetime.Singleton).AsSelf();


            builder.RegisterComponentInNewPrefab(dragInputHandlerPrefab, Lifetime.Scoped)
                .AsSelf();

            builder.RegisterComponentInNewPrefab(heroCatcherPrefab, Lifetime.Scoped)
                .AsSelf();

            builder.RegisterComponentInNewPrefab(slingShotVisualizerPrefab, Lifetime.Scoped)
                .AsSelf();

            builder.RegisterEntryPoint<SlingShotController>(Lifetime.Scoped)
                .AsSelf();

            builder.RegisterComponentInNewPrefab(gameInitiatorPrefab, Lifetime.Scoped)
                .AsSelf();
        }

        protected override void Awake()
        {
            base.Awake();

            Container.Resolve<GameInitiator>();
            Container.Resolve<SlingShotController>();   
        }
    }
}
