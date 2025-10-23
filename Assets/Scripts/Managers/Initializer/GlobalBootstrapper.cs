using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class GlobalBootstrapper : Bootstrapper
    {
        public override async UniTask BindingObjects()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3));
        }

        public override async UniTask Initialize()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3));
        }

        public override async UniTask InjectDependencies()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3));
        }

        private async void Start()
        {
            await BindingObjects();

            using (var loadingScreenDisposable = new ShowLoadingScreenDisposable(loadingScreenController))
            {
                loadingScreenDisposable.SetLoadingBarPercent(0);
                await Initialize();
                loadingScreenDisposable.SetLoadingBarPercent(0.33f);
                await InjectDependencies();
                loadingScreenDisposable.SetLoadingBarPercent(0.66f);
                loadingScreenDisposable.SetLoadingBarPercent(1f);
                await UniTask.Delay(TimeSpan.FromSeconds(3));
            }

        }
    }
}