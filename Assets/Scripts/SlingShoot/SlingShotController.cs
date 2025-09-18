using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotController : MonoBehaviour
{
    [SerializeField] private DragInputHandler inputHandler;
    [SerializeField] private SlingShotVisualizer visualizer;

    private void Awake()
    {
        if (inputHandler == null || visualizer == null)
        {
            Debug.LogError("InputHandler �Ǵ� Visualizer�� ������� �ʾҽ��ϴ�.");
            return;
        }

        inputHandler.OnDragging += HandleDragging;
        inputHandler.OnDragEnded += HandleDragEnd;
    }

    private void OnDestroy()
    {
        if (inputHandler != null)
        {
            inputHandler.OnDragging -= HandleDragging;
            inputHandler.OnDragEnded -= HandleDragEnd;
        }
    }

    private void HandleDragging(Vector2 startPos, Vector2 currentPos)
    {
        
         visualizer.ShowAimLine();
         visualizer.UpdateAimLine(startPos, currentPos);
        
    }

    private void HandleDragEnd(Vector2 startPos, Vector2 endPos)
    {
        visualizer.HideAimLine();

    }

}
