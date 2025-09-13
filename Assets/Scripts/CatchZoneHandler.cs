using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatchZoneHandler : MonoBehaviour
{
    //임시 
    public float catchZoneHeightRatio;

    [SerializeField] private InputAction screenTouch;
    private Rect catchZone;

    private Vector2 startPoint;
    [SerializeField]private Transform target;

    private void Start()
    {
        startPoint = Vector2.zero;
        catchZone = new Rect(0, 0, Screen.width , Screen.height * catchZoneHeightRatio);
    }

    private void OnEnable()
    {
        screenTouch.started += context => OnCatchStared(context.ReadValue<Vector2>());
        screenTouch.performed += context => OnCatchPerformed(context.ReadValue<Vector2>());
        screenTouch.canceled += context => OnCatchCanceld(context.ReadValue<Vector2>());
        screenTouch.Enable();

    }
    private void OnDisable()
    {
        //screenTouch.performed
        screenTouch.Disable();   
    }

   /* private void Update()
    {
        //mouse는 임시
        var mouse = Mouse.current;
        if (mouse == null)
            return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector2 touchScreenPosition = mouse.position.ReadValue();

            if(catchZone.Contains(touchScreenPosition))
            {
                Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchScreenPosition);
                touchWorldPosition.z = 0;
                
                //이때 불릿 감지에 성공하면 그 오브젝트의 포지션을 리턴으로 받아 그 위치 기준으로 땡겨서 놓으면 다른 이벤트를 다시 작동 
                
            }
        }
    }*/

    // 캐쳐존 핸들러의 역할 터치가 들어왔을때 다른 곳에 현재 터치된 위치에서 실행되야 될 함수들이 있으면 실행됨
    // 만약 true였으면 땡겨서 쏘는 이벤트를 작동시킴

    private void OnCatchStared(Vector2 touchScreenPos)
    {
       if (catchZone.Contains(touchScreenPos))
       {
            //공이 존안에 들어와있는지 검사하는걸 해야됨
            //target setting 어디선가 가져와야됨

            startPoint = touchScreenPos + target.position;
       }
       
    }


    private void OnCatchPerformed(Vector2 dragScreenPos)
    {
        if(target != null)
        {

        }
    }

    private void OnCatchCanceld (Vector2 endScreenPos)
    {

    }
}
