using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    
    public Transform shootingPoint;

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
            StartCoroutine(TestShoot(Instantiate(projectile, shootingPoint.position, shootingPoint.rotation), 3));
        }

        Vector3 dir = (FindMouseWorldPos(mouse) - this.transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = targetRotation;

    }

    private IEnumerator TestShoot(GameObject bullet, float time)
    {
        bullet.GetComponent<Rigidbody>().AddForce(shootingPoint.forward * 50f, ForceMode.Impulse);

        yield return new WaitForSeconds(time);
        
        Destroy(bullet);
    }

    private Vector3 FindMouseWorldPos(Mouse mouse)
    {
        Vector2 mouseScreenPosition = mouse.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            Vector3 worldPosition = hit.point;
            Debug.Log("마우스가 가리키는 월드 좌표: " + worldPosition);
            return worldPosition;
        }
        return Vector3.forward;
    }

}
