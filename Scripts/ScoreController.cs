using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;

public class ScoreController : MonoBehaviour {
    [SerializeField]
    private Text RopeText;
    [SerializeField]
    private Text ArrowText;
    [SerializeField]
    GameObject Player;
    private Vector3 xScale;
    // Start is called before the first frame update
    void Start() {
        xScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("r")) {
            SceneManager.LoadScene(0);
        }
        
    }

    public void UpdateArrows(int mod) {
        int arrowNum;
        arrowNum = Int32.Parse(ArrowText.text);
        ArrowText.text = (arrowNum + mod).ToString();
    }
    public void UpdateRope(int mod) {
        int ropeNum;
        ropeNum = Int32.Parse(RopeText.text);
        RopeText.text = (ropeNum + mod).ToString();
    }
}
