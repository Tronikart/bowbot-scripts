using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabToggle : MonoBehaviour
{

    public GameObject[] lights;
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            for (int i = 0; i < lights.Length; i++) {
                lights[i].SetActive(true);
            }
            // this.gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab)) {
            for (int i = 0; i < lights.Length; i++) {
                lights[i].SetActive(false);
            }
            //this.gameObject.SetActive(false);

        }
    }
}
