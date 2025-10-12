using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    [System.Serializable]
    public struct PatternUnit
    {
        public EnemyType enemyType;
        public Vector2 relativePosition;
    }

    [CreateAssetMenu(fileName = "New SpawnPatternSO Data", menuName = "ScriptableObjects/SpawnPatternSO Data")]
    public class SpawnPatternSO : ScriptableObject
    {
        [SerializeField] private List<PatternUnit> patternUnits;
    }
}