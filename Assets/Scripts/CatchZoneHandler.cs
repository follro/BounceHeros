using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatchZoneHandler : MonoBehaviour
{
    //�ӽ� 
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
        //mouse�� �ӽ�
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
                
                //�̶� �Ҹ� ������ �����ϸ� �� ������Ʈ�� �������� �������� �޾� �� ��ġ �������� ���ܼ� ������ �ٸ� �̺�Ʈ�� �ٽ� �۵� 
                
            }
        }
    }*/

    // ĳ���� �ڵ鷯�� ���� ��ġ�� �������� �ٸ� ���� ���� ��ġ�� ��ġ���� ����Ǿ� �� �Լ����� ������ �����
    // ���� true������ ���ܼ� ��� �̺�Ʈ�� �۵���Ŵ

    private void OnCatchStared(Vector2 touchScreenPos)
    {
       if (catchZone.Contains(touchScreenPos))
       {
            //���� ���ȿ� �����ִ��� �˻��ϴ°� �ؾߵ�
            //target setting ��𼱰� �����;ߵ�

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
