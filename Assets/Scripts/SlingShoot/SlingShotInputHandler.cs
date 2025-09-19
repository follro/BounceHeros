using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BounceHeros
{
    public class SlingShotInputHandler : MonoBehaviour
    {
        public bool IsDragValid { get; private set; }

        public event Action<Vector2> OnDragValidated;
        public event Action<Vector2, Vector2> OnDragging;
        public event Action<Vector2, Vector2> OnDragEnd;

        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask layerMask;

        [Header("Input")]
        [SerializeField] private InputAction holdAction;
        [SerializeField] private InputAction dragAction;

        [Header("Dragging Pos")]
        [SerializeField] private Vector2 dragStartPos;
        [SerializeField] private Vector2 dragEndPos;

        private void Start()
        {
            layerMask = 1 << gameObject.layer;
        }


        private void OnEnable()
        {
            holdAction.started += SetDragActionEnabled;
            holdAction.canceled += SetDragActionEnabled;

            dragAction.started += DragStart;
            dragAction.performed += Dragging;
            dragAction.canceled += DragEnd;

            holdAction.Enable();
            dragAction.Disable();
        }

        private void OnDisable()
        {
            holdAction.started -= SetDragActionEnabled;
            holdAction.canceled -= SetDragActionEnabled;

            dragAction.started -= DragStart;
            dragAction.performed -= Dragging;
            dragAction.canceled -= DragEnd;

            holdAction.Disable();
            dragAction.Disable();
        }

        private void SetDragActionEnabled(InputAction.CallbackContext context)
        {
            bool isHolded = context.ReadValueAsButton();
            IsDragValid = isHolded;

            if (isHolded)
                dragAction.Enable();
            else
            {
                dragStartPos = dragEndPos = Vector2.zero;
                dragAction.Disable();
            }
        }

        private void DragStart(InputAction.CallbackContext context)
        {
            Vector2 startPos = context.ReadValue<Vector2>();
            if (IsDragAble(startPos))
            {
                dragStartPos = startPos;
                OnDragValidated?.Invoke(startPos);
            }
            else
                IsDragValid = false;
        }

        private bool IsDragAble(Vector2 startPos)
        {
            Vector2 touchPos = mainCamera.ScreenToWorldPoint(startPos);
            return Physics2D.OverlapPoint(touchPos, layerMask) != null;
        }

        private void Dragging(InputAction.CallbackContext context)
        {
            if (!IsDragValid) return;

            dragEndPos = context.ReadValue<Vector2>();
            OnDragging?.Invoke(dragStartPos, dragEndPos);
        }

        private void DragEnd(InputAction.CallbackContext context)
        {
            dragEndPos = context.ReadValue<Vector2>();
            OnDragEnd?.Invoke(dragStartPos, dragEndPos);
        }


    }
}
