using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BounceHeros
{
    public class HeroCatcher : MonoBehaviour
    {
        [SerializeField] LayerMask heroLayer;
        public BaseHero Hero { get; private set; }    

        private void OnTriggerEnter2D(Collider2D other)
        {
            int layer = other.gameObject.layer;
            BaseHero hero = null;
            if (((1 << layer) & heroLayer.value) > 0)
                other.gameObject.TryGetComponent<BaseHero>(out hero);
            Hero = hero;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (Hero == null)
                return;
            if (other.gameObject == Hero.gameObject)
                Hero = null;
        }

        public void Catch() => Hero?.Catched();

        public void Shoot(Vector2 direction, float forceAmount) => Hero?.Rush(direction, forceAmount);
    }
}