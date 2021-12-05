using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class SoulBlock : MonoBehaviour, ITakeHook, ITakeHit {
	[SerializeField] private bool LockX = false, LockY = false;
	[SerializeField] private new Rigidbody2D rigidbody2D;
	private void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	public void Hit(Vector2 direction, float damage)
	{
		rigidbody2D.AddForce(direction, ForceMode2D.Impulse);
	}

	public Vector2 Hook(Vector2 direction)
	{
		rigidbody2D.velocity = Vector2.zero;
		direction.Set(LockX ? transform.position.x : direction.x, LockY ? transform.position.y : direction.y);
		transform.position = direction;
		return direction;
	}
}
