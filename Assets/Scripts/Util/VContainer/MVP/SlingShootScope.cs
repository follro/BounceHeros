using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BounceHeros
{
    public class SlingShootScope : LifetimeScope
    {
        [SerializeField] DragInputHandler dragInputHandlerPrefab;
        [SerializeField] HeroCatcher heroCatcherPrefab;
        [SerializeField] SlingShotVisualizer slingShotVisualizerPrefab;
        protected override void Configure(IContainerBuilder builder)
        {
           /* builder.RegisterComponentInNewPrefab(dragInputHandlerPrefab, Lifetime.Scoped).AsSelf();

            builder.RegisterComponentInNewPrefab(heroCatcherPrefab, Lifetime.Scoped).AsSelf();

            builder.RegisterComponentInNewPrefab(slingShotVisualizerPrefab, Lifetime.Scoped).AsSelf();*/
            builder.RegisterEntryPoint<SlingShotController>(Lifetime.Scoped).AsSelf();
            builder.RegisterComponent(GetComponent<SlingShotVisualizer>());
            builder.RegisterComponent(GetComponent<DragInputHandler>());
            builder.RegisterComponent(GetComponent<HeroCatcher>());

        }

        protected override void Awake()
        {
            base.Awake();

            Container.Resolve<SlingShotController>();
        }
    }
}
