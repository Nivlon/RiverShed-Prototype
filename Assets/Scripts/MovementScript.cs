using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {
	// Start is called before the first frame update
	[SerializeField]
	GameObject playerObject;
	[SerializeField]
	float groundDetectionRange = 0.1f, MovmentSpeed = 5, Jump = 50;

	[SerializeField]
	GameObject raycastSource;
	Rigidbody2D playerRb;
	bool isGrounded;
	[SerializeField]
	Animator animator;
	[SerializeField]
	SpriteRenderer playerSpriteNormal;
	[SerializeField]
	SpriteRenderer playerSpriteSoul;
	[SerializeField] private Vector2 VerticalVelocity = Vector2.zero, MovementInput = Vector2.zero, gravity = new Vector2(0, -9.8f);

	private void Start()
	{
		playerRb = playerObject.GetComponent<Rigidbody2D>();
		isGrounded = false;
	}


	void Update()
	{
		MovementInput.Set(Input.GetAxis("Horizontal"), 0);
		MovementInput *= Time.deltaTime;
		MovementInput *= MovmentSpeed;

		isGrounded = Physics2D.Raycast(raycastSource.transform.position, Vector2.down, groundDetectionRange);
		if(isGrounded) {
			if(Input.GetButtonDown("Jump")) {
				playerRb.AddForce(Vector2.up * Jump, ForceMode2D.Impulse);
			}
		}

		var MovementVector = MovementInput;

		playerRb.position = playerRb.position + MovementVector;

		if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f) {
			animator.SetBool("Running", true);
			if(Input.GetAxis("Horizontal") < 0) {
				playerSpriteNormal.flipX = true;
				playerSpriteSoul.flipX = true;
			} else {
				playerSpriteNormal.flipX = false;
				playerSpriteSoul.flipX = false;
			}

		} else {
			animator.SetBool("Running", false);
		}
		animator.SetBool("InAir", !isGrounded);

	}
}