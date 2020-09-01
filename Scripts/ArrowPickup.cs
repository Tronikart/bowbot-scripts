using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowPickup : MonoBehaviour {
    [SerializeField]
    BowScript bowScript;
    [SerializeField]
    ScoreController scoreController;
    bool collidingArrow;
    Collider2D currentCollidingArrow = null;
    // Start is called before the first frame update    
    private void Start() {
        collidingArrow = false;
    }
    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown("e") && collidingArrow) {
            bowScript.AddAmmo(1);
            scoreController.UpdateArrows(1);
            if (currentCollidingArrow.transform.gameObject.name == "Arrow Rope(Clone)") {
                bowScript.AddRope(1);
                scoreController.UpdateRope(1);
            }
            Destroy(currentCollidingArrow.transform.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.gameObject.name == "Arrow Rope(Clone)" || collision.transform.gameObject.name == "Arrow(Clone)") {
            currentCollidingArrow = collision;
            if (collision.transform.gameObject.GetComponent<Rigidbody2D>().isKinematic && 
                !(collision.transform.gameObject.GetComponent<ArrowFall>().hasHitTower && collision.transform.gameObject.name == "Arrow Rope(Clone)")
                ) {
                collision.transform.gameObject.GetComponentInChildren<Text>().enabled = true;
                collidingArrow = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.transform.gameObject.name == "Arrow Rope(Clone)" || collision.transform.gameObject.name == "Arrow(Clone)") {
            currentCollidingArrow = collision;
            if (collision.transform.gameObject.GetComponent<Rigidbody2D>().isKinematic &&
                !(collision.transform.gameObject.GetComponent<ArrowFall>().hasHitTower && collision.transform.gameObject.name == "Arrow Rope(Clone)")) {
                collision.transform.gameObject.GetComponentInChildren<Text>().enabled = true;
                collidingArrow = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform.gameObject.name == "Arrow Rope(Clone)" || collision.transform.gameObject.name == "Arrow(Clone)") {
            if (collision.transform.gameObject.GetComponent<Rigidbody2D>().isKinematic &&
                !(collision.transform.gameObject.GetComponent<ArrowFall>().hasHitTower && collision.transform.gameObject.name == "Arrow Rope(Clone)")) {
                collision.transform.gameObject.GetComponentInChildren<Text>().enabled = false;
                collidingArrow = false;
            }
            currentCollidingArrow = null;
        }
    }
}
