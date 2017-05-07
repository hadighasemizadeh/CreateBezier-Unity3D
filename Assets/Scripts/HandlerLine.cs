using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class HandlerLine : MonoBehaviour {

    private LineRenderer lr;
    public Transform MainPoint;

    void Start() {
		lr = GetComponent<LineRenderer>();
        lr.SetVertexCount(2);
    }

    void Update() {
		lr.SetPosition(0,MainPoint.position);
		lr.SetPosition(1,transform.position);
    }
}
