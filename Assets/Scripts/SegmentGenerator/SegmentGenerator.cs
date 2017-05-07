using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour {
	
	private string Point_Prefix 	    = "Point";
	private string Left_Handler_Prefix  = "L";
	private string Right_Handler_Prefix = "R";
	private int Index_point = 0;

	public GameObject PointPrefab;
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenerateSegment(){

		Bezier bz = GetComponent<Bezier> ();
		if(bz == null){
			print("Attached this script to wrong object!!");
			return;
		}

		int controlPointsLength = (int) (Mathf.Floor(bz.controlPoints.Length/2)-1);

		GameObject pointGo = Instantiate(PointPrefab,new Vector3(0,0,0),transform.rotation);

		//Main GameObject name
		pointGo.name = Point_Prefix + controlPointsLength;

		// Childs name
		pointGo.transform.GetChild(0).name = pointGo.name;
		pointGo.transform.GetChild(1).name = Left_Handler_Prefix+ controlPointsLength;
		pointGo.transform.GetChild(2).name = Right_Handler_Prefix+ controlPointsLength;

		// add point to the point controller
		var listContrllerPoints = new List<Transform>(bz.controlPoints);
		listContrllerPoints.Add (pointGo.transform.GetChild(1).transform);
		listContrllerPoints.Add (pointGo.transform.GetChild(0).transform);
		listContrllerPoints.Add (pointGo.transform.GetChild(2).transform);

		// again put it on the script
		bz.controlPoints = listContrllerPoints.ToArray();

		bz.curveCount = (int)bz.controlPoints.Length / 3;
	}
}
