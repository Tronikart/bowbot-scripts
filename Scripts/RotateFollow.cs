using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFollow : MonoBehaviour {
    [SerializeField]
    GameObject target;
    Vector3 lookAt;
    float angle;
    float rad;
    // Start is called before the first frame update
    void Start() {
        // temp = target.transform.position;
    }

    // Update is called once per frame
    void Update() {
        lookAt = target.transform.position;

        rad = Mathf.Atan2(lookAt.y - transform.position.y, lookAt.x - transform.position.x);
        angle = ((180 / Mathf.PI) * rad) - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
