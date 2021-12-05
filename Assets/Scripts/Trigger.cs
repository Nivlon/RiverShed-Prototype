using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Trigger : MonoBehaviour {
	public UnityEvent onTrigger;

	private new Collider2D collider2D = null;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		onTrigger.Invoke();
	}

	private void OnValidate()
	{
		if(!TryGetComponent(out collider2D)) {
			collider2D = gameObject.AddComponent<Collider2D>();
		}
		collider2D.hideFlags = HideFlags.HideInInspector;
		collider2D.isTrigger = true;
	}
}
