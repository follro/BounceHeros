using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Shooter : MonoBehaviour
{
    public BaseBullet projectile;
    
    public Transform shootingPoint;

    public float shooterPower;

    private void Start()
    {
    }

    private void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null)
        {
            return; 
        }

        if(mouse.leftButton.wasPressedThisFrame)
        {
            TestFire(Instantiate(projectile, shootingPoint.position, shootingPoint.rotation));
        }

        Vector3 dir = (FindMouseWorldPos(mouse) - this.transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = targetRotation;

    }

    private void TestFire(BaseBullet bullet)
    {
        Vector3 forward = shootingPoint.forward;
        forward.y = 0.5f;
        bullet.Initialize(forward, shooterPower);

        
    }

    private Vector3 FindMouseWorldPos(Mouse mouse)
    {
        Vector2 mouseScreenPosition = mouse.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            Vector3 worldPosition = hit.point;
            return worldPosition;
        }
        return Vector3.forward;
    }

}
