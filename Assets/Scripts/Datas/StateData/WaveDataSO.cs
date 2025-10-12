using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public enum EnemyType
    {
        None,
        Slime,
        Bat,

        //Boss

        End
    }

    [System.Serializable]
    public class SpawnEvent
    {
        public float timestamp;
        public SpawnPatternSO pattern;
    }

    [CreateAssetMenu(fileName = "New Wave Data", menuName = "ScriptableObjects/Wave Data")]
    public class WaveDataSO : ScriptableObject
    {
        [SerializeField] private List<SpawnEvent> spawnEvents;
        [Header("EnemyStats")]
        [SerializeField] private float enemyStatMultiplier = 1f;

        [Header("Random Spawn Datas")]
        [SerializeField] private float minFillerSpawnInterval = 0.5f;
        [SerializeField] private float maxFillerSpawnInterval = 1.5f;

        public float EnemyStatMultiplier => enemyStatMultiplier;  
        
    }
}