using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Win : MonoBehaviour {
	public UnityEvent onTriggerEnter;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		onTriggerEnter.Invoke();
	}
}
