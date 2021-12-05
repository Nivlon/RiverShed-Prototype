using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Vector3> Location;
    public List<GameObject> Enemies;
    public bool Activated = false;

    public GameObject mirrorObject;
    public Transform target;
    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject tmp;
        if (!Activated) {
            Activated = true;
            int index = 0;
            foreach (var enemy in Enemies) {
                enemy.GetComponent<HotGuyMovement>().target = target;
                tmp = Instantiate(enemy, Location[index], Quaternion.identity).GetComponentInChildren<MirroringScript>().mirrorPointObject = mirrorObject;
                
                index++;
            }
        }
    }
}
