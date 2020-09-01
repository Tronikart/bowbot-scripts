using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour {

    Vector3 mousePosition;
    public GameObject baseDistance;
    public GameObject bow;
    [SerializeField]
    float bowStrength;

    float angleRad;
    float angleDeg;


    void Awake() {
    }

    // Update is called once per frame
    void Update() {
        // Bow Spin
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angleRad = Mathf.Atan2(mousePosition.y - bow.transform.position.y, mousePosition.x - bow.transform.position.x);
        angleDeg = (180 / Mathf.PI) * angleRad;
        bow.transform.rotation = Quaternion.Euler(0, 0, angleDeg);;
        //


        
    }
}
