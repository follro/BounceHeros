using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class BaseHero : MonoBehaviour
    {
        public enum HeroState
        {
            Catched,
            Rush,
            Bouceable,
            Dead,
        }

        private Rigidbody2D rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();

        }

        public void Catched()
        {
            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0;
        }
    }
}