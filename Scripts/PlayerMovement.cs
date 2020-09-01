using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class PlayerMovement : MonoBehaviour {
    Rigidbody2D rb2d;
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool isGrounded;
    bool isRunning;
    float movementSpeed;
    Vector3 xScale;
    Vector2 mousePos;
    float currentFrame;

    // Lights info
    UnityEngine.Experimental.Rendering.Universal.Light2D brightLightRight;
    UnityEngine.Experimental.Rendering.Universal.Light2D brightLightLeft;
    UnityEngine.Experimental.Rendering.Universal.Light2D globalLight;
    Vector3 lookAt;
    float rightAngleRad;
    float rightAngleDeg;
    float leftAngleRad;
    float leftAngleDeg;
    float targLeftDeg;
    float targRightDeg;
    bool lightsOn = true;
    bool dimLights = false;
    bool lightsChange = false;
    public bool collidingAnchor = false;
    public GameObject collidingAnchorGameObject;
    [Header("Lights")]
    [SerializeField]
    [Range(0,1)]
    float brightLightThreshold = 0.4f;
    [SerializeField]
    float defaultLightRight = 0.6f;
    [SerializeField]
    float defaultLightLeft = 1f;
    [SerializeField]
    float dimLightRight = 0.2f;
    [SerializeField]
    float dimLightLeft = 0.3f;

    [Header("Movement")]
    [SerializeField]
    float horizontalSpeed = 5f;
    [SerializeField]
    float jumpSpeed = 7f;
    [SerializeField]
    float sprintSpeed = 6f; 

    [Header("Jumping Tweaking")]
    [SerializeField]
    float fallMultiplier = 2.5f;
    float lowJumpMultiplier = 2f;


    [Header("GroundChecks")]
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform groundCheckR;
    [SerializeField]
    Transform groundCheckL;
    


    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        xScale = rb2d.transform.localScale;
        
        globalLight = GameObject.Find("Global Light").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        brightLightRight = GameObject.Find("Right Eye Bright Light").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        brightLightLeft = GameObject.Find("Left Eye Bright Light").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        brightLightRight.intensity = defaultLightRight;
        brightLightLeft.intensity = defaultLightLeft;

    }

    // Update is called once per frame
    void Update() {
      

    }

    private void FixedUpdate() {

        //// Ground Checks
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) {
            if (!isGrounded) {
                FindObjectOfType<AudioManager>().Play("Landing");
            }
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }

        if ((Input.GetKey("left shift") )) {
            isRunning = true;
            movementSpeed = sprintSpeed;
        }
        else {
            isRunning = false;
            movementSpeed = horizontalSpeed;
        }

        //// Handling movement
        if ((Input.GetKey("d") || Input.GetKey("right")) && !(Input.GetKey("a") || Input.GetKey("left"))) {
            // Walking Right
            if (isGrounded) {
                // currentFrame = animator.GetCurrentAnimatorStateInfo(0).normalizeTime;
                if (rb2d.transform.localScale.x > 0) {
                    animator.Play("Run");
                } else {
                    animator.Play("Run Backwards");
                }

            }
            else {
                //animator.Play("Fall");
            }
            rb2d.velocity = new Vector2(movementSpeed, rb2d.velocity.y);
            
        }
        else if ((Input.GetKey("a") || Input.GetKey("left")) && !(Input.GetKey("d") || Input.GetKey("right"))) {
            // Walking Left
            if (isGrounded) {
                // currentFrame = animator.GetCurrentAnimatorStateInfo(0).normalizeTime;
                if (rb2d.transform.localScale.x < 0) {
                    animator.Play("Run");
                } else {
                    animator.Play("Run Backwards");
                }

            }
            else {
            }

            rb2d.velocity = new Vector2(-movementSpeed, rb2d.velocity.y);
        } 
        else {
            // Idle
            if (isGrounded) {
                currentFrame = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    animator.Play("Idle");
            }
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        // Jumping
        if (Input.GetKey("space") && isGrounded) {
            rb2d.velocity = Vector2.up * jumpSpeed;
            animator.Play("Jump");
            FindObjectOfType<AudioManager>().Play("Jump");
        }
        // Better Jumping Feel
        if (rb2d.velocity.y < 0) {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            if (rb2d.velocity.y < -1) {
                animator.Play("Fall");
            }
        } 
        else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        // Flipping
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x > transform.position.x) {
            rb2d.transform.localScale = xScale;
        } else {
            rb2d.transform.localScale = new Vector3(-xScale.x, xScale.y, xScale.z);
        }



        //// Lights
        if (globalLight.intensity < brightLightThreshold) {
            lightsOn = true;
        } else {
            lightsOn = false;
        }

        if (lightsOn) {
            // Lights Dimming
            if (isRunning && !dimLights) {
                dimLights = true;
                lightsChange = true;
            }
            else if (!isRunning) {
                dimLights = false;
                lightsChange = true;
            }

            if (dimLights && lightsChange && rb2d.velocity.x != 0) {
                brightLightLeft.intensity = dimLightLeft;
                brightLightRight.intensity = dimLightRight;
                lightsChange = false;
            } else if (lightsChange){
                brightLightLeft.intensity = defaultLightLeft;
                brightLightRight.intensity = defaultLightRight;
                lightsChange = false;
            }
            // Lights rotation
            lookAt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            rightAngleRad = Mathf.Atan2(lookAt.y - brightLightRight.transform.position.y, lookAt.x - brightLightRight.transform.position.x);
            rightAngleDeg = (180 / Mathf.PI) * rightAngleRad;
            
            leftAngleRad = Mathf.Atan2(lookAt.y - brightLightLeft.transform.position.y, lookAt.x - brightLightLeft.transform.position.x);
            leftAngleDeg = (180 / Mathf.PI) * leftAngleRad;

            if (rb2d.transform.localScale.x > 0) {
                // Looking to the right
                targRightDeg = rightAngleDeg - 90;
                targLeftDeg = leftAngleDeg - 90;
                // These numbers lack sense
                targRightDeg = Mathf.Clamp(targRightDeg, -135, -45);
                targLeftDeg = Mathf.Clamp(targLeftDeg, -135, -45);

            }
            else {
                // Looking to the left
                targRightDeg = rightAngleDeg - 90;
                targLeftDeg = leftAngleDeg - 90;
                if ((targRightDeg <= 89f && targRightDeg >= 45f) || (targRightDeg > -269f && targRightDeg < -225f)) {
                    // These too lack sense
                }
                else if (targRightDeg < 45f && targRightDeg > -120f) {
                    targRightDeg = 45f;
                    targLeftDeg = 45f;
                } else if (targRightDeg < -120f && targRightDeg > -225f) {
                    targRightDeg = -225f;
                    targLeftDeg = -225f;
                } else {
                }
            }


            brightLightRight.transform.rotation = Quaternion.Euler(0, 0, targRightDeg);
            brightLightLeft.transform.rotation = Quaternion.Euler(0, 0, targLeftDeg);
            // brightLightRight.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(rightAngleDeg-90, 1, 180));
        }
        else {
            brightLightLeft.intensity = 0f;
            brightLightRight.intensity = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Colliding with " + collision.transform.gameObject.name);
        if (collision.transform.gameObject.name == "Anchor") {
            this.collidingAnchor = true;
            this.collidingAnchorGameObject = collision.transform.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform.gameObject.name == "Anchor") {
            this.collidingAnchor = false;
            this.collidingAnchorGameObject = null;
        }
    }

    public void PlayStep() {
        Debug.Log("Beep");
    }


}
