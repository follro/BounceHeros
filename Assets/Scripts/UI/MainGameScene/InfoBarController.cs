using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BounceHeros
{
    public class InfoBarController : MonoBehaviour , ILevelObserver
    {
        private Label lifeLabel;
        private Label waveLabel;
        private Label scoreLabel;

        public void OnLifeChanged(int life)
        {
            lifeLabel.text = life.ToString();    
        }

        public void OnScoreChanged(int newScore)
        {
            scoreLabel.text = newScore.ToString();
        }

        public void OnLevelChanged(int newLevel)
        {
            waveLabel.text = newLevel.ToString();
        }

        private void Awake()
        {
            var uiDocument = GetComponent<UIDocument>();
            var root = uiDocument.rootVisualElement;

            lifeLabel = root.Q<Label>("LifeValue");
            waveLabel = root.Q<Label>("WaveValue");
            scoreLabel = root.Q<Label>("ScoreValue");

            if (lifeLabel == null) Debug.Log("널이네");
            if (waveLabel == null) Debug.Log("널이네");
            if (scoreLabel == null) Debug.Log("널이네");

        }


    }
}