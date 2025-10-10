using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace BounceHeros
{
    public class GameDataContext 
    {
        public Camera MainCamera { get; set; }

        //Level
        public LevelTextUI WaveUI { get; set; }

        // Map
        public GameObject Map { get; set; }

        // Gameplay
        public GameObject Hero { get; set; } // ½ºÆ÷³Ê·Î ¹Ù²ã¾ßµÊ
        public List<GameObject> Enemies { get; set; } = new();

    }
}