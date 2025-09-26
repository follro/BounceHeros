using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    [CreateAssetMenu(fileName = "New Hero Data", menuName = "Game/Hero Data")]
    public class HeroDataSO : ScriptableObject
    {
        [Header("Base Stats - �ʱⰪ")]
        public float baseMaxHP;
        public float baseAttackDamage;
        public float baseAttackSpeed;
        public float baseMoveSpeed;

        [Header("Static Data - ������ �ʴ� ��")]
        public string heroName;
        public Sprite heroIcon;
        //public RuntimeAnimatorController animatorController;
        public LayerMask hitableLayerMask;
    }
}
