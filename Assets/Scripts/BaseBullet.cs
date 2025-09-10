using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float defaultPower;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        
    }

    public void Initialize(Vector3 forward, float shootingPower)
    {
        
        rb.AddForce(forward * shootingPower * defaultPower, ForceMode.Impulse);
    }

 
}
