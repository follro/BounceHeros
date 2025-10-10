using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BounceHeros
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "ScriptableObjects/Enemy Data")]
    public class EnemyDataSO : CharacterDataSO
    {
        public int score;

    }
}
