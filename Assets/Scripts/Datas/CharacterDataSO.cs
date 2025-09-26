using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public abstract class CharacterDataSO : ScriptableObject
    {
        [Header("Default Character Value")]
        public float baseMaxHP;
        public float baseAttackDamage;

        public LayerMask hitableLayerMask;
    }
}
