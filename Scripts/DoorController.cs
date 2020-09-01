using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
    [SerializeField]
    Sprite doorOpen;
    Animator animator;
    BoxCollider2D collider;
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("t")) {
            openDoor();
        }
    }

    public void openDoor() {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Door Open")) {
                animator.Play("Door Open");
                collider.enabled = false;

        }

    }
}
