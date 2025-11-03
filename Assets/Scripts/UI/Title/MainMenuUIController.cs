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

        private void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();
            var root = uiDocument.rootVisualElement;

            playButton = root.Q<Button>("PlayButton");

            playButton.clicked += LoadMainScene;
        }

        private void OnDisable()
        {
            if (playButton != null)
            {
                playButton.clicked -= LoadMainScene;
            }
        }

        private void LoadMainScene()
        {
            SceneManager.LoadSceneAsync("MainScene");
        }
    }
}
