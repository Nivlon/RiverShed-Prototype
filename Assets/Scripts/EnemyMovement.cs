using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour, ITakeHit {
	public Transform target;
	public float maxSpeed;
	public float acceleration;

	private Rigidbody2D rigidBody;
	private Vector2 currentVelocity;

	public void Hit(Vector2 direction, float damage)
	{
		rigidBody.AddForce(direction * damage, ForceMode2D.Impulse);

	}

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		Vector2 direction = target.transform.position - transform.position;
		rigidBody.velocity += direction * acceleration * Time.deltaTime;
		if(rigidBody.velocity.magnitude > maxSpeed) {
			rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
		}
		// turning of the look cause it doesnt really work with the sprite
		//transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, direction));
	}
}