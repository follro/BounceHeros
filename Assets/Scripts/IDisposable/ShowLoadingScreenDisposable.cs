using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLoadingScreenDisposable : IDisposable
{
    private readonly LoadingScreen loadingScreen;

    public ShowLoadingScreenDisposable(LoadingScreen loadingScreen)
    {
        this.loadingScreen = loadingScreen;
        loadingScreen.Show();
    }

    public void SetLoadingBarPercent(float percent)
    {
        loadingScreen.CurrentBarPercent = percent;
    }

    public void Dispose()
    {
        loadingScreen.Hide();
    }
}
