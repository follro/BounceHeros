using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BounceHeros
{
    public interface IHitable
    {
        public event Action<float> OnHitEvent;
        public float MaxHP { get; }
        public float HP { get; }
        public void OnHit(float damage);
    }
}
