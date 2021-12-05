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
    public Transform soulTargetForTheWispImSoSorryForThisMess;
    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject tmp;
        if (!Activated) {
            Activated = true;
            int index = 0;
            foreach (var enemy in Enemies) {
                if (true)
                {

                }
                HotGuyMovement hotguy = enemy.GetComponent<HotGuyMovement>();
                if (hotguy != null)
                {
                    hotguy.target = target;
                }
                EnemyMovement soulenemy = enemy.GetComponent<EnemyMovement>();
                if (soulenemy != null)
                {
                    soulenemy.target = soulTargetForTheWispImSoSorryForThisMess;
                }
                tmp = Instantiate(enemy, Location[index], Quaternion.identity).GetComponentInChildren<MirroringScript>().mirrorPointObject = mirrorObject;
                
                index++;
            }
        }
    }
}
