using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragInputHandler : MonoBehaviour
{
    public event Action OnDragStarted;
    public event Action<Vector2, Vector2> OnDragging;
    public event Action OnDragEnded;

    [Header("Input")]
    [SerializeField] private InputAction pressAction, screenPosAction;

    [SerializeField] private Vector2 startPos, endPos;

    private void Awake()
    {
        InitializedPos();
    }

    private void OnEnable()
    {
        pressAction.performed += OnPressStarted;
        pressAction.canceled += OnPressEnded;
        screenPosAction.performed += UpdateDragPos;

        pressAction.Enable();
        screenPosAction.Enable();
    }

    private void OnDisable()
    {
        pressAction.Disable();
        screenPosAction.Disable();
        
        pressAction.performed -= OnPressStarted;
        pressAction.canceled -= OnPressEnded;
        screenPosAction.performed -= UpdateDragPos;
    }

    private void InitializedPos()
    {
        endPos = startPos = Vector2.zero;
    }

    private void UpdateDragPos(InputAction.CallbackContext context)
    {
        if (pressAction.IsPressed())
        {
            endPos = context.ReadValue<Vector2>();
            OnDragging?.Invoke(startPos, endPos);
        }
    }

    private void OnPressStarted(InputAction.CallbackContext context)
    {
        startPos = screenPosAction.ReadValue<Vector2>();
        OnDragStarted?.Invoke();
    }

    private void OnPressEnded(InputAction.CallbackContext context)
    {
        OnDragEnded?.Invoke();
        InitializedPos();
    }

}
