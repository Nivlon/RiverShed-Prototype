using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField]
   GameObject playerObject;
   [SerializeField]
   float hSpeed = 10f, fallMultiplier=2.5f,lowJumpMultiplier = 2f, jumpForce = 10f;
   Rigidbody2D playerRb;
   bool isGrounded;
    [SerializeField]
    Animator animator;
    [SerializeField]
    SpriteRenderer playerSpriteNormal;
    [SerializeField]
    SpriteRenderer playerSpriteSoul;

    private void Start() {
        playerRb = playerObject.GetComponent<Rigidbody2D>();
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * hSpeed * Time.deltaTime,0,0);
        //playerRb.MovePosition(new Vector2(transform.position.x + Input.GetAxis("Horizontal") * hSpeed * Time.deltaTime,playerObject.transform.position.y));
        if(isGrounded) {
            if(Input.GetButtonDown("Jump")) {
                playerRb.AddForce(Vector2.up * jumpForce);
            }
            if(playerRb.velocity.y < 0) {
                playerRb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier -1) * Time.deltaTime;
            } else if(playerRb.velocity.y>0 && !Input.GetButton("Jump")) {
                playerRb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier -1) * Time.deltaTime;
            }
        }
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            animator.SetBool("Running", true);
            if (Input.GetAxis("Horizontal") < 0)
            {
                playerSpriteNormal.flipX = true;
                playerSpriteSoul.flipX = true;
            }
            else
            {
                playerSpriteNormal.flipX = false;
                playerSpriteSoul.flipX = false;
            }

        }
        else
        {
            animator.SetBool("Running", false);
        }
        animator.SetBool("InAir", !isGrounded);

    }

    private void OnCollisionStay2D(Collision2D other) {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        isGrounded = false;
    }
}