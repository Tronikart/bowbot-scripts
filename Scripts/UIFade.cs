using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour {
    SpriteRenderer[] sprites;
    Text[] texts;
    LineRenderer[] lines;
    Color tmp;
    // Start is called before the first frame update
    void Start() {
        sprites = this.GetComponentsInChildren<SpriteRenderer>();
        texts = this.GetComponentsInChildren<Text>();
        lines = this.GetComponentsInChildren<LineRenderer>();

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            fadeIn();
        }
        if (Input.GetKeyUp(KeyCode.Tab)) {
            fadeAway();
        }
    }

    public void fadeAway() {
        Debug.Log("fadeAway()");
        for (float i = 1f; i >= 0f; i -= .01f) {
            for (int j = 0; j < sprites.Length; j++) {
                sprites[j].gameObject.SetActive(false);
                /*
                 * tmp = sprites[j].color;
                 * tmp.a = i;
                 * sprites[j].color = tmp;
                 */
            }
            for (int j = 0; j < texts.Length; j++) {
                texts[j].gameObject.SetActive(false);
                /*
                 * tmp = texts[j].color;
                 * tmp.a = i;
                 * texts[j].color = tmp;
                 */
            }
        }
        for (int i = 0; i < lines.Length; i++) {
            lines[i].enabled = false;
        }
    }

    public void fadeIn() {
        Debug.Log("fadeIn()");
        for (float i = 0f; i <= .8f; i += .1f) {
            for (int j = 0; j < sprites.Length; j++) {
                sprites[j].gameObject.SetActive(true);
                /*
                 * tmp = sprites[j].color;
                 * tmp.a = i;
                 * sprites[j].color = tmp;
                 */
            }
            for (int j = 0; j < texts.Length; j++) {
                texts[j].gameObject.SetActive(true);
                /*
                 * tmp = texts[j].color;
                 * tmp.a = i;
                 * texts[j].color = tmp;
                 */
            }
        }
        for (int i = 0; i < lines.Length; i++) {
            lines[i].enabled = true;
        }
    }
}