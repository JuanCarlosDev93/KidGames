using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineNumber : MonoBehaviour
{
    [SerializeField] private DrawNumbers drawGame;
    public ElementDrawNumber elementDraw;
    [SerializeField] private LineRenderer lineRendPrefab;
    [SerializeField] private LineRenderer lineRenderer;
    public Transform lineParent;
    private List<Vector3> linePoints;
    [SerializeField] private Vector3[] circlePos;
    public bool canDraw;
    public bool lastPointReached;

    private void Start()
    {
        InitializeLine();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            lineRenderer = Instantiate(lineRendPrefab, lineParent);

            if (linePoints != null)
            {
                linePoints.Clear();
            }
            drawGame.hand.gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (canDraw)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPos = lineParent.InverseTransformPoint(new Vector3(worldPos.x, worldPos.y, 0));

                if (linePoints.Contains(worldPos) == false)
                {
                    linePoints.Add(worldPos);
                    lineRenderer.positionCount = linePoints.Count;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPos);
                }
            }

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (!lastPointReached)
            {
                if (lineRenderer != null)
                {
                    lineRenderer.positionCount = 0;
                }
                if (canDraw)
                {
                    elementDraw.ResetElement();
                    //elementDrawShape.ResetElement();

                }
            }
            else
            {
                lastPointReached = false;
            }
            //if (drawGame.activeHand)
            //{
            //    drawGame.hand.gameObject.SetActive(true);
            //}
            //canDraw = true;
        }

        //if (!canDraw)
        //{
        //   linePoints.Clear();
        //   lineRenderer.positionCount = 0;
        //}
    }

    private void InitializeLine()
    {
        linePoints = new List<Vector3>();
    }
}
