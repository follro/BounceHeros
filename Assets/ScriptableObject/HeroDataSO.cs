using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    [CreateAssetMenu(fileName = "New Hero Data", menuName = "Game/Hero Data")]
    public class HeroDataSO : ScriptableObject
    {
        [Header("Base Stats - 초기값")]
        public float baseMaxHP;
        public float baseAttackDamage;
        public float baseAttackSpeed;
        public float baseMoveSpeed;

        [Header("Static Data - 변하지 않는 값")]
        public string heroName;
        public Sprite heroIcon;
        //public RuntimeAnimatorController animatorController;
        public LayerMask hitableLayerMask;
    }
}
