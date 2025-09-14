using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatchZoneHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask catchAbleLayer;

    [Header("Input")]
    [SerializeField] private InputAction pressScreen;
    [SerializeField] private InputAction dragPostion;

    [Header("Dragging Pos")]
    [SerializeField] private Vector2 dragStartPos;
    [SerializeField] private Vector2 dragEndPos;

    
    private BaseHero hero;

    private void Start()
    {
        hero = null;
    }

    private void OnEnable()
    {
        pressScreen.started += (context) => ScreenPressedAction(context.ReadValueAsButton());
        pressScreen.canceled += (context) => ScreenPressedAction(context.ReadValueAsButton());

        dragPostion.started += (context) => dragStartPos = context.ReadValue<Vector2>();
        dragPostion.performed += (context) => Dragging(dragStartPos, context.ReadValue<Vector2>());
        dragPostion.canceled += (context) => dragEndPos = context.ReadValue<Vector2>();

        pressScreen.Enable();
        dragPostion.Disable();
    }
    private void OnDisable()
    {
        pressScreen.started -= (context) => ScreenPressedAction(context.ReadValueAsButton());
        pressScreen.canceled -= (context) => ScreenPressedAction(context.ReadValueAsButton());

        dragPostion.started -= (context) => dragStartPos = context.ReadValue<Vector2>();
        dragPostion.performed -= (context) => Dragging(dragStartPos, context.ReadValue<Vector2>());
        dragPostion.canceled -= (context) => dragEndPos = context.ReadValue<Vector2>();

        pressScreen.Disable();
        dragPostion.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hero = collision.GetComponent<BaseHero>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hero = null;
    }

    private void ScreenPressedAction(bool isPressed)
    {
        if (isPressed)
            dragPostion.Enable();
        else
        {
            dragStartPos = dragEndPos = Vector2.zero;
            dragPostion.Disable();
        }
    }

    private void Dragging(Vector2 start, Vector2 end)
    {

    }

    private void DragEnd(Vector2 endPos)
    {
        dragEndPos = endPos;    
    }

}
