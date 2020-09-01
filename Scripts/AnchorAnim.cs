using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;


public class AnchorAnim : MonoBehaviour {
    Animator animator;
 
    [SerializeField]
    GameObject idleSound;
    [SerializeField]
    GameObject connectedSound;
    float number;
    void Start() {
        animator = this.gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update() {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Anchor Connected") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Anchor Volt")) {
            number = Random.Range(0f, 1f);
            if (number > .999f) {
                idleSound.GetComponent<AudioSource>().Play();
                animator.Play("Anchor Volt");
            } else {
                animator.Play("Anchor");
            }
        }
    }
}
