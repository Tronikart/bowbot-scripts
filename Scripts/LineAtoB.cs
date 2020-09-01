using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAtoB : MonoBehaviour {
    [SerializeField]
    float flatLineOffset = -0.5f;
    [SerializeField]
    GameObject target;
    [SerializeField]
    private float lineWidth = 0.02f;
    LineRenderer lineRenderer;
    Vector3 targetPosition;
    Vector3 fromPosition;
    // Start is called before the first frame update
    void Start() {
        this.lineRenderer = GetComponent<LineRenderer>();
        targetPosition = target.transform.position;
        fromPosition = this.transform.position;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
    }

    // Update is called once per frame
    void Update() {
        targetPosition = target.transform.position;
        fromPosition = this.transform.position;
        lineRenderer.SetPosition(0, new Vector3(fromPosition.x - flatLineOffset, fromPosition.y, fromPosition.z));
        lineRenderer.SetPosition(1, fromPosition);
        lineRenderer.SetPosition(2, targetPosition);
        lineRenderer.SetPosition(3, new Vector3(targetPosition.x + flatLineOffset/2, targetPosition.y, targetPosition.z));
    }
}
