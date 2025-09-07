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
            Debug.Log("�۵�");
            Vector3 normal = collision.contacts[0].normal;

            transform.position += normal * 0.01f;

            // ���� �ӵ� ũ�⸦ ����
            float currentSpeed = rb.velocity.magnitude;

            // ���ο� ���� ���
            Vector3 newDirection = Vector3.Reflect(rb.velocity.normalized, normal);
            
            Debug.Log("Reflect Dir: " + newDirection + "Current Dir: " + gameObject.transform.forward);
            // ���ο� �ӵ� ����
            rb.velocity = newDirection * currentSpeed;
        }
    }
}
