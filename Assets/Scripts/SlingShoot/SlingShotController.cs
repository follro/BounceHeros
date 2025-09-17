using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotController : MonoBehaviour
{
    [SerializeField] private SlingShotInputHandler inputHandler;
    [SerializeField] private SlingShotVisualizer visualizer;

    private void Awake()
    {
        if (inputHandler == null || visualizer == null)
        {
            Debug.LogError("InputHandler �Ǵ� Visualizer�� ������� �ʾҽ��ϴ�.");
            return;
        }

        inputHandler.OnDragging += HandleDragging;
        inputHandler.OnDragEnd += HandleDragEnd;
    }

    private void OnDestroy()
    {
        if (inputHandler != null)
        {
            inputHandler.OnDragging -= HandleDragging;
            inputHandler.OnDragEnd -= HandleDragEnd;
        }
    }

    private void HandleDragging(Vector2 startPos, Vector2 currentPos)
    {
        if (inputHandler.IsDragValid)
        {
            visualizer.ShowAimLine();
            visualizer.UpdateAimLine(startPos, currentPos);
        }
    }

    private void HandleDragEnd(Vector2 startPos, Vector2 endPos)
    {
        visualizer.HideAimLine();

    }

}
