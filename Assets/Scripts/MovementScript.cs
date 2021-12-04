using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField]
   GameObject playerObject, mirrorPlayerObject,mirrorPointObject;
   [SerializeField]
   float hSpeed = 10f, fallMultiplier=2.5f,lowJumpMultiplier = 2f, jumpForce = 10f;
   Rigidbody2D playerRb;
   bool isGrounded;
   

    private void Start() {
        playerRb = playerObject.GetComponent<Rigidbody2D>();
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * hSpeed * Time.deltaTime,0,0);
        //playerRb.MovePosition(new Vector2(transform.position.x + Input.GetAxis("Horizontal") * hSpeed * Time.deltaTime,playerObject.transform.position.y));
        mirrorPlayerObject.transform.position = new Vector2(playerObject.transform.position.x,2*mirrorPointObject.transform.position.y - playerObject.transform.position.y);
        
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
        
    }

    private void OnCollisionStay2D(Collision2D other) {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        isGrounded = false;
    }
}