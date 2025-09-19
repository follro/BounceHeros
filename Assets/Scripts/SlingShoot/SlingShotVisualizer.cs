using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BounceHeros
{
    public class SlingShotVisualizer : MonoBehaviour
    {
        [SerializeField] private LineRenderer aimLine;
        [Range(0f, 5f)][SerializeField] private float lineValue;

        [SerializeField] private LayerMask reflectableLayer;
        [SerializeField] private List<Vector2> reflectPositions;

        private void Awake()
        {
            if (aimLine == null) aimLine = GetComponent<LineRenderer>();
            reflectPositions = new List<Vector2>();

        }

        public void ShowAimLine()
        {
            aimLine.enabled = true;
        }

        public void HideAimLine()
        {
            aimLine.enabled = false;
        }

        public void UpdateAimLine(Vector2 startPosition, Vector2 currentDragVector)
        {
            Vector2 endPosition = startPosition - currentDragVector;
            Vector2 endVector = endPosition - startPosition;
            float length = endVector.magnitude;

            CalculateReflectVectors(startPosition, endVector.normalized, length);

            aimLine.positionCount = reflectPositions.Count;

            for (int i = 0; i < reflectPositions.Count; i++)
                aimLine.SetPosition(i, new Vector3(reflectPositions[i].x, reflectPositions[i].y, 0));
        }

        private void CalculateReflectVectors(Vector2 origin, Vector2 direction, float totalLength)
        {
            reflectPositions.Clear();
            reflectPositions.Add(origin);

            Vector2 currentPosition = origin;
            Vector2 currentDirection = direction.normalized;
            float remainingLength = totalLength;
            int maxBounces = 5;


            for (int i = 0; i < maxBounces; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(currentPosition, currentDirection, remainingLength, reflectableLayer);

                if (hit.collider != null)
                {
                    reflectPositions.Add(hit.point);
                    remainingLength -= hit.distance;

                    Vector2 reflectedDirection = Vector2.Reflect(currentDirection, hit.normal);
                    currentPosition = hit.point + reflectedDirection * 0.001f;
                    currentDirection = reflectedDirection;
                }
                else
                {
                    reflectPositions.Add(currentPosition + currentDirection * remainingLength);
                    break;
                }
            }
        }

    }
}