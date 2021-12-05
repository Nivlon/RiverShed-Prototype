using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	[SerializeField] List<GameObject> ObjectsToSpawn = new List<GameObject>();

	void Spawn()
	{
		foreach(var ObjectToSpawn in ObjectsToSpawn) {
			Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
		}
	}
}
