using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class HotGuyMovement : MonoBehaviour, ITakeHit {
	public Transform target;
	public float maxSpeed;
	public float acceleration;
    public bool isFlying = false;

	[SerializeField]
	Animator anim;
	[SerializeField]
	SpriteRenderer normalSprite;
	[SerializeField]
	SpriteRenderer soulSprite;

	private Rigidbody2D rigidBody;
	private Vector2 currentVelocity;

	public void Hit(Vector2 direction, float damage)
	{
		rigidBody.AddForce(direction * 1000000f, ForceMode2D.Impulse);

	}

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
        Vector2 direction;
        if(isFlying) {
            direction = (target.transform.position - transform.position).normalized;
        } else {
            direction = new Vector2(target.transform.position.x - transform.position.x,0).normalized;
        }
		rigidBody.velocity += direction * acceleration * Time.deltaTime;
		if(rigidBody.velocity.magnitude > maxSpeed) {
			rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
		}

        if (Mathf.Abs(rigidBody.velocity.x) < 0.05f)
        {
			anim.SetBool("Running", false);
		}
        else
		{
			anim.SetBool("Running", true);
			if (direction.x < 0)
			{
				normalSprite.flipX = true;
				soulSprite.flipX = true;

			}
            else
            {
				normalSprite.flipX = false;
				soulSprite.flipX = false;
			}


		}

		// turning of the look cause it doesnt really work with the sprite
		//transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, direction));
	}
}