using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShotBehaviour : MonoBehaviour
{
    public LineRenderer ChainRenderer = null;
    public float zDepth = 0;

    public GameObject HookPrefab = null;

    private GameObject hook = null;

    private bool isRetracting = false, isGrabbing = false;

    private Transform hookTarget = null;
    private Vector3 mPos = Vector3.zero;
    private bool hookReady = true;
    private bool isHooking = false;

    private void Awake() {
        ChainRenderer.positionCount = 2;
        hook = Instantiate(HookPrefab, transform); ;
        ChainRenderer.SetPosition(0, transform.position);
    }

    public void Grab() {
        Debug.Log("Grabbing");
        isGrabbing = true;

    }

    public void Hook(Transform HookTarget) {
        hookTarget = HookTarget;
        isHooking = true;
    }

    public void HookTo(Transform HookTarget) {
        transform.position = Vector3.MoveTowards(hook.transform.position, -HookTarget.position, 0.2f);
        ChainRenderer.SetPosition(1, hook.transform.position);
    }


    void ThrowHook(Vector3 direction) {
        if (!isRetracting) {
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, -direction * 1000, 0.2f);
            ChainRenderer.SetPosition(1, hook.transform.position);
            Debug.Log("Throwing");
        }

    }

    void RetractHook(float retractSpeed = .5f) {
        if (hook.transform.position != Vector3.zero) {
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, Vector3.zero, retractSpeed);
            ChainRenderer.SetPosition(1, hook.transform.position);
        }

    }

    private void Update() {
        if (Input.GetButton("Fire1") && !isGrabbing) {
            if (hookReady) {
                mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mPos.z = zDepth;
                hookReady = false;
            }            
            ThrowHook((transform.position - mPos).normalized);
        } else if (!isGrabbing) {
            RetractHook();
        }
        if (hook.transform.position == Vector3.zero) {
            hookReady = true;
        }

        if (isHooking) {
            HookTo(hookTarget);
            if (transform.position == hookTarget.transform.position) {
                Debug.Log("done hooking");
                isHooking = false;
                hookTarget = null;
            }
        }

        if (Input.GetButton("Fire2") && isGrabbing) {
            RetractHook(0.2f);
        }
    }
    
}
