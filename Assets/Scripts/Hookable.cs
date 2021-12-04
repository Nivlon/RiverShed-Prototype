using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookable : MonoBehaviour {
	internal void Push(Vector2 push)
	{
		transform.position = (Vector2)transform.position + push;
	}
}
