using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Bezier))]
public class SegmentGenerator : MonoBehaviour {
	
	private string Point_Prefix 	    = "Point ";
	private string Left_Handler_Prefix  = "L";
	private string Right_Handler_Prefix = "R";
	private int Index_point = 0;

	private Bezier bz;

	public InputField enteredIndex;

	public GameObject PointPrefab;

	void Start(){
		bz = GetComponent<Bezier> ();

		if(bz == null){
			print("Attached this script to wrong object!!");
			return;
		}
	}

	// Generate new point
	public void GenerateSegment(){

		 int curveCount = bz.controlPoints.Count;

		Vector3 rndPosition = new Vector3 (Random.Range(-15,15),0,Random.Range(-6,6));
		GameObject pointGo = Instantiate(PointPrefab,rndPosition,transform.rotation);

		// Name different new node
		pointGo.name = Point_Prefix + curveCount;
		pointGo.transform.GetChild(0).name = Left_Handler_Prefix  + curveCount;
		pointGo.transform.GetChild(1).name = Right_Handler_Prefix + curveCount;

		// add point to the point controller
		bz.controlPoints.Add (pointGo.transform.transform);

		// Update line renderer
		bz.isReadyToUpdate = true;
	}

	// Delete segmentation
	public void DeleteSegment(){
		// Check is there enough item to delete
		if (bz.controlPoints.Count < 1) {
			print("There is no enough item to delete!!");
			return;
		}

		int deleteIndex = 0;

		// is index filled
		if (int.TryParse (enteredIndex.text, out deleteIndex)) {
			// Securly limit input number to dont receive out of range error
			deleteIndex = Mathf.Clamp (deleteIndex, 0, bz.controlPoints.Count-1);
		} else {
			// It didn't assigned because so index is last item of list
			deleteIndex = bz.controlPoints.Count - 1;
		}

		// Time to destroy game object and its index on the list
		GameObject tempGO = bz.controlPoints[deleteIndex].gameObject;
		bz.controlPoints.Remove (bz.controlPoints[deleteIndex]);
		Destroy (tempGO);

		// It should be update again
		bz.isReadyToUpdate = true;

		// Hide when number of points is less than 30
		if (bz.controlPoints.Count < 2) {
			// So we dont need to do anything because it is assigned
			bz.lineRenderer.numPositions = 0;
		}
	}
}
