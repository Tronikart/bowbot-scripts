using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    //offset from the viewport center to fix damping
    [SerializeField]
    private float m_DampTime = 10f;
    [SerializeField]
    private Transform m_Target;
    [SerializeField]
    private float m_XOffset = 0;
    [SerializeField]
    private float m_YOffset = 0;
    [SerializeField]
    private float margin = 0.1f;

    void Start() {
        if (m_Target == null) {
            m_Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update() {
        if (m_Target) {
            float targetX = m_Target.position.x + m_XOffset;
            float targetY = m_Target.position.y + m_YOffset;

            if (Mathf.Abs(transform.position.x - targetX) > margin)
                targetX = Mathf.Lerp(transform.position.x, targetX, 1 / m_DampTime * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - targetY) > margin)
                targetY = Mathf.Lerp(transform.position.y, targetY, m_DampTime * Time.deltaTime);
            Debug.Log(targetY);
            transform.position = new Vector3(targetX, targetY, transform.position.z);
        }
    }
}