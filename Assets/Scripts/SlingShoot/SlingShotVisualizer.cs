using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotVisualizer : MonoBehaviour
{
    [SerializeField] private LineRenderer aimLine;
    [Range(0f, 5f)][SerializeField] private float lineValue;
    private void Awake()
    {
        if (aimLine == null) aimLine = GetComponent<LineRenderer>();
    }

    public void ShowAimLine()
    {
        aimLine.enabled = true;
    }

    public void HideAimLine()
    {
        aimLine.enabled = false;
    }

    public void UpdateAimLine(Vector2 startPos, Vector2 currentPos)
    {
        Vector3 startScreenPosWithZ = new Vector3(startPos.x, startPos.y, 0);
        Vector3 currentScreenPosWithZ = new Vector3(currentPos.x, currentPos.y, 0);

        // ���� ��ǥ�� ��ȯ
        Vector3 startWorldPos = Camera.main.ScreenToWorldPoint(startScreenPosWithZ);
        Vector3 currentWorldPos = Camera.main.ScreenToWorldPoint(currentScreenPosWithZ);

        // �巡�� ���� ��� (������ - ������)
        Vector2 dragVector = new Vector2(startWorldPos.x - currentWorldPos.x, startWorldPos.y - currentWorldPos.y);

        // ���� ���� ��� (���� ����)
        Vector3 endWorldPos = startWorldPos + new Vector3(dragVector.x, dragVector.y, 0) * lineValue;

        // ���� �������� ��ġ ����
        aimLine.positionCount = 2;
        aimLine.SetPosition(0, startWorldPos);
        aimLine.SetPosition(1, endWorldPos);
    }
}
