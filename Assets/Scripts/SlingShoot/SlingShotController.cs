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
            Debug.LogError("InputHandler 또는 Visualizer가 연결되지 않았습니다.");
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
