using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider slider;
    [SerializeField] private float animationDuration;
    [SerializeField] private UIDocument loadingUI;
    public float CurrentBarPercent 
    {
        get => slider.value;
        set
        {
            slider.DOKill(true);
            slider.DOValue(value, animationDuration);
        }
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    
}
