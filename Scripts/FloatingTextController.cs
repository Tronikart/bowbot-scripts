using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FloatingTextController : MonoBehaviour {
    Text textElement;
    [SerializeField]
    string stringText;
    [SerializeField]
    GameObject FloatingTextAnim;
    Animator animator;
    // Start is called before the first frame update
    void Start() {
        textElement = GetComponentInChildren<Text>();
        animator = FloatingTextAnim.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.name == "Player") {
            textElement.text = System.Text.RegularExpressions.Regex.Unescape(stringText);
            animator.Play("FloatingTextOn");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform.name == "Player") {
            textElement.text = null;
            animator.Play("FloatingText");
        }
    }
}
