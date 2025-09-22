using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BounceHeros
{
    public class HeroCatcher : MonoBehaviour
    {
        [SerializeField] LayerMask heroLayer;
        public GameObject Hero { get; private set; }    

        private void OnTriggerEnter2D(Collider2D other)
        {
            int layer = other.gameObject.layer;

            if (((1 << layer) & heroLayer.value) > 0)
                Hero = other.gameObject;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (Hero == null)
                return;
            if (other.gameObject == Hero)
                Hero = null;
        }
    }
}