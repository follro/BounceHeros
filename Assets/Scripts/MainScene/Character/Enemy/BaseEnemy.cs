using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BounceHeros
{
    public class BaseEnemy : Character, IProduct
    {
        string IProduct.ProductId { get; set; }

        bool IProduct.IsActive { get => gameObject.activeSelf; }

        public void OnReturnToPool()
        {
            throw new NotImplementedException();
        }

        public void OnSpawnFromPool()
        {
            throw new NotImplementedException();
        }

        public void ResetToDefault()
        {
            throw new NotImplementedException();
        }

        public void SetTransform(Vector3 position, Quaternion rotation)
        {
            throw new NotImplementedException();
        }

        private void Awake()
        {
            hp = MaxHP;
            OnHitEvent += (damage) => { spriteRenderer.DOColor(Color.red, 0.2f).SetLoops(2, LoopType.Yoyo); };
            OnHitEvent += (damage) => { if (HP <= 0) gameObject.SetActive(false); };
        }





    }
}