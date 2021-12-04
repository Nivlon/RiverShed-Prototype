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
        // MATI: im so sorry but i was too lazy to fix a bug in the rendering system
        Vector3 actualPosition = new Vector3(
            playerObject.transform.position.x + posOffset.x,            
            (playerObject.transform.position.y + posOffset.y)/2f,
            playerObject.transform.position.z + posOffset.z);

        transform.position = Vector3.Lerp(transform.position, actualPosition, lerpFollowSpeed);
    }
}
