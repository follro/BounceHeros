using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Catcher : MonoBehaviour
{
    public GameObject bullet;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null)
            return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            Debug.Log("´©¸§");
            Vector2 mouseScreenPosition = mouse.position.ReadValue();

            Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);

            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 worldPosition = hit.point;
                Instantiate(bullet, worldPosition, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }



}
