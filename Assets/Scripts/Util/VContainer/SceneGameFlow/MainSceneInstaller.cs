using System.Collections;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using System.Collections.Generic;

namespace BounceHeros
{
    public class MainSceneInstaller : LifetimeScope
    {
        [SerializeField] GameInitiator gameInitiatorPrefab;
        //매니저들 여기에 구현
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(gameInitiatorPrefab, Lifetime.Scoped).AsSelf();
         
            //builder.Register<LevelSystem>(Lifetime.Singleton);

        }

        protected override void Awake()
        {
            base.Awake();

            Container.Resolve<GameInitiator>();
        }
    }
}
