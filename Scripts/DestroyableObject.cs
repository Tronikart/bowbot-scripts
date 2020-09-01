using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour {
    [SerializeField]
    int hp;
    List<GameObject> arrows;
    // Start is called before the first frame update
    void Start() {
        arrows = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        if (hp < 1) {
            Destroy(this.gameObject);
            foreach (GameObject arrow in arrows) {
                arrow.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.gameObject.name.Contains("Arrow")) {
            hp -= 1;
            arrows.Add(collision.gameObject);
        }
    }
}
