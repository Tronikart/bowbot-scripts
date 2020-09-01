using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

    Animator animator;
    public ScoreController scoreController;
    AudioSource onSound;
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        onSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("LightTowerAnimOn")) {
            if (collision.transform.gameObject.name == "Arrow Rope(Clone)") {
                // onSound.pitch = Random.Range(2.24f, 3f);
                onSound.Play();
                animator.Play("LightTowerAnimOn");
                // scoreController.UpdateObjective(1);
            }
        }
    }
}
