using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    [CreateAssetMenu(fileName = "New Hero Data", menuName = "ScriptableObjects/Hero Data")]
    public class HeroDataSO : CharacterDataSO
    {
        [Header("Idle State")]
        public float baseMoveSpeed;

        [Header("Rush State")]
        public float baseRushSpeed;
        public int baseRushableCount;
        public float baseRushDamageMultiplier;

        public string heroName;
        public Sprite heroIcon;
        //public RuntimeAnimatorController animatorController;
        //public LayerMask hitableLayerMask;
    }
}
