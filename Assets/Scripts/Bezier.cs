using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class Bezier : MonoBehaviour
{
	public List<Transform> controlPoints = new List<Transform> ();

	[HideInInspector] public LineRenderer lineRenderer;

    private int layerOrder = 0;

	// Number of segment to draw each curve between two points
    private int SEGMENT_COUNT = 30;

	private bool isLoopCurve = false;
	public  bool isReadyToUpdate = true;

    void Start()
    {
		lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.sortingLayerID = layerOrder;
    }

    void Update()
    {
		// Update beizer curve
		if (isReadyToUpdate) {
			DrawCurve ();
		}
    }
    
    void DrawCurve()
    {
		for (int j = 0; j < controlPoints.Count-1; j++)
		{		
	        for (int i = 1; i <= SEGMENT_COUNT; i++)
	        {
	            float t = i / (float)SEGMENT_COUNT;

				Vector3 pixel = CalculateCubicBezierPoint(t, controlPoints [j].position, controlPoints [j].GetChild(1).position,
															 controlPoints [j+1].GetChild(0).position, controlPoints [j+1].position);

				lineRenderer.numPositions = (j * SEGMENT_COUNT) + i;
	            lineRenderer.SetPosition((j * SEGMENT_COUNT) + (i - 1), pixel);
	        }
	    }

		isReadyToUpdate = false;
    }

	// Bezier formula
    private Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
		
        Vector3 p = uuu * p0; 
        p += 3 * uu * t * p1; 
        p += 3 * u * tt * p2; 
        p += ttt * p3; 
		
        return p;
    }
}
