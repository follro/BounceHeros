using BounceHeros;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLoadingScreenDisposable : IDisposable
{
    private readonly LoadingScreenController loadingScreenController;

    public ShowLoadingScreenDisposable(LoadingScreenController loadingScreenController)
    {
        this.loadingScreenController = loadingScreenController;
        loadingScreenController.Show();
    }

    public void SetLoadingBarPercent(float percent)
    {
        loadingScreenController.CurrentBarPercent = percent;
    }

    public void Dispose()
    {
        loadingScreenController.Hide();
    }
}
