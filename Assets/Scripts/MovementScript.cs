using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField]
   GameObject playerObject, mirrorPlayerObject,mirrorPointObject;
   [SerializeField]
   float hSpeed = 10f, fallMultiplier=2.5f,lowJumpMultiplier = 2f;
   Rigidbody2D playerRb;
   

    private void Start() {
        playerRb = playerObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * hSpeed * Time.deltaTime,0,0);
        mirrorPlayerObject.transform.position = new Vector2(playerObject.transform.position.x,2*mirrorPointObject.transform.position.y - playerObject.transform.position.y);
        if(playerRb.velocity.y <0) {
            playerRb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier -1) * Time.deltaTime;
        } else if(playerRb.velocity.y>0 && !Input.GetKey(KeyCode.Space)) {
            playerRb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier -1) * Time.deltaTime;
        }
    }
}