using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Legacy;
using UnityEngine;
using UnityEngine.UIElements;

namespace BounceHeros
{
    public class SettingUIController : MonoBehaviour
    {
        private SliderHandler[] soundSliderHanders;
        
        private void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();
            var root = uiDocument.rootVisualElement;

            soundSliderHanders = new SliderHandler[(int)SoundManager.AudioType.End];
            soundSliderHanders[(int)SoundManager.AudioType.Master]  = SetSliderHander(root, SoundManager.AudioType.Master.ToString());
            soundSliderHanders[(int)SoundManager.AudioType.BGM]     = SetSliderHander(root, SoundManager.AudioType.BGM.ToString());
            soundSliderHanders[(int)SoundManager.AudioType.SFX]     = SetSliderHander(root, SoundManager.AudioType.SFX.ToString());

        }

        private SliderHandler SetSliderHander(VisualElement root, string containerName)
        {
            var slider = root.Q<VisualElement>(containerName).Q<Slider>("SettingsSlider");
            var dragger = slider.Q<VisualElement>("unity-dragger");
            
            return new SliderHandler(slider, dragger);
        }

        
    }
}