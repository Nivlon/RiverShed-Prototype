using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HookBehaviour : MonoBehaviour {
	public HookShotBehaviour hookShotBehaviour;
	private Hookable hookedObject = null;


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.TryGetComponent(out Hookable hookable)) {
			hookedObject = hookable;
			hookShotBehaviour.Retract();
		}
	}

	private void Update()
	{
		if(hookedObject) {
			hookedObject.transform.position = transform.position;
		}
	}
}
