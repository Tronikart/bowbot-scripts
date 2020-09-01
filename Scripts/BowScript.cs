using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BowScript : MonoBehaviour {
    Vector2 direction;
    PlayerMovement playerMovementScript;
    public ScoreController scoreController;
    Animator animator;

    [Header("Ammo")]
    [SerializeField]
    int Ammo;
    [SerializeField]
    int RopeArrows;
    [SerializeField]
    GameObject Arrow;
    [SerializeField]
    GameObject RopeArrow;

    [Header("Bow")]
    [SerializeField]
    float maxLaunchForce;
    [SerializeField]
    float launchForceIncrement;
    [SerializeField]
    float perfectShot;

    [SerializeField]
    UnityEngine.Experimental.Rendering.Universal.Light2D topLight;
    [SerializeField]
    UnityEngine.Experimental.Rendering.Universal.Light2D botLight;
    [SerializeField]
    GameObject arms;

    Vector3 xScale;
    float currentLaunchForce = 0;
    Vector2 mousePos;
    Vector2 bowPos;
    SpriteRenderer sprite;
    GameObject player;

    // Start is called before the first frame update
    void Start() {
        xScale = transform.localScale;
        playerMovementScript = transform.parent.gameObject.GetComponent<PlayerMovement>();
        scoreController.UpdateRope(RopeArrows);
        scoreController.UpdateArrows(Ammo);
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bowPos = transform.position;

        direction = mousePos - bowPos;

        FaceMouse(mousePos);

        if (Input.GetMouseButton(0) && Ammo > 0) {
            animator.Play("Bow Draw");
            if (currentLaunchForce == 0) {
                FindObjectOfType<AudioManager>().Play("BowInit");
            }
            if (currentLaunchForce < maxLaunchForce) {
                currentLaunchForce += launchForceIncrement;
                topLight.intensity = currentLaunchForce / 100;
                botLight.intensity = currentLaunchForce / 100;
            }
            else {
                currentLaunchForce = maxLaunchForce;
            }
        } else if (Input.GetMouseButton(0)) {
            animator.Play("Bow Empty");
        }
        if (Input.GetMouseButtonUp(0) && Ammo > 0) {
            FindObjectOfType<AudioManager>().Stop("BowInit");
            animator.Play("Bow Base");
            Shoot(currentLaunchForce);
            topLight.intensity = 1;
            botLight.intensity = 1;
            currentLaunchForce = 0;
        } else if (Input.GetMouseButtonUp(0)) {
            FindObjectOfType<AudioManager>().Stop("BowInit");
            animator.Play("Bow Base");
        }
    }

    public void AddAmmo(int ammoToAdd) {
        Ammo += ammoToAdd;
    }

    public void AddRope(int ropeToAdd) {
        RopeArrows += ropeToAdd;
    }

    void FaceMouse(Vector2 mousePosition) {
        transform.right = direction;
        if (mousePosition.x > transform.position.x) {
            transform.localScale = xScale;
            arms.transform.localScale = xScale;
        }
        else {
            transform.localScale = new Vector3(-xScale.x, xScale.y, xScale.z);
            arms.transform.localScale = new Vector3(xScale.x, -xScale.y, xScale.z);
        }
    }

    void Shoot(float launchForce) {
        GameObject ArrowIns;
        if (playerMovementScript.collidingAnchor && Ammo > 0 && RopeArrows > 0) {
            FindObjectOfType<AudioManager>().Play("Shoot");
            ArrowIns = Instantiate(RopeArrow, transform.position, transform.rotation);
            ArrowIns.GetComponent<Rigidbody2D>().AddForce(transform.right * launchForce);
            playerMovementScript.collidingAnchorGameObject.GetComponent<Animator>().Play("Anchor Connected");
            playerMovementScript.collidingAnchorGameObject.transform.Find("Connected Sound").GetComponent<AudioSource>().Play();
            Ammo -= 1;
            RopeArrows -= 1;
            scoreController.UpdateArrows(-1);
            scoreController.UpdateRope(-1);
        }
        else {
            if (Ammo > 0) {
                FindObjectOfType<AudioManager>().Play("Shoot");
                ArrowIns = Instantiate(Arrow, transform.position, transform.rotation);
                ArrowIns.GetComponent<Rigidbody2D>().AddForce(transform.right * launchForce);
                Ammo -= 1;
                scoreController.UpdateArrows(-1);

            }
        }
    }
}
    