using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRecycler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BaseBullet>() != null)
            Destroy(other.gameObject);  
    }
}
