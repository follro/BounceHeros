using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace BounceHeros
{
    public class GameContext 
    {
        public Camera MainCamera { get; set; }
        
        //Level, life, score
        public InfoBarController infoBarController { get; set; }

        // Map
        public GameObject Map { get; set; }

        // Gameplay
        public GameObject Hero { get; set; } // ½ºÆ÷³Ê·Î ¹Ù²ã¾ßµÊ
        public List<GameObject> Enemies { get; set; } = new();

        //System
        public PauseSystem PauseController { get; set; }
        public LevelSystem LevelController { get; set; }

    }
}