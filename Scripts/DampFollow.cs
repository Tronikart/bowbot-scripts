using UnityEngine;
using System.Collections;
     
public class DampFollow : MonoBehaviour {
    [SerializeField]
    Transform target;
    [SerializeField]
    float smoothTime = 0.3f;
    Vector3 velocity = Vector3.zero;

    void FixedUpdate() {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 1.156f, target.position.z));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}