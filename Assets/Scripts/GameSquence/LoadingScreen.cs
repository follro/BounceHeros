using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float animationDuration;
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
