using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowFall : MonoBehaviour {

    Rigidbody2D rb;
    [HideInInspector]
    public bool hasHit = false;
    [SerializeField]
    AudioSource arrowHit;
    [SerializeField]
    AudioSource hitTower;
    [HideInInspector]
    public bool hasHitTower = false;
    Text pickupText;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        pickupText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update() {
        if (hasHit) {
            pickupText.transform.rotation = Quaternion.Euler(0, 0, -this.transform.rotation.z);
        } else {
            trackMovement();
        }
    }

    void trackMovement() {
        Vector2 direction = rb.velocity;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!hasHitTower) {
            arrowHit.pitch = UnityEngine.Random.Range(2.24f, 3f);
            arrowHit.Play();
        }
        // FindObjectOfType<AudioManager>().Play("HitMetal");
        hasHit = true;
        this.GetComponent<Animator>().enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.gameObject.name.Contains("Light Tower")) {
            Debug.Log(collision.transform.gameObject.name);
            hasHitTower = true;
            hitTower.Play();
        }
    }
}
