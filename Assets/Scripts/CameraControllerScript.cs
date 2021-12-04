using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    [SerializeField]
    GameObject playerObject;
    [SerializeField] [RangeAttribute(0.01f,1f)]
    float lerpFollowSpeed = 0.5f;
    Vector3 posOffset;

    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position - playerObject.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,playerObject.transform.position + posOffset,lerpFollowSpeed);
    }
}
