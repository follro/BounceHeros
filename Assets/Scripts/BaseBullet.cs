using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float power;
    private Rigidbody rb;
    private const float lifetime = 3f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, lifetime);
    }

    private void Start()
    {
        
    }

    public void Shoot(Vector3 forward)
    {
        rb.velocity = forward * power;
        Debug.Log("velocity: " +  rb.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Debug.Log("작동");
            Vector3 normal = collision.contacts[0].normal;

            transform.position += normal * 0.01f;

            // 현재 속도 크기를 저장
            float currentSpeed = rb.velocity.magnitude;

            // 새로운 방향 계산
            Vector3 newDirection = Vector3.Reflect(rb.velocity.normalized, normal);
            
            Debug.Log("Reflect Dir: " + newDirection + "Current Dir: " + gameObject.transform.forward);
            // 새로운 속도 적용
            rb.velocity = newDirection * currentSpeed;
        }
    }
}
