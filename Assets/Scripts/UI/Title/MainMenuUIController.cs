using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace BounceHeros
{
    public class MainMenuUI : MonoBehaviour
    {
        Button playButton;
        Button settingButton;
        public GameObject settingsUI;


        private void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();
            var root = uiDocument.rootVisualElement;

            playButton = root.Q<Button>("PlayButton");
            playButton.clicked += LoadMainScene;
            settingButton = root.Q<Button>("SettingButton");
            settingButton.clicked += SettingButtonEvent;

        }

        private void OnDisable()
        {
            if (playButton != null)
            {
                playButton.clicked -= LoadMainScene;
            }

            if (settingButton != null)
            { settingButton.clicked -= SettingButtonEvent; }
        }

        private void LoadMainScene()
        {
            SceneManager.LoadSceneAsync("MainScene");
        }

        private void SettingButtonEvent()
        {
            if(settingsUI.activeSelf)
                settingsUI.SetActive(false);
            else settingsUI.SetActive(true);
        }
    }
}
