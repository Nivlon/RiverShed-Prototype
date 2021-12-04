using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBlock : MonoBehaviour, ITakeHit {
	public void Hit(Vector2 direction, float damage)
	{
		transform.Translate(direction);
	}
}
