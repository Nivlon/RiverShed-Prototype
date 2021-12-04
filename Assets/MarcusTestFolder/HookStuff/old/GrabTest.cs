using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabTest : MonoBehaviour
{
    private bool isGrabbed = false;
    private GameObject grabber = null;
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Hit!");
        //collision.gameObject.GetComponentInParent<HookShotBehaviour>().Hook(this.transform);
        isGrabbed = true;
        grabber = collision.gameObject;
    }

    private void Update() {
        //if (isGrabbed) {
        //    transform.position = grabber.transform.position;
        //}
    }
}
